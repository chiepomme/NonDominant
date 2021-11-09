using System.Reactive.Linq;

namespace NonDominant
{
    public class Button
    {
        readonly StateMachine? fsm;

        public bool Pressed { get; private set; }
        public bool Cancelled { get; set; }

        protected Button() { }

        public Button(VirtualKeyboard keyboard, IObservable<bool> pressedStream, Button l, Button r, ActionSet actionSet)
        {
            fsm = new StateMachine(this, keyboard, actionSet, l, r);
            pressedStream.Subscribe(pressed => Pressed = pressed);
        }

        public void Update()
        {
            fsm?.Tick();
        }

        public void Cancel()
        {
            fsm?.Cancel();
        }
    }
}
