using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Square : MonoBehaviour
{
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
            MyColor = ChessColor.BLACK;
        else
            MyColor = ChessColor.WHITE;

        if (MyColor == ChessColor.WHITE)
            MyRenderer.material.color = Constants.COLOR_WHITE;
        else
            MyRenderer.material.color = Constants.COLOR_BLACK;

        transform.localScale = new Vector3(Constants.SQUARE_SIZE, 1, Constants.SQUARE_SIZE);
        transform.position = new Vector3(Constants.SQUARE_SIZE * col, 10, Constants.SQUARE_SIZE * row);
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
