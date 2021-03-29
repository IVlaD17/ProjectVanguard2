namespace ProjectVanguard.Models.Entities
{
    public class Player
    {
        public bool IsAIControlled { get; private set; }
        public string Name { get; private set; }

        public Player(bool isAIControlled, string name)
        {
            IsAIControlled = isAIControlled;
            Name = name;
        }
    }
}
