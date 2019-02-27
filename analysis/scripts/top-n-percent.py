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

from nltk.sentiment.vader import SentimentIntensityAnalyzer
from nltk import tokenize

from nltk import ngrams

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
parser.add_argument('out_file_name', metavar='out_file', type=str, help="The output CSV file to write, with word frequency data. Without extension.");
parser.add_argument('--cidx-comment', type=int, default=1, help="The index of the column to pull comments from, in the CSV.");
parser.add_argument('--cidx-score', type=int, default=4, help="The index of the score column in the CSV.");
parser.add_argument('--percent', type=float, default=0.1, help="The percent (normalised) to pull from.");
parser.add_argument('--stopwords', type=int, default=1, help="Whether or not to remove stopwords.");
parser.add_argument('--sentiment', type=int, default=1, help="Whether or not to add a column for sentiment scores.");
parser.add_argument('--n', type=int, default=2, help="The n in n-grams (bigrams, trigrams, etc).");
parser.add_argument('--posts', type=int, default=0, help="Whether to include posts or just bigrams.");

args = parser.parse_args();

csv_file = open(args.csv_file_name, 'r', newline='', encoding='utf-8');
csv_reader = csv.reader(csv_file);

dt = datetime.now();

scores = [];

for row in csv_reader:
    try:
        scores.append((row[args.cidx_comment], int(row[args.cidx_score])));
    except:
        pass;
    
csv_file.seek(0);
#smallest  = min(scores);
#largest = max(scores);

scores.sort(key = lambda x: x[1], reverse=True);
    
topStart = 0;
topEnd = floor(len(scores) * args.percent);

botEnd = len(scores) - 1;
botStart = floor(len(scores) * (1.0 - args.percent));

print("top: %d to %d (%d)" % (topStart, topEnd, topEnd - topStart));
print("bot: %d to %d (%d)" % (botStart, botEnd, topEnd - topStart));

top = [ x[0] for x in scores[topStart : topEnd] ];
bot = [ x[0] for x in scores[botStart : botEnd] ];


for arr in [ top, bot ]:
    
    results = {};
    comments = [];
    
    for comment in arr:
        
        # append comment and skip
        if args.posts == 1:
            comments.append(comment);
            continue;
            
            
        #pre-processing: remove punctuation
        comment = re.sub(r'[^\w\s]', '', comment, re.UNICODE | re.IGNORECASE | re.MULTILINE);
        
        #pre-processing: remove punctuation
        comment = re.sub(r'\n', '', comment, re.UNICODE | re.IGNORECASE | re.MULTILINE);
    
        #Make the comment lowercase -- for case insensitive frequencies
        comment = comment.lower();
        
        #split by space
        tokens = comment.split(" ");
    
        #Split them up
        tokens = [ re.sub(r'[^\w\s]', '', token, re.UNICODE | re.IGNORECASE | re.MULTILINE) for token in tokens if (len(token) > 1) ];
    
        if True:
            tokens = [ x for x in tokens if (x not in stopword_list) ];
    
        #ngramify
        grams = ngrams(tokens, args.n);
    
        for gram in grams:
            
            try:
                results[gram] += 1;
            except Exception as e:
                results[gram] = 1;
    
    if args.posts != 1:
        sorted_results = OrderedDict(sorted(results.items(), key=itemgetter(1), reverse=True));
        
        suffix = "-" + str(floor(args.percent * 100)) + "-top.csv";
        
        if arr == bot:
            suffix = "-" + str(floor(args.percent * 100)) + "-bottom.csv";
        
        with open(args.out_file_name + suffix, 'w', encoding='utf-8') as f:

            for (k, v) in sorted_results.items():
                gram_str = ",".join(k);
                f.write("%s,%d\n" % (gram_str, v));
                
    else:
                
        
        
        suffix = "-" + str(floor(args.percent * 100)) + "-top.csv";
        
        if arr == bot:
            suffix = "-" + str(floor(args.percent * 100)) + "-bottom.csv";
            
        out_csv_file = open(args.out_file_name + suffix, 'w+', newline='', encoding='utf-8');
        csv_writer = csv.writer(out_csv_file);
        
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
        
        for i, comment in enumerate(comments):
            print("[write] writing comment %d" % (i+1));
            
            
            if args.sentiment != 1:
                csv_writer.writerow([i, comment]);
            else:
                sentiment_score = get_sentiment(comment);
                csv_writer.writerow([i, comment, sentiment_score]);
        
        out_csv_file.close();
        
        
            
    #for result in sorted_results:         
        #print(sorted_results[result]);

#for row in csv_reader:
#    
#    try:
#        score = row[args.cidx_score];
#    
#        print("%d" % int(score));
#        
#    except:
#        pass;
    
    #print(int(score) / largest);


