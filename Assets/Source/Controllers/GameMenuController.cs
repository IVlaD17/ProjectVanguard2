using UnityEngine;

using ProjectVanguard.Views;

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
            Debug.Log("Quitting the game.");
            // TODO: Quit the game
            // TODO: Implement singleton Game class. (Acceptable)
        }
        public void OnModalPanelNoButtonClicked()
        {
            View.ToggleModalPanel();
        }
    }
}