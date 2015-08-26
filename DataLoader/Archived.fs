module Archived
//
//    let movAvgStrategyTest =
//        let p = portfolio
//        resAvg 
//        |> Array.iter (fun x -> 
//            match x with
//            | {direction=SignalDirection.Long} -> 
//                //p.ClearPositions x.item
//                p.Buy x.item
//            | {direction=SignalDirection.Short} -> 
//                //p.ClearPositions x.item
//                p.Sell x.item)
//        let lastFxRate = loader.Rates |> List.last 
//        p.ClearPositions lastFxRate
//        printfn "Balance: %s Assetts: %i"  (p.CasheBalance.ToString()) p.UnitBalance        
//        p
//    