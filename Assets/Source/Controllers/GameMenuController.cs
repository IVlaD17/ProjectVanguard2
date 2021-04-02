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
            if (Models.Entities.Game.Instance.IsSessionPaused() && !view.IsThePausedPanelVisible())
                view.TogglePausedPanel();

            if (Models.Entities.Game.Instance.IsSessionPlaying() && view.IsThePausedPanelVisible())
                view.TogglePausedPanel();

            if (Models.Entities.Game.Instance.IsSessionPlaying() && view.IsTheModalPanelVisible())
                view.ToggleModalPanel();
        }

        public void OnPausedPanelResumeButtonClicked()
        {
            Models.Entities.Game.Instance.ResumeSession();
        }
        public void OnPausedPanelQuitButtonClicked()
        {
            view.ToggleModalPanel();
        }

        public void OnModalPanelYesButtonClicked()
        {
            view.ToggleModalPanel();
            view.TogglePausedPanel();
            Models.Entities.Game.Instance.ExitSession();
        }
        public void OnModalPanelNoButtonClicked()
        {
            view.ToggleModalPanel();
        }
    }
}