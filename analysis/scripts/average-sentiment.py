from multiprocessing.pool import ThreadPool
import csv;
from datetime import datetime;
import nltk
import re
import string
from collections import OrderedDict;
from operator import itemgetter
from nltk.corpus import stopwords
from math import floor, ceil

from wordcloud import WordCloud, STOPWORDS, ImageColorGenerator

import matplotlib.pyplot as plt
import numpy as np

import argparse

results = {};

thread_count = 4
before = datetime.now();
lowest = 5;



stopword_list = stopwords.words("english");

parser = argparse.ArgumentParser(description='Generate a score histogram from a CSV file.');
parser.add_argument('csv_file_name', metavar='in_file', type=str, help="The input CSV file to analyse.");
parser.add_argument('out_file_name', metavar='out_file', type=str, help="The output CSV file to write, with word frequency data.");
parser.add_argument('--cidx-comment', type=int, default=1, help="The index of the column to pull comments from, in the CSV.");

args = parser.parse_args();

csv_file = open(args.csv_file_name, 'r', newline='', encoding='utf-8');
csv_reader = csv.reader(csv_file);

for row in csv_reader:
    
    if len(row) <= 1:
        continue;
        
    print(row);
    
#print(text);

    
csv_file.close();

