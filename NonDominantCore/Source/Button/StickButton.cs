using JoyConSharp;

namespace NonDominant
{
    public class StickButton : IButton
    {
        readonly StateMachine? fsm;

        JoyCon JoyCon { get; }
        StickDirection Direction { get; }
        bool IsLeftStick { get; }

        bool LastPressed { get; set; }
        public bool Pressed { get; private set; }
        public bool Cancelled { get; set; }

        public StickButton(JoyCon joyCon, StickDirection direction, bool isLeftStick, VirtualKeyboard keyboard, Button l, Button r, ActionSet actionSet)
        {
            JoyCon = joyCon;
            Direction = direction;
            IsLeftStick = isLeftStick;
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

            var x = IsLeftStick ? report.LeftStickX : report.RightStickX;
            var y = IsLeftStick ? report.LeftStickY : report.RightStickY;

            Pressed = StickInterpreter.Interpret(x, y) == Direction;
            if (!LastPressed && Pressed)
            {
                _ = RumbleAsync();
            }

            fsm?.Tick();
        }

        async ValueTask RumbleAsync()
        {
            JoyCon.Rumble(150, 0.5);
            await Task.Delay(30);
            JoyCon.Rumble(150, 0);
        }
    }
}
