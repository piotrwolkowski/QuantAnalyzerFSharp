namespace TradingPlatformDomain

open System

    type Frame = {
        date:DateTime;
        opn:decimal;
        high:decimal;
        low:decimal;
        close:decimal;
        volume:decimal;
        adjClose:decimal;
        }