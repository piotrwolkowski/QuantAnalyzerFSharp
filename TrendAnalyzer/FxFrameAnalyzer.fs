module FxFrameAnalyzer
    open TradingPlatformDomain

    let curWithinPrev (cur:FxFrame) (prev:FxFrame) =
        prev.low < cur.low && prev.high > cur.high