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

import matplotlib.pyplot as plt
import numpy as np

import argparse

results = {};

thread_count = 4
before = datetime.now();
lowest = 5;

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
    
stopword_list = stopwords.words("english");

parser = argparse.ArgumentParser(description='Generate a score histogram from a CSV file.');
parser.add_argument('csv_file_name', metavar='in_file', type=str, help="The input CSV file to analyse.");
parser.add_argument('out_file_name', metavar='out_file', type=str, help="The output CSV file to write, with word frequency data.");
parser.add_argument('--cidx-comment', type=int, default=1, help="The index of the column to pull comments from, in the CSV.");
parser.add_argument('--cidx-score', type=int, default=4, help="The index of the score column in the CSV.");
parser.add_argument('--bins', type=int, default=8, help="The amount of bins to use.");

args = parser.parse_args();


csv_file = open(args.csv_file_name, 'r', newline='', encoding='utf-8');
csv_reader = csv.reader(csv_file);

dt = datetime.now();

row_count = 0;

for row in csv_reader:
    row_count += 1;
    
bin_size = ceil(row_count / args.bins);

print("[info] there are %d bins" % args.bins);
print("[info] that's %d elements per bin" % bin_size);

bins = [ 0 for x in range(0, args.bins+1) ];
scores = [];

csv_file.seek(0);
for i, row in enumerate(csv_reader):
    try:
        #scores.append(int(row[args.cidx_score]));
        score = get_sentiment(row[args.cidx_comment]);
        scores.append(score);
        
        if i % 100 == 0:
            print("[info] analysed score for comment %d: %f" % (i, score));
        
    except:
        pass;
        
smallest  = min(scores);
largest = max(scores);

def is_outlier(points, thresh=3.5):
    """
    Returns a boolean array with True if points are outliers and False 
    otherwise.

    Parameters:
    -----------
        points : An numobservations by numdimensions array of observations
        thresh : The modified z-score to use as a threshold. Observations with
            a modified z-score (based on the median absolute deviation) greater
            than this value will be classified as outliers.

    Returns:
    --------
        mask : A numobservations-length boolean array.

    References:
    ----------
        Boris Iglewicz and David Hoaglin (1993), "Volume 16: How to Detect and
        Handle Outliers", The ASQC Basic References in Quality Control:
        Statistical Techniques, Edward F. Mykytka, Ph.D., Editor. 
    """
    if len(points.shape) == 1:
        points = points[:,None]
        
    median = np.median(points, axis=0)
    diff = np.sum((points - median)**2, axis=-1)
    diff = np.sqrt(diff)
    med_abs_deviation = np.median(diff)

    modified_z_score = 0.6745 * diff / med_abs_deviation

    return modified_z_score > thresh
        
scores = np.array(scores);
filtered = scores[~is_outlier(scores)];    
print(filtered);

#plt.rc('text', usetex=True);
plt.rc('font', family='serif');

plt.xlim(-1, 1);
plt.title("Sentiment frequency of Reddit comments containing the term 'dyslexia'");
plt.hist(scores, bins=args.bins);
plt.ylabel("# of posts");
plt.xlabel("Sentiment value");
    
plt.savefig(args.out_file_name + "-" + str(args.bins) + "-bins.pdf");

print(datetime.now() - before);
csv_file.close();

