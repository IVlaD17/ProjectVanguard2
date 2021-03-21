﻿using System.Collections.Generic;

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
        private float moveLabelWidth;
        private float moveLabelHeight;
        private RectTransform moveLabel;

        // Details defining the overall moves list.
        private float movesListWidth;
        private float movesListHeight;
        private RectTransform movesList;

        // Moves list state holding variables.
        private int moveNumber;
        private List<Text> moveLabels;

        public HUDView()
        {
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
        }
    }
}
