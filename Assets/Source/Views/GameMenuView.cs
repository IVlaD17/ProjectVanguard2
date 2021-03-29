using UnityEngine;

namespace ProjectVanguard.Views
{
    public class GameMenuView
    {
        // Panel that shows up when the game is paused.
        public GameObject PausedPanel { get; private set; }

        // Confirm action modal box.
        public GameObject ModalPanel { get; private set; }

        public GameMenuView()
        {
            PausedPanel = GameObject.Find("PausedPanel");
            ModalPanel = GameObject.Find("ModalPanel");
        }

        public void TogglePausedPanel()
        {
            PausedPanel.SetActive(!PausedPanel.activeSelf);
        }
        public void ToggleModalPanel()
        {
            ModalPanel.SetActive(!ModalPanel.activeSelf);
        }
    }
}
