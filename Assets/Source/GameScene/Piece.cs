using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{
    public Renderer MyRenderer { get; private set; }
    public Material WhiteMaterial { get; private set; }
    public Material BlackMaterial { get; private set; }

    public ChessColor MyColor { get; private set; }

    public Player MyPlayer { get; private set; }
    public Square MySquare { get; private set; }

    public List<Move> Moves { get; private set; }

    public bool HasMoved { get; private set; }
    public bool WasCaptured { get; private set; }

    public int Value { get; protected set; }

    // Awake is called when the script instance is being loaded
    protected virtual void Awake()
    {
        MyRenderer = gameObject.GetComponent<Renderer>();
        Moves = new List<Move>();
    }

    // Use this for initialization
    protected virtual void Start()
    {
        WasCaptured = false;

        GameObject playerObject = GameObject.Find(name.Substring(0, 11));
        if (playerObject != null)
            MyPlayer = playerObject.GetComponent<Player>();
        else
            Debug.Log("Couldn't find object named " + name.Substring(0, 11));

        MyColor = MyPlayer.MyColor;
        if (MyColor == ChessColor.WHITE)
            MyRenderer.material.color = Constants.COLOR_L_BROWN;
        else
            MyRenderer.material.color = Constants.COLOR_D_BROWN;
    }

    // Update is called once per frame
    protected virtual void Update()
    {      
        if (!WasCaptured && MySquare != null)
            transform.position = MySquare.transform.position;

        if (!WasCaptured && MySquare == null)
            WasCaptured = true;

        if (WasCaptured)
            transform.position = new Vector3(-1000, -1000, -1000);
    }

    public virtual void UpdatePosition(Square newSquare)
    {
        if(newSquare == null)
        {
            MySquare = null;
        }
        else
        {
            if(MySquare != null)
                MySquare.UpdatePiece(null);
            MySquare = newSquare;
            MySquare.UpdatePiece(this);
        }
    }

    public virtual void SavePosition()
    {
        Debug.Log(name + " position saved!");
        HasMoved = true;
        if (MySquare == null)
            WasCaptured = true;
    }

    public bool CanReach(Square target)
    {
        int sCol = MySquare.name[0] - 97;
        int sRow = MySquare.name[1] - 49;

        int eCol = target.name[0] - 97;
        int eRow = target.name[1] - 49;

        int deltaRow = eRow - sRow;
        int deltaCol = eCol - sCol;

        for (int moveIndex = 0; moveIndex < Moves.Count; moveIndex++)
        {
            if (Moves[moveIndex].DeltaRow == deltaRow && Moves[moveIndex].DeltaCol == deltaCol)
            {
                if (name.Contains("Pawn"))
                {
                    if (!Moves[moveIndex].MyName.Contains("capture") && target.MyPiece != null)
                        return false;

                    if (Moves[moveIndex].MyName.Contains("double") && HasMoved)
                        return false;

                    if (Moves[moveIndex].MyName.Contains("capture") && target.MyPiece == null)
                        return false;
                }

                if(name.Contains("King"))
                {
                    if(Moves[moveIndex].MyName.Contains("castle") && !HasMoved)
                    {
                        if(Moves[moveIndex].MyName.Contains("left") && MyPlayer.Pieces[0].HasMoved)
                            return false;

                        if (Moves[moveIndex].MyName.Contains("right") && MyPlayer.Pieces[7].HasMoved)
                            return false;
                    }

                    if (Moves[moveIndex].MyName.Contains("castle") && HasMoved)
                        return false;
                }
                return true;
            }
        }
        return false;
    }
}
