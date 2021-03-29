namespace ProjectVanguard.Models.Entities
{
    public class Session
    {
        public Time GameTime { get; private set; }
        public Time TurnTime { get; private set; }

        public Player[] Players { get; private set; }

        public Session(Player[] players, float turnTime)
        {
            GameTime = new Time(0);
            TurnTime = new Time(turnTime);

            Players = players;
        }
    }
}
