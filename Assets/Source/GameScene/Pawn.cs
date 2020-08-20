using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : Piece
{
    // Awake is called when the script instance is being loaded
    protected override void Awake()
    {
        base.Awake();
    }

    // Use this for initialization
    protected override void Start()
    {
        base.Start();
        if (MyPlayer.name.Contains("White"))
        {
            Moves.Add(new Move("move_regular", 1, 0));
            Moves.Add(new Move("move_double", 2, 0));
            Moves.Add(new Move("move_capture_left", 1, -1));
            Moves.Add(new Move("move_capture_right", 1, 1));
        }
        else
        {
            Moves.Add(new Move("move_regular", -1, 0));
            Moves.Add(new Move("move_double", -2, 0));
            Moves.Add(new Move("move_capture_left", -1, -1));
            Moves.Add(new Move("move_capture_right", -1, 1));
        }

        Value = 1;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if (!WasCaptured && MySquare != null)
        {
            Vector3 position = MySquare.transform.position;
            position.y = 15f;
            transform.position = position;
        }
    }
}
