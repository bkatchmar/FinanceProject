# Introduction

I've been really getting into options trading along with dividend investing. As I build my portfolio up, the basic problem is: what do I invest in? I have compiled the following criteria

* Stock must allow for weekly options
* Stock must at least have a "Hold" rating by wall street analysts
* Being a dividend paying stock is a plus but not a requirement

I am combining my coding background with this new interest to help automate some of my data gathering, possibly then use this information going forward to help automate some of my decision making

## Manual Steps

This whole project was born out of both the desire for a coding project exercise and to automate the steps I take in helping me make invesment decisions. The steps involved are:

1. Extract the tickers from [Weekly Options USA](https://www.weeklyoptionsusa.com/which-stocks-have-weekly-options.html) and put them in a CSV file
2. Pair the tickers with the rating publishing on [Stock Analysis](https://stockanalysis.com/stocks/tsla/forecast/)
3. Put the data in my Google Sheets document and update the Google Lookout Studio Report to visualize the data

If we're dealing with only 5-6 stock tickers, this isn't that bad, but of course, I want to see everything possible, so naturally, I end up having to go through very copius amounts of data that can be very time consuming.

# TickerExtract

## Reading and Collecting the Tickers

The purpose of this library is to collect the stock tickers from any type of source, for now it is a text file I copy and paste from a web site, but what if I want to store this data elsewhere at a later point in time? I am hoping this can be expanded upon and automated more at a later point in time.

To accomplish this task of gathering tickers, two interfaces are used:

```
IReaderConfig
ITickerCollector
```

The primary purpose of `IReaderConfig` is to store any actual information about how and where information is stored. Once we know this, `ITickerCollector` reads the info from the desired source and generates a list of the necessary tickers.

## Manually Adding and Omitting Tickers

The main purpose here is to manually add tickers that the file pull may not pick up but we know meet our criteria and to also omit tickers if we do not wish to see specific tickers processed.

# FinanceApi

The purpose of this library is to collect market data for financial instrumnents. Right now we're using a Yahoo Quotes NuGet package, but designed to call other APIs should the need to arise.

# TO DO

- [x] Use the [YahooQuotesApi](https://www.nuget.org/packages/YahooQuotesApi/) nuget package and understand its basic functions
- [x] Create a base class structure around this API, with the possibility of using a different API in the future
- [x] Port over my ticker extraction code from a previous machine I was using, have that read from a list and gather the data for final compiliation
- [x] Generate a CSV file that acts as my core sheet
- [ ] Define my decision making process over what to invest in next, or what my next moves may be
- [ ] Currenly go with a week-over-week decision making model, but open the possibility to make bi-weekly or even monthly moves as my experience increases
- [ ] Graduate from a CSV file and automatically update a Google Sheet document, this opens the possibility to use Google Lookout Studio for future visualizations of my data
- [ ] Open up the possibilty for Forex trading down the road