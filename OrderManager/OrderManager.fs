module OrderManager

open TradingPlatformDomain

let buy (fxFrame:FxFrame) qty =
    let trade = {
        totalValue = fxFrame.rate * (decimal)qty;
        direction = buySell.buy;
        qty = qty;
        rate = fxFrame.rate;
        date = fxFrame.date;
    }
    trade

let sell (fxFrame:FxFrame) qty =
    let trade = {
        totalValue = fxFrame.rate * (decimal)qty;
        direction = buySell.sell;
        qty = qty;
        rate = fxFrame.rate;
        date = fxFrame.date;
    }
    trade



    
