using UnityEngine;

using ProjectVanguard.Models.Systems;

namespace ProjectVanguard.Models.Entities
{
    public class Session
    {
        public bool HasEnded { get; private set; }

        public VTime GameTime { get; private set; }
        public VTime TurnTime { get; private set; }

        public Player[] Players { get; private set; }

        public Session(Player[] players, float turnTime)
        {
            HasEnded = false;

            GameTime = new VTime(0);
            TurnTime = new VTime(turnTime);

            Players = players;
            GameUpdater.AddUpdateCallback(Update);
        }

        public void Quit()
        {
            HasEnded = true;
        }

        private void Update()
        {
            
        }
    }
}
