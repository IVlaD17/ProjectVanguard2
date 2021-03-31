using UnityEngine;

using ProjectVanguard.Views;
using ProjectVanguard.Models;
using ProjectVanguard.Models.Entities;

namespace ProjectVanguard.Controllers
{
    public class MainMenuController : MonoBehaviour
    {
        private MainMenuView view;

        private Models.Entities.Session session;

        // Start is called before the first frame update
        private void Start()
        {
            view = new MainMenuView();
            view.Display();
        }

        // Update is called once per frame
        private void Update()
        {
            if (!Models.Entities.Game.Instance.IsPlaying())
            {
                if(!view.IsVisible())
                    view.Reset();

                if (view.IsSinglePlayerPanelActive())
                {
                    view.UpdateSinglePlayerTimerPanel();
                    view.UpdateSinglePlayerPlayButton();
                }

                if (view.IsMultiPlayerPanelActive())
                {
                    view.UpdateMultiPlayerTimerPanel();
                    view.UpdateMultiPlayerPlayButton();
                }
            }
        }

        public void OnSinglePlayerButtonClicked()
        {
            view.DisplaySinglePlayerPanel();
        }
        public void OnSinglePlayerPlayButtonClicked()
        {
            float turnTime = view.GetSinglePlayerTurnTime();
            string[] playerNames = view.GetSinglePlayerNames();

            Models.Entities.Game.Instance.CreateGameSession(playerNames, true, turnTime);

            view.ToggleMainMenu();
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
            float turnTime = view.GetMultiPlayerTurnTime();
            string[] playerNames = view.GetMultiPlayerNames();

            Models.Entities.Game.Instance.CreateGameSession(playerNames, false, turnTime);

            view.ToggleMainMenu();
        }
        public void OnMultiPlayerTimerToggleValueChanged()
        {
            view.ToggleMultiPlayerTimerPanel();
        }

        public void OnExitButtonClicked()
        {
            Models.Entities.Game.Instance.ExitGame();
        }
    }
}
