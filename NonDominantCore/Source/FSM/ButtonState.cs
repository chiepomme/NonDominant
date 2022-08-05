namespace NonDominant
{
    public abstract class ButtonState
    {
        public IButton? Button { protected get; set; }
        public VirtualKeyboard? Keyboard { protected get; set; }
        public ActionSet? ActionSet { protected get; set; }
        public Button? L { private get; set; }
        public Button? R { private get; set; }

        protected bool Pressed => Button!.Pressed;
        protected bool LPressed => L!.Pressed;
        protected bool RPressed => R!.Pressed;

        public virtual void Enter() { }
        public virtual ButtonState? Tick() { return null; }
        public virtual void Exit() { }
        public virtual ButtonState? Cancel() { return null; }

        void CancelLButtonAction() => L!.Cancel();
        void CancelRButtonAction() => R!.Cancel();

        protected void Click(ButtonShift shift, ActionKey key, ActionKey lKey, ActionKey rKey)
        {
            ExecuteKeyboardAction(shift, key, lKey, rKey, true, Keyboard.Click);
        }

        protected void PressDown(ButtonShift shift, ActionKey key, ActionKey lKey, ActionKey rKey)
        {
            ExecuteKeyboardAction(shift, key, lKey, rKey, true, Keyboard.PressDown);
        }

        protected void PressUp(ButtonShift shift, ActionKey key, ActionKey lKey, ActionKey rKey)
        {
            ExecuteKeyboardAction(shift, key, lKey, rKey, false, Keyboard.PressUp);
        }

        void ExecuteKeyboardAction(ButtonShift shift, ActionKey key, ActionKey lKey, ActionKey rKey, bool cancelShiftAction, Action<ActionKey> action)
        {
            switch (shift)
            {
                case ButtonShift.None:
                    if (key.Empty) break;
                    action(key);
                    break;
                case ButtonShift.ZL:
                    if (lKey.Empty) break;
                    if (cancelShiftAction) CancelLButtonAction();
                    action(lKey);
                    break;

                case ButtonShift.ZR:
                    if (rKey.Empty) break;
                    if (cancelShiftAction) CancelRButtonAction();
                    action(rKey);
                    break;
            }
        }
    }
}
