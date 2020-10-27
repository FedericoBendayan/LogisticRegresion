using System;

namespace ConsoleSteps.ModelState
{
    public class Context
    {
        // A reference to the current state of the Context.
        private AbstractState _state = null;

        public Context(AbstractState state)
        {
            this.TransitionTo(state);
        }

        // The Context allows changing the State object at runtime.
        public void TransitionTo(AbstractState state)
        {
            Console.WriteLine($"Context: Transition to {state.GetType().Name}.");
            this._state = state;
            this._state.SetContext(this);
        }

        // The Context delegates part of its behavior to the current State
        // object.
        public void ResolveModelState()
        {
            this._state.Resolve();
        }

    }
}
