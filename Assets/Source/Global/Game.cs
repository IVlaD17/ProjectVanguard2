using UnityEngine;
using UnityEngine.SceneManagement;

using ProjectVanguard.Models;

public class Game : MonoBehaviour
{
    public GameState MyGameState { get; private set; }

    public string Player1Name { get; private set; }
    public string Player2Name { get; private set; }
    public bool IsAIEnabled { get; private set; }
    public bool IsTurnTimerEnabled { get; private set; }
    public float TurnTime { get; private set; }

    // Awake is called when the script instance is being loaded
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        MyGameState = GameState.InMenu;
    }

    // Update is called once per frame
    void Update()
    {
        switch (MyGameState)
        {
            case GameState.Loading:
                MyGameState = GameState.InMenu;
                break;
            case GameState.InMenu:               
                break;
            case GameState.Playing:
                break;
            case GameState.Exiting:
                ShutDown();
                break;
        }
    }

    void ShutDown()
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

    public void LoadGameData(string player1Name, string player2Name, bool isAIEnabled, bool isTurnTimerEnabled, float turnTime)
    {
        Player1Name = player1Name;
        Player2Name = player2Name;
        IsAIEnabled = isAIEnabled;

        IsTurnTimerEnabled = isTurnTimerEnabled;
        TurnTime = turnTime * 60;
    }

    public void PlayGame()
    {
        MyGameState = GameState.Playing;
    }

    public void QuitGame()
    {
        MyGameState = GameState.InMenu;
    }

    public void ExitGame()
    {
        MyGameState = GameState.Exiting;
    }
}
