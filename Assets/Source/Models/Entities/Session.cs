using System.Collections.Generic;

using UnityEngine;

using ProjectVanguard.Models.Systems;

namespace ProjectVanguard.Models.Entities
{
    public class Session
    {
        private int activePlayerIndex;

        public bool HasEnded { get; private set; }

        public VTime GameTime { get; private set; }

        public VTime TurnTime { get; private set; }
        public float DefaultTurnTime { get; private set; }
        public List<string> Moves { get; private set; }

        public Player[] Players { get; private set; }

        public SessionState SessionState { get; private set; }
        
        public Session(Player[] players, float turnTime)
        {
            activePlayerIndex = 0;

            HasEnded = false;

            GameTime = new VTime(1);

            DefaultTurnTime = turnTime;
            TurnTime = new VTime(turnTime);
            
            Players = players;
            GameUpdater.AddUpdateCallback(Update);

            SessionState = SessionState.Playing;
            Moves = new List<string>();
        }

        public void Quit()
        {
            HasEnded = true;
            SessionState = SessionState.GameOver;
        }
        public void Pause()
        {
            if (SessionState == SessionState.Playing)
                SessionState = SessionState.Paused;
        }
        public void Resume()
        {
            if (SessionState == SessionState.Paused)
                SessionState = SessionState.Playing;
        }

        public void EndTurn(string moveNotation)
        {
            Players[activePlayerIndex % 2].EndTurn();
            activePlayerIndex++;
            Players[activePlayerIndex % 2].StartTime();
            Moves.Add(moveNotation);
        }

        // Update is called once per frame
        private void Update()
        {
            if(SessionState == SessionState.Playing)
            {
                GameTime.IncreaseTime(Time.deltaTime);

                if (DefaultTurnTime != 0)
                {
                    if (!TurnTime.IsZero())
                    {
                        TurnTime.DecreaseTime(Time.deltaTime);
                    }
                    else
                    {
                        EndTurn("N/A");
                        TurnTime = new VTime(DefaultTurnTime);
                    }
                }
            }
        }
    }
}
