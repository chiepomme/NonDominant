namespace NonDominant
{
    public class Click : ButtonState
    {
        readonly ButtonShift shift;

        public Click(ButtonShift shift)
        {
            this.shift = shift;
        }

        public override void Enter()
        {
            Click(shift, ActionSet!.Click, ActionSet!.LClick, ActionSet!.RClick);
        }

        public override ButtonState? Tick()
        {
            if (!Pressed)
            {
                return new Up(shift);
            }

            return null;
        }
    }
}
