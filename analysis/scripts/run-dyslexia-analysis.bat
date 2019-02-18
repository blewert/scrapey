@echo off

python ngrams.py --n 2 --thread-count 8 --column-index 1 --stopwords ../dyslexia-reddit.csv --lowest-frequency 20 ../dyslexia-reddit/ngrams/bigrams-without-stopwords.csv

python ngrams.py --n 3 --thread-count 8 --column-index 1 --stopwords ../dyslexia-reddit.csv --lowest-frequency 20 ../dyslexia-reddit/ngrams/trigrams-without-stopwords.csv

python ngrams.py --n 2 --thread-count 8 --column-index 1 ../dyslexia-reddit.csv --lowest-frequency 20 ../dyslexia-reddit/ngrams/bigrams-with-stopwords.csv

python ngrams.py --n 3 --thread-count 8 --column-index 1 ../dyslexia-reddit.csv --lowest-frequency 20 ../dyslexia-reddit/ngrams/trigrams-with-stopwords.csv

python word-frequency.py --thread-count 8 --lowest-frequency 20 --column-index 1 ../dyslexia-reddit.csv ../dyslexia-reddit/word-frequency/word-frequency.csv
