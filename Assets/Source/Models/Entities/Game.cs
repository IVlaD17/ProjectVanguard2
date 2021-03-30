using UnityEngine;

namespace ProjectVanguard.Models.Entities
{
    // Implemented as a singleton class without lazy loading.
    public class Game
    {
        private Game() { }
        public static Game Instance { get; } = new Game();

        public GameState GameState { get; private set; } = GameState.InMenu;

        public void ChangeGameState(GameState newState)
        {
            GameState = newState;
        }

        public void ExitGame()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif

#if UNITY_STANDALONE_WIN
            Application.Quit();
#endif

#if UNITY_STANDALONE_OSX
        Application.Quit();
#endif
        }
    }
}
