import time
import argparse
from writers.SpreadSheetWriter import *
from writers.CSVWriter import *
from scrapers.TwitterScraper import *
from scrapers.RedditScraper import *
from scrapers.SteamScraper import *
from functools import reduce

if __name__ == '__main__':

    parser = argparse.ArgumentParser();
    parser.add_argument("--since", type=str, default="2017-01-01", help="The date to scrape from (until now), in yyyy-mm-dd format.");
    parser.add_argument("--clear",  action='store_true', help="Whether or not to clear the sheets used.");
    parser.add_argument("--streamonly", action='store_true', help="Whether or not to only stream tweets and not backlog.")
    parser.add_argument("--csv", action='store_true', help="Whether or not to output to local CSV storage instead.");
    parser.add_argument("spreadsheet_id", type=str, metavar="s_id", help="The spreadsheet ID of the google sheet or the search name for the CSV writer.");
    parser.add_argument("scraper_spec", type=str, metavar="S", help="The specified scrapers to use. A single string, with each character corresponding to a type of scraper. t: twitter, p: pornhub, r: reddit.");
    parser.add_argument("terms", type=str, metavar="T", nargs="+", help="The terms to search for each scraper specified in the scraper spec. Make sure each term is wrapped in quotation marks.");

    #Parse arguments
    args = parser.parse_args();

    if len(args.scraper_spec) != len(args.terms):
        print("[error] Amount of scrapers specified is not equal to the number of search terms.");
        exit(1);

    print("Setting up writer..");


    #Not a csv writer? Set up a sheets writer.
    if not args.csv:
        writer = SpreadSheetWriter(args.spreadsheet_id); # "1SWy0gA6CqIKybjD1tXaCuEzTSD7MGu5VS09Qe2hw-L4"

    #CSV writer? Set up a csv writer.
    else:
        writer = CSVWriter(args.spreadsheet_id);

    scraper_types = {
        't' : 'twitter',
        'r' : 'reddit',
        'p' : 'pornhub',
        's' : 'steam'
    };

    try:
        sheets = [ scraper_types[x] for i, x in enumerate(args.scraper_spec) ];

    except KeyError:
        print("[error] Unrecognized scraper specified in spec.");
        print("[error] Valid choices: ");
        print("        - t: Twitter");
        print("        - p: Pornhub");
        print("        - r: Reddit");
        print("        - s: Steam");
        exit(1);

    print(sheets);

    if args.clear and isinstance(writer, CSVWriter):
        print("Clearing sheets...");
        writer.clear_sheets(sheets);

    print("Creating non-existent sheets...");
    writer.create_sheets(sheets);

    if args.clear and isinstance(writer, SpreadSheetWriter):
        print("Clearing sheets...");
        writer.clear_sheets(sheets);

    scrapers = [];

    for i, c in enumerate(args.scraper_spec):
        if c == "t":
            scrapers.append(TwitterScraper(args.spreadsheet_id, sheets[i], args.terms[i], since=args.since));

        elif c == "r":
            scrapers.append(RedditScraper(args.spreadsheet_id, sheets[i], args.terms[i], since=args.since, streamonly=args.streamonly));

        elif c == "s":
            scrapers.append(SteamScraper(args.spreadsheet_id, sheets[i], args.terms[i], since=args.since));

    print("Created %d scraper(s):" % len(scrapers));

    for scraper in scrapers:
        print("\t" + str(scraper));
        scraper.append_header(writer);

    print("");

    while True:
        for scraper in scrapers:

            if args.streamonly:
                scraper.stopped = True;

            if scraper.dead:
                print("[error] wuh-oh, streaming has died.. going to create another one");
                print("[error] waiting for 5 seconds...");
                time.sleep(5);
                scraper.dead = False;
                scraper.stopped = True;
                scraper.streaming = False;
                continue;

            if scraper.stopped:
                scraper.start_stream(writer);
                continue;

            try:
                data = scraper.next();

            except StopIteration:
                print("[scrape] Scraper for term '%s' stopped. (Exhausted all comments)" % (scraper.query));
                scraper.stopped = True;
                continue;

            try:
                print("[scrape] Writing comment %d into %s..." % (writer.entries, scraper.sheet));
                writer.append_row(data, scraper.sheet);

            except Exception as e:
                print(e);

                if not isinstance(writer, CSVWriter):
                    print("[rate limit] Google sheets rate limit hit! Trying again in 100 seconds.. zzzz..");
                    time.sleep(100);
                else:
                    print("[error] Error when writing to CSV. Sleeping for a second.. zzzz..");
                    time.sleep(1);
