namespace Scripts.Utilities
{
    public sealed class InputManager : AInputManager<InputManager>
    {
        public InputInfo Move { get; private set; }

        protected sealed override void Init()
        {
            Move = Setup(_ia.Player.Move, InputType.Value2);
        }
    }
}
