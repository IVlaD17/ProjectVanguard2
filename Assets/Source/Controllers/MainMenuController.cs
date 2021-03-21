using UnityEngine;

using ProjectVanguard.Views;

namespace ProjectVanguard.Controllers
{
    public class MainMenuController : MonoBehaviour
    {
        private MainMenuView view;

        // Start is called before the first frame update
        void Start()
        {
            view = new MainMenuView();
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
