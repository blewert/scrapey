## THREADING/IO
import csv;
import argparse
from datetime import datetime;
from multiprocessing.pool import ThreadPool

## MEMORY
import os
import psutil

## STRUCTS
import re
import string
from operator import itemgetter
from collections import OrderedDict;

## NLTK
import nltk
from nltk import ngrams
from nltk.corpus import stopwords

results = {};

thread_count = 4
before = datetime.now();
lowest = 5;

stopword_list = stopwords.words("english");

parser = argparse.ArgumentParser(description='Analyse individual ngram frequencies from a CSV file.');
parser.add_argument('csv_file_name', metavar='in_file', type=str, help="The input CSV file to analyse.");
parser.add_argument('out_file_name', metavar='out_file', type=str, help="The output CSV file to write, with ngram frequency data.");
parser.add_argument('--n', type=int, default=2, help="The n to use -- 2 for bigrams, 3 for trigrams, etc.");
parser.add_argument('--thread-count', type=int, default=4, help="The number of threads to spawn.");
parser.add_argument('--column-index', type=int, default=1, help="The index of the column to pull comments from, in the CSV.");
parser.add_argument("--lowest-frequency", type=int, default=2, help="The lowest vertex degree, connections under this amount will be ignored.");
parser.add_argument("--stopwords",  action='store_true', help="Whether or not to remove stopwords.");

args = parser.parse_args();

jobs_ran = 0;
active_thread_count = 0;

def process_row(x):

    #Read/write to global not local
    global results;
    global active_thread_count;

    active_thread_count += 1;

    #The comment is at the index passed by the args
    comment = x[args.column_index];

    #pre-processing: remove punctuation
    comment = re.sub(r'[^\w\s]', '', comment, re.UNICODE | re.IGNORECASE | re.MULTILINE);

    #Make the comment lowercase -- for case insensitive frequencies
    comment = comment.lower();

    #split by space
    tokens = comment.split(" ");

    #remove stopwords if needed
    if args.stopwords:
        tokens = [ x for x in tokens if x not in stopword_list ];

    for token in tokens:

        if len(token) <= 1:
            continue;

        #Get all other tokens
        other_tokens = [ x for x in tokens if token != x.lower() ];

        for other_token in other_tokens:

            if other_token in results:
                if token in results[other_token]:
                    continue;

            if token not in results:
                results[token] = {};
            else:
                if other_token in results[token]:
                    results[token][other_token] += 1;
                else:
                    results[token][other_token] = 1;


    active_thread_count -= 1;

    global jobs_ran
    jobs_ran += 1;

    if jobs_ran % 250 == 0:
        process = psutil.Process(os.getpid());
        info = process.memory_info().rss / (1024 ** 2);
        print("[ran] %d rows processed so far. (%d active, %.2f MB)" % (jobs_ran, active_thread_count, info));

        if jobs_ran % 500 == 0:

            results = sorted(results, lambda x: x[2], reverse = True);

            results = results[0:50];
            #for i in range(0, len(results)):
            #    results[i] = { k:v for (k, v) in results[i].items() if v > args.lowest_frequency };

    #return 0;
    #print("%s" % (threading.current_thread().name));

if __name__ == '__main__':

    csv_file = open(args.csv_file_name, 'r', newline='', encoding='utf-8');

    pool = ThreadPool(processes=args.thread_count);

    csv_reader = csv.reader(csv_file);

    dt = datetime.now();

    i = 0;
    for row in csv_reader:
        pool.map_async(process_row, (row,));

        if i % 1000 == 0:
            dt_after = datetime.now() - dt;
            dt = datetime.now();
            print("[send] (%s) row num: %d, data len: %d" % (dt_after, i, len(results)));

        i += 1;

    pool.close();
    pool.join();
    print(datetime.now() - before);
    #print(datetime.now() - before);


    csv_file.close();

    print("unique vertices found: %d" % len(results));
    print("removing vertices with degree <= lowest..");

    #print(results);

    end_results = {};

    for (k, v) in results.items():

        new_item = {};

        for (ki, vi) in v.items():

            if vi > 1:
                new_item[ki] = vi;

        if len(new_item) >= args.lowest_frequency:
            end_results[k] = new_item;

    results = {};

    print("removed. converting to pairs...");

    pairs = [ (k, ki, vi) for (k, v) in end_results.items() for (ki, vi) in v.items() if ("\n" not in k) and ("\n" not in ki) and (k != "") and (ki != "") ];

    print("converted. sorting by pair frequency...");

    sorted_pairs = sorted(pairs, key = lambda x: x[2], reverse = True);
    pairs = [];

    print("-" * 26);
    print("top 25 most frequent word pairs");
    print("-" * 26);

    i = 0;

    for pair in sorted_pairs:

        if i >= 25:
            break;

        i += 1;
        print("#%2d: %s => %s, %d occurences" % (i, pair[0], pair[1], pair[2]));


    print("writing output to file..");

    i = 0;

    with open(args.out_file_name, 'w', encoding='utf-8') as f:

        for pair in sorted_pairs:
            f.write("%s,%s,%d\n" % (pair[0], pair[1], pair[2]));

            if i % 500 == 0:
               print("Writing row %d.." % i);

            i += 1;


    print("written.");
