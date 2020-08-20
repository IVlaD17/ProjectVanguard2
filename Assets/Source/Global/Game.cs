using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    Scene myActiveScene;

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
        MyGameState = GameState.LOADING;
        myActiveScene = SceneManager.GetActiveScene();
    }

    // Update is called once per frame
    void Update()
    {
        myActiveScene = SceneManager.GetActiveScene();
        switch (MyGameState)
        {
            case GameState.LOADING:
                MyGameState = GameState.IN_MENU;
                break;
            case GameState.IN_MENU:
                if (myActiveScene.name != Constants.MENU_SCENE)
                    SceneManager.LoadScene(Constants.MENU_SCENE);                
                break;
            case GameState.PLAYING:
                if(myActiveScene.name != Constants.GAME_SCENE)
                    SceneManager.LoadScene(Constants.GAME_SCENE);
                break;
            case GameState.EXITING:
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
        MyGameState = GameState.PLAYING;
    }

    public void QuitGame()
    {
        MyGameState = GameState.IN_MENU;
    }

    public void ExitGame()
    {
        MyGameState = GameState.EXITING;
    }
}
