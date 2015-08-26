module Archived


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
//    let buyAndClose fxFrm (p:IPortfolio) holdPrdDays qty =
//        p.Buy fxFrm
//        let trimmedSeries = trimSeries usdEurRates 0 holdPrdDays // trim the series so there is no risk the searched date exceeds the current set
//        let sellFrm = (findFrameByDate trimmedSeries (fxFrm.date.AddDays((float)holdPrdDays)))
//        match sellFrm with
//        | Some sellFrm -> p.Sell sellFrm
//        | None -> ()
//
//    let sellAndClose fxFrm (p:IPortfolio) holdPrdDays =
//        p.Sell fxFrm
//        let trimmedSeries = trimSeries usdEurRates 0 holdPrdDays
//        let buyFrm = (findFrameByDate trimmedSeries (fxFrm.date.AddDays((float)holdPrdDays)))
//        match buyFrm with
//        | Some buyFrm -> p.Buy buyFrm
//        | None -> ()
//       
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

//    let resAvg = movingAvgShortAboveLongBuy usdEurRates 15 30 120
//
//    let movAvgStrategyTest holdPrd=
//        let p = portfolio
//        resAvg |> Array.iter (fun x -> buyAndClose x p holdPrd 100)
//        // close the last position
//        p.ClearPositions (Array.last resAvg)
//        printfn "Holding prd: %i Balance: %s Assetts: %i" holdPrd (p.CasheBalance.ToString()) p.UnitBalance
//        p