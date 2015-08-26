namespace TradingPlatformDomain

type SignalDirection =
    | Long
    | Short
    | CloseLong
    | CloseShort

type Signal =
    {
        direction:SignalDirection
        item:FxFrame
    }

type GenericSignal<'a> =
    {
        direction:SignalDirection
        item:'a
    }