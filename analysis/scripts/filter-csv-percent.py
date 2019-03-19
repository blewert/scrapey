## THREADING/IO
import csv;
import argparse
from datetime import datetime;
from multiprocessing.pool import ThreadPool

## STRUCTS
import re
import string
import random
from math import floor
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
parser.add_argument('--percent', type=float, default=2, help="The n to use -- 2 for bigrams, 3 for trigrams, etc.");

args = parser.parse_args();

csv_file = open(args.csv_file_name, 'r', newline='', encoding='utf-8');
csv_reader = csv.reader(csv_file);

rows = [];


for i, row in enumerate(csv_reader):
    rows.append(row);

header = rows[0];
rows = rows[1:];

print("there are %d rows" % len(rows));
amount_to_take = floor(len(rows) * args.percent);
print("I need to take %d rows (%f percent)" % (amount_to_take, args.percent));

random.shuffle(rows);

#If not display only, then open up output file
out_csv_file = open(args.out_file_name, 'w+', newline='', encoding='utf-8');
csv_writer = csv.writer(out_csv_file);

csv_writer.writerow(header);

for i, row in enumerate(rows):

    if i >= amount_to_take:
        break;

    csv_writer.writerow(row);
