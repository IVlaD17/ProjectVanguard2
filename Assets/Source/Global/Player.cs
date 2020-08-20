using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Pieces")]
    public GameObject PawnPrefab;
    public GameObject RookPrefab;
    public GameObject KingPrefab;
    public GameObject QueenPrefab;
    public GameObject KnightPrefab;
    public GameObject BishopPrefab;

    public ChessColor MyColor { get; private set; }

    public string MyName { get; private set; }
    public bool IsActive { get; private set; }

    public bool IsAI { get; private set; }

    public int NumberOfPieces { get; private set; }
    public List<Piece> Pieces { get; private set; }

    public King King { get; private set; }
    public bool IsInCheck { get; private set; }

    public Piece SelectedPiece { get; private set; }

    //public AI MyAI { get; private set; }

    // Awake is called when the script instance is being loaded
    void Awake()
    {
        IsAI = false;
        IsActive = false;
        IsInCheck = false;
        Pieces = new List<Piece>();

        Pieces.Add(Instantiate(RookPrefab).GetComponent<Rook>());
        Pieces[Pieces.Count - 1].name = name + "Rook" + 1;

        Pieces.Add(Instantiate(KnightPrefab).GetComponent<Knight>());
        Pieces[Pieces.Count - 1].name = name + "Knight" + 1;

        Pieces.Add(Instantiate(BishopPrefab).GetComponent<Bishop>());
        Pieces[Pieces.Count - 1].name = name + "Bishop" + 1;

        Pieces.Add(Instantiate(QueenPrefab).GetComponent<Queen>());
        Pieces[Pieces.Count - 1].name = name + "Queen";

        Pieces.Add(Instantiate(KingPrefab).GetComponent<King>());
        King = (King)Pieces[Pieces.Count - 1];
        King.name = name + "King";

        Pieces.Add(Instantiate(BishopPrefab).GetComponent<Bishop>());
        Pieces[Pieces.Count - 1].name = name + "Bishop" + 2;

        Pieces.Add(Instantiate(KnightPrefab).GetComponent<Knight>());
        Pieces[Pieces.Count - 1].name = name + "Knight" + 2;

        Pieces.Add(Instantiate(RookPrefab).GetComponent<Rook>());
        Pieces[Pieces.Count - 1].name = name + "Rook" + 2;

        for (int pieceIndex = 1; pieceIndex <= 8; pieceIndex++)
        {
            Pieces.Add(Instantiate(PawnPrefab).GetComponent<Pawn>());
            Pieces[Pieces.Count - 1].name = name + "Pawn" + pieceIndex;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        MyName = "TempName";

        if (name == "WhitePlayer")
            MyColor = ChessColor.WHITE;
        else
            MyColor = ChessColor.BLACK;

        SelectedPiece = null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnableAI()
    {
        IsAI = true;
    }

    public void ToggleActive()
    {
        IsActive = !IsActive;
        if (!IsActive && SelectedPiece != null)
            DeselectPiece();
    }

    public void SelectPiece(Piece selectedPiece)
    {
        SelectedPiece = selectedPiece;
        SelectedPiece.MyRenderer.material.color = Constants.COLOR_YELLOW;
    }

    public void DeselectPiece()
    {
        if (name == "WhitePlayer")
            SelectedPiece.MyRenderer.material.color = Constants.COLOR_L_BROWN;
        else
            SelectedPiece.MyRenderer.material.color = Constants.COLOR_D_BROWN;
        SelectedPiece = null;
    }
}
