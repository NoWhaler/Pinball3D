namespace UI.States
{
    public class GameplayState: UIBaseState
    {
        public GameplayState(UIStateMachine currentStateContext, UIStateFactory stateFactory) : base(currentStateContext, stateFactory)
        {
        }

        public override void EnterState()
        {
            StateContext.GamePlayCanvas.gameObject.SetActive(true);
        }

        protected override void UpdateState()
        {
            CheckStates();
        }

        protected override void ExitState()
        {
            StateContext.GamePlayCanvas.gameObject.SetActive(false);
        }
        
        private void CheckStates()
        {
            if (StateContext.IsSettingsButtonPressed)
            {
                SwitchState(StateFactory.Settings());
                StateContext.IsSettingsButtonPressed = false;
            }
            if (StateContext.IsLevelEnded)
            {
                SwitchState(StateFactory.Win());
            }
        }
    }
}