namespace ProjectVanguard.Models.Entities
{
    public class Session
    {
        public VTime GameTime { get; private set; }
        public VTime TurnTime { get; private set; }

        public Player[] Players { get; private set; }

        public Session(Player[] players, float turnTime)
        {
            GameTime = new VTime(0);
            TurnTime = new VTime(turnTime);

            Players = players;
        }
    }
}
