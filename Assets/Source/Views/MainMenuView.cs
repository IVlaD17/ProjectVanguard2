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

        public MainMenuView()
        {
            menuPanel = GameObject.Find("MenuPanel");

            // Singleplayer Panel Elements
            singlePlayerPanel = GameObject.Find("SingleplayerPanel");
            singlePlayerButton = GameObject.Find("SingleplayerButton").GetComponent<Button>();

            playerNameField = GameObject.Find("PlayerNameInput").GetComponent<InputField>();

            singlePlayerTimerPanel = GameObject.Find("SPTimerPanel");
            singlePlayerTimerToggle = GameObject.Find("SPTimerToggle").GetComponent<Toggle>();
            singlePlayerTimerSlider = GameObject.Find("SPTimerSlider").GetComponent<Slider>();
            singlePlayerTimerLabel = GameObject.Find("SPTimerValueLabel").GetComponent<Text>();

            singlePlayerPlayButton = GameObject.Find("SPPlayButton").GetComponent<Button>();

            // Multiplayer Panel Elements
            multiPlayerPanel = GameObject.Find("MultiplayerPanel");
            multiPlayerButton = GameObject.Find("MultiplayerButton").GetComponent<Button>();

            player1NameField = GameObject.Find("Player1NameInput").GetComponent<InputField>();
            player2NameField = GameObject.Find("Player2NameInput").GetComponent<InputField>();

            multiPlayerTimerPanel = GameObject.Find("MPTimerPanel");
            multiPlayerTimerToggle = GameObject.Find("MPTimerToggle").GetComponent<Toggle>();
            multiPlayerTimerSlider = GameObject.Find("MPTimerSlider").GetComponent<Slider>();
            multiPlayerTimerLabel = GameObject.Find("MPTimerValueLabel").GetComponent<Text>();

            multiPlayerButton = GameObject.Find("MPPlayButton").GetComponent<Button>();
        }

        public void DisplaySinglePlayerPanel()
        {
            singlePlayerPanel.SetActive(true);
            singlePlayerButton.interactable = false;

            multiPlayerPanel.SetActive(false);
            multiPlayerButton.interactable = true;
        }
        public void DisplayMultiPlayerPanel()
        {
            singlePlayerPanel.SetActive(false);
            singlePlayerButton.interactable = true;

            multiPlayerPanel.SetActive(true);
            multiPlayerButton.interactable = false;
        }
    }
}
