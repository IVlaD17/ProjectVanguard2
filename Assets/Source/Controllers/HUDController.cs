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

        // Start is called before the first frame update
        void Start()
        {
            view = new HUDView();
            moves = new List<string>();

            view.Hide();
        }

        // Update is called once per frame
        void Update()
        {
            if (Game.Instance.IsPlaying())
            {
                if (!view.IsVisible())
                    view.Display(Game.Instance.GetPlayerName(1), Game.Instance.GetPlayerName(2));

                if (Game.Instance.IsPlaying())
                {
                    view.UpdateGameTimeLabel(Game.Instance.GetCurrentSession().GameTime.ToString());
                    view.UpdateTurnTimeLabel(Game.Instance.GetCurrentSession().TurnTime.ToString());

                    int recordedMovesCount = Game.Instance.GetCurrentSession().Moves.Count;
                    if (recordedMovesCount != moves.Count)
                        UpdateMovesList(Game.Instance.GetCurrentSession().Moves[recordedMovesCount - 1]);
                }
            }
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
