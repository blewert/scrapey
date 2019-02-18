### @file      SpreadSheetWriter.py
### @author    Benjamin Williams <bwilliams@lincoln.ac.uk>
### @copyright CC 3.0 <https://creativecommons.org/licenses/by/3.0/>
###

#Import http and os functions
import httplib2
import os

#Import google api stuff for sheets
from apiclient import discovery
from oauth2client import client
from oauth2client import tools
from oauth2client.file import Storage

#Globals to be used by the writer.. these shouldn't really need to be changed
SCOPES = "https://www.googleapis.com/auth/spreadsheets";
CLIENT_SECRET_FILE = "client_secret.json";
APPLICATION_NAME = "twitter-scrape";

class SpreadSheetWriter:
    """A class used to provide an easy-to-use interface for appending and
    modifying google sheets.
    """


    def __init__(self, spreadsheetID):
        """Constructs a new instance of a spreadsheet writer.

        Args:
            spreadsheetID (str): The ID of the spreadsheet to write to. For example, if
            your spreadsheet has the URL `https://docs.google.com/spreadsheets/d/1DWy0gA6CqIKybjD1tXaCuEzASD7MGu5VS09Qe2hw-L4/edit#gid=976092889`,
            then the ID is `1DWy0gA6CqIKybjD1tXaCuEzASD7MGu5VS09Qe2hw-L4`.

        Examples:
            Create a new writer for a spreadsheet, and append a sample row to the "tweets" sheet.
            >>> writer = SpreadSheetWriter("1DWy0gA6CqIKybjD1tXaCuEzASD7MGu5VS09Qe2hw-L4");
            >>> writer.append_row(["here is a tweet!", "18/03/2018". "@bob"], "tweets");
        """

        #Set up spreadsheet id for this writer later on.
        self.id = spreadsheetID;

        #Attempt to get credentials from file and authorise using them.
        self.credentials = self.get_credentials();
        self.authorize();

        #Entries
        self.entries = 0;


    def close_sheets(self):
        pass;


    def get_credentials(self):
        """ Gets OAuth credentials for the api, from file (/.credentials/) """

        #We want to look in the current directory
        home_dir = '.';

        #Find credentials directory inside the current directory
        credential_dir = os.path.join(home_dir, '.credentials');

        #Does it exist? If not.. create it
        if not os.path.exists(credential_dir):
            os.makedirs(credential_dir)

        #Find credentials path (this is just from the quickstart example)
        credential_path = os.path.join(credential_dir, 'sheets.googleapis.com-python-quickstart.json');

        #Get credentials from file:
        store = Storage(credential_path);
        credentials = store.get();

        if not credentials or credentials.invalid:

            #If there are no credentials or invalid ones, then store them.. setup
            flow = client.flow_from_clientsecrets(CLIENT_SECRET_FILE, SCOPES);
            flow.user_agent = APPLICATION_NAME;

            #And actually store the files in this folder
            credentials = tools.run_flow(flow, store, None);
            print('Storing credentials to ' + credential_path);

        #Finally, return the credentials
        return credentials;





    def authorize(self):
        """ The authorisation step to set up the API for later usage """

        #Just call authorize on credentials over http
        self.http = self.credentials.authorize(httplib2.Http());

        #Find discovery url for api
        self.discoveryUrl = ('https://sheets.googleapis.com/$discovery/rest?'
                    'version=v4');

        #And find the service so we can use it to modify sheets. (this is the
        #main variable used to access the api)
        self.service = discovery.build('sheets', 'v4', http=self.http,
                              discoveryServiceUrl=self.discoveryUrl);





    def clear_sheets(self, sheets):
        """ Clears all data from the `sheets` array passed to this function.

        Args:
            sheets (list): The list of sheets to clear of all data.

        Examples:
            Clears all data from the "apples" and "pears" sheets, leaving them blank.

            >>> writer.clear_sheets(["apples", "pears"])
        """

        #Find all data for this spread sheet
        sheet_data = self.service.spreadsheets().get(spreadsheetId=self.id).execute();

        #Get the sheet data for this spreadsheet
        returned_sheets = sheet_data.get("sheets", "");

        #Filter the sheets into their titles and ids
        sheet_titles = [ sheet["properties"]["title"] for sheet in returned_sheets ];
        sheet_ids = [ sheet["properties"]["sheetId"] for sheet in returned_sheets ];

        #Iterator for loop
        i = 0;

        for sheet in sheet_titles:

            #Run through every sheet title. Increment i
            i += 1;

            #If this sheet (the one hosted) is not in the list of sheets to clear
            #(passed to function) then skip
            if not sheet in sheets:
                continue;

            #Otherwise build a body for batchUpdate
            body = {
                    "requests": [
                    {
                        "updateCells": {
                            "range": {
                            "sheetId": sheet_ids[i-1]
                            },
                    "fields": "userEnteredValue"
                    }
                }]
            };

            #And call batchUpdate with this body
            self.service.spreadsheets().batchUpdate(spreadsheetId=self.id,body=body).execute();



    def create_sheets(self, sheets):
        """ Creates all the sheets specified by title in the array `sheets`, but
        only if they don't exist.

        Args:
            sheets (list): The list of sheets to create, by their titles.

        Examples:
            Create the sheets "apples", "pears" and "mangoes" but only if they
            don't exist.

            >>> writer.create_sheets(["apples", "pears", "mangoes"]);
        """

        #Get all spreadsheet data
        sheet_data = self.service.spreadsheets().get(spreadsheetId=self.id).execute();

        #Find sheet data only
        returned_sheets = sheet_data.get("sheets", "");

        #Filter to get a list of sheet titles for this spreadsheet
        sheet_titles = [ sheet["properties"]["title"] for sheet in returned_sheets ];

        for sheet in sheets:

            #Run through each sheet passed to this function.
            found = False;

            #If this sheet is found in the list of sheets (remote) then we don't
            #need to create it -- it has been found, break
            if sheet in sheet_titles:
                found = True;
                break;

            #If the sheet has been found, we don't need to create it so skip!
            if found:
                continue;

            #Otherwise build a body for batchUpdate
            body = {
                "requests": [
                    {
                        "addSheet": {
                            "properties": {
                                "title" : sheet
                            }
                        }
                    }
                ]
            };

            #And call batchUpdate
            self.service.spreadsheets().batchUpdate(spreadsheetId=self.id,body=body).execute();




    def append_row(self, data, sheet="Sheet1", range="A2:B2"):
        """ Appends a row to a sheet specified by `sheet`, starting at the range
        `range`, with the cell data (1D array) `data`.

        Args:
            data (list): The row of data to append.
            sheet (str, optional): The sheet to insert this data into.
            range (str, optional): The range to look for data to append below.

        Examples:
            Appends five rows of three random numbers to the "results" sheet.

            >>> import random
            >>> for i in range(0, 5):
            >>>     row = [random.random() for j in range(0, 3)];
            >>>     writer.append_row(row, "results");

            Append a single row with the cells [1, 2, 3] to the "testing" sheet,
            starting at cell B2 (data is automatically found by the api and appended
            below these rows).
            >>> writer.append_row([1, 2, 3], "testing", "B2:B2");
        """

        #If the data passed isn't a list, make it into a single-cell list
        if not isinstance(data, list):
            data = [data];

        #Values to append to the sheet
        values = [ data ];

        #Build the body of the request
        body = { 'values' : values };

        #And append the row to the sheet
        result = self.service.spreadsheets().values().append(
            spreadsheetId = self.id,
            range = "'" + sheet + "'!" + range,
            valueInputOption="USER_ENTERED",
            body=body
        ).execute();

        #Increment entries
        self.entries += 1;
