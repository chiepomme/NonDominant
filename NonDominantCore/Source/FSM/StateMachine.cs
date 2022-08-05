using System.Diagnostics;

namespace NonDominant
{
    public class StateMachine
    {
        ButtonState state;

        readonly IButton button;
        readonly VirtualKeyboard keyboard;

        readonly ActionSet actionSet;
        readonly Button l;
        readonly Button r;

        public StateMachine(IButton button, VirtualKeyboard keyboard, ActionSet actionSet, Button l, Button r)
        {
            this.button = button;
            this.keyboard = keyboard;
            this.actionSet = actionSet;
            this.l = l;
            this.r = r;

            ChangeState(new Idle());
        }

        void ChangeState(ButtonState next)
        {
            var prevState = state;
            state?.Exit();
            state = next;

            state.Button = button;
            state.Keyboard = keyboard;
            state.ActionSet = actionSet;
            state.L = l;
            state.R = r;

            state.Enter();

            if (prevState == null)
            {
                Debug.WriteLine($"({button.Name} {GetHashCode()}) {state.GetType().Name}");
            }
            else
            {
                Debug.WriteLine($"({button.Name} {GetHashCode()}) {prevState.GetType().Name} -> {state.GetType().Name}");
            }

            state.Tick();
        }

        public void Tick()
        {
            var next = state.Tick();
            if (next == null) return;
            ChangeState(next);
        }

        public void Cancel()
        {
            var next = state.Cancel();
            if (next == null) return;
            ChangeState(next);
        }
    }
}
