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

import os

from nltk.sentiment.vader import SentimentIntensityAnalyzer
from nltk import tokenize

import matplotlib.pyplot as plt
import numpy as np

import argparse

# The actual sentiment analyser
analyser = SentimentIntensityAnalyzer();

def get_sentiment(text):
    
    global analyser;
    
    # Use the tokenizer to tokenize the paragraph into individual sentences
    #sentences = tokenize.sent_tokenize(text);
    
    #print(sentences);
    
    # The final scores (this is the same output that VADER gives, but we're
    # going to summate the scores of each sentence
    final_score = { "neg" : 0, "neu" : 0, "pos" : 0, "compound" : 0 };
    scores = analyser.polarity_scores(text);
        
    return scores['compound'];
    
parser = argparse.ArgumentParser(description='Generate a score histogram from a CSV file.');
parser.add_argument('csv_file_name', metavar='in_file', type=str, help="The input CSV file to analyse.");
parser.add_argument('out_file_name', metavar='out_file', type=str, help="The output CSV file to write, with word frequency data.");
parser.add_argument('--cidx-score', type=int, default=2, help="The index of the score column in the CSV.");
parser.add_argument('--sentiment', type=int, default=0, help="The index of the score column in the CSV.");
parser.add_argument('--xmax', type=int, default=0, help="The index of the score column in the CSV.");
parser.add_argument('--xmin', type=int, default=0, help="The index of the score column in the CSV.");
parser.add_argument('--ptsize', type=float, default=1.0, help="The index of the score column in the CSV.");
args = parser.parse_args();

csv_file = open(args.csv_file_name, 'r', newline='', encoding='utf-8');
csv_reader = csv.reader(csv_file);

data = [];

for i, row in enumerate(csv_reader):
    
    if i == 0:
        continue;
        
    if args.sentiment != 1:
        
        if i % 100 == 0:
            print("[info] reading data for row %d" % i);
            
        data.append([row[args.cidx_score], row[3]]);
    else:
        
        if i % 100 == 0:
            print("[info] generating sentiment for row %d" % i);
            
        sentiment = get_sentiment(row[1]);
        data.append([row[args.cidx_score], sentiment]);
    

x = [ x[0] for x in data ];
y = [ x[1] for x in data ];
s = [ args.ptsize for n in range(len(y)) ];

#plt.rc('text', usetex=True);
plt.rc('font', family='serif');
#
if args.xmax != 0:
    plt.xlim(args.xmin, args.xmax);
    
plt.title("Sentiment vs post score (" + os.path.basename(args.csv_file_name) + ")");
plt.scatter(x, y, s=s);
#plt.hist(scores, bins=args.bins);
plt.xlabel("Post score");
plt.ylabel("Sentiment value");
#plt.show();
#    
plt.savefig(args.out_file_name);
#
#print(datetime.now() - before);
csv_file.close();

