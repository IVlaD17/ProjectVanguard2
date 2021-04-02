using UnityEngine;

using ProjectVanguard.Models.Systems;
using ProjectVanguard.Models.Entities;

namespace ProjectVanguard.Models.Components
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
                if (Input.GetKeyUp(KeyCode.V))
                {
                    //ChangeView();
                }

                if (Input.GetKeyUp(KeyCode.Escape))
                {
                    if (Entities.Game.Instance.IsSessionPaused())
                        Entities.Game.Instance.ResumeSession();
                    else if (Entities.Game.Instance.IsSessionPlaying())
                        Entities.Game.Instance.PauseSession();
                }

                // Left Click
                if (Input.GetMouseButtonUp(0))
                {
                    if(Player.IsActive)
                    {
                        // If player has selected a piece, execute move
                        // If player has not selected a piece, select piece 
                    }
                }

                // Right Click
                if (Input.GetMouseButtonUp(1))
                {
                    if(Player.IsActive)
                    {
                        // Deselect piece
                    }
                }
            }
        }
    }
}
