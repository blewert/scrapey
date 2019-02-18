## THREADING/IO
import csv;
import argparse
from datetime import datetime;
from multiprocessing.pool import ThreadPool

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
parser.add_argument("--lowest-frequency", type=int, default=2, help="The lowest gram frequency -- gram with frequencies equal to or under this value are ignored.");
parser.add_argument("--stopwords",  action='store_true', help="Whether or not to remove stopwords.");

args = parser.parse_args();

jobs_ran = 0;

def process_row(x):

    #Read/write to global not local
    global results;

    #The comment is at the index passed by the args
    comment = x[args.column_index];

    #pre-processing: remove punctuation
    comment = re.sub(r'[^\w\s]', '', comment, re.UNICODE | re.IGNORECASE | re.MULTILINE);

    #Make the comment lowercase -- for case insensitive frequencies
    comment = comment.lower();

    #split by space
    tokens = comment.split(" ");

    #Split them up
    tokens = [ re.sub(r'[^\w\s]', '', token, re.UNICODE | re.IGNORECASE | re.MULTILINE) for token in tokens if (len(token) > 1) ];

    if args.stopwords:
        tokens = [ x for x in tokens if (x not in stopword_list) ];

    #ngramify
    grams = ngrams(tokens, args.n);

    for gram in grams:

        try:
            results[gram] += 1;
        except Exception as e:
            results[gram] = 1;

    global jobs_ran;

    jobs_ran += 1;

    if (jobs_ran % 250) == 0:
        print("%d rows processed so far." % jobs_ran);


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
            print("(%s) row num: %d, data len: %d" % (dt_after, i, len(results)));

        i += 1;

    pool.close();
    pool.join();
    print(datetime.now() - before);
    #print(datetime.now() - before);


    csv_file.close();

    print("unique ngrams found: %d" % len(results));
    print("sorting ngrams..");

    results = { k:v for k, v in results.items() if v >= args.lowest_frequency and "\n" not in "".join(k)  };
    print("results converted.");

    sorted_results = OrderedDict(sorted(results.items(), key=itemgetter(1), reverse=True));
    print("results sorted.");

    print("-" * 26);
    print("top 25 most frequent ngrams");
    print("-" * 26);

    i = 0;

    for (k, v) in sorted_results.items():

        if i >= 25:
            break;

        print("#%2d: %s, %d occurences" % (i, k, v));

        i += 1

    print("writing output to file..");

    i = 0;

    with open(args.out_file_name, 'w', encoding='utf-8') as f:

        for (k, v) in sorted_results.items():
            gram_str = ",".join(k);
            f.write("%s,%d\n" % (gram_str, v));

            #if i % 500 == 0:
            #   print("Writing row %d.." % i);

            i += 1;


    print("written.");
