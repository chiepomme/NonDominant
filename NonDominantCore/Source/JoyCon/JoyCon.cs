using JoyConSharp;

namespace NonDominant
{
    public class JoyCon : IDisposable
    {
        readonly Button y;
        readonly Button x;
        readonly Button b;
        readonly Button a;

        readonly Button rightSR;
        readonly Button rightSL;
        readonly Button zr;
        readonly Button r;

        readonly Button minus;
        readonly Button plus;
        readonly Button rStick;
        readonly Button lStick;
        readonly Button home;
        readonly Button capture;
        readonly Button chargingGrip;

        readonly Button down;
        readonly Button up;
        readonly Button right;
        readonly Button left;

        readonly Button leftSR;
        readonly Button leftSL;
        readonly Button zl;
        readonly Button l;

        readonly StickButton lStickUp;
        readonly StickButton lStickUpLeft;
        readonly StickButton lStickUpRight;
        readonly StickButton lStickDown;
        readonly StickButton lStickDownLeft;
        readonly StickButton lStickDownRight;

        readonly StickButton rStickUp;
        readonly StickButton rStickUpLeft;
        readonly StickButton rStickUpRight;
        readonly StickButton rStickDown;
        readonly StickButton rStickDownLeft;
        readonly StickButton rStickDownRight;

        readonly Button[] AllButtons;
        readonly StickButton[] StickButtons;
        readonly JoyConSharp.JoyCon joyCon;

        public JoyCon(AppConfig config, VirtualKeyboard keyboard, JoyConSharp.JoyCon joyCon)
        {
            this.joyCon = joyCon;

            zl = new Button(JoyConButtons.ZL, keyboard, new NullButton(), new NullButton(), config.ZL);
            zr = new Button(JoyConButtons.ZR, keyboard, new NullButton(), new NullButton(), config.ZR);

            y = new Button(JoyConButtons.Y, keyboard, zl, zr, config.Y);
            x = new Button(JoyConButtons.X, keyboard, zl, zr, config.X);
            b = new Button(JoyConButtons.B, keyboard, zl, zr, config.B);
            a = new Button(JoyConButtons.A, keyboard, zl, zr, config.A);

            rightSR = new Button(JoyConButtons.RightSR, keyboard, zl, zr, config.RightSR);
            rightSL = new Button(JoyConButtons.RightSL, keyboard, zl, zr, config.RightSL);
            r = new Button(JoyConButtons.R, keyboard, zl, zr, config.R);

            minus = new Button(JoyConButtons.Minus, keyboard, zl, zr, config.Minus);
            plus = new Button(JoyConButtons.Plus, keyboard, zl, zr, config.Plus);
            rStick = new Button(JoyConButtons.RStick, keyboard, zl, zr, config.RStick);
            lStick = new Button(JoyConButtons.LStick, keyboard, zl, zr, config.LStick);
            home = new Button(JoyConButtons.Home, keyboard, zl, zr, config.Home);
            capture = new Button(JoyConButtons.Capture, keyboard, zl, zr, config.Capture);
            chargingGrip = new Button(JoyConButtons.ChargingGrip, keyboard, zl, zr, config.ChargingGrip);

            down = new Button(JoyConButtons.Down, keyboard, zl, zr, config.Down);
            up = new Button(JoyConButtons.Up, keyboard, zl, zr, config.Up);
            right = new Button(JoyConButtons.Right, keyboard, zl, zr, config.Right);
            left = new Button(JoyConButtons.Left, keyboard, zl, zr, config.Left);

            leftSR = new Button(JoyConButtons.LeftSR, keyboard, zl, zr, config.LeftSR);
            leftSL = new Button(JoyConButtons.LeftSL, keyboard, zl, zr, config.LeftSL);
            l = new Button(JoyConButtons.L, keyboard, zl, zr, config.L);

            lStickUp = new StickButton(this, StickDirection.Up, true, keyboard, zl, zr, config.LStickUp);
            lStickUpLeft = new StickButton(this, StickDirection.UpLeft, true, keyboard, zl, zr, config.LStickUpLeft);
            lStickUpRight = new StickButton(this, StickDirection.UpRight, true, keyboard, zl, zr, config.LStickUpRight);
            lStickDown = new StickButton(this, StickDirection.Down, true, keyboard, zl, zr, config.LStickDown);
            lStickDownLeft = new StickButton(this, StickDirection.DownLeft, true, keyboard, zl, zr, config.LStickDownLeft);
            lStickDownRight = new StickButton(this, StickDirection.DownRight, true, keyboard, zl, zr, config.LStickDownRight);

            rStickUp = new StickButton(this, StickDirection.Up, false, keyboard, zl, zr, config.RStickUp);
            rStickUpLeft = new StickButton(this, StickDirection.UpLeft, false, keyboard, zl, zr, config.RStickUpLeft);
            rStickUpRight = new StickButton(this, StickDirection.UpRight, false, keyboard, zl, zr, config.RStickUpRight);
            rStickDown = new StickButton(this, StickDirection.Down, false, keyboard, zl, zr, config.RStickDown);
            rStickDownLeft = new StickButton(this, StickDirection.DownLeft, false, keyboard, zl, zr, config.RStickDownLeft);
            rStickDownRight = new StickButton(this, StickDirection.DownRight, false, keyboard, zl, zr, config.RStickDownRight);

            AllButtons = new[] {
                zl, zr,

                y, x, a, b,
                rightSR, rightSL, r,

                minus, plus, rStick, lStick, home, capture, chargingGrip,

                down, up, right, left,
                leftSR, leftSL, l,
            };

            StickButtons = new[]
            {
                lStickUp, lStickUpLeft, lStickUpRight, lStickDown, lStickDownLeft, lStickDownRight,
                rStickUp, rStickUpLeft, rStickUpRight, rStickDown, rStickDownLeft, rStickDownRight,
            };

            _ = StartJoyConAsync();
        }

        async ValueTask StartJoyConAsync()
        {
            joyCon.Reported += OnReported;
            joyCon.Start();
            await Task.Delay(1000);
            joyCon.EnableVibration(true);
            joyCon.SetPlayerLights(LightState.On, LightState.Off, LightState.Off, LightState.Off);
        }

        public void Tick()
        {
            foreach (var button in AllButtons)
            {
                button.Tick();
            }

            foreach (var stickButton in StickButtons)
            {
                stickButton.Tick();
            }
        }

        void OnReported(JoyConSharp.JoyCon _, StandardInputReport report)
        {
            foreach (var button in AllButtons)
            {
                button.ReportForUp(report);
            }

            foreach (var button in AllButtons)
            {
                button.ReportForDown(report);
            }

            foreach (var stickButton in StickButtons)
            {
                stickButton.ReportForUp(report);
            }

            foreach (var stickButton in StickButtons)
            {
                stickButton.ReportForDown(report);
            }
        }

        public void Rumble(int freq, double amp)
        {
            joyCon.Rumble(freq, amp);
        }

        public void Dispose()
        {
            joyCon.Reported -= OnReported;
        }
    }
}
