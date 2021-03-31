using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

namespace ProjectVanguard.Views
{
    public class HUDView
    {
        // HUD Panel which displays the in-game details of the game, session and players.        
        private GameObject hudPanel;
        private Text gameTimeLabel;
        private Text turnTimeLabel;
        private Text crosshairLabel;
        private Text player1NameLabel;
        private Text player2NameLabel;
        private Text errorMessageLabel;

        // HUD Sub-panel which displays all the moves done so far.
        private GameObject movesPanel;
        private GameObject movesListView;
        private GameObject moveLabelPrefab;

        // Details defining each move label.
        private float moveLabelHeight;
        private RectTransform moveLabel;

        // Details defining the overall moves list.
        private float movesListHeight;
        private RectTransform movesList;

        private List<Text> moveLabels;

        public HUDView()
        {
            moveLabels = new List<Text>();
            hudPanel = GameObject.Find("HUDPanel");

            crosshairLabel = GameObject.Find("Crosshair").GetComponent<Text>();
            gameTimeLabel = GameObject.Find("GameTimeLabel").GetComponent<Text>();
            turnTimeLabel = GameObject.Find("TurnTimeLabel").GetComponent<Text>();
            player1NameLabel = GameObject.Find("Player1NameLabel").GetComponent<Text>();
            player2NameLabel = GameObject.Find("Player2NameLabel").GetComponent<Text>();
            errorMessageLabel = GameObject.Find("ErrorMessageLabel").GetComponent<Text>();

            movesPanel = GameObject.Find("MovesPanel");
            movesListView = GameObject.Find("MovesListContent");
            moveLabelPrefab = Resources.Load("UIElements/MoveLabel") as GameObject;

            movesList = movesListView.GetComponent<RectTransform>();
            moveLabel = moveLabelPrefab.GetComponent<RectTransform>();
        }

        public void Hide()
        {
            hudPanel.SetActive(false);
            movesPanel.SetActive(false);
        }
        public void Display(string player1Name, string player2Name)
        {
            UpdateErrorMessage("");

            player1NameLabel.text = player1Name;
            player2NameLabel.text = player2Name;

            hudPanel.SetActive(true);
            movesPanel.SetActive(true);
        }
        public bool IsVisible()
        {
            return hudPanel.activeSelf;
        }

        public void UpdateErrorMessage(string message)
        {
            errorMessageLabel.text = message;
        }

        public void UpdateGameTimeLabel(string time)
        {
            gameTimeLabel.text = $"Game Time: {time}";
        }
        public void UpdateTurnTimeLabel(string time)
        {
            turnTimeLabel.text = $"Turn Time: {time}";
        }

        public void ToggleCrosshairDisplay()
        {
            crosshairLabel.enabled = !crosshairLabel.enabled;
        }
        
        public void CreateNewMoveLabel(string move)
        {
            GameObject moveLabelGO = Object.Instantiate(moveLabelPrefab);
            Text moveLabel = moveLabelGO.GetComponent<Text>();

            moveLabel.text = move;
            moveLabels.Add(moveLabel);

            moveLabelGO.name = $"MoveLabel{moveLabels.Count}";
            moveLabelGO.transform.SetParent(movesListView.transform);

            movesListHeight += moveLabelHeight;
            movesList.sizeDelta = new Vector2(0, movesListHeight);
        }
        public void UpdateMoveLabel(string move)
        {
            moveLabels[moveLabels.Count - 1].text += $" - {move}";
        }
    }
}
