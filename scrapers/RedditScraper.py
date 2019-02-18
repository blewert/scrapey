import urllib.request
import json
import math
import praw
import time
import threading
from datetime import datetime
from writers.SpreadSheetWriter import *
from writers.CSVWriter import *


class RedditScraper:

    CLIENT_ID = "nVIlWMHDGEQHuQ";
    CLIENT_SECRET = "0aZfqH3y5iUYsUBgrusYMIJYrzE";
    USER_AGENT = "PRAW live streamer by /u/trewelb (https://github.com/blewert)";
    USERNAME = "scrapey-bot";
    PASSWORD = "RhP1WmbUo3";

    def __init__(self, id, sheet, query, since="2006-01-01", ignore_rts="True", streamonly=False):

        self.spreadsheet = id;
        self.sheet = sheet;
        self.query = query.replace(" ", "+");
        self.index = 0;
        self.streamonly = streamonly;

        self.dead = False;
        self.stopped = False;
        self.streaming = False;

        self.last_date = "99999999999";

        if not self.streamonly:
            self.find_comments();

    def append_header(self, writer):
        writer.append_row(["author", "comment", "created_utc", "permalink", "score", "subreddit"], self.sheet);

    def stream_thread(self, writer, sheet):

        #Create a PRAW reddit service
        self.reddit = praw.Reddit(  client_id=RedditScraper.CLIENT_ID,
                                    client_secret=RedditScraper.CLIENT_SECRET,
                                    user_agent=RedditScraper.USER_AGENT,
                                    username=RedditScraper.USERNAME,
                                    password=RedditScraper.PASSWORD);

        #Iterator
        i = 0;

        for comment in self.reddit.subreddit("all").stream.comments():

            #For each comment that is being live-fed from reddit, increment i
            i += 1;

            if i > 100:

                #We're not looking at historical data. Get the message body:
                body = comment.body;

                if self.query in body:

                    #Get author, permalink and creation date
                    author = comment.author.name;
                    permalink = comment.permalink;
                    created = "%s" % datetime.fromtimestamp(comment.created_utc);
                    subreddit = comment.subreddit;
                    score = comment.score;

                    #Make data
                    data = [ author, body, created, permalink, subreddit.display_name, score ];

                    print("[stream] Writing comment %d into %s..." % (self.writer.entries, self.sheet));

                    try:
                        #Append row!
                        writer.append_row(data, sheet);

                    except Exception as e:

                        while True:
                            try:

                                if not isinstance(writer, CSVWriter):
                                    # thread interrupted -- spawn a new one somehow -- signal to main thread
                                    print(e);
                                    print("[rate limit] Google sheets rate limit hit! Trying again in 100 seconds.. zzzz..");
                                    time.sleep(100);

                                else:
                                    print(e);
                                    print("[error] Error writing to CSV. Trying again in 1 second...");
                                    time.sleep(1);

                            except Exception:
                                continue;

                            break;


    def start_stream(self, writer):

        #Already spawned a thread? Return
        if self.streaming:
            return;

        #Set writer
        self.writer = writer;

        #Otherwise set streaming to true
        self.streaming = True;

        #Show that we're starting
        print("[stream] Started streaming (reddit)...");
        print("[stream] Starting thread for reddit scraper.");

        #And start the thread
        thread = threading.Thread(target=self.stream_thread, args=(self.writer, self.sheet));
        thread.start();

    def find_comments(self):
        self.index = 0;
        self.comments = [];

        self.base_api_url = "https://api.pushshift.io/reddit/search?q=";

        self.opener = urllib.request.FancyURLopener({});
        self.url = self.base_api_url + self.query + "&limit=9999999&before=" + self.last_date;

        print("[scrape] Opening reddit comment search api... (" + self.url + ")");
        f = self.opener.open(self.url);
        content = f.read();

        print("[scrape] Loading as json data...");
        obj = json.loads(content);

        data = obj["data"];

        print("[scrape] " + str(len(data)) + " comments found.");

        self.comments = [  ];

        comment_count = len(data);

        if comment_count == 0:
            raise StopIteration();

        gap_size = math.pow(10, math.floor(math.log(comment_count, 10)));

        last_date_set = "";

        for i in range(0, comment_count):

            item = data[i];

            row = [];

            query = self.query.replace("+", " ");

            row.append(item["author"]);
            row.append(item["body"]);
            row.append(item["created_utc"]);

            #row.append("%s" % datetime.fromtimestamp(item["created_utc"]));

            if "permalink" in item:
                row.append("reddit.com" + item["permalink"]);
            else:
                row.append(item["id"] + " (id), " + item["link_id"] + " (link id)");

            row.append(item["score"]);
            row.append(item["subreddit"]);

            if i % gap_size == 0:
                print("[scrape] Appending row %d of %d..." % (i, comment_count));

            if i == comment_count - 1:
                last_date_set = row[2];

            if not query.lower() in item["body"].lower():
                continue;

            self.comments.append(row);

        #self.comments[-1][2]
        self.last_date = str(last_date_set);

        print("[scrape] Appended all rows.");
        print("[scrape] Last date is %s" % self.last_date);

    def next(self):

        if self.index == 0 and len(self.comments) == 0:
            raise StopIteration();

        comment = self.comments[self.index];

        self.index += 1;

        if self.index >= len(self.comments):
            print("[scrape] I'm hungry from another page of data! nom nom nom!");
            print("[scrape] Looking for reddit comments before %s..." % str(datetime.fromtimestamp(int(self.last_date))));
            self.find_comments();

        return comment;
