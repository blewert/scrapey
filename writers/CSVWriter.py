### @file      CSVWriter.py
### @author    Benjamin Williams <bwilliams@lincoln.ac.uk>
### @copyright CC 3.0 <https://creativecommons.org/licenses/by/3.0/>
###

#Import os functions
import os
import codecs
import threading
from filelock import Timeout, FileLock

lock = FileLock("lockfile");

class CSVWriter:

    WRITER_FOLDER_NAME = "csv";

    def __init__(self, search_name):

        #Set search name for prepending later
        self.search_name = search_name;

        #Output dir doesn't exist? make it
        if not os.path.exists("output"):
            os.makedirs("output");

        #Output subdir doesn't exist? make it
        if not os.path.exists("output/" + CSVWriter.WRITER_FOLDER_NAME):
            os.makedirs("output/" + CSVWriter.WRITER_FOLDER_NAME);

        #Set up base paths
        self.base_path = "output/" + CSVWriter.WRITER_FOLDER_NAME + "/";
        self.full_base_path = self.base_path + self.search_name + "-";
        self.entries = 0;

        #string -> file map
        self.csvs = {};



    def append_row(self, data, sheet=None, range=None):

        #With the lock (this thread has blocked)
        with lock:

            #No sheet passed? Use the first one available
            if sheet == None:
                sheet = next(iter(self.csvs.values()));

            #If the data passed isn't a list, make it into a single-cell list
            if not isinstance(data, list):
                data = [data];

            #The line to write
            line = ",".join([ "\"" + str(x).replace("\"", "\"\"") + "\"" for x in data ]) + "\n";

            #Write the data in CSV format
            self.csvs[sheet].write(line);
            self.csvs[sheet].flush();

            #Increment entries
            self.entries += 1;


    def close_sheets(self):

        print("closing sheets");

        #Close files
        for k, v in self.csvs.items():
            v.close();



    def create_sheets(self, sheets):

        #Run through each sheet
        for sheet in sheets:

            #Open up this file for writing
            self.csvs[sheet] = codecs.open(self.full_base_path + sheet + ".csv", "a+", "utf-8-sig");




    def clear_sheets(self, sheets):

        #Run through each sheet
        for sheet in sheets:

            #Does this csv file already exist? If so.. delete it.
            if os.path.isfile(self.full_base_path + sheet + ".csv"):
                os.remove(self.full_base_path + sheet + ".csv");
