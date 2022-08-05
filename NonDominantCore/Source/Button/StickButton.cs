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

        public string Name => $"Stick {(IsLeftStick ? "L" : "R")} {Direction}";

        public StickButton(JoyCon joyCon, StickDirection direction, bool isLeftStick, VirtualKeyboard keyboard, Button l, Button r, ActionSet actionSet)
        {
            JoyCon = joyCon;
            Direction = direction;
            IsLeftStick = isLeftStick;
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

        bool IsPressed(StandardInputReport report)
        {
            var x = IsLeftStick ? report.LeftStickX : report.RightStickX;
            var y = IsLeftStick ? report.LeftStickY : report.RightStickY;

            return StickInterpreter.Interpret(x, y) == Direction;
        }

        public void ReportForUp(StandardInputReport report)
        {
            lock (this)
            {
                if (!IsPressed(report))
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
                LastPressed = Pressed;

                if (IsPressed(report))
                {
                    Pressed = true;
                }

                if (!LastPressed && Pressed)
                {
                    _ = RumbleAsync();
                }

                fsm?.Tick();
            }
        }

        async ValueTask RumbleAsync()
        {
            JoyCon.Rumble(150, 0.5);
            await Task.Delay(30);
            JoyCon.Rumble(150, 0);
        }
    }
}
