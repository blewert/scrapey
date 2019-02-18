@echo off
sphinx-apidoc --force --separate -A "Benjamin Williams" -H "twitter-scrape" -o doc/rst ./ "scraper"
rm -rf doc/html
mkdir "doc/html"
sphinx-build -b singlehtml doc/rst doc/html doc/rst/index.rst
