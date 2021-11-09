using JoyConSharp;
using System.Data;
using System.Reactive.Linq;
using System.Reactive.Subjects;

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

        readonly Button lStickUp;
        readonly Button lStickUpLeft;
        readonly Button lStickUpRight;
        readonly Button lStickDown;
        readonly Button lStickDownLeft;
        readonly Button lStickDownRight;

        readonly Button rStickUp;
        readonly Button rStickUpLeft;
        readonly Button rStickUpRight;
        readonly Button rStickDown;
        readonly Button rStickDownLeft;
        readonly Button rStickDownRight;

        readonly JoyConSharp.JoyCon joyCon;
        readonly ISubject<StandardInputReport> reportStream;
        readonly IDisposable tickStreamSubscription;

        public JoyCon(AppConfig config, VirtualKeyboard keyboard, JoyConSharp.JoyCon joyCon)
        {
            this.joyCon = joyCon;

            reportStream = new Subject<StandardInputReport>();
            joyCon.Reported += OnReported;
            joyCon.Start();

            joyCon.EnableVibration(true);
            joyCon.SetPlayerLights(LightState.On, LightState.Off, LightState.Off, LightState.Off);

            var zlStream = CreatePressedStream(reportStream, JoyConButtons.ZL);
            var zrStream = CreatePressedStream(reportStream, JoyConButtons.ZR);

            zl = new Button(keyboard, zlStream, new NullButton(), new NullButton(), config.ZL);
            zr = new Button(keyboard, zrStream, new NullButton(), new NullButton(), config.ZR);

            y = new Button(keyboard, CreatePressedStream(reportStream, JoyConButtons.X), zl, zr, config.Y);
            x = new Button(keyboard, CreatePressedStream(reportStream, JoyConButtons.Y), zl, zr, config.X);
            b = new Button(keyboard, CreatePressedStream(reportStream, JoyConButtons.A), zl, zr, config.B);
            a = new Button(keyboard, CreatePressedStream(reportStream, JoyConButtons.B), zl, zr, config.A);

            rightSR = new Button(keyboard, CreatePressedStream(reportStream, JoyConButtons.RightSR), zl, zr, config.RightSR);
            rightSL = new Button(keyboard, CreatePressedStream(reportStream, JoyConButtons.RightSL), zl, zr, config.RightSL);
            r = new Button(keyboard, CreatePressedStream(reportStream, JoyConButtons.R), zl, zr, config.R);

            minus = new Button(keyboard, CreatePressedStream(reportStream, JoyConButtons.Minus), zl, zr, config.Minus);
            plus = new Button(keyboard, CreatePressedStream(reportStream, JoyConButtons.Plus), zl, zr, config.Plus);
            rStick = new Button(keyboard, CreatePressedStream(reportStream, JoyConButtons.RStick), zl, zr, config.RStick);
            lStick = new Button(keyboard, CreatePressedStream(reportStream, JoyConButtons.LStick), zl, zr, config.LStick);
            home = new Button(keyboard, CreatePressedStream(reportStream, JoyConButtons.Home), zl, zr, config.Home);
            capture = new Button(keyboard, CreatePressedStream(reportStream, JoyConButtons.Capture), zl, zr, config.Capture);
            chargingGrip = new Button(keyboard, CreatePressedStream(reportStream, JoyConButtons.ChargingGrip), zl, zr, config.ChargingGrip);

            down = new Button(keyboard, CreatePressedStream(reportStream, JoyConButtons.Down), zl, zr, config.Down);
            up = new Button(keyboard, CreatePressedStream(reportStream, JoyConButtons.Up), zl, zr, config.Up);
            right = new Button(keyboard, CreatePressedStream(reportStream, JoyConButtons.Right), zl, zr, config.Right);
            left = new Button(keyboard, CreatePressedStream(reportStream, JoyConButtons.Left), zl, zr, config.Left);

            leftSR = new Button(keyboard, CreatePressedStream(reportStream, JoyConButtons.LeftSR), zl, zr, config.LeftSR);
            leftSL = new Button(keyboard, CreatePressedStream(reportStream, JoyConButtons.LeftSL), zl, zr, config.LeftSL);
            l = new Button(keyboard, CreatePressedStream(reportStream, JoyConButtons.L), zl, zr, config.L);

            lStickUp = new Button(keyboard, CreateStickStream(reportStream, StickDirection.Up, leftStick: true), zl, zr, config.LStickUp);
            lStickUpLeft = new Button(keyboard, CreateStickStream(reportStream, StickDirection.UpLeft, leftStick: true), zl, zr, config.LStickUpLeft);
            lStickUpRight = new Button(keyboard, CreateStickStream(reportStream, StickDirection.UpRight, leftStick: true), zl, zr, config.LStickUpRight);
            lStickDown = new Button(keyboard, CreateStickStream(reportStream, StickDirection.Down, leftStick: true), zl, zr, config.LStickDown);
            lStickDownLeft = new Button(keyboard, CreateStickStream(reportStream, StickDirection.DownLeft, leftStick: true), zl, zr, config.LStickDownLeft);
            lStickDownRight = new Button(keyboard, CreateStickStream(reportStream, StickDirection.DownRight, leftStick: true), zl, zr, config.LStickDownRight);

            rStickUp = new Button(keyboard, CreateStickStream(reportStream, StickDirection.Up, leftStick: false), zl, zr, config.RStickUp);
            rStickUpLeft = new Button(keyboard, CreateStickStream(reportStream, StickDirection.UpLeft, leftStick: false), zl, zr, config.RStickUpLeft);
            rStickUpRight = new Button(keyboard, CreateStickStream(reportStream, StickDirection.UpRight, leftStick: false), zl, zr, config.RStickUpRight);
            rStickDown = new Button(keyboard, CreateStickStream(reportStream, StickDirection.Down, leftStick: false), zl, zr, config.RStickDown);
            rStickDownLeft = new Button(keyboard, CreateStickStream(reportStream, StickDirection.DownLeft, leftStick: false), zl, zr, config.RStickDownLeft);
            rStickDownRight = new Button(keyboard, CreateStickStream(reportStream, StickDirection.DownRight, leftStick: false), zl, zr, config.RStickDownRight);

            tickStreamSubscription = Observable.Interval(TimeSpan.FromMilliseconds(20)).Subscribe(_ =>
            {
                zl.Update();
                zr.Update();

                y.Update();
                x.Update();
                b.Update();
                a.Update();

                rightSR.Update();
                rightSL.Update();
                r.Update();

                minus.Update();
                plus.Update();
                rStick.Update();
                lStick.Update();
                home.Update();
                capture.Update();
                chargingGrip.Update();

                down.Update();
                up.Update();
                right.Update();
                left.Update();

                leftSR.Update();
                leftSL.Update();
                l.Update();

                lStickUp.Update();
                lStickUpLeft.Update();
                lStickUpRight.Update();
                lStickDown.Update();
                lStickDownLeft.Update();
                lStickDownRight.Update();

                rStickUp.Update();
                rStickUpLeft.Update();
                rStickUpRight.Update();
                rStickDown.Update();
                rStickDownLeft.Update();
                rStickDownRight.Update();
            });
        }

        void OnReported(JoyConSharp.JoyCon _, StandardInputReport report)
        {
            reportStream.OnNext(report);
        }

        static IObservable<bool> CreatePressedStream(IObservable<StandardInputReport> reportObservable, JoyConButtons button)
        {
            return reportObservable
                        .Select(report => report.GetPressed(button))
                        .DistinctUntilChanged();
        }

        IObservable<bool> CreateStickStream(IObservable<StandardInputReport> reportObservable, StickDirection direction, bool leftStick)
        {
            var stickStream = reportObservable
                        .Select(report =>
                        {
                            var x = leftStick ? report.LeftStickX : report.RightStickX;
                            var y = leftStick ? report.LeftStickY : report.RightStickY;
                            return StickInterpreter.Interpret(x, y) == direction;
                        })
                        .DistinctUntilChanged();

            stickStream
                .Where(pressed => pressed)
                .Subscribe(_ =>
                {
                    joyCon.Rumble(150, 0.5);
                    Thread.Sleep(30);
                    joyCon.Rumble(150, 0);
                });

            return stickStream;
        }

        public void Dispose()
        {
            tickStreamSubscription.Dispose();

            joyCon.Reported -= OnReported;
            reportStream.OnCompleted();
        }
    }
}
