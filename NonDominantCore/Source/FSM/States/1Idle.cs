namespace NonDominant
{
    public class Idle : ButtonState
    {
        public override ButtonState? Tick()
        {
            if (LPressed)
            {
                return new ZLIdle();
            }

            if (RPressed)
            {
                return new ZRIdle();
            }

            if (Pressed)
            {
                if (!ActionSet!.Click.Empty)
                {
                    return new BranchClick(ButtonShift.None);
                }
                else
                {
                    return new Hold(ButtonShift.None);
                }
            }

            return null;
        }
    }

    public class ZLIdle : ButtonState
    {
        public override ButtonState? Tick()
        {
            if (!LPressed)
            {
                return new Idle();
            }

            if (Pressed)
            {
                if (!ActionSet!.LClick.Empty)
                {
                    return new BranchClick(ButtonShift.ZL);
                }
                else
                {
                    return new Hold(ButtonShift.ZL);
                }
            }

            return null;
        }
    }

    public class ZRIdle : ButtonState
    {
        public override ButtonState? Tick()
        {
            if (!RPressed)
            {
                return new Idle();
            }

            if (Pressed)
            {
                if (!ActionSet!.RClick.Empty)
                {
                    return new BranchClick(ButtonShift.ZR);
                }
                else
                {
                    return new Hold(ButtonShift.ZR);
                }
            }

            return null;
        }
    }
}
