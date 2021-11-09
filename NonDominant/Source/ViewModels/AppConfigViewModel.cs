using Prism.Mvvm;

namespace NonDominant.ViewModels
{
    class AppConfigViewModel : BindableBase
    {
        public string AppPath { get; set; }

        public ActionSetViewModel Y { get; set; }
        public ActionSetViewModel X { get; set; }
        public ActionSetViewModel B { get; set; }
        public ActionSetViewModel A { get; set; }

        public ActionSetViewModel RightSR { get; set; }
        public ActionSetViewModel RightSL { get; set; }
        public ActionSetViewModel R { get; set; }
        public ActionSetViewModel ZR { get; set; }

        public ActionSetViewModel Minus { get; set; }
        public ActionSetViewModel Plus { get; set; }
        public ActionSetViewModel RStick { get; set; }
        public ActionSetViewModel LStick { get; set; }
        public ActionSetViewModel Home { get; set; }
        public ActionSetViewModel Capture { get; set; }
        public ActionSetViewModel ChargingGrip { get; set; }

        public ActionSetViewModel Down { get; set; }
        public ActionSetViewModel Up { get; set; }
        public ActionSetViewModel Right { get; set; }
        public ActionSetViewModel Left { get; set; }

        public ActionSetViewModel LeftSR { get; set; }
        public ActionSetViewModel LeftSL { get; set; }
        public ActionSetViewModel L { get; set; }
        public ActionSetViewModel ZL { get; set; }

        public ActionSetViewModel LStickUp { get; set; }
        public ActionSetViewModel LStickUpLeft { get; set; }
        public ActionSetViewModel LStickUpRight { get; set; }
        public ActionSetViewModel LStickDown { get; set; }
        public ActionSetViewModel LStickDownLeft { get; set; }
        public ActionSetViewModel LStickDownRight { get; set; }

        public ActionSetViewModel RStickUp { get; set; }
        public ActionSetViewModel RStickUpLeft { get; set; }
        public ActionSetViewModel RStickUpRight { get; set; }
        public ActionSetViewModel RStickDown { get; set; }
        public ActionSetViewModel RStickDownLeft { get; set; }
        public ActionSetViewModel RStickDownRight { get; set; }

        public AppConfigViewModel(AppConfig appConfig)
        {
            AppPath = appConfig.AppPath;

            Y = new ActionSetViewModel(false, appConfig.Y);
            X = new ActionSetViewModel(false, appConfig.X);
            B = new ActionSetViewModel(false, appConfig.B);
            A = new ActionSetViewModel(false, appConfig.A);

            RightSR = new ActionSetViewModel(false, appConfig.RightSR);
            RightSL = new ActionSetViewModel(false, appConfig.RightSL);
            R = new ActionSetViewModel(false, appConfig.R);
            ZR = new ActionSetViewModel(false, appConfig.ZR);

            Minus = new ActionSetViewModel(false, appConfig.Minus);
            Plus = new ActionSetViewModel(false, appConfig.Plus);
            RStick = new ActionSetViewModel(false, appConfig.RStick);
            LStick = new ActionSetViewModel(true, appConfig.LStick);
            Home = new ActionSetViewModel(false, appConfig.Home);
            Capture = new ActionSetViewModel(true, appConfig.Capture);
            ChargingGrip = new ActionSetViewModel(false, appConfig.ChargingGrip);

            Down = new ActionSetViewModel(true, appConfig.Down);
            Up = new ActionSetViewModel(true, appConfig.Up);
            Right = new ActionSetViewModel(true, appConfig.Right);
            Left = new ActionSetViewModel(true, appConfig.Left);

            LeftSR = new ActionSetViewModel(true, appConfig.LeftSR);
            LeftSL = new ActionSetViewModel(true, appConfig.LeftSL);
            L = new ActionSetViewModel(true, appConfig.L);
            ZL = new ActionSetViewModel(true, appConfig.ZL);

            RStickUp = new ActionSetViewModel(false, appConfig.RStickUp);
            RStickUpLeft = new ActionSetViewModel(false, appConfig.RStickUpLeft);
            RStickUpRight = new ActionSetViewModel(false, appConfig.RStickUpRight);
            RStickDown = new ActionSetViewModel(false, appConfig.RStickDown);
            RStickDownLeft = new ActionSetViewModel(false, appConfig.RStickDownLeft);
            RStickDownRight = new ActionSetViewModel(false, appConfig.RStickDownRight);

            LStickUp = new ActionSetViewModel(true, appConfig.LStickUp);
            LStickUpLeft = new ActionSetViewModel(true, appConfig.LStickUpLeft);
            LStickUpRight = new ActionSetViewModel(true, appConfig.LStickUpRight);
            LStickDown = new ActionSetViewModel(true, appConfig.LStickDown);
            LStickDownLeft = new ActionSetViewModel(true, appConfig.LStickDownLeft);
            LStickDownRight = new ActionSetViewModel(true, appConfig.LStickDownRight);
        }

        public AppConfig ToModel() => new()
        {
            AppPath = AppPath,

            Y = Y.ToModel(),
            X = X.ToModel(),
            B = B.ToModel(),
            A = A.ToModel(),

            RightSR = RightSR.ToModel(),
            RightSL = RightSL.ToModel(),
            R = R.ToModel(),
            ZR = ZR.ToModel(),

            Minus = Minus.ToModel(),
            Plus = Plus.ToModel(),
            RStick = RStick.ToModel(),
            LStick = LStick.ToModel(),
            Home = Home.ToModel(),
            Capture = Capture.ToModel(),
            ChargingGrip = ChargingGrip.ToModel(),

            Down = Down.ToModel(),
            Up = Up.ToModel(),
            Right = Right.ToModel(),
            Left = Left.ToModel(),

            LeftSR = LeftSR.ToModel(),
            LeftSL = LeftSL.ToModel(),
            L = L.ToModel(),
            ZL = ZL.ToModel(),

            LStickUp = LStickUp.ToModel(),
            LStickUpLeft = LStickUpLeft.ToModel(),
            LStickUpRight = LStickUpRight.ToModel(),
            LStickDown = LStickDown.ToModel(),
            LStickDownLeft = LStickDownLeft.ToModel(),
            LStickDownRight = LStickDownRight.ToModel(),

            RStickUp = RStickUp.ToModel(),
            RStickUpLeft = RStickUpLeft.ToModel(),
            RStickUpRight = RStickUpRight.ToModel(),
            RStickDown = RStickDown.ToModel(),
            RStickDownLeft = RStickDownLeft.ToModel(),
            RStickDownRight = RStickDownRight.ToModel(),
        };
    }
}
