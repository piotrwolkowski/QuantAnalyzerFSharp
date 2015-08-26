module MovingAverage

open System

let movingAverage (lookBack:int) (series:decimal array) = 
    let lb = (Math.Abs lookBack) - 1 
    // Array slicing takes item including starting index 
    // and including ending index. The lookBack variable
    // specifies the number of items hence it has to decremented by one.
    series
    |> Array.mapi (fun i x ->
       let stInd = if i - lb < 0 then 0 else i - lb
       Array.average series.[stInd..i])