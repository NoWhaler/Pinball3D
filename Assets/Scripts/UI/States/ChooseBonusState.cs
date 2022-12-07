using UnityEngine;

namespace UI.States
{
    public class ChooseBonusState : UIBaseState
    {
        public ChooseBonusState(UIStateMachine currentStateContext, UIStateFactory stateFactory) : base(currentStateContext, stateFactory)
        {
        }

        public override void EnterState()
        {
            StateContext.ChooseBonusCanvas.gameObject.SetActive(true);
            Time.timeScale = 0;
        }

        protected override void UpdateState()
        {
            CheckStates();
        }

        protected override void ExitState()
        {
            StateContext.ChooseBonusCanvas.gameObject.SetActive(false);
            Time.timeScale = 1;
        }
        
        private void CheckStates()
        {
            if (!StateContext.IsChoosingBonus)
            {
                SwitchState(StateFactory.Gameplay());
            }
        }
    }
}