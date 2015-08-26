//open GetData
//open FSharp.Data.CsvExtensions
//open TradingPlatformDomain
//open TrendAnalyzer
//open OrderManager
open TradeManager
open Portfolio
open FSharp.Charting
open System

[<EntryPoint>]
let main argv = 
    let trdMgr = new TradeManager("EURUSD", new FxPortfolio(1000.0m, 0.1m))
    let lng, shrt, res = trdMgr.BestResult
    
    printfn "Best"
    printfn "Long: %i Short: %i Cashe: %f Units: %i" lng shrt res.CasheBalance res.UnitBalance

    Chart.Line(trdMgr.CashAndDates).ShowChart() |> ignore

    System.Windows.Forms.Application.Run()

    0 // return an integer exit code

//    let usdEur = 
//        new GetCurrencyData "GBPUSD" 
//        |> (fun x -> x.getRates) 
//        |> (fun x -> x.Rows)
//    
//    let usdEurRates = 
//        usdEur 
//        |> Seq.map (fun x -> 
//                        let frm = {
//                            date = System.DateTime.Parse x?Date;
//                            rate = System.Decimal.Parse x?Rate;
//                            high = System.Decimal.Parse x.["High (est)"];
//                            low = System.Decimal.Parse x.["Low (est)"];
//                        }
//                        frm)
//        |> Seq.toList
    //usdEurRates |> Seq.take 15 |> Seq.iter (fun x -> printfn "%A" x)
    
    
    //let res = findEnclosedfn usdEurRates

    //res |> List.iter (fun x -> printfn "%A" x)
//    let findFrameByDate series date =
//        series 
//        |> List.sortBy (fun x -> x.date)
//        |> List.tryFind (fun x -> x.date >= date)
//
//    let trimSeries series trimStart trimEnd =
//        let takeCount = (List.length series) - trimStart - trimEnd
//        series 
//        |> List.sortBy (fun x -> x.date)
//        |> List.skip trimStart 
//        |> List.take takeCount
//
//    let buyAndClose fxFrm (p:Portfolio) holdPrdDays qty =
//        p.Buy fxFrm qty
//        let trimmedSeries = trimSeries usdEurRates 0 holdPrdDays // trim the series so there is no risk the searched date exceeds the current set
//        let sellFrm = (findFrameByDate trimmedSeries (fxFrm.date.AddDays((float)holdPrdDays)))
//        match sellFrm with
//        | Some sellFrm -> p.Sell sellFrm qty
//        | None -> ()
//
//    let sellAndClose fxFrm (p:Portfolio) holdPrdDays qty =
//        p.Sell fxFrm qty
//        let trimmedSeries = trimSeries usdEurRates 0 holdPrdDays
//        let buyFrm = (findFrameByDate trimmedSeries (fxFrm.date.AddDays((float)holdPrdDays)))
//        match buyFrm with
//        | Some buyFrm -> p.Buy buyFrm qty
//        | None -> ()
       
//    let holdStrategyTest =
//        let testHoldPrd holdPrd =
//            let p = new Portfolio()
//            res 
//            |> List.iter (fun x -> buyAndClose x p holdPrd 100)
//            printfn "Holding prd: %i Balance: %s Assetts: %i" holdPrd (p.CasheBalance.ToString()) p.UnitBalance
//            p
        // todo: plot results 
//
//        let p1 = testHoldPrd 1
//        let p3 = testHoldPrd 3
//        let p7 = testHoldPrd 7
//        let p14 = testHoldPrd 14
//        let p28 = testHoldPrd 28
//        let p60 = testHoldPrd 60
//    
//        Chart.Line(p28.CasheBalanceRecord).ShowChart() |> ignore
    
    //let resAvg = movingAvgShortAboveLong usdEurRates 5 10 25
//
//    let resAvg = movingAvgShortAboveLong usdEurRates 15 30 120
//
//    let movAvgStrategyTest holdPrd=
//        let p = new Portfolio()
//        resAvg
//        |> Array.iter (fun x -> buyAndClose x p holdPrd 100)
//        // close the last position
//        let lastFrm = Array.last resAvg
//        if p.UnitBalance > 0 
//            then p.Sell lastFrm p.UnitBalance
//            else p.Buy lastFrm p.UnitBalance
//        printfn "Holding prd: %i Balance: %s Assetts: %i" holdPrd (p.CasheBalance.ToString()) p.UnitBalance
//        p
//
//    let p1 = movAvgStrategyTest 1
//    let p3 = movAvgStrategyTest 3
//    let p7 = movAvgStrategyTest 7
//    let p14 = movAvgStrategyTest 14
//    let p28 = movAvgStrategyTest 28
//    let p60 = movAvgStrategyTest 60
//
//    let lenTrd = List.length p28.trades
//    let lenCB = List.length p28.CasheBalanceRecord
//
//    let dates = p28.trades |> List.map (fun x -> x.date)
//    let casheDate = List.zip dates p28.CasheBalanceRecord

    
