namespace TradeManager

open GetData
open FSharp.Data
open FSharp.Data.CsvExtensions
open TradingPlatformDomain
open TrendAnalyzer
open OrderManager
open Portfolio
open System
open MovingAverageStrategy

type TradeManager (pairName:string, portfolio:IPortfolio) =
    let pairName = pairName
    
    let loader = new DataLoader(pairName) 
    //let resAvg = movingAvgShortAboveLongBuySell usdEurRates 20 40 120
    //let resAvg = movAvgStrategyFxFrame 90 30 loader.Rates

    let executeAlgorithm timeSeries (portfolio:IPortfolio) algorithm param1 param2 =
        let res = algorithm param1 param2 timeSeries
        res 
        |> Array.iter (fun x -> 
            match x.direction with
            | SignalDirection.Long -> 
                //p.ClearPositions x.item
                portfolio.Buy x.item
            | SignalDirection.Short -> 
                //p.ClearPositions x.item
                portfolio.Sell x.item)
        let lastFxRate = loader.Rates |> List.last 
        portfolio.ClearPositions lastFxRate
        portfolio

    let finalResult = 0

    let rnd = new System.Random()
    
    // find best pair of parameters
    let maSamples = 
        Seq.initInfinite (fun _ -> 
            let lng = rnd.Next(2,150)
            let shrt = rnd.Next(1,lng)
            (lng,shrt)) 
        |> Seq.mapi (fun i lngShrt ->
            let lng = (fst lngShrt)
            let shrt = (snd lngShrt)
            let res = executeAlgorithm loader.Rates (new FxPortfolio(1000m, 0.1m):>IPortfolio) movAvgStrategyFxFrame lng shrt
            printfn "%i Long: %i Short: %i Cashe: %f Units: %i" i lng shrt res.CasheBalance res.UnitBalance
            lng,shrt,res
           )
        |> Seq.take 1000

    let bestMA = maSamples |> Seq.maxBy (fun (a, b, c) -> c.CasheBalance) 
    let lng, shrt, ptf = bestMA
    // run using the best parameters 
    let p28 = 
        executeAlgorithm 
            loader.Rates 
            (new FxPortfolio(1000m, 0.1m):>IPortfolio) 
            movAvgStrategyFxFrame 
            lng
            shrt 

    let lenTrd = List.length p28.Trades
    let lenCB = List.length p28.CasheBalanceRecord

    let dates = p28.Trades |> List.map (fun x -> x.date)
    let cashDate = List.zip dates p28.CasheBalanceRecord

    member this.BestResult
        with get() = lng, shrt, ptf

    member this.CashAndDates
        with get() = cashDate

    // TODO: Add ToString to portfolio to print out all the data