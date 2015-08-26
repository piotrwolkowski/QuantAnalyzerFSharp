module GetSpData

open FSharp.Data
open FSharp.Data.CsvExtensions

open System
open TradingPlatformDomain

type private spLoader() =

    // TODO make the type to accept dates. currently the last date is hardcoded.
    let goldDataUrl = @"http://real-chart.finance.yahoo.com/table.csv?s=%5EGSPC&a=00&b=3&c=1950&d=07&e=22&f=2015&g=d&ignore=.csv"
    
    member this.getRates =
        let rates = CsvFile.Load(goldDataUrl)
        rates
    
type SpLoader(pair:string) =
    let pair = pair
    let loader = new spLoader()
    let rawData = loader.getRates.Rows
    let rates = 
        rawData
        |> Seq.map (fun x -> 
                        let frm = {
                            date = System.DateTime.Parse x?Date;
                            opn = System.Decimal.Parse x?Open;
                            high = System.Decimal.Parse x?High;
                            low = System.Decimal.Parse x?Low;
                            close = System.Decimal.Parse x?Close;
                            volume = System.Decimal.Parse x?Volume;
                            adjClose = System.Decimal.Parse x.["Low (est)"];
                        }
                        frm)
        |> Seq.toList

    member this.RawData with get() = rawData
    member this.Rates with get() = rates