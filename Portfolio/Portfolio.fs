namespace Portfolio

open TradingPlatformDomain

type Portfolio(casheBalance, quantity) =
    let qty = quantity

    let mutable casheBalance = casheBalance

    let mutable casheBalanceRecord : decimal List = []

    let mutable unitBalanceRecord : int List = []

    new () = Portfolio (0.0m, 100)
    
    [<DefaultValue>]
    val mutable unitBalance : int

    [<DefaultValue>]
    val mutable trades : trade list

    member private this.createBuyTrade (fxFrame:FxFrame) quantity =
        let trade = {
            totalValue = fxFrame.rate * quantity;
            direction = buySell.buy;
            qty = qty;
            rate = fxFrame.rate;
            date = fxFrame.date;
        }
        trade
    
    member private this.createSellTrade (fxFrame:FxFrame) quantity =
        let trade = {
            totalValue = fxFrame.rate * quantity;
            direction = buySell.sell;
            qty = qty;
            rate = fxFrame.rate;
            date = fxFrame.date;
        }
        trade

    interface IPortfolio with
        member this.CasheBalanceRecord
            with get() = List.skip 1 casheBalanceRecord

        member this.UnitBalanceRecord
            with get() = List.skip 1 unitBalanceRecord

        member this.Trades
            with get() = this.trades

        member this.CasheBalance
            with get() = casheBalance
            and set(value) =
                casheBalance <- value
                casheBalanceRecord <- casheBalance :: casheBalanceRecord

        member this.UnitBalance
            with get() = this.unitBalance
            and set(value) =
                this.unitBalance <- value
                unitBalanceRecord <- this.unitBalance :: unitBalanceRecord 
        
        member this.Buy fxFrame = 
            let trd = this.createBuyTrade fxFrame ((decimal)qty)
            (this :> IPortfolio).CasheBalance <- (this :> IPortfolio).CasheBalance - trd.totalValue
            (this :> IPortfolio).UnitBalance <- (this :> IPortfolio).UnitBalance + qty
            this.trades <- trd :: this.trades
        
        member this.Sell fxFrame = 
            let trd = this.createSellTrade fxFrame ((decimal)qty)
            (this :> IPortfolio).CasheBalance <- (this :> IPortfolio).CasheBalance + trd.totalValue
            (this :> IPortfolio).UnitBalance <- (this :> IPortfolio).UnitBalance - qty
            this.trades <- trd :: this.trades

        member this.ClearPositions fxFrame = 
            if (this :> IPortfolio).UnitBalance > 0
                then 
                    let sellTrd = this.createSellTrade fxFrame ((decimal)this.unitBalance)
                    (this :> IPortfolio).CasheBalance <- (this :> IPortfolio).CasheBalance + sellTrd.totalValue
                    (this :> IPortfolio).UnitBalance <- (this :> IPortfolio).UnitBalance - qty
                    this.trades <- sellTrd :: this.trades
                else 
                    let buyTrd = this.createBuyTrade fxFrame ((decimal)this.unitBalance)
                    (this :> IPortfolio).CasheBalance <- (this :> IPortfolio).CasheBalance - buyTrd.totalValue
                    (this :> IPortfolio).UnitBalance <- (this :> IPortfolio).UnitBalance + qty
                    this.trades <- buyTrd :: this.trades