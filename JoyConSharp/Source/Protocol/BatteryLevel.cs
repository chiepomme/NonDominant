namespace JoyConSharp
{
    public enum BatteryLevel : byte
    {
        Empty = 0,
        Critical = 1 << 1,
        Low = 1 << 2,
        Medium = 1 << 3,
        Full = 1 << 4,
    }
}
