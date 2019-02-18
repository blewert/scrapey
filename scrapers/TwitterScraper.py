### @file      TwitterScraper.py
### @author    Benjamin Williams <bwilliams@lincoln.ac.uk>
### @copyright CC 3.0 <https://creativecommons.org/licenses/by/3.0/>
###

#Import tweepy and time
import tweepy
import time
from scrapers.streamers.TwitterStreamListener import *

#Consumer key and secret for api
consumer = {
    "key" : "IYyL6r8fXp9egT9wzcrxI08dq",
    "secret" : "iRqcbjWHWxROdQYs6QuCnaZnzW2X4ABS0FURcZltGB6a7SdgK2"
};

#Access token for api
access_token = {
    "key" : "900704564541784064-PVkHs0bqYOrdOv75YdYrbWvQSkHnfGa",
    "secret" : "sAy8KeFF2PvdLg8UIppvP8TR3JTRCjyFybqhtffZhqjYh"
};


class TwitterScraper:
    """A class used to scrape tweets containing a specific term, and then return
    the data in an array format, to be used with a google sheet.
    """



    def __init__(self, id, sheet, query, since="2006-01-01", ignore_rts="True"):
        """Constructs a new twitter scraper with a given google sheet title,
        query (including operators), an optional date range since, and whether or
        not to filter out retweets.

        Args:
            sheet (str): The name of the google sheet to append data to.
            query (str): The query string to send to twitter's API.
            since (str, optional): A date-string which specifies how long to look back. Make sure this is in "YYYY-MM-DD" format.
            ignore_rts(str, optional): Whether or not to ignore retweets ("True" or "False").
        """

        #Set up google sheet title for this scraper, and the query.
        self.sheet = sheet;
        self.query = query;

        #Type of scraper
        self.type = "twitter";

        #The scraper is not stopped yet
        self.stopped = False;
        self.streaming = False;
        self.dead = False;
        self.spreadsheet = id;

        #Set up the internal index for this scraper
        self.index = 0;

        #Authorise with twitter API
        self.auth = tweepy.OAuthHandler(consumer["key"], consumer["secret"]);
        self.auth.set_access_token(access_token["key"], access_token["secret"]);

        #Get access to a reference to the twitter API itself.
        self.api = tweepy.API(self.auth);

        #If retweets should be ignored, append a filter to filter them out.
        if ignore_rts:
            self.query += " -filter:retweets";

        #And probably most importantly, set up the cursor object which is
        #basically the meat of this class!
        self.cursor = tweepy.Cursor(self.api.search,
                                q=self.query,
                                since=since,
                                tweet_mode="extended");

        #Get the items
        self.items = self.cursor.items();


    def start_stream(self, writer):

        if self.streaming:
            return;

        print("[stream] Started streaming...");

        self.stopped = True;
        self.streaming = True;

        self.streamListener = TwitterStreamListener();
        self.streamListener.set_scraper(self);
        self.streamListener.create_writer(writer, self.query, self.spreadsheet, self.sheet);
        self.stream = tweepy.Stream(auth=self.api.auth, listener=self.streamListener, tweet_mode='extended');

        terms = [ x.strip() for x in self.query.replace("-filter:retweets", "").split("OR") ];
        self.stream.filter(track=terms, async=True);

    def append_header(self, writer):
        writer.append_row(["username", "tweet", "date / time", "favourites", "retweets"], self.sheet);

    def __str__(self):
        return "<%s, '%s', %s>" % (self.type, self.query, self.sheet);

    def __repr__(self):
        return "<%s, '%s', %s>" % (self.type, self.query, self.sheet);


    def limit_handled_next_tweet(self):
        """Tries to get the next tweet from the internal list of tweets, but
        fails and sleeps for a minute.
        """

        while True:
            try:
                #Return the next tweet
                return self.items.next();

            except (tweepy.RateLimitError, tweepy.TweepError):
                #The twitter rate limit was hit.. wait for 60 seconds
                print("[rate limit] Twitter rate limit hit! Trying again in 60 seconds.. zzzz..");
                time.sleep(60);





    def next(self):
        """Returns the next tweet from the list of tweets. Also uses `limit_handled`
        to handle twitter rate limits.

        The data returned contains (in order):
        - tweet username
        - tweet text (full)
        - creation date/time
        - favourite count
        - retweet count

        Examples:
            Creates a twitter scraper and returns the first tweet found.
            >>> tweetScraper = TwitterScraper("apples-sheet", "apples");
            >>> row_data = tweetScraper.next();
            ... ["Wine Drunk", "cathy and the candy apples are shaking", "2018-03-19 13:20:35", "0", "29733"]
        """

        #Just return the tweet from the rate limiter function
        tweet = self.limit_handled_next_tweet();

        #Increase tweet index for pretty printing
        self.index += 1;

        #And return the row of data
        return [tweet.user.name, tweet.full_text, str(tweet.created_at), tweet.favorite_count, tweet.retweet_count];
