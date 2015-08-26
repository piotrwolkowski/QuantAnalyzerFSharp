namespace Portfolio

open TradingPlatformDomain

type public IPortfolio =

   abstract member UnitBalanceRecord : int list with get

   abstract member CasheBalanceRecord : decimal list with get

   abstract member UnitBalance : int with get, set

   abstract member CasheBalance : decimal with get, set

   abstract member Trades : trade list

   abstract member Sell : FxFrame -> unit

   abstract member Buy : FxFrame -> unit

   abstract member ClearPositions : FxFrame -> unit