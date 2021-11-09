using System.Diagnostics;

namespace NonDominant
{
    public class BranchClick : ButtonState
    {
        Stopwatch? stopwatch;
        bool waitingForPressOfDoubleClick;

        readonly ButtonShift shift;

        public BranchClick(ButtonShift shift)
        {
            this.shift = shift;
        }

        public override void Enter()
        {
            stopwatch = Stopwatch.StartNew();
            Click(shift, ActionSet!.Down, ActionSet!.LDown, ActionSet!.RDown);
        }

        public override ButtonState? Tick()
        {
            var hasDoubleClickAction = (shift == ButtonShift.None && !ActionSet.DoubleClick.Empty) ||
                                       (shift == ButtonShift.ZL && !ActionSet.LDoubleClick.Empty) ||
                                       (shift == ButtonShift.ZR && !ActionSet.RDoubleClick.Empty);

            if (!hasDoubleClickAction || stopwatch!.Elapsed > TimeSpan.FromMilliseconds(200))
            {
                return new Click(shift);
            }

            if (!Pressed)
            {
                waitingForPressOfDoubleClick = true;
            }

            if (waitingForPressOfDoubleClick && Pressed)
            {
                return new DoubleClick(shift);
            }

            return null;
        }

        public override ButtonState Cancel()
        {
            return new Canceled();
        }
    }
}
