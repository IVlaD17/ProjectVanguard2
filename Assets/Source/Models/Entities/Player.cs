using ProjectVanguard.Models.Components;

namespace ProjectVanguard.Models.Entities
{
    public class Player
    {
        public string Name { get; private set; }
        public bool IsActive { get; private set; }
        public bool IsAIControlled { get; private set; }

        public PlayerView PlayerViewComponent { get; private set; }
        public PlayerInput PlayerInputComponent { get; private set; }

        public Player(string name, bool isActive, bool isAIControlled)
        {
            Name = name;
            IsActive = isActive;
            IsAIControlled = isAIControlled;

            PlayerViewComponent = new PlayerView(this);
            PlayerInputComponent = new PlayerInput(this);
        }
    }
}
