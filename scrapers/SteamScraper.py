import io
import requests
import sys
import json as jsondump
import re
import time

class SteamScraper:

    ALL_APPS_REQUEST_URL = "http://api.steampowered.com/ISteamApps/GetAppList/v0002/?format=json";

    def __init__(self, id, sheet, query, since="2006-01-01", ignore_rts="True"):

        self.query = query;
        self.spreadsheet = id;
        self.sheet = sheet;

        #Scraper stopped & streaming
        self.stopped = False;
        self.streaming = False;
        self.dead = False;

        #The index into this page of reviews, the current page offset and the app index
        self.review_index = 0;
        self.current_offset = 0;
        self.app_index = 0;
        self.index = 0;

        #Last data for detecting page changes
        self.last_data = None;
        self.reviews = [];
        self.offset = 0;

        #Find all applications that we need to iterate over when scraping
        self.get_all_apps();

        pass;

    def append_header(self, writer):
        writer.append_row([ "author", "text", "game", "author_playtime", "author_lastplay", "creation_time", "votes_up", "recommended" ],self.sheet);
        pass;

    def start_stream(self):
        pass;

    def get_reviews(self, appid, offset):

        try:
            #Base url - split it up so it looks ok
            #B#56439
            base_url = "http://store.steampowered.com/appreviews/{0}".format(appid);

            #The parameters (found at https://partner.steamgames.com/doc/store/getreviews)
            #- Input offset into url
            url = base_url + "?json=1&start_offset={0}&day_range={1}&filter=all&language=english&review_type=all&purchase_type=all".format(offset, '9223372036854776000');

            #Request is none initially
            request = None;

            while True:
                try:
                    #Get the json text for this page
                    request = requests.get(url, timeout=3);

                    #Break after blocking
                    break;

                except Exception as e:
                    print(e);
                    print("[scrape] Got an error whilst trying to request a page from Steam API. Waiting for 5 seconds before starting again.");
                    time.sleep(5);

            #Convert to a python dictionary
            json = request.json();

            if json.get('success') == 1:

                #There was json.. so build an array text responses
                reviews = [x for x in json['reviews']];

                #And return them
                return reviews;

            else:

                #Otherwise return null
                return None;

        except Exception:
            return None;


    def get_review_page(self, appid):

        #Default values for data/last_data
        data = -2;

        #The initial offset -- this is what page we're on.. get 0, then 20, then 40
        #etc.
        offset = 0;

        #The list of all comments
        review_text = [];

        if data == self.last_data:

            #All data has been exhausted
            self.offset = 0;

            #print("reset offset");
            #print(data);
            #print(self.last_data);

            return None;

        else:

            #Get the comments and add them to the array
            data = self.get_reviews(appid, self.offset);

            #While the data is still unique (i.e. we haven't loaded something that
            #is the same as the previous request)
            self.last_data = data;

            if data == None:
                return None;

            review_text.extend(data);

            #Print some stuff out and increment offset by 20 for next page
            #print("Trying offset %4d.. (page %3d)" % (self.offset, self.offset / 20));
            self.offset += 20;

            #Increase index
            self.index += 1;

            #When all is done return all the comments
            return review_text;


    def next(self):

        while True:

            app = self.apps[self.app_index];

            if len(self.reviews) == 0:

                if self.offset > 0:
                    print("[scrape] Getting next page [%d, parsed %d] of steam reviews for %s (%s)..." % (self.offset / 20, self.offset, app["name"], app["appid"]));

                #If there are no reviews, find some more
                self.reviews = self.get_review_page(self.apps[self.app_index]["appid"]);

                if self.reviews == None or len(self.reviews) == 0:

                    app = self.apps[self.app_index+1];
                    #print("[scrape] All steam reviews exhausted.");
                    print("[scrape] Skipping to the next app: %s (%s)" % (app["name"], app["appid"]));

                    #No more reviews were found! So go to the next game.
                    self.reviews = [];
                    self.review_index = 0;
                    self.last_data = -1;
                    self.app_index += 1;
                    self.offset = 0;

                    # All apps have been exhausted
                    if self.app_index >= len(self.apps):
                        print("[scrape] Steam scraper has crawled all games (%d/%d)" % (self.app_index, len(self.apps)));
                        raise StopIteration();

                    continue;

            if self.review_index < len(self.reviews):

                #print("review index in range: %d <= %d" % (self.review_index, len(self.reviews)));
                #Review index is in range..
                return_val = self.reviews[self.review_index];
                self.review_index += 1;

                author = return_val["author"]["steamid"];
                author_playtime = return_val["author"]["playtime_forever"];
                author_lastplay = return_val["author"]["last_played"];
                creation_time = return_val["timestamp_updated"];
                votes_up = return_val["votes_up"];
                text = return_val["review"];
                game = app["name"] + " (" + str(app["appid"]) + ")";
                recommended = return_val["voted_up"];

                if re.search(self.query, text, re.IGNORECASE):
                    return [ author, text, game, author_playtime, author_lastplay, creation_time, votes_up, recommended ];
                else:
                    continue;

            else:

                #print("reset! %d" % len(self.reviews));
                #Review index is outside of reviews array
                self.reviews = [];
                self.review_index = 0;
                continue;


        # 0) while True:
            # 1) get page of reviews for current app using current offset
            # 2) if current review index + 1 > number of reviews,
                # 2.1) try to get another page of results
                    # 2.1.1) if successful, then reset review index
                    # 2.1.2) otherwise:
                        # 2.1.2.1) reset review index
                        # 2.1.2.2) increment app index
            # 3) otherwise increment current review index
                # 3.1) and return the current review using this index

        pass;





    def get_all_apps(self):

        #Request the contents from the page
        request = requests.get(SteamScraper.ALL_APPS_REQUEST_URL);

        #Convert to json format
        json = request.json();

        #Set up apps variable
        self.apps = [ x for x in reversed(json["applist"]["apps"]) ];
