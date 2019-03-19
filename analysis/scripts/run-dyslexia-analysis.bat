@echo off

SET in_data=../dyslexia-subreddit.csv
SET out_folder=../dyslexia-subreddit

python filter-csv-percent.py --percent 0.01 "%in_data%" "%out_folder%"/filtered-1-percent.csv
python filter-csv-percent.py --percent 0.05 "%in_data%" "%out_folder%"/filtered-5-percent.csv
python filter-csv-percent.py --percent 0.1 "%in_data%" "%out_folder%"/filtered-10-percent.csv
pause
exit

rem CREATE FOLDER STRUCTURE
rem =======================

rem rm -rf "%out_folder%"

mkdir "%out_folder%"
mkdir "%out_folder%"/ngrams
mkdir "%out_folder%"/ngrams/top-n-bigrams/
mkdir "%out_folder%"/ngrams/top-n-posts/
mkdir "%out_folder%"/plots/
mkdir "%out_folder%"/word-frequency/


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



rem SENTIMENT VOTES SCATTERPLOT
rem ============================


python plots/sentiment-votes-plot.py "%out_folder%"/ngrams/top-n-posts/top-30-bottom.csv "%out_folder%"/plots/sentiment-votes-top-30-bottom.pdf
python plots/sentiment-votes-plot.py "%out_folder%"/ngrams/top-n-posts/top-30-top.csv    "%out_folder%"/plots/sentiment-votes-top-30-top.pdf
python plots/sentiment-votes-plot.py --ptsize 0.5 --sentiment 1 --cidx-score 4 --xmax 500 --xmin -100 "%in_data%" "%out_folder%"/plots/sentiment-votes-full-data.pdf



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

rem python ngrams.py --n 2 --thread-count 8 --column-index 1 --stopwords "%in_data%" --lowest-frequency 20 "%out_folder%"/ngrams/bigrams-without-stopwords.csv
rem python ngrams.py --n 2 --thread-count 8 --column-index 1 "%in_data%" --lowest-frequency 20 "%out_folder%"/ngrams/bigrams-with-stopwords.csv

python ngrams.py --n 4 --thread-count 8 --column-index 1 "%in_data%" --lowest-frequency 10 "%out_folder%"/ngrams/quadgrams-with-stopwords.csv
python ngrams.py --n 4 --thread-count 8 --column-index 1 --stopwords "%in_data%" --lowest-frequency 10 "%out_folder%"/ngrams/quadgrams-without-stopwords.csv

python ngrams.py --n 3 --thread-count 8 --column-index 1 "%in_data%" --lowest-frequency 10 "%out_folder%"/ngrams/trigrams-with-stopwords.csv
python ngrams.py --n 3 --thread-count 8 --column-index 1 --stopwords "%in_data%" --lowest-frequency 10 "%out_folder%"/ngrams/trigrams-without-stopwords.csv





rem SENTIMENT FREQUENCY HISTOGRAM
rem =============================

python plots/sentiment-frequency-histogram.py --bins 8   "%in_data%" "%out_folder%"/plots/sentiment-frequency
python plots/sentiment-frequency-histogram.py --bins 16  "%in_data%" "%out_folder%"/plots/sentiment-frequency
python plots/sentiment-frequency-histogram.py --bins 32  "%in_data%" "%out_folder%"/plots/sentiment-frequency
python plots/sentiment-frequency-histogram.py --bins 64  "%in_data%" "%out_folder%"/plots/sentiment-frequency
python plots/sentiment-frequency-histogram.py --bins 128 "%in_data%" "%out_folder%"/plots/sentiment-frequency

pause
