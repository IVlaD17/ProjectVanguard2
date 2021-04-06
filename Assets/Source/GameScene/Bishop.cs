using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ProjectVanguard.Models.Entities;

public class Bishop : Piece
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
        Moves.Add(new Move("move_up_left_1", -1, -1));
        Moves.Add(new Move("move_up_left_2", -2, -2));
        Moves.Add(new Move("move_up_left_3", -3, -3));
        Moves.Add(new Move("move_up_left_4", -4, -4));
        Moves.Add(new Move("move_up_left_5", -5, -5));
        Moves.Add(new Move("move_up_left_6", -6, -6));
        Moves.Add(new Move("move_up_left_7", -7, -7));
        Moves.Add(new Move("move_up_right_1", -1, 1));
        Moves.Add(new Move("move_up_right_2", -2, 2));
        Moves.Add(new Move("move_up_right_3", -3, 3));
        Moves.Add(new Move("move_up_right_4", -4, 4));
        Moves.Add(new Move("move_up_right_5", -5, 5));
        Moves.Add(new Move("move_up_right_6", -6, 6));
        Moves.Add(new Move("move_up_right_7", -7, 7));
        Moves.Add(new Move("move_down_left_1", 1, -1));
        Moves.Add(new Move("move_down_left_2", 2, -2));
        Moves.Add(new Move("move_down_left_3", 3, -3));
        Moves.Add(new Move("move_down_left_4", 4, -4));
        Moves.Add(new Move("move_down_left_5", 5, -5));
        Moves.Add(new Move("move_down_left_6", 6, -6));
        Moves.Add(new Move("move_down_left_7", 7, -7));
        Moves.Add(new Move("move_down_right_1", 1, 1));
        Moves.Add(new Move("move_down_right_2", 2, 2));
        Moves.Add(new Move("move_down_right_3", 3, 3));
        Moves.Add(new Move("move_down_right_4", 4, 4));
        Moves.Add(new Move("move_down_right_5", 5, 5));
        Moves.Add(new Move("move_down_right_6", 6, 6));
        Moves.Add(new Move("move_down_right_7", 7, 7));

        Value = 3;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if (!WasCaptured && MySquare != null)
        {
            Vector3 position = transform.position;
            position.y = 20f;
            transform.position = position;
        }
    }
}
