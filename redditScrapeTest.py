import praw
import re
import datetime

from pprint import pprint



reddit = praw.Reddit(client_id=client_id, client_secret=client_secret, user_agent=user_agent, username=username, password=password);

print(reddit.user.me());

i = 0;

for comment in reddit.subreddit("all").stream.comments():

    i += 1;

    if i > 100:
        body = comment.body;

        if re.search("(graphics)|(content)", body):
            author = comment.author.name;
            permalink = comment.permalink;
            created = "%s" % datetime.datetime.fromtimestamp(comment.created_utc);

            data = [ author, body, permalink, created ];

            print(data);
