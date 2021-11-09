namespace NonDominant
{
    public class DoubleClick : ButtonState
    {
        readonly ButtonShift shift;

        public DoubleClick(ButtonShift shift)
        {
            this.shift = shift;
        }

        public override void Enter()
        {
            Click(shift, ActionSet!.DoubleClick, ActionSet!.LDoubleClick, ActionSet!.RDoubleClick);
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
