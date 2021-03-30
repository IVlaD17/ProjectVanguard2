using UnityEngine;

using ProjectVanguard.Views;
using ProjectVanguard.Models;
using ProjectVanguard.Models.Entities;

namespace ProjectVanguard.Controllers
{
    public class MainMenuController : MonoBehaviour
    {
        private MainMenuView view;

        public Models.Entities.Session Session { get; private set; }

        // Start is called before the first frame update
        private void Start()
        {
            view = new MainMenuView();
            view.Display();
        }

        // Update is called once per frame
        private void Update()
        {
            if (Models.Entities.Game.Instance.GameState == GameState.InMenu)
            {
                if(!view.MenuPanel.activeSelf)
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

            Session = new Models.Entities.Session(CreatePlayers(playerNames, 0), turnTime);
            Models.Entities.Game.Instance.ChangeGameState(GameState.Playing);

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

            Session = new Models.Entities.Session(CreatePlayers(playerNames, -1), turnTime);
            Models.Entities.Game.Instance.ChangeGameState(GameState.Playing);

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

        private Models.Entities.Player[] CreatePlayers(string[] playerNames, int aiPlayerIndex)
        {
            Models.Entities.Player[] players = new Models.Entities.Player[playerNames.Length];
            for(int index = 0; index < playerNames.Length; index++)
            {
                if (index == aiPlayerIndex)
                    players[index] = new Models.Entities.Player(true, playerNames[index]);
                else
                    players[index] = new Models.Entities.Player(false, playerNames[index]);
            }

            return players;
        }
    }
}
