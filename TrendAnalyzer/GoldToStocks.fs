module GoldToStocks

open System
open TradingPlatformDomain

// TODO: Add dates to determine from where
// Long fall in stocks triggers buying gold.
// Hold for the period of stock rising. If stocks start falling sell gold. 
// Determine a fall period that triggers selling stocks.
let goldToSpStrategy stockFallPeriod (gold:List<Frame>) (index:List<Frame>) =
    
    let initDate (series:List<Frame>) = 
        series
        |> List.map (fun x -> x.date)
        |> List.min 
    let endDate series =
        series
        |> List.map (fun x -> x.date)
        |> List.max

    let startDate = if (initDate gold) > (initDate index) then (initDate gold) else (initDate index)
    let stopDate = if (endDate gold) < (endDate index) then (endDate gold) else (endDate index)
    let trimOrdSeries series start stop =
        series 
        |> List.sortBy (fun x -> x.date) 
        |> List.skipWhile (fun x -> x.date >= start)
        |> List.takeWhile (fun x -> x.date <= stop)

    let ordG = trimOrdSeries gold startDate stopDate
    let ordI = trimOrdSeries index startDate stopDate
    
    let lng = Math.Abs (stockFallPeriod - 1)
    let shr = (int) (Math.Floor ((decimal)lng/3m))

    ordG
    |> List.map (fun x -> x.close)
    |> List.mapi (fun i x ->
        let lngI = if i - lng < 0 then 0 else i - lng
        let shrI = if i - shr < 0 then 0 else i - shr
        let lngAvg = 
            ordI
            |> List.toArray 
            |> fun a -> a.[lngI..i]
            |> Array.averageBy (fun z -> z.close)
        let shrAvg = 
            ordI
            |> List.toArray 
            |> fun a -> a.[shrI..i]
            |> Array.averageBy (fun z -> z.close)
        if shrAvg < lngAvg then 
            Some ({direction=SignalDirection.Long;item=x})
        else
            Some ({direction=SignalDirection.Short;item=x})
    )