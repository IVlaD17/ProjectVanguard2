using UnityEngine;

using ProjectVanguard.Views;

namespace ProjectVanguard.Controllers
{
    public class HUDController : MonoBehaviour
    {
        private HUDView view;

        // Start is called before the first frame update
        void Start()
        {
            view = new HUDView();
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
