### @file      sentiment-test.py
### @author    Benjamin Williams <bwilliams@lincoln.ac.uk>
### @copyright CC 3.0 <https://creativecommons.org/licenses/by/3.0/>
###

#We're going to read/write csv files, and use nltk
import csv
import nltk
#..
#In particular, we are going to use VADER for sentiment analysis
from nltk.sentiment.vader import SentimentIntensityAnalyzer
from nltk import tokenize
#..

# The file to test on
file_name = "../replayable-search-reddit.csv";
file_name_out_neg = "../sentiment-split-negative.csv";
file_name_out_pos = "../sentiment-split-positive.csv";
file_name_out_neu = "../sentiment-split-neutral.csv";

# Open the csv file
csv_file = open(file_name, 'r', newline='', encoding='utf-8');
out_csv_file_neg = open(file_name_out_neg, 'w+', newline='', encoding='utf-8');
out_csv_file_pos = open(file_name_out_pos, 'w+', newline='', encoding='utf-8');
out_csv_file_neu = open(file_name_out_neu, 'w+', newline='', encoding='utf-8');

# Make a csv reader from the open file
csv_reader = csv.reader(csv_file);
csv_writer_neg = csv.writer(out_csv_file_neg);
csv_writer_pos = csv.writer(out_csv_file_pos);
csv_writer_neu = csv.writer(out_csv_file_neu);

# The actual sentiment analyser
analyser = SentimentIntensityAnalyzer();

# And the headers to prepend to the csv file
headers = ["row_num", "comment", "verdict", "neg", "neu", "pos", "compound"];

# The current row number which is going to be written
i = 0;

# Prepend & write the headers to the csv file
csv_writer_neg.writerow(headers);
csv_writer_pos.writerow(headers);
csv_writer_neu.writerow(headers);

score_neg = 0;
score_neu = 0;
score_pos = 0;

for row in csv_reader:

    # Run through every row in the input csv file. Get the comment
    # from this csv file.
    comment = row[1];

    # The row to write to the csv file -- this will be appeneded to
    row_to_write = [ i, comment ];

    # Use the tokenizer to tokenize the paragraph into individual sentences
    sentences = tokenize.sent_tokenize(comment);

    # The final scores (this is the same output that VADER gives, but we're
    # going to summate the scores of each sentence
    final_score = { "neg" : 0, "neu" : 0, "pos" : 0, "compound" : 0 };

    for sentence in sentences:

        # Run through each tokenized sentence. Find the polarity scores for
        # this sentence
        scores = analyser.polarity_scores(sentence);

        # Add the relevant values for each key (this is just lazy code)
        final_score["neg"] += scores["neg"];
        final_score["neu"] += scores["neu"];
        final_score["pos"] += scores["pos"];
        final_score["compound"] += scores["compound"];

    # Scores and verdicts to choose from
    scores = [ final_score["neg"], final_score["pos"], final_score["neu"] ];
    verdicts = [ "negative", "positive", "neutral" ];
    writers = [ csv_writer_neg, csv_writer_pos, csv_writer_neu ];

    # We need to find the index of the highest value in scores
    max_index, max_value = max(enumerate(scores), key = lambda x: x[1]);

    # Calculate the verdict
    verdict = verdicts[max_index];

    # And write the verdict
    row_to_write.append(verdicts[max_index]);

    # Finally, extend the row we're going to write to the csv (multi append)
    row_to_write.extend([final_score["neg"], final_score["neu"], final_score["pos"], final_score["compound"]]);

    # And actually use the csv write to write the row to the csv file
    writers[max_index].writerow(row_to_write);

    # Add scores so we can see in the console
    if max_index == 0: score_neg += 1;
    if max_index == 1: score_pos += 1;
    if max_index == 2: score_neu += 1;

    # Increment the processed/written row count so it can be displayed in the console
    i += 1;

    # If the processed row count is some multiple of 100, then just write out a message
    # so we're aware of the progress
    if i % 100 == 0:
        print("row %d processed + written. (%d neg, %d pos, %d neu)" % (i, score_neg, score_pos, score_neu));

# Finally, close the output csv file
out_csv_file_neg.close();
out_csv_file_pos.close();
out_csv_file_neu.close();

# And close it
csv_file.close();

# Close output files
out_csv_file_neg.close();
out_csv_file_pos.close();
out_csv_file_neu.close();
