using System.Diagnostics;

namespace NonDominant
{
    public class Hold : ButtonState
    {
        Stopwatch? stopwatch;
        bool waitingForPressOfDoubleClick;

        readonly ButtonShift shift;

        public Hold(ButtonShift shift)
        {
            this.shift = shift;
        }

        public override void Enter()
        {
            stopwatch = Stopwatch.StartNew();

            Click(shift, ActionSet!.Down, ActionSet!.LDown, ActionSet!.RDown);
            PressDown(shift, ActionSet.Hold, ActionSet.LHold, ActionSet.RHold);
        }

        public override ButtonState? Tick()
        {
            var hasDoubleClickAction = (shift == ButtonShift.None && !ActionSet!.DoubleClick.Empty) ||
                                       (shift == ButtonShift.ZL && !ActionSet!.LDoubleClick.Empty) ||
                                       (shift == ButtonShift.ZR && !ActionSet!.RDoubleClick.Empty);
            var inDoubleClickDuration = stopwatch!.Elapsed <= TimeSpan.FromMilliseconds(200);

            if (!Pressed && (!hasDoubleClickAction || !inDoubleClickDuration))
            {
                return new Up(shift);
            }

            if (hasDoubleClickAction && !Pressed)
            {
                waitingForPressOfDoubleClick = true;
            }

            if (waitingForPressOfDoubleClick && Pressed && inDoubleClickDuration)
            {
                return new DoubleClick(shift);
            }

            return null;
        }

        public override void Exit()
        {
            PressUp(shift, ActionSet!.Hold, ActionSet!.LHold, ActionSet!.RHold);
        }

        public override ButtonState? Cancel()
        {
            return new Canceled();
        }
    }
}
