using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ProjectVanguard.Models;

public class Square : MonoBehaviour
{

    public readonly Color32 ColorBlack = new Color32(0, 0, 0, 255);
    public readonly Color32 ColorWhite = new Color32(255, 255, 255, 255);

    public const int SquareSize = 20;

    public Renderer MyRenderer { get; private set; }

    public ChessColor MyColor { get; private set; }

    public char RowNumber { get; private set; }
    public char ColLetter { get; private set; }

    public Piece MyPiece { get; private set; }

    // Awake is called when the script instance is being loaded
    void Awake()
    {
        MyRenderer = gameObject.GetComponent<Renderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        ColLetter = gameObject.name[0];
        RowNumber = gameObject.name[1];

        int col = ColLetter - 97;
        int row = RowNumber - 49;

        if ((row % 2 == 0 && col % 2 == 0) || (row % 2 != 0 && col % 2 != 0))
            MyColor = ChessColor.Black;
        else
            MyColor = ChessColor.White;

        if (MyColor == ChessColor.White)
            MyRenderer.material.color = ColorWhite;
        else
            MyRenderer.material.color = ColorBlack;

        transform.localScale = new Vector3(SquareSize, 1, SquareSize);
        transform.position = new Vector3(SquareSize * col, 10, SquareSize * row);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdatePiece(Piece newPiece)
    {
        if (MyPiece != null)
            MyPiece.UpdatePosition(null);

        MyPiece = newPiece;
    }
}
