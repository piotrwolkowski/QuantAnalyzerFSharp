namespace Portfolio

open TradingPlatformDomain
open System

type FxPortfolio(casheBalance, tradePercentage) =

    let mutable casheBalance = casheBalance

    let tradePercentage = tradePercentage

    let mutable casheBalanceRecord : decimal List = []

    let mutable unitBalanceRecord : int List = []

    [<DefaultValue>]
    val mutable private unitBalance : int

    [<DefaultValue>]
    val mutable private trades : trade list
    
    member private this.createBuyTrade (fxFrame:FxFrame) qty =
        let trade = {
            totalValue = fxFrame.rate * (decimal)qty;
            direction = buySell.buy;
            qty = qty;
            rate = fxFrame.rate;
            date = fxFrame.date;
        }
        trade
    
    member private this.createSellTrade (fxFrame:FxFrame) qty =
        let trade = {
            totalValue = fxFrame.rate * (decimal)qty;
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

        member this.UnitBalance
            with get() = this.unitBalance
            and set(value) =
                this.unitBalance <- value
                unitBalanceRecord <- this.unitBalance :: unitBalanceRecord 

        member this.CasheBalance
            with get() = casheBalance
            and set(value) =
                casheBalance <- value
                casheBalanceRecord <- casheBalance :: casheBalanceRecord
    
        member this.Buy fxFrame =
            let qty = Math.Abs((int) (Math.Floor ((tradePercentage * (this :> IPortfolio).CasheBalance) / fxFrame.rate)))
            let trd = this.createBuyTrade fxFrame qty
            (this :> IPortfolio).CasheBalance <- (this :> IPortfolio).CasheBalance - trd.totalValue
            (this :> IPortfolio).UnitBalance <- (this :> IPortfolio).UnitBalance + qty
            this.trades <- trd :: this.trades

        member this.Sell fxFrame = 
            let qty = Math.Abs((int) (Math.Floor ((tradePercentage * (this :> IPortfolio).CasheBalance) / fxFrame.rate)))
            let trd = this.createSellTrade fxFrame qty
            (this :> IPortfolio).CasheBalance <- (this :> IPortfolio).CasheBalance + trd.totalValue
            (this :> IPortfolio).UnitBalance <- (this :> IPortfolio).UnitBalance - qty
            this.trades <- trd :: this.trades

        member this.ClearPositions fxFrame =
            if (this :> IPortfolio).UnitBalance > 0
                then
                    let qty = (Math.Abs this.unitBalance)
                    let sellTrd = this.createSellTrade fxFrame this.unitBalance
                    (this :> IPortfolio).CasheBalance <- (this :> IPortfolio).CasheBalance + sellTrd.totalValue
                    (this :> IPortfolio).UnitBalance <- (this :> IPortfolio).UnitBalance - qty
                    this.trades <- sellTrd :: this.trades
                else 
                    let qty = (Math.Abs this.unitBalance)
                    let buyTrd = this.createBuyTrade fxFrame qty
                    (this :> IPortfolio).CasheBalance <- (this :> IPortfolio).CasheBalance - buyTrd.totalValue
                    (this :> IPortfolio).UnitBalance <- (this :> IPortfolio).UnitBalance + qty
                    this.trades <- buyTrd :: this.trades

// Add ToString that prints out the data.
// Add a field to keep the initial portfolio value.