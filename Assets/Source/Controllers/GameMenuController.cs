using UnityEngine;

using ProjectVanguard.Views;

namespace ProjectVanguard.Controllers
{
    public class GameMenuController : MonoBehaviour
    {
        private GameMenuView view;

        // Start is called before the first frame update
        void Start()
        {
            view = new GameMenuView();
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}