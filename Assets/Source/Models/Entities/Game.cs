using System;
using System.Collections.Generic;

using UnityEngine;

namespace ProjectVanguard.Models.Entities
{
    // Implemented as a singleton class without lazy loading.
    public class Game
    {
        private List<Session> sessions;

        public static Game Instance { get; } = new Game();

        public GameState GameState { get; private set; } = GameState.InMenu;

        private Game() 
        {
            sessions = new List<Session>();
        }

        public void CreateGameSession(string[] playerNames, bool isAIEnabled, float turnTime)
        {
            sessions.Add(new Session(CreatePlayers(playerNames, isAIEnabled), turnTime));
            EnterSession();

        }
        public Session GetCurrentSession()
        {
            if (sessions.Count == 0)
                return null;

            Session currentSession = sessions[sessions.Count - 1];

            if (currentSession.HasEnded)
                return null;

            return currentSession;
        }

        public string GetPlayerName(int playerNumber)
        {
            Session currentSession = GetCurrentSession();
            if (currentSession != null)
                return currentSession.Players[playerNumber - 1].Name;
            else
                throw new NullReferenceException();
        }

        public bool IsPlaying()
        {
            if (GameState == GameState.Playing)
                return true;
            else
                return false;
        }
        public void ExitSession()
        {
            if (GameState == GameState.Playing)
                GameState = GameState.InMenu;

            GetCurrentSession().Quit();
        }
        public void EnterSession()
        {
            if (GameState == GameState.InMenu)
                GameState = GameState.Playing;
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

        private Player[] CreatePlayers(string[] playerNames, bool isAIEnabled)
        {
            int aiPlayerIndex = -1;
            if (isAIEnabled)
                aiPlayerIndex = 0;

            Player[] players = new Player[playerNames.Length];
            for (int index = 0; index < playerNames.Length; index++)
            {
                if (index == aiPlayerIndex)
                    players[index] = new Player(true, playerNames[index]);
                else
                    players[index] = new Player(false, playerNames[index]);
            }

            return players;
        }
    }
}
