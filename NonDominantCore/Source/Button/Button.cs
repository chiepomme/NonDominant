using JoyConSharp;

namespace NonDominant
{
    public class Button : IButton
    {
        readonly StateMachine? fsm;
        JoyConButtons JoyConButton { get; }

        public bool Pressed { get; private set; }
        public bool Cancelled { get; set; }

        public string Name => JoyConButton.ToString();

        protected Button() { }

        public Button(JoyConButtons joyConButton, VirtualKeyboard keyboard, Button l, Button r, ActionSet actionSet)
        {
            JoyConButton = joyConButton;
            fsm = new StateMachine(this, keyboard, actionSet, l, r);
        }

        public void Tick()
        {
            lock (this)
            {
                fsm?.Tick();
            }
        }

        public void Cancel()
        {
            lock (this)
            {
                fsm?.Cancel();
            }
        }

        public void ReportForUp(StandardInputReport report)
        {
            lock (this)
            {
                if (!report.GetPressed(JoyConButton))
                {
                    Pressed = false;
                }

                fsm?.Tick();
            }
        }

        public void ReportForDown(StandardInputReport report)
        {
            lock (this)
            {
                if (report.GetPressed(JoyConButton))
                {
                    Pressed = true;
                }

                fsm?.Tick();
            }
        }
    }
}
