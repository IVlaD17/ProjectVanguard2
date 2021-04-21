﻿using UnityEngine;

using ProjectVanguard.Models.Systems;
using ProjectVanguard.Models.Components;

namespace ProjectVanguard.Models.Entities
{
    public class Player
    {
        public string Name { get; private set; }
        public bool IsActive { get; private set; }
        public bool IsAIControlled { get; private set; }
        public ChessColor ChessColor { get; private set; }

        public GameObject PlayerObject { get; private set; }

        public PlayerView PlayerViewComponent { get; private set; }
        public PlayerInput PlayerInputComponent { get; private set; }

        public Player(string name, int playerNumber, bool isActive, bool isAIControlled)
        {
            Name = name;
            IsActive = isActive;
            IsAIControlled = isAIControlled;

            if (playerNumber == 1)
                ChessColor = ChessColor.White;
            else
                ChessColor = ChessColor.Black;

            PlayerObject = GameObject.Find($"{ChessColor}Player");

            PlayerViewComponent = new PlayerView(this);
            if (IsActive)
                PlayerInputComponent = new PlayerInput(this);
        }

        public void EndTurn()
        {
            IsActive = false;
            PlayerInputComponent = null;
        }
        public void StartTurn()
        {
            IsActive = true;
            PlayerInputComponent = new PlayerInput(this);
        }

        public void ToggleViews()
        {
            PlayerViewComponent.ToggleViews();
            PlayerViewComponent.ResetCamera();
        }
        public void LookAround(float inputAxisX, float inputAxisY)
        {
            PlayerViewComponent.AdjustCamera(inputAxisX, inputAxisY);
        }
    }
}
