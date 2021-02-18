using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuScene : MonoBehaviour
{
    Game myGameManager;

    int iDefaultTimerValue = 5;

    float fPanelHeight;
    float fPanelWidth;

    public GameObject MenuPanel;

    [Header("Singleplayer Panel")]
    public GameObject SingleplayerPanel;
    public Button SingleplayerButton;

    public InputField PlayerNameField;

    public Toggle SPTimerToggle;
    public GameObject SPTimerPanel;
    public Slider SPTimerSlider;
    public Text SPTimerLabel;

    public Button SPPlayButton;

    [Header("Multiplayer Panel")]
    public GameObject MultiplayerPanel;
    public Button MultiplayerButton;

    public InputField Player1NameField;
    public InputField Player2NameField;

    public Toggle MPTimerToggle;
    public GameObject MPTimerPanel;
    public Slider MPTimerSlider;
    public Text MPTimerLabel;


    public Button MPPlayButton;

    // Awake is called when the script instance is being loaded
    void Awake()
    {

    }
    // Start is called before the first frame update
    void Start()
    {
        // Get the Game Controller
        GameObject gameManager = GameObject.Find(Constants.GAME_MANAGER);
        if (gameManager != null)
            myGameManager = gameManager.GetComponent<Game>();
        else
            Debug.Log(Constants.NO_GAME_MANAGER_FOUND);

        // Setup UI Menu Size
        RectTransform spPanelRT = SingleplayerPanel.GetComponent<RectTransform>();
        RectTransform mpPanelRT = MultiplayerPanel.GetComponent<RectTransform>();
        RectTransform mPanelRT = MenuPanel.GetComponent<RectTransform>();

        fPanelHeight = spPanelRT.rect.height;
        fPanelWidth = spPanelRT.rect.width;

        float fMagnificationFactor = Screen.width / 4 / mPanelRT.rect.width;

        mPanelRT.sizeDelta = new Vector2(Screen.width / 4, 0);
        spPanelRT.sizeDelta = new Vector2(fPanelWidth, fPanelHeight * fMagnificationFactor);
        mpPanelRT.sizeDelta = new Vector2(fPanelWidth, fPanelHeight * fMagnificationFactor);

        // Setup Singleplayer Panel Input Fields
        SingleplayerPanel.SetActive(false);
        SPTimerToggle.isOn = false;
        SPTimerPanel.SetActive(false);
        SPPlayButton.interactable = false;

        // Setup Multiplayer Panel Input Fields
        MultiplayerPanel.SetActive(false);
        MPTimerToggle.isOn = false;
        MPTimerPanel.SetActive(false);
        MPPlayButton.interactable = false;
    }
    // Update is called once per frame
    void Update()
    {
        // When the Singleplayer Panel is active
        if (SingleplayerPanel.activeSelf)
        {
            // Disable the Singleplayer Button
            SingleplayerButton.interactable = false;

            // Manage the Timer Panel based on whether the Timer Toggle is checkd or not
            if (SPTimerToggle.isOn)
                SPTimerPanel.SetActive(true);
            else
                SPTimerPanel.SetActive(false);

            // Update Timer Label
            if (SPTimerToggle.isOn)
            {
                if (SPTimerSlider.value == 1)
                    SPTimerLabel.text = "1 minute";
                else
                    SPTimerLabel.text = Convert.ToString(SPTimerSlider.value) + " minutes";
            }

            // Verify whether the player provided a name
            bool bInputVerified = false;

            if (!String.IsNullOrWhiteSpace(PlayerNameField.text))
                bInputVerified = true;

            // If the player provided a name, enable the play button
            if (bInputVerified)
                SPPlayButton.interactable = true;
            else
                SPPlayButton.interactable = false;
        }
        else
        {
            // Enable Singleplayer button
            SingleplayerButton.interactable = true;

            // Clear Player Name text field
            PlayerNameField.text = null;

            // Reset Timer Toggle, Panel and Slider
            SPTimerToggle.isOn = false;
            SPTimerPanel.SetActive(false);
            SPTimerSlider.value = iDefaultTimerValue;
            SPTimerLabel.text = "5 minutes";

            // Disable the play button
            SPPlayButton.interactable = false;
        }

        // When the Multiplayer Panel is active
        if (MultiplayerPanel.activeSelf)
        {
            // Disable the Multiplayer button
            MultiplayerButton.interactable = false;

            // Manage the Timer Panel based on whether the Timer Toggle is checked or not
            if (MPTimerToggle.isOn)
                MPTimerPanel.SetActive(true);
            else
                MPTimerPanel.SetActive(false);

            // Update Timer Label
            if (MPTimerToggle.isOn)
            {
                if (MPTimerSlider.value == 1)
                    MPTimerLabel.text = "1 minute";
                else
                    MPTimerLabel.text = Convert.ToString(MPTimerSlider.value) + " minutes";
            }

            // Verify whether the players provided names
            bool bInputVerified = false;

            if (!String.IsNullOrWhiteSpace(Player1NameField.text) && !String.IsNullOrWhiteSpace(Player2NameField.text))
                bInputVerified = true;

            // If both players provided a name, enable the play button
            if (bInputVerified)
                MPPlayButton.interactable = true;
            else
                MPPlayButton.interactable = false;
        }
        else
        {
            // Enable Multiplayer button
            MultiplayerButton.interactable = true;

            // Clear Player Names text fields
            Player1NameField.text = null;
            Player2NameField.text = null;

            // Reset Timer Toggle, Panel and Slider
            MPTimerToggle.isOn = false;
            MPTimerPanel.SetActive(false);
            MPTimerSlider.value = iDefaultTimerValue;
            MPTimerLabel.text = "5 minutes";

            // Disable the play button
            MPPlayButton.interactable = false;
        }
    }

    public void OnSingleplayerButtonClicked()
    {
        MultiplayerPanel.SetActive(false);
        SingleplayerPanel.SetActive(true);
    }
    public void OnMultiplayerButtonClicked()
    {
        SingleplayerPanel.SetActive(false);
        MultiplayerPanel.SetActive(true);
    }
    public void OnPlayButtonClicked()
    {
        if (MultiplayerPanel.activeSelf)
        {
            string player1Name = Player1NameField.text;
            string player2Name = Player2NameField.text;

            bool isTurnTimerEnabled = MPTimerToggle.isOn;
            float turnTime = 0f;
            if (isTurnTimerEnabled)
                turnTime = MPTimerSlider.value;

            myGameManager.LoadGameData(player1Name, player2Name, false, isTurnTimerEnabled, turnTime);
        }
        else
        {
            string playerName = PlayerNameField.text;

            bool isTurnTimerEnabled = SPTimerToggle.isOn;
            float turnTime = 0f;
            if (isTurnTimerEnabled)
                turnTime = SPTimerSlider.value;

            myGameManager.LoadGameData(playerName, "Le Bot", true, isTurnTimerEnabled, turnTime);
        }

        myGameManager.PlayGame();
    }
    public void OnExitButtonClicked()
    {
        myGameManager.ExitGame();
    }
}