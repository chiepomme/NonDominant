namespace NonDominant
{
    public class Up : ButtonState
    {
        readonly ButtonShift shift;

        public Up(ButtonShift shift)
        {
            this.shift = shift;
        }

        public override void Enter()
        {
            Click(shift, ActionSet!.Up, ActionSet!.LUp, ActionSet!.RUp);
        }

        public override ButtonState? Tick()
        {
            return new Idle();
        }
    }
}
