using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using ProjectVanguard.Views;

namespace ProjectVanguard.Controllers
{
    public class HUDController : MonoBehaviour
    {
        private HUDView view;

        // Moves list state holding variables.
        private int moveNumber;
        private List<Text> moveLabels;

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
