using UnityEngine;

using ProjectVanguard.Views;
using ProjectVanguard.Models;
using ProjectVanguard.Models.Entities;

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
            if (Game.Instance.IsSessionPaused() && !view.IsThePausedPanelVisible())
                view.TogglePausedPanel();

            if (Game.Instance.IsSessionPlaying() && view.IsThePausedPanelVisible())
                view.TogglePausedPanel();

            if (Game.Instance.IsSessionPlaying() && view.IsTheModalPanelVisible())
                view.ToggleModalPanel();
        }

        public void OnPausedPanelResumeButtonClicked()
        {
            Game.Instance.ResumeSession();
        }
        public void OnPausedPanelQuitButtonClicked()
        {
            view.ToggleModalPanel();
        }

        public void OnModalPanelYesButtonClicked()
        {
            view.ToggleModalPanel();
            view.TogglePausedPanel();
            Game.Instance.ExitSession();
        }
        public void OnModalPanelNoButtonClicked()
        {
            view.ToggleModalPanel();
        }
    }
}