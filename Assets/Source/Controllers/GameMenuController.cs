using UnityEngine;

using ProjectVanguard.Views;
using ProjectVanguard.Models;

namespace ProjectVanguard.Controllers
{
    public class GameMenuController : MonoBehaviour
    {
        public GameMenuView View { get; private set; }

        // Start is called before the first frame update
        private void Start()
        {
            View = new GameMenuView();
            View.ToggleModalPanel();
            View.TogglePausedPanel();
        }

        // Update is called once per frame
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                View.TogglePausedPanel();
        }

        public void OnPausedPanelResumeButtonClicked()
        {
            View.TogglePausedPanel();
        }
        public void OnPausedPanelQuitButtonClicked()
        {
            View.ToggleModalPanel();
        }

        public void OnModalPanelYesButtonClicked()
        {
            View.ToggleModalPanel();
            View.TogglePausedPanel();
            Models.Entities.Game.Instance.ChangeGameState(GameState.InMenu);
        }
        public void OnModalPanelNoButtonClicked()
        {
            View.ToggleModalPanel();
        }
    }
}