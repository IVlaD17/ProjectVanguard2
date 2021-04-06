using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ProjectVanguard.Models.Entities;

public class Knight : Piece
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
        Moves.Add(new Move("move_up_left_far", -2, -1));
        Moves.Add(new Move("move_up_left_close", -1, -2));
        Moves.Add(new Move("move_up_right_far", -2, 1));
        Moves.Add(new Move("move_up_right_close", -1, 2));
        Moves.Add(new Move("move_down_left_far", 2, -1));
        Moves.Add(new Move("move_down_left_close", 1, -2));
        Moves.Add(new Move("move_down_right_far", 2, 1));
        Moves.Add(new Move("move_down_right_close", 1, 2));

        Value = 3;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if (!WasCaptured && MySquare != null)
        {
            Vector3 position = MySquare.transform.position;
            position.y = 20f;
            transform.position = position;
        }
    }
}
