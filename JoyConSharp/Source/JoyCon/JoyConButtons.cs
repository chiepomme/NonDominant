namespace JoyConSharp
{
    [Flags]
    public enum JoyConButtons
    {
        None = 0,

        Y = 1 << 1,
        X = 1 << 2,
        B = 1 << 3,
        A = 1 << 4,
        RightSR = 1 << 5,
        RightSL = 1 << 6,
        R = 1 << 7,
        ZR = 1 << 8,

        Minus = 1 << 9,
        Plus = 1 << 10,
        RStick = 1 << 11,
        LStick = 1 << 12,
        Home = 1 << 13,
        Capture = 1 << 14,
        ChargingGrip = 1 << 15,

        Down = 1 << 16,
        Up = 1 << 17,
        Right = 1 << 18,
        Left = 1 << 19,
        LeftSR = 1 << 20,
        LeftSL = 1 << 21,
        L = 1 << 22,
        ZL = 1 << 23,
    }
}
