namespace NonDominant
{
    public class Canceled : ButtonState
    {
        public override ButtonState? Tick()
        {
            if (!Pressed)
            {
                return new Idle();
            }

            return null;
        }
    }
}
