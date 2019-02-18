from multiprocessing.pool import ThreadPool
import csv;
from datetime import datetime;
import nltk
import re
import string
from collections import OrderedDict;
from operator import itemgetter
from nltk.corpus import stopwords

import argparse

results = {};

thread_count = 4
before = datetime.now();
lowest = 5;

stopword_list = stopwords.words("english");

parser = argparse.ArgumentParser(description='Analyse individual word frequencies from a CSV file.');
parser.add_argument('csv_file_name', metavar='in_file', type=str, help="The input CSV file to analyse.");
parser.add_argument('out_file_name', metavar='out_file', type=str, help="The output CSV file to write, with word frequency data.");
parser.add_argument('--thread-count', type=int, default=4, help="The number of threads to spawn.");
parser.add_argument("--lowest-frequency", type=int, default=1, help="The lowest word frequency -- words with frequencies equal to or under this value are ignored.");
parser.add_argument('--column-index', type=int, default=1, help="The index of the column to pull comments from, in the CSV.");

args = parser.parse_args();

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

    for token in tokens:

        #Run through each
        if len(token) <= 1:
            continue;

        token = re.sub(r'[^\w\s]', '', token, re.UNICODE | re.IGNORECASE | re.MULTILINE);

        if token in stopword_list:
            continue;

        try:
            results[token] += 1;
        except Exception as e:
            results[token] = 1;


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

    print("unique words: %d" % len(results));
    print("sorting words..");

    results = { k:v for k, v in results.items() if v >= args.lowest_frequency };
    print("results converted.");

    sorted_results = OrderedDict(sorted(results.items(), key=itemgetter(1), reverse=True));
    print("results sorted.");

    print("-" * 26);
    print("top 25 most frequent words");
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
            f.write("%s,%d\n" % (k, v));

            if i % 500 == 0:
                print("Writing row %d.." % i);

            i += 1;
