import argparse
import csv
import re
from colorama import init, Fore, Back, Style

#Call init for colorama
init();

parser = argparse.ArgumentParser(description='Search a particular output file for key phrases.');
parser.add_argument('--column-index', type=int, default=1, help="The index of the column to pull comments from, in the CSV.");
parser.add_argument('csv_file_name', metavar='in_file', type=str, help="The input CSV file to analyse.");
parser.add_argument('out_file_name', metavar='out_file', type=str, help="The output CSV file to write, with the matched phrases.");
parser.add_argument('search_phrase', metavar='search_phrase', type=str, help="The phrase to search for.");
parser.add_argument("--display",  action='store_true', help="Whether or not to only display the output in the console");

args = parser.parse_args();

print("\r\nStarting search:")
print("-" * 10);
print("Out file: %s" % (args.out_file_name));
print("In file: %s" % (args.csv_file_name));
print("Search phrase: %s" % (args.search_phrase));
print("-" * 10 + "\r\n");

#Open the input/output csv files
in_csv_file  = open(args.csv_file_name, 'r',  newline='', encoding='utf-8');

#And make a reader for this file
csv_reader = csv.reader(in_csv_file);

if not args.display:

    #If not display only, then open up output file
    out_csv_file = open(args.out_file_name, 'w+', newline='', encoding='utf-8');
    csv_writer = csv.writer(out_csv_file);


#Count of found lines
found_lines = 0;
comment_count = 0;

for row in csv_reader:

    #Get the comment
    comment = row[args.column_index];

    #Add one to the number of comments sifted through
    comment_count += 1;

    if re.search(args.search_phrase, comment):

        #Increment found lines
        found_lines += 1;


        #And write to out csv
        if not args.display:

            #Print out match
            print("[match] match %d, comment %d" % (found_lines, comment_count));

            #And write row to csv
            csv_writer.writerow(row);

        else:

            iter = re.finditer(args.search_phrase, comment);
            indices = [(m.start(), m.end()) for m in iter ];

            output = "";

            i = 0;

            for index in indices:
                output += "".join([comment[i:index[0]], Fore.WHITE + Back.GREEN, comment[index[0]:index[1]], Back.BLACK + Fore.WHITE]);

            print(output);

            print("-" * 20);


#Close the csvs finally
in_csv_file.close();

if not args.display:
    out_csv_file.close();
