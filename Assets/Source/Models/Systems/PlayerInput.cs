﻿using UnityEngine;

using ProjectVanguard.Models.Entities;

namespace ProjectVanguard.Models.Systems
{
    public class PlayerInput
    {
        public Entities.Player Player { get; private set; }

        public PlayerInput(Entities.Player player)
        {
            Player = player;
            GameUpdater.AddUpdateCallback(Update);
        }

        // Update is called once per frame
        void Update()
        {
            if (Player.IsActive)
            {
                if (Game.Instance.IsSessionPlaying())
                    Player.LookAround(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

                if (Input.GetKeyUp(KeyCode.V))
                {
                    if (Game.Instance.IsSessionPlaying())
                        Player.ToggleViews();
                }

                if (Input.GetKeyUp(KeyCode.Escape))
                {
                    if (Game.Instance.IsSessionPaused())
                        Game.Instance.ResumeSession();
                    else if (Game.Instance.IsSessionPlaying())
                        Game.Instance.PauseSession();
                }

                // Left Click
                if (Input.GetMouseButtonUp(0))
                {
                    if (Player.IsActive)
                    {
                        // If player has selected a piece, execute move
                        // If player has not selected a piece, select piece 
                    }
                }

                // Right Click
                if (Input.GetMouseButtonUp(1))
                {
                    if (Player.IsActive)
                    {
                        // Deselect piece
                    }
                }
            }
        }
    }
}