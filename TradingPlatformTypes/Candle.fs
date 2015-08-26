namespace TradingPlatformDomain

open System

type CandleTimeSpan =
    | second = 1
    | minute = 2
    | minute5 = 3
    | minute10 = 4
    | minute15 = 5
    | minute30 = 6
    | hour = 7
    | hour4 = 8
    | hour12 = 9
    | day = 10
    | week = 11

type Candle = 
    {
        symbol:string;
        low:decimal;
        high:decimal;
        opn:decimal;
        close:decimal;
        timeStamp:DateTime;
        timeSpan:CandleTimeSpan
    }

