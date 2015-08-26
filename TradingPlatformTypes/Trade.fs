namespace TradingPlatformDomain

open System

type buySell =
    | buy = 1
    | sell = 2

type trade = {
    totalValue:decimal;
    direction:buySell;
    qty:int;
    rate:decimal;
    date:DateTime;
    }