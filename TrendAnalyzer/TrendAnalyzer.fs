module TrendAnalyzer

open TradingPlatformDomain
open FxFrameAnalyzer

let propagateEvent frm =
    let patternFoundEvent = new Event<_>()
    patternFoundEvent.Trigger(frm)

let orderTimeSeries frms =
    frms |> List.sortBy (fun x -> x.date)

/// candleFoundHandler - can be event trigger or a method that creates a trad signal or simply true/false
let findEnclosed fxFrames handlerFn =
    let ordered = fxFrames |> orderTimeSeries
    let rec iterate frms =
        match (frms) with
        | [] -> ignore
        | [_] -> ignore
        | cur::prev::tail ->
            //if curWithinPrev cur prev then candleWithinFoundEvent.Trigger(cur.time)
            if curWithinPrev cur prev then handlerFn(cur)
            iterate tail
    iterate ordered

let findEnclosedfn frms =
    frms 
    |> List.sortBy (fun x -> x.date)
    |> List.pairwise
    |> List.filter (fun (a, b) -> a.high > b.high && a.low < b.low)
    |> List.map (fun (a, b) -> b)

let movingAvgShortAboveLongBuy fxFrames maShort maMedium maLong =
    let ordered = fxFrames |> orderTimeSeries |> List.toArray
    ordered
    |> Array.indexed
    |> Array.filter (fun ix ->
        let i = fst ix
        let iS = if i - maShort < 0 then 0 else i - maShort
        let iM = if i - maMedium < 0 then 0 else i - maMedium
        let iL = if i - maLong < 0 then 0 else i - maLong
        let x = snd ix
        let shortMA = ordered.[iS..i] |> Array.averageBy (fun x -> (x.high + x.low) / 2m)
        let mediumMA = ordered.[iM..i] |> Array.averageBy (fun x -> (x.high + x.low) / 2m)
        let longMA = ordered.[iL..i] |> Array.averageBy (fun x -> (x.high + x.low) / 2m)
        (mediumMA > shortMA && shortMA > longMA))
    |> Array.skipWhile (fun ix -> (snd ix).date >= (ordered.[maLong]).date)
    |> Array.map (fun ix -> snd ix)

// compare long medium short values with prev step to make sure there was a change.
let movingAvgShortAboveLongBuySell fxFrames maShort maMedium maLong =
    let ordered = fxFrames |> orderTimeSeries |> List.toArray
    let mutable ps = 0m
    let mutable pm = 0m
    let mutable pl = 0m
    ordered 
    |> Array.indexed
    |> Array.map (fun ix ->
        let i = fst ix
        let iS = if i - maShort < 0 then 0 else i - maShort
        let iM = if i - maMedium < 0 then 0 else i - maMedium
        let iL = if i - maLong < 0 then 0 else i - maLong
        let x = snd ix
        let shortMA = ordered.[iS..i] |> Array.averageBy (fun x -> x.rate)
        let mediumMA = ordered.[iM..i] |> Array.averageBy (fun x -> x.rate)
        let longMA = ordered.[iL..i] |> Array.averageBy (fun x -> x.rate)
        let isLong = mediumMA > shortMA && shortMA > longMA 
        let isShort = mediumMA < shortMA && shortMA < longMA
        let isPrevLong = pm < ps && ps < pl
        let isPrevShort = pm > ps && ps > pl
        ps <- shortMA
        pm <- mediumMA
        pl <- longMA
        if isLong && isPrevShort then
            Some {direction=SignalDirection.Long;item=x}
        else if isShort && isPrevLong then
            Some {direction=SignalDirection.Short;item=x}
        else
            None)
    |> Array.filter (fun ix -> ix.IsSome)
    |> Array.map (fun ix -> ix.Value)
    |> Array.skipWhile (fun ix -> ix.item.date >= (ordered.[maLong]).date)