module GetFxData

open FSharp.Data
open FSharp.Data.CsvExtensions

open System
open TradingPlatformDomain

type private fxLoader (pair:string) =

    let pair = pair

    member private this.getRatesByUrl (url:string) =
        let rates = CsvFile.Load(url)
        rates

    member this.getRates =
        let baseUrl = sprintf "https://www.quandl.com/api/v1/datasets/CURRFX/%s.csv" pair
        this.getRatesByUrl baseUrl

    //https://www.quandl.com/help/api
    member this.getRatesByDate (startDate:DateTime) (endDate:DateTime) =
        let baseUrl = sprintf "https://www.quandl.com/api/v1/datasets/CURRFX/%s.csv" pair
        let url = 
            System.String.Format("{0}?trim_start={1}&trim_end={2}",
                baseUrl,
                startDate.ToString("yyyy-mm-dd"),
                endDate.ToString("yyyy-mm-dd"))
        this.getRatesByUrl url
    
type FxLoader(pair:string) =
    let pair = pair
    let loader = new fxLoader(pair)
    let rawData = loader.getRates.Rows
    let rates = 
        rawData
        |> Seq.map (fun x -> 
                        let frm = {
                            date = System.DateTime.Parse x?Date;
                            rate = System.Decimal.Parse x?Rate;
                            high = System.Decimal.Parse x.["High (est)"];
                            low = System.Decimal.Parse x.["Low (est)"];
                        }
                        frm)
        |> Seq.toList

    member this.RawData with get() = rawData
    member this.Rates with get() = rates
    member this.RatesByDate startDate stopDate = loader.getRatesByDate startDate stopDate