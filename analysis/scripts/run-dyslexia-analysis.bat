@echo off

SET in_data=../dyslexia-reddit.csv
SET out_folder=../dyslexia-reddit-all


rem CREATE FOLDER STRUCTURE
rem =======================

rm -rf "%out_folder%"

mkdir "%out_folder%"
mkdir "%out_folder%"/ngrams
mkdir "%out_folder%"/ngrams/top-n-bigrams/
mkdir "%out_folder%"/ngrams/top-n-posts/
mkdir "%out_folder%"/plots/
mkdir "%out_folder%"/word-frequency/


rem WORD FREQUENCY AND WORDCLOUD
rem ============================

python word-frequency.py --thread-count 8 --lowest-frequency 20 --column-index 1 "%in_data%" "%out_folder%"/word-frequency/word-frequency.csv
python plots/wordcloud_gen.py "%out_folder%"/word-frequency/word-frequency.csv "%out_folder%"/plots/wordcloud.png



rem UPVOTE_HISTOGRAM
rem ============================

python plots/upvote-histogram.py --bins 8   "%in_data%" "%out_folder%"/plots/upvote
python plots/upvote-histogram.py --bins 16  "%in_data%" "%out_folder%"/plots/upvote
python plots/upvote-histogram.py --bins 32  "%in_data%" "%out_folder%"/plots/upvote
python plots/upvote-histogram.py --bins 64  "%in_data%" "%out_folder%"/plots/upvote
python plots/upvote-histogram.py --bins 128 "%in_data%" "%out_folder%"/plots/upvote



rem GENERATE N-GRAMS
rem =======================

python ngrams.py --n 2 --thread-count 8 --column-index 1 --stopwords "%in_data%" --lowest-frequency 20 "%out_folder%"/ngrams/bigrams-without-stopwords.csv
python ngrams.py --n 3 --thread-count 8 --column-index 1 --stopwords "%in_data%" --lowest-frequency 20 "%out_folder%"/ngrams/trigrams-without-stopwords.csv
python ngrams.py --n 2 --thread-count 8 --column-index 1 "%in_data%" --lowest-frequency 20 "%out_folder%"/ngrams/bigrams-with-stopwords.csv
python ngrams.py --n 3 --thread-count 8 --column-index 1 "%in_data%" --lowest-frequency 20 "%out_folder%"/ngrams/trigrams-with-stopwords.csv



rem TOP N BIGRAMS
rem ============================

python top-n-percent.py --posts 0 --percent 0.1 --sentiment 1 "%in_data%" "%out_folder%"/ngrams/top-n-bigrams/top
python top-n-percent.py --posts 0 --percent 0.2 --sentiment 1 "%in_data%" "%out_folder%"/ngrams/top-n-bigrams/top
python top-n-percent.py --posts 0 --percent 0.3 --sentiment 1 "%in_data%" "%out_folder%"/ngrams/top-n-bigrams/top



rem TOP N POSTS
rem ============================

python top-n-percent.py --posts 1 --percent 0.1 --sentiment 1 "%in_data%" "%out_folder%"/ngrams/top-n-posts/top
python top-n-percent.py --posts 1 --percent 0.2 --sentiment 1 "%in_data%" "%out_folder%"/ngrams/top-n-posts/top
python top-n-percent.py --posts 1 --percent 0.3 --sentiment 1 "%in_data%" "%out_folder%"/ngrams/top-n-posts/top



rem SENTIMENT FREQUENCY HISTOGRAM
rem =============================

python plots/sentiment-frequency-histogram.py --bins 8   "%in_data%" "%out_folder%"/plots/sentiment-frequency
python plots/sentiment-frequency-histogram.py --bins 16  "%in_data%" "%out_folder%"/plots/sentiment-frequency
python plots/sentiment-frequency-histogram.py --bins 32  "%in_data%" "%out_folder%"/plots/sentiment-frequency
python plots/sentiment-frequency-histogram.py --bins 64  "%in_data%" "%out_folder%"/plots/sentiment-frequency
python plots/sentiment-frequency-histogram.py --bins 128 "%in_data%" "%out_folder%"/plots/sentiment-frequency

pause