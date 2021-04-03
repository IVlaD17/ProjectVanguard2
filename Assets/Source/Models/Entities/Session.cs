using UnityEngine;

using ProjectVanguard.Models.Systems;

namespace ProjectVanguard.Models.Entities
{
    public class Session
    {
        public bool HasEnded { get; private set; }

        public VTime GameTime { get; private set; }

        public VTime TurnTime { get; private set; }
        public float DefaultTurnTime { get; private set; }

        public Player[] Players { get; private set; }

        public SessionState SessionState { get; private set; }

        // Update is called once per frame
        public Session(Player[] players, float turnTime)
        {
            HasEnded = false;

            GameTime = new VTime(0);

            DefaultTurnTime = turnTime;
            TurnTime = new VTime(turnTime);
            
            Players = players;
            GameUpdater.AddUpdateCallback(Update);

            SessionState = SessionState.Playing;
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
                        // Change Turns
                        TurnTime = new VTime(DefaultTurnTime);
                    }
                }
            }
        }
    }
}
