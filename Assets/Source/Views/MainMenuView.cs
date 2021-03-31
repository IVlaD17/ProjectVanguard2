using System;

using UnityEngine;
using UnityEngine.UI;

namespace ProjectVanguard.Views
{
    public class MainMenuView
    {
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

            multiPlayerPlayButton = GameObject.Find("MPPlayButton").GetComponent<Button>();
        }        

        public void Reset()
        {
            menuPanel.SetActive(true);

            // Setup Singleplayer Panel Input Fields
            singlePlayerButton.interactable = true;
            singlePlayerPanel.SetActive(false);
            playerNameField.text = "";
            singlePlayerTimerToggle.isOn = false;
            singlePlayerTimerPanel.SetActive(false);
            singlePlayerPlayButton.interactable = false;

            // Setup Multiplayer Panel Input Fields
            multiPlayerButton.interactable = true;
            multiPlayerPanel.SetActive(false);
            player1NameField.text = "";
            player2NameField.text = "";
            multiPlayerTimerToggle.isOn = false;
            multiPlayerTimerPanel.SetActive(false);
            multiPlayerPlayButton.interactable = false;
        }
        public void Display()
        {
            // Setup Singleplayer Panel Input Fields
            singlePlayerButton.interactable = true;
            singlePlayerPanel.SetActive(false);
            singlePlayerTimerToggle.isOn = false;
            singlePlayerTimerPanel.SetActive(false);
            singlePlayerPlayButton.interactable = false;

            // Setup Multiplayer Panel Input Fields
            multiPlayerButton.interactable = true;
            multiPlayerPanel.SetActive(false);
            multiPlayerTimerToggle.isOn = false;
            multiPlayerTimerPanel.SetActive(false);
            multiPlayerPlayButton.interactable = false;
        }

        public bool IsVisible()
        {
            return menuPanel.activeSelf;
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

        public void ToggleSinglePlayerTimerPanel()
        {
            singlePlayerTimerPanel.SetActive(!singlePlayerTimerPanel.activeSelf);
        }
        public void ToggleMultiPlayerTimerPanel()
        {
            multiPlayerTimerPanel.SetActive(!multiPlayerTimerPanel.activeSelf);
        }

        public void ToggleMainMenu()
        {
            menuPanel.SetActive(!menuPanel.activeSelf);
        }

        public void UpdateSinglePlayerTimerPanel()
        {
            if (singlePlayerTimerPanel.activeSelf)
            {
                if (singlePlayerTimerSlider.value == 1)
                {
                    singlePlayerTimerLabel.text = "1 minute";
                }
                else
                {
                    singlePlayerTimerLabel.text = Convert.ToString(singlePlayerTimerSlider.value) + " minutes";
                }
            }
        }
        public void UpdateMultiPlayerTimerPanel()
        {
            if (multiPlayerTimerPanel.activeSelf)
            {
                if (multiPlayerTimerSlider.value == 1)
                {
                    multiPlayerTimerLabel.text = "1 minute";
                }
                else
                {
                    multiPlayerTimerLabel.text = Convert.ToString(multiPlayerTimerSlider.value) + " minutes";
                }
            }
        }

        public void UpdateSinglePlayerPlayButton()
        {
            if(!string.IsNullOrWhiteSpace(playerNameField.text))
            {
                singlePlayerPlayButton.interactable = true;
            }
            else
            {
                singlePlayerPlayButton.interactable = false;
            }
        }
        public void UpdateMultiPlayerPlayButton()
        {
            if(!string.IsNullOrWhiteSpace(player1NameField.text) && !string.IsNullOrWhiteSpace(player2NameField.text))
            {
                multiPlayerPlayButton.interactable = true;
            }
            else
            {
                multiPlayerPlayButton.interactable = false;
            }
        }

        public bool IsSinglePlayerPanelActive()
        {
            return singlePlayerPanel.activeSelf;
        }
        public bool IsMultiPlayerPanelActive()
        {
            return multiPlayerPanel.activeSelf;
        }

        public string[] GetSinglePlayerNames()
        {
            string[] playerNames = new string[] { "", "Le Bot" };
            if (!string.IsNullOrWhiteSpace(playerNameField.text))
            {
                playerNames[0] = playerNameField.text;
            }
            else
            {
                playerNames[0] = "";
            }
            return playerNames;
        }
        public string[] GetMultiPlayerNames()
        {
            string[] playerNames = new string[2];
            if (!string.IsNullOrWhiteSpace(player1NameField.text))
            {
                playerNames[0] = player1NameField.text;
            }
            else
            {
                playerNames[0] = "";
            }

            if (!string.IsNullOrWhiteSpace(player2NameField.text))
            {
                playerNames[1] = player2NameField.text;
            }
            else
            {
                playerNames[1] = "";
            }
            return playerNames;
        }

        public float GetSinglePlayerTurnTime()
        {
            float turnTime = 0f;
            if (singlePlayerTimerToggle.isOn)
            {
                turnTime = singlePlayerTimerSlider.value;
            }
            return turnTime;
        }
        public float GetMultiPlayerTurnTime()
        {
            float turnTime = 0f;
            if (multiPlayerTimerToggle.isOn)
            {
                turnTime = multiPlayerTimerSlider.value;
            }
            return turnTime;
        }
    }
}
