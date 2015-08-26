namespace TradingPlatformDomain

open System

    type FxFrame = {
        date:DateTime;
        rate:decimal;
        high:decimal;
        low:decimal;
        }