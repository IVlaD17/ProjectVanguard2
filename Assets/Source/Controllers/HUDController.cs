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

        private VTime gameTime;
        private VTime turnTime;

        // Start is called before the first frame update
        void Start()
        {
            view = new HUDView();
            moves = new List<string>();

            gameTime = new VTime(0f);
            turnTime = new VTime(0f);
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
