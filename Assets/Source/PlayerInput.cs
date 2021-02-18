using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.V))
        {
            //ChangeView();
        }

        if (Input.GetKeyUp(KeyCode.Escape))
        {
            //PauseGame();
        }

        // Left Click
        if (Input.GetMouseButtonUp(0))
        {
            // If player has selected a piece, execute move
            // If player has not selected a piece, select piece            
        }

        // Right Click
        if (Input.GetMouseButtonUp(1))
        {
            // Deselect piece
        }
    }
}
