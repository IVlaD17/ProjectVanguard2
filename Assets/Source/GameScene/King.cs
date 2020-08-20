using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class King : Piece
{
    string sMoveName;

    // Awake is called when the script instance is being loaded
    protected override void Awake()
    {
        base.Awake();
    }

    // Use this for initialization
    protected override void Start()
    {
        base.Start();
        Moves.Add(new Move("move_up_left", -1, -1));
        Moves.Add(new Move("move_up", -1, 0));
        Moves.Add(new Move("move_up_right", -1, 1));
        Moves.Add(new Move("move_right", 0, 1));
        Moves.Add(new Move("move_down_right", 1, 1));
        Moves.Add(new Move("move_down", 1, 0));
        Moves.Add(new Move("move_down_left", 1, -1));
        Moves.Add(new Move("move_left", 0, -1));
        Moves.Add(new Move("move_castle_left", 0, -2));
        Moves.Add(new Move("move_castle_right", 0, 2));

        Value = 1000;
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

    public override void UpdatePosition(Square newSquare)
    {
        if(newSquare != null && MySquare != null)
        {
            int sCol = MySquare.name[0] - 97;
            int sRow = MySquare.name[1] - 49;

            int eCol = newSquare.name[0] - 97;
            int eRow = newSquare.name[1] - 49;

            int deltaRow = eRow - sRow;
            int deltaCol = eCol - sCol;

            for (int moveIndex = 0; moveIndex < Moves.Count; moveIndex++)
                if (Moves[moveIndex].DeltaRow == deltaRow && Moves[moveIndex].DeltaCol == deltaCol)
                    if (Moves[moveIndex].MyName.Contains("castle"))
                        sMoveName = Moves[moveIndex].MyName;
        }

        base.UpdatePosition(newSquare);
    }

    public override void SavePosition()
    {
        Piece rook = null;
        Square newRookPos;

        char rowNumber = MySquare.RowNumber;
        int col = 0;

        if (sMoveName == "move_castle_left")
        {
            rook = MyPlayer.Pieces[0];
            col = rook.MySquare.ColLetter - 97 + 3;
        }

        if(sMoveName == "move_castle_right")
        {
            rook = MyPlayer.Pieces[7];
            col = rook.MySquare.ColLetter - 97 - 2;
        }

        if (rook != null)
        {
            char colLetter = (char)(col + 97);
            string squareName = colLetter.ToString() + rowNumber.ToString();
            newRookPos = GameObject.Find(squareName).GetComponent<Square>();

            rook.UpdatePosition(newRookPos);
            rook.SavePosition();
        }
        base.SavePosition();
    }
}
