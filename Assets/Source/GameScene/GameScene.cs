using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameScene : MonoBehaviour
{
    string gameManagerObjectName = "GameManager";
    Game myGameManager;
    Session mySessionManager;

    RectTransform movesListRT;
    float fMovesListHeight;
    float fMovesListWidth;

    RectTransform moveLabelRT;
    float fMoveLabelHeight;
    float fMoveLabelWidth;

    [Header("HUD Panel")]
    public GameObject HUDPanel;
    public Text Player1NameLabel;
    public Text Player2NameLabel;
    public Text GameTimeLabel;
    public Text ErrorMessageLabel;
    public Text TurnTimeLabel;
    public Text CrosshairLabel;

    [Header("Moves Panel")]
    public GameObject MovesPanel;
    public GameObject MovesListView;
    public GameObject MoveLabelPrefab;

    [Header("Paused Panel")]
    public GameObject PausedPanel;

    [Header("Popup Panel")]
    public GameObject PopupPanel;

    List<Text> moveLabels;
    int moveNumber = 0;

    // Awake is called when the script instance is being loaded
    void Awake()
    {
        moveLabels = new List<Text>();
    }

    // Start is called before the first frame update
    void Start()
    {
        // Get the Game Controller
        GameObject gameManager = GameObject.Find(gameManagerObjectName);
        if (gameManager != null)
            myGameManager = gameManager.GetComponent<Game>();
        else
            Debug.LogErrorFormat("Failed to find the object named {0}.", gameManagerObjectName);

        GameObject sessionManager = GameObject.Find(gameManagerObjectName);
        if (sessionManager != null)
            mySessionManager = sessionManager.GetComponent<Session>();
        else
            Debug.LogErrorFormat("Failed to find the object named {0}.", gameManagerObjectName);

        movesListRT = MovesListView.GetComponent<RectTransform>();
        fMovesListHeight = movesListRT.rect.height;
        fMovesListWidth = movesListRT.rect.width;

        moveLabelRT = MoveLabelPrefab.GetComponent<RectTransform>();
        fMoveLabelHeight = moveLabelRT.rect.height;
        fMoveLabelWidth = moveLabelRT.rect.width;

        ErrorMessageLabel.text = "";

        Player1NameLabel.text = myGameManager.Player1Name;
        Player2NameLabel.text = myGameManager.Player2Name;


        string turnMinutes = ((int)(myGameManager.TurnTime / 60)).ToString();
        if (turnMinutes.Length == 1)
            turnMinutes = "0" + turnMinutes;
        string turnSeconds = ((int)(myGameManager.TurnTime % 60)).ToString();
        if (turnSeconds.Length == 1)
            turnSeconds = "0" + turnSeconds;

        if (myGameManager.IsTurnTimerEnabled)
            TurnTimeLabel.text = "Turn Time: " + turnMinutes + ":" + turnSeconds;
        else
            TurnTimeLabel.text = "";

        PausedPanel.SetActive(false);
        PopupPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        switch (mySessionManager.MySessionState)
        {
            case SessionState.PLAYING:
                string turnMinutes = ((int)(mySessionManager.TurnTime / 60)).ToString();
                if (turnMinutes.Length == 1)
                    turnMinutes = "0" + turnMinutes;
                string turnSeconds = ((int)(mySessionManager.TurnTime % 60)).ToString();
                if (turnSeconds.Length == 1)
                    turnSeconds = "0" + turnSeconds;
                TurnTimeLabel.text = "Turn Time: " + turnMinutes + ":" + turnSeconds;

                string gameHours = ((int)(mySessionManager.GameTime / 3600)).ToString();
                if (gameHours.Length == 1)
                    gameHours = "0" + gameHours;
                string gameMinutes = ((int)(mySessionManager.GameTime % 3600 / 60)).ToString();
                if (gameMinutes.Length == 1)
                    gameMinutes = "0" + gameMinutes;
                string gameSeconds = ((int)(mySessionManager.GameTime % 60)).ToString();
                if (gameSeconds.Length == 1)
                    gameSeconds = "0" + gameSeconds;
                GameTimeLabel.text = "Game Time: " + gameHours + ":" + gameMinutes + ":" + gameSeconds;

                PausedPanel.SetActive(false);
                CrosshairLabel.enabled = true;
                break;
            default:
                PausedPanel.SetActive(true);
                CrosshairLabel.enabled = false;
                break;
        }
    }

    public void OnResumeButtonClicked()
    {
        mySessionManager.ResumeGame();
    }

    public void OnQuitButtonClicked()
    {
        PopupPanel.SetActive(true);
    }

    public void OnYesButtonClicked()
    {
        myGameManager.QuitGame();
    }

    public void OnNoButtonClicked()
    {
        PopupPanel.SetActive(false);
    }

    public void AddNewMoveLabel(string move)
    {
        if (moveNumber % 2 == 0)
        {
            GameObject moveLabelGO = Instantiate(MoveLabelPrefab);
            Text moveLabel = moveLabelGO.GetComponent<Text>();

            moveLabelGO.name = "MoveLabel" + moveNumber.ToString();
            moveLabel.text = move;

            moveLabelGO.transform.SetParent(MovesListView.transform);

            fMovesListHeight += fMoveLabelHeight;
            movesListRT.sizeDelta = new Vector2(0, fMovesListHeight);

            moveLabels.Add(moveLabel);
        }
        else
        {
            moveLabels[moveLabels.Count - 1].text += " - " + move;
        }

        moveNumber++;
    }
}
