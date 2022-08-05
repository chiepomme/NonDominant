using JoyConSharp;

namespace NonDominant
{
    public class Button : IButton
    {
        readonly StateMachine? fsm;
        JoyConButtons JoyConButton { get; }

        bool LastPressed { get; set; }
        public bool Pressed { get; private set; }
        public bool Cancelled { get; set; }

        protected Button() { }

        public Button(JoyConButtons joyConButton, VirtualKeyboard keyboard, Button l, Button r, ActionSet actionSet)
        {
            JoyConButton = joyConButton;
            fsm = new StateMachine(this, keyboard, actionSet, l, r);
        }

        public void Tick()
        {
            fsm?.Tick();
        }

        public void Cancel()
        {
            fsm?.Cancel();
        }

        public void Report(StandardInputReport report)
        {
            LastPressed = Pressed;
            Pressed = report.GetPressed(JoyConButton);
        }
    }
}
