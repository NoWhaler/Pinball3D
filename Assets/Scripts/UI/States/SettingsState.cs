﻿namespace UI.States
{
    public class SettingsState: UIBaseState
    {
        public SettingsState(UIStateMachine currentStateContext, UIStateFactory stateFactory) : base(currentStateContext, stateFactory)
        {
        }

        public override void EnterState()
        {
            StateContext.SettingsCanvas.gameObject.SetActive(true);
        }

        protected override void UpdateState()
        {
            CheckStates();
        }

        protected override void ExitState()
        {
            StateContext.SettingsCanvas.gameObject.SetActive(false);
        }
        
        private void CheckStates()
        {
            if (StateContext.IsCancelButtonPressed)
            {
                SwitchState(StateFactory.Gameplay());
                StateContext.IsCancelButtonPressed = false;
            }
        }
    }
}