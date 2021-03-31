using System;
using UnityEngine;

namespace ProjectVanguard.Models.Systems
{
    public class GameUpdater : MonoBehaviour
    {
        private Action update;
        private static GameUpdater instance = null;

        // Update is called once per frame
        private void Update()
        {
            update();
        }

        public static void AddUpdateCallback(Action method)
        {
            if (instance == null)
                instance = new GameObject("GameUpdater").AddComponent<GameUpdater>();

            instance.update += method;
        }
    }
}