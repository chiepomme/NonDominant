using System.Numerics;

namespace NonDominant
{
    public static class StickInterpreter
    {
        const int CenterX = 2000;
        const int CenterY = 2000;
        const int DeadZoneRadius = 500;

        public static StickDirection Interpret(int x, int y)
        {
            if (x == 0 && y == 0) return StickDirection.Neutral;

            var position = new Vector2(x, y);
            var center = new Vector2(CenterX, CenterY);
            var vector = position - center;

            if (vector.LengthSquared() < DeadZoneRadius * DeadZoneRadius)
            {
                return StickDirection.Neutral;
            }

            var degree = Math.Atan2(vector.Y, vector.X) / Math.PI * 180.0;
            while (degree < 0) degree += 360;

            return (StickDirection)(degree / (360.0 / 6.0));
        }
    }
}
