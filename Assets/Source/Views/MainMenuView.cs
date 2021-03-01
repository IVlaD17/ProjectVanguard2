using UnityEngine;
using UnityEngine.UI;

namespace ProjectVanguard.Views
{
    public class MainMenuView
    {
        private float panelWidth;
        private float panelHeight;

        private GameObject menuPanel;

        // Panel that is used when starting a single player game.
        private Button singlePlayerButton;
        private GameObject singlePlayerPanel;

        private InputField playerNameField;

        private Text singlePlayerTimerLabel;
        private Toggle singlePlayerTimerToggle;
        private Slider singlePlayerTimerSlider;
        private GameObject singlePlayerTimerPanel;

        private Button singlePlayerPlayButton;

        // Panel that is used when starting a multi player game.
        private Button multiPlayerButton;
        private GameObject multiPlayerPanel;

        private InputField player1NameField;
        private InputField player2NameField;

        private Text multiPlayerTimerLabel;
        private Toggle multiPlayerTimerToggle;
        private Slider multiPlayerTimerSlider;
        private GameObject multiPlayerTimerPanel;


        private Button multiPlayerPlayButton;
    }
}
