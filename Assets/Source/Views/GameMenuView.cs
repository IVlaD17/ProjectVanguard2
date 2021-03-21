﻿using UnityEngine;

namespace ProjectVanguard.Views
{
    public class GameMenuView
    {
        // Panel that shows up when the game is paused.
        private GameObject pausedPanel;

        // Confirm action modal box.
        private GameObject modalPanel;

        public GameMenuView()
        {
            pausedPanel = GameObject.Find("PausedPanel");
            modalPanel = GameObject.Find("PopupPanel");
        }
    }
}
