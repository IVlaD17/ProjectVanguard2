using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ProjectVanguard.Models.Entities;

public class Queen : Piece
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
        Moves.Add(new Move("move_up_1", -1, 0));
        Moves.Add(new Move("move_up_2", -2, 0));
        Moves.Add(new Move("move_up_3", -3, 0));
        Moves.Add(new Move("move_up_4", -4, 0));
        Moves.Add(new Move("move_up_5", -5, 0));
        Moves.Add(new Move("move_up_6", -6, 0));
        Moves.Add(new Move("move_up_7", -7, 0));
        Moves.Add(new Move("move_down_1", 1, 0));
        Moves.Add(new Move("move_down_2", 2, 0));
        Moves.Add(new Move("move_down_3", 3, 0));
        Moves.Add(new Move("move_down_4", 4, 0));
        Moves.Add(new Move("move_down_5", 5, 0));
        Moves.Add(new Move("move_down_6", 6, 0));
        Moves.Add(new Move("move_down_7", 7, 0));
        Moves.Add(new Move("move_left_1", 0, -1));
        Moves.Add(new Move("move_left_2", 0, -2));
        Moves.Add(new Move("move_left_3", 0, -3));
        Moves.Add(new Move("move_left_4", 0, -4));
        Moves.Add(new Move("move_left_5", 0, -5));
        Moves.Add(new Move("move_left_6", 0, -6));
        Moves.Add(new Move("move_left_7", 0, -7));
        Moves.Add(new Move("move_right_1", 0, 1));
        Moves.Add(new Move("move_right_2", 0, 2));
        Moves.Add(new Move("move_right_3", 0, 3));
        Moves.Add(new Move("move_right_4", 0, 4));
        Moves.Add(new Move("move_right_5", 0, 5));
        Moves.Add(new Move("move_right_6", 0, 6));
        Moves.Add(new Move("move_right_7", 0, 7));
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

        Value = 9;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if (!WasCaptured && MySquare != null)
        {
            Vector3 position = MySquare.transform.position;
            position.y = 18f;
            transform.position = position;
        }
    }
}
