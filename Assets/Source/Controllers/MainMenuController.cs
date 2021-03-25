using UnityEngine;

using ProjectVanguard.Views;

namespace ProjectVanguard.Controllers
{
    public class MainMenuController : MonoBehaviour
    {
        private MainMenuView view;

        // Start is called before the first frame update
        private void Start()
        {
            view = new MainMenuView();
            view.Display();
        }

        // Update is called once per frame
        private void Update()
        {
            if(view.IsSinglePlayerPanelActive())
            {
                view.UpdateSinglePlayerTimerPanel();
                view.UpdateSinglePlayerPlayButton();
            }

            if(view.IsMultiPlayerPanelActive())
            {
                view.UpdateMultiPlayerTimerPanel();
                view.UpdateMultiPlayerPlayButton();
            }
        }

        public void OnSinglePlayerButtonClicked()
        {
            view.DisplaySinglePlayerPanel();
        }
        public void OnSinglePlayerPlayButtonClicked()
        {
            string[] playerNames = view.GetSinglePlayerNames();
            float turnTime = view.GetSinglePlayerTurnTime();
        }
        public void OnSinglePlayerTimerToggleValueChanged()
        {
            view.ToggleSinglePlayerTimerPanel();
        }

        public void OnMultiPlayerButtonClicked()
        {
            view.DisplayMultiPlayerPanel();
        }
        public void OnMultiPlayerPlayButtonClicked()
        {
            string[] playerNames = view.GetMultiPlayerNames();
            float turnTime = view.GetMultiPlayerTurnTime();
        }
        public void OnMultiPlayerTimerToggleValueChanged()
        {
            view.ToggleMultiPlayerTimerPanel();
        }

        public void OnExitButtonClicked()
        {

        }
    }
}
