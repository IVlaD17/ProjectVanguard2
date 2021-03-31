using UnityEngine;

using ProjectVanguard.Views;
using ProjectVanguard.Models;

namespace ProjectVanguard.Controllers
{
    public class GameMenuController : MonoBehaviour
    {
        private GameMenuView view;

        // Start is called before the first frame update
        private void Start()
        {
            view = new GameMenuView();
            view.ToggleModalPanel();
            view.TogglePausedPanel();
        }

        // Update is called once per frame
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                view.TogglePausedPanel();
        }

        public void OnPausedPanelResumeButtonClicked()
        {
            view.TogglePausedPanel();
        }
        public void OnPausedPanelQuitButtonClicked()
        {
                view.ToggleModalPanel();
        }

        public void OnModalPanelYesButtonClicked()
        {
            view.ToggleModalPanel();
            view.TogglePausedPanel();
            Models.Entities.Game.Instance.ChangeGameState(GameState.InMenu);
        }
        public void OnModalPanelNoButtonClicked()
        {
            view.ToggleModalPanel();
        }
    }
}