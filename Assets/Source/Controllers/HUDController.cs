using System.Collections.Generic;

using UnityEngine;

using ProjectVanguard.Views;

using ProjectVanguard.Models.Entities;

namespace ProjectVanguard.Controllers
{
    public class HUDController : MonoBehaviour
    {
        private HUDView view;

        // Moves list state holding variables.
        private List<string> moves;

        private Models.Entities.Time gameTime;
        private Models.Entities.Time turnTime;

        // Start is called before the first frame update
        void Start()
        {
            view = new HUDView();
            moves = new List<string>();

            gameTime = new Models.Entities.Time(0f);
            turnTime = new Models.Entities.Time(0f);
        }

        // Update is called once per frame
        void Update()
        {
            view.UpdateGameTimeLabel(gameTime.ToString());
            view.UpdateTurnTimeLabel(turnTime.ToString());
        }

        public void UpdateMovesList(string move)
        {
            moves.Add(move);
            if (moves.Count % 2 != 0)
                view.CreateNewMoveLabel(move);
            else
                view.UpdateMoveLabel(move);
        }
    }
}
