
import tweepy
import time
import sys
import json
from traceback import print_exc
from writers.CSVWriter import *
from writers.SpreadSheetWriter import *


class TwitterStreamListener(tweepy.StreamListener):


    def create_writer(self, writer, query, spreadsheet, sheet):
        self.spreadsheet = spreadsheet;
        self.sheet = sheet;
        self.query = query;
        self.writer = writer;
        #self.writer.create_sheets([ self.sheet ]);
        #self.writer.clear_sheets([ self.sheet ]);
        self.index = 0;

    def set_scraper(self, scraper):
        self.scraper = scraper;

    def on_error(self, error):
        return False;

    def on_status(self, status):

        if self.scraper.dead == True:
            return False;

        try:

            if status.text.startswith("RT @"):
                return;

            #print(self.query.replace("\"", "").replace(" -filter:retweets", "").split(" OR "));
            #print(status.text);

            if not any(x in status.text for x in self.query.replace("\"", "").replace(" -filter:retweets", "").split(" OR ")):
                return;

            self.index += 1;
            print("[stream] Writing comment %d into %s..." % (self.writer.entries, self.sheet));
            data = [status.user.name, status.text, str(status.created_at), status.favorite_count, status.retweet_count];
            #print(data);
            self.writer.append_row(data, self.sheet);

        except Exception as e:

            while True:
                try:
                    # thread interrupted -- spawn a new one somehow -- signal to main thread
                    print_exc();

                    #print("[rate limit] Google sheets rate limit hit! Trying again in 100 seconds.. zzzz..");
                    time.sleep(1);

                except Exception:
                    continue;

                break;

    def on_exception(self, exception):

        print("[error] exception in streaming thread!");
        self.scraper.dead = True;
        self.running = False;

        return False;
