module MovingAverageStrategy

open System
open TradingPlatformDomain

let movAvgStrategy (long:int) (short:int) (series:decimal array) =
    let lMA = (Math.Abs long) - 1
    let sMA = (Math.Abs short) - 1
    series
    |> Array.mapi (fun i x ->
        let sIndex = if i - sMA < 0 then 0 else i - sMA
        let lIndex = if i - lMA < 0 then 0 else i - lMA
        let sAvg = Array.average series.[sIndex..i]
        let lAvg = Array.average series.[lIndex..i]

        let j = if i - 1 < 0 then 0 else i - 1
        let sIndexPrev = if j - sMA < 0 then 0 else j - sMA
        let lIndexPrev = if j - lMA < 0 then 0 else j - lMA
        let sAvgPrev = Array.average series.[sIndex..j]
        let lAvgPrev = Array.average series.[lIndex..j]

        if (sAvg > lAvg && sAvgPrev <= lAvgPrev) then
            Some ({direction=SignalDirection.Long;item=x})
        else if (sAvg < lAvg && sAvgPrev >= lAvgPrev) then
            Some ({direction=SignalDirection.Short;item=x})
        else
            None)

let movAvgStrategyFxFrame (long:int) (short:int) (series:FxFrame list) =
    let lMA = (Math.Abs long) - 1
    let sMA = (Math.Abs short) - 1
    let arrSeries = series |> List.toArray
    arrSeries
    |> Array.mapi (fun i x ->
        let sIndex = if i - sMA < 0 then 0 else i - sMA
        let lIndex = if i - lMA < 0 then 0 else i - lMA
        let sAvg = Array.averageBy (fun x -> x.rate) arrSeries.[sIndex..i]
        let lAvg = Array.averageBy (fun x -> x.rate) arrSeries.[lIndex..i]

        let j = if i - 1 < 0 then 0 else i - 1
        let sIndexPrev = if j - sMA < 0 then 0 else j - sMA
        let lIndexPrev = if j - lMA < 0 then 0 else j - lMA
        let sAvgPrev = Array.averageBy (fun x -> x.rate) arrSeries.[sIndexPrev..j]
        let lAvgPrev = Array.averageBy (fun x -> x.rate) arrSeries.[lIndexPrev..j]

        if (sAvg > lAvg && sAvgPrev <= lAvgPrev) then
            Some ({direction=SignalDirection.Long;item=x})
        else if (sAvg < lAvg && sAvgPrev >= lAvgPrev) then
            Some ({direction=SignalDirection.Short;item=x})
        else
            None)
    |> Array.filter (fun x -> x.IsSome)
    |> Array.map (fun x -> x.Value)
    |> Array.skipWhile (fun x -> x.item.date >= arrSeries.[long].date)
