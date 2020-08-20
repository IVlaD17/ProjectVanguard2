using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Session : MonoBehaviour
{
    public GameScene MyGameScene { get; private set; }

    public Game MyGameManager { get; private set; }
    public bool IsAIEnabled { get; private set; }
    public bool IsTimerEnabled { get; private set; }
    public float DefaultTurnTime { get; private set; }
    public float TurnTime { get; private set; }
    public float GameTime { get; private set; }

    [Header("Prefabs")]
    public GameObject SquarePrefab;

    public Camera MainCamera;
    public ViewController MyViewController { get; private set; }

    public SessionState MySessionState { get; private set; }

    public Square[,] Board { get; private set; }
    public List<Square[,]> History { get; private set; }

    public Player[] Players { get; private set; }

    // Awake is called when the script instance is being loaded
    void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        MyGameScene = GameObject.Find("SceneController").GetComponent<GameScene>();

        MyGameManager = GameObject.Find("GameManager").GetComponent<Game>();
        IsAIEnabled = MyGameManager.IsAIEnabled;
        IsTimerEnabled = MyGameManager.IsTurnTimerEnabled;
        DefaultTurnTime = MyGameManager.TurnTime;
        TurnTime = DefaultTurnTime;

        MyViewController = MainCamera.GetComponent<ViewController>();

        MySessionState = SessionState.PLAYING;

        Board = new Square[Constants.NUMBER_OF_ROWS, Constants.NUMBER_OF_COLS];
        for(int rowIndex = 0; rowIndex < Constants.NUMBER_OF_ROWS; rowIndex++)
        {
            for(int colIndex = 0; colIndex < Constants.NUMBER_OF_COLS; colIndex++)
            {
                char rowNumber = (char)(rowIndex + 49);
                char colLetter = (char)(colIndex + 97);

                GameObject newSquareObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
                newSquareObject.name = colLetter.ToString() + rowNumber.ToString();

                Square newSquare = newSquareObject.AddComponent<Square>();

                Board[rowIndex, colIndex] = newSquare;               
            }
        }

        History = new List<Square[,]>();

        Players = new Player[Constants.NUMBER_OF_PLAYERS];
        GameObject playerObject = GameObject.Find("WhitePlayer");
        if (playerObject != null)
            Players[0] = playerObject.GetComponent<Player>();
        else
            Debug.Log(Constants.NO_PLAYER_OBJECT_FOUND + "WhitePlayer");


        playerObject = GameObject.Find("BlackPlayer");
        if (playerObject != null)
            Players[1] = playerObject.GetComponent<Player>();
        else
            Debug.Log(Constants.NO_PLAYER_OBJECT_FOUND + "WhitePlayer");

        foreach (Player player in Players)
        {
            for (int pieceIndex = 0; pieceIndex < player.Pieces.Count; pieceIndex++)
            {
                if (player.name == "WhitePlayer")
                    player.Pieces[pieceIndex].UpdatePosition(Board[pieceIndex / Constants.NUMBER_OF_COLS, pieceIndex % Constants.NUMBER_OF_ROWS]);
                else
                    player.Pieces[pieceIndex].UpdatePosition(Board[Constants.NUMBER_OF_COLS - pieceIndex / Constants.NUMBER_OF_COLS - 1, pieceIndex % Constants.NUMBER_OF_ROWS]);
            }
        }

        Players[0].ToggleActive();

        Cursor.lockState = CursorLockMode.Locked;

        GameTime = 0f;

        if (IsAIEnabled)
            Players[1].EnableAI();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(MySessionState);
        if(MySessionState == SessionState.PLAYING)
        {
            Player activePlayer = GetActivePlayer();
            GameTime += Time.deltaTime;

            if (IsTimerEnabled)
            {
                if (TurnTime >= 0)
                    TurnTime -= Time.deltaTime;
                else
                {
                    EndTurn("N/A");
                    TurnTime = DefaultTurnTime;
                }
            }

            if (Input.GetKeyUp(KeyCode.V))
                ChangeView();

            if (Input.GetKeyUp(KeyCode.Escape))
                PauseGame();

            // If it is not the AI's turn
            if (!activePlayer.IsAI)
            {
                // Left Click
                if (Input.GetMouseButtonUp(0))
                {
                    GameObject clickedObject = GetObjectTargeted();
                    Square square = GetSquare(clickedObject);

                    // If player clicked inside the board
                    if (square != null)
                    {
                        // If square has no piece on it
                        if (square.MyPiece == null)
                        {
                            // If user has a selected piece
                            if (activePlayer.SelectedPiece != null)
                            {
                                if (VerifyMove(activePlayer.SelectedPiece, square))
                                {
                                    // EXECUTE MOVE
                                    string sMoveNot = GetMoveNotation(activePlayer.SelectedPiece, square);
                                    activePlayer.SelectedPiece.UpdatePosition(square);
                                    activePlayer.SelectedPiece.SavePosition();
                                    History.Add(Board);
                                    EndTurn(sMoveNot);
                                }
                            }
                        }
                        // If square has a piece on it
                        else
                        {
                            // If user has a selected piece
                            if (activePlayer.SelectedPiece != null)
                            {
                                // If square piece is owned by user
                                if (square.MyPiece.MyPlayer == activePlayer)
                                {
                                    activePlayer.DeselectPiece();
                                    activePlayer.SelectPiece(square.MyPiece);
                                }
                                // If square piece is not owned by user
                                else
                                {
                                    if (VerifyMove(activePlayer.SelectedPiece, square))
                                    {
                                        // EXECUTE CAPTURE
                                        string sMoveNot = GetMoveNotation(activePlayer.SelectedPiece, square);
                                        activePlayer.SelectedPiece.UpdatePosition(square);
                                        activePlayer.SelectedPiece.SavePosition();
                                        History.Add(Board);
                                        EndTurn(sMoveNot);
                                    }
                                }
                            }
                            // If user does not have a selected piece
                            else
                            {
                                // If square piece is owned by user
                                if (square.MyPiece.MyPlayer == activePlayer)
                                {
                                    activePlayer.SelectPiece(square.MyPiece);
                                }
                            }
                        }
                    }
                    // If player clicked outside the board
                    else
                    {
                        // If player has a selected piece
                        if (activePlayer.SelectedPiece != null)
                        {
                            activePlayer.DeselectPiece();
                        }
                    }
                }


                // Right Click
                if (Input.GetMouseButtonUp(1))
                {
                    if (activePlayer.SelectedPiece != null)
                    {
                        activePlayer.DeselectPiece();
                    }
                }
            }

            // If it is the AI's turn
            if(activePlayer.IsAI)
            {
                int bestMoveScore = -1;
                Piece bestPiece = null;
                Square bestDest = null;

                foreach (Piece piece in activePlayer.Pieces)
                {
                    if (!piece.WasCaptured)
                    {
                        foreach (Move move in piece.Moves)
                        {
                            int sCol = piece.MySquare.name[0] - 97;
                            int sRow = piece.MySquare.name[1] - 49;

                            int eCol = sCol + move.DeltaCol;
                            int eRow = sRow + move.DeltaRow;

                            if (eRow >= 0 && eRow <= 7 && eCol >= 0 && eCol <= 7)
                            {
                                if (VerifyMove(piece, Board[eRow, eCol]))
                                {
                                    if (Board[eRow, eCol].MyPiece != null)
                                    {
                                        if (Board[eRow, eCol].MyPiece.Value > bestMoveScore)
                                        {
                                            bestMoveScore = Board[eRow, eCol].MyPiece.Value;
                                            bestPiece = piece;
                                            bestDest = Board[eRow, eCol];
                                        }
                                    }
                                    else
                                    {
                                        bestMoveScore = 0;
                                        bestPiece = piece;
                                        bestDest = Board[eRow, eCol];
                                    }
                                }
                            }
                        }
                    }                    
                }
            }

            if(IsCheckmate())
            {
                MySessionState = SessionState.GAME_OVER;
            }

            if(IsStalemate())
            {
                MySessionState = SessionState.STALEMATE;
            }
        }
        else if (MySessionState == SessionState.PAUSED)
        {
            if (Input.GetKeyUp(KeyCode.Escape))
                ResumeGame();
        }
    }
    
    Square GetSquare(GameObject clicked)
    {
        if (clicked != null)
        {
            // If the clicked object is a piece
            if (!clicked.name.Equals("WhitePlayer") && !clicked.name.Equals("BlackPlayer") && clicked.name.Contains("Player"))
                return clicked.GetComponent<Piece>().MySquare;

            // If the clicked object is a board square
            if (!clicked.name.Contains("Player") && !clicked.name.Equals("Terrain"))
            {
                int col = clicked.name[0] - 97;
                int row = clicked.name[1] - 49;

                if (col >= 0 && col < Constants.NUMBER_OF_COLS && row >= 0 && row < Constants.NUMBER_OF_ROWS)
                    return Board[row, col];
            }
        }

        return null;
    }

    GameObject GetObjectTargeted()
    {
        Ray ray = MainCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            return hit.transform.gameObject;
        }
        else
        {
            return null;
        }
    }

    Player GetActivePlayer()
    {
        for (int playerIndex = 0; playerIndex < Constants.NUMBER_OF_PLAYERS; playerIndex++)
        {
            if (Players[playerIndex].IsActive)
                return Players[playerIndex];
        }

        return null;
    }

    Player GetOpponent(Player player)
    {
        for (int playerIndex = 0; playerIndex < Players.Length; playerIndex++)
            if (Players[playerIndex] != player)
                return Players[playerIndex];

        return null;
    }

    string GetMoveNotation(Piece piece, Square end)
    {
        if (piece.name.Contains("Pawn"))
        {
            if (end.MyPiece == null)
                return end.name;
            else
                return piece.MySquare.name[0] + "x" + end.name;
        }
        else if (piece.name.Contains("Knight"))
        {
            if (end.MyPiece == null)
                return char.ToUpperInvariant(piece.name[12]) + end.name;
            else
                return char.ToUpperInvariant(piece.name[12]) + "x" + end.name;
        }
        else if(piece.name.Contains("King"))
        {
            if(GetMovePath(piece.MySquare, end).Count == 1)
            {
                int sCol = piece.MySquare.name[0] - 97;
                int sRow = piece.MySquare.name[1] - 49;

                int eCol = end.name[0] - 97;
                int eRow = end.name[1] - 49;

                int deltaRow = eRow - sRow;
                int deltaCol = eCol - sCol;

                string sMoveName = "";

                for (int moveIndex = 0; moveIndex < piece.Moves.Count; moveIndex++)
                    if (piece.Moves[moveIndex].DeltaRow == deltaRow && piece.Moves[moveIndex].DeltaCol == deltaCol)
                        if (piece.Moves[moveIndex].MyName.Contains("castle"))
                            sMoveName = piece.Moves[moveIndex].MyName;

                if (sMoveName.Contains("castle_left"))
                    return "0-0-0";
                else
                    return "0-0";
            }
            else
            {
                if (end.MyPiece == null)
                    return piece.name[11] + end.name;
                else
                    return piece.name[11] + "x" + end.name;
            }

        }
        else
        {
            if (end.MyPiece == null)
                return piece.name[11] + end.name;
            else
                return piece.name[11] + "x" + end.name;
        }

    }

    List<Piece> GetAttackingPieces(Square target, Player player)
    {
        List<Piece> attackingPieces = new List<Piece>();

        for (int pieceIndex = 0; pieceIndex < player.Pieces.Count; pieceIndex++)
            if (player.Pieces[pieceIndex].CanReach(target))
                if (VerifyMove(player.Pieces[pieceIndex], target))
                    attackingPieces.Add(player.Pieces[pieceIndex]);

        return attackingPieces;
    }

    List<Square> GetMovePath(Square start, Square end)
    {
        List<Square> path = new List<Square>();

        int col = start.name[0] - 97;
        int row = start.name[1] - 49;

        int eCol = end.name[0] - 97;
        int eRow = end.name[1] - 49;

        int dCol = eCol - (start.name[0] - 97);
        int dRow = eRow - (start.name[1] - 49);

        if (dRow != 0)
            row += dRow / Mathf.Abs(dRow);
        if (dCol != 0)
            col += dCol / Mathf.Abs(dCol);

        while (row != eRow || col != eCol)
        {
            path.Add(Board[row, col]);

            if (dRow != 0)
                row += dRow / Mathf.Abs(dRow);
            if (dCol != 0)
                col += dCol / Mathf.Abs(dCol);
        }

        return path;
    }

    bool VerifyMove(Piece piece, Square end)
    {
        bool isMoveVerified = false;

        Square start = piece.MySquare;
        Piece endPiece = end.MyPiece;

        if (endPiece == null || endPiece.MyPlayer != piece.MyPlayer)
        {
            if (piece.CanReach(end))
            {
                List<Square> path = null;
                if (!piece.name.Contains("Knight"))
                    path = GetMovePath(start, end);

                if (piece.name.Contains("Knight") || IsPathClear(path))
                {
                    // Condition for castling - Initial King Position Must Not Be In Check
                    if (piece.name.Contains("King") && path.Count == 1 && IsSquareInCheck(piece.MyPlayer, piece.MySquare))
                        return false;

                    // Move Piece to destination
                    piece.UpdatePosition(end);

                    // Check if king is in check
                    if (IsSquareInCheck(piece.MyPlayer, piece.MyPlayer.King.MySquare))
                        isMoveVerified = false;
                    else
                        isMoveVerified = true;

                    // Condition for castling - King Must Not Go Through Check
                    if (piece.name.Contains("King") && path.Count == 1 && IsSquareInCheck(piece.MyPlayer, path[0]))
                        return false;

                    // Undo Move
                    piece.UpdatePosition(start);

                    if (endPiece != null)
                        endPiece.UpdatePosition(end);
                }
            }
        }

        return isMoveVerified;
    }

    bool IsPathClear(List<Square> path)
    {
        foreach (Square square in path)
            if (square.MyPiece != null)
                return false;

        return true;
    }

    bool IsSquareInCheck(Player player, Square square)
    {
        Player opponent = GetOpponent(player);

        if (opponent != null)
            foreach (Piece piece in opponent.Pieces)
                if (piece.MySquare != null && !piece.WasCaptured)
                    if (piece.CanReach(square) && IsPathClear(GetMovePath(piece.MySquare, square)))
                        return true;

        return false;
    }

    bool AreKingSurroundingsInCheck(Player player)
    {
        Square kingSquare = player.King.MySquare;

        int col = kingSquare.name[0] - 97;
        int row = kingSquare.name[1] - 49;

        if (row - 1 > 0 && row - 1 < 7 && col - 1 > 0 && col - 1 < 7)
            if (!IsSquareInCheck(player, Board[row - 1, col - 1]) && Board[row - 1, col - 1].MyPiece == null)
                return false;
        if (row + 1 > 0 && row + 1 < 7 && col + 1 > 0 && col + 1 < 7)
            if (!IsSquareInCheck(player, Board[row + 1, col + 1]) && Board[row + 1, col + 1].MyPiece == null)
                return false;
        if (row - 1 > 0 && row - 1 < 7 && col + 1 > 0 && col + 1 < 7)
            if (!IsSquareInCheck(player, Board[row - 1, col + 1]) && Board[row - 1, col + 1].MyPiece == null)
                return false;
        if (row + 1 > 0 && row + 1 < 7 && col - 1 > 0 && col - 1 < 7)
            if (!IsSquareInCheck(player, Board[row + 1, col - 1]) && Board[row + 1, col - 1].MyPiece == null)
                return false;
        if (row - 1 > 0 && row - 1 < 7 && col > 0 && col < 7)
            if (!IsSquareInCheck(player, Board[row - 1, col]) && Board[row - 1, col].MyPiece == null)
                return false;
        if (row + 1 > 0 && row + 1 < 7 && col > 0 && col < 7)
            if (!IsSquareInCheck(player, Board[row + 1, col]) && Board[row + 1, col].MyPiece == null)
                return false;
        if (row > 0 && row < 7 && col - 1 > 0 && col - 1 < 7)
            if (!IsSquareInCheck(player, Board[row, col - 1]) && Board[row, col - 1].MyPiece == null)
                return false;
        if (row > 0 && row < 7 && col + 1 > 0 && col + 1 < 7)
            if (!IsSquareInCheck(player, Board[row, col + 1]) && Board[row, col + 1].MyPiece == null)
                return false;

        return true;
    }

    bool CanCaptureAttackingPiece(Player player)
    {
        List<Piece> attackingPieces;
        Player opponent = GetOpponent(player);
        Square kingSquare = player.King.MySquare;

        if (opponent != null)
        {
            attackingPieces = GetAttackingPieces(kingSquare, opponent);
            if (attackingPieces.Count == 1)
                foreach (Piece piece in player.Pieces)
                    if (VerifyMove(piece, attackingPieces[0].MySquare))
                    {
                        Debug.Log(piece.name + " can capture " + attackingPieces[0]);
                        return true;
                    }
        }

        return false;
    }

    bool CanBlockAttackingPiece(Player player)
    {
        List<Piece> attackingPieces;

        Player opponent = GetOpponent(player);
        Square kingSquare = player.King.MySquare;

        if (opponent != null)
        {
            attackingPieces = GetAttackingPieces(kingSquare, opponent);
            if (attackingPieces.Count == 1 && !attackingPieces[0].name.Contains("Knight"))
            {
                List<Square> path = GetMovePath(attackingPieces[0].MySquare, kingSquare);
                foreach (Square square in path)
                    foreach (Piece piece in player.Pieces)
                        if (VerifyMove(piece, square))
                            return true;
            }
            else
            {
                List<Square> chokepoints = new List<Square>();
                List<List<Square>> paths = new List<List<Square>>();

                foreach (Piece piece in attackingPieces)
                    if (piece.name.Contains("Knight"))
                        return false;

                foreach (Piece piece in attackingPieces)
                    paths.Add(GetMovePath(piece.MySquare, kingSquare));

                foreach (List<Square> path in paths)
                    foreach (Square square in path)
                        chokepoints.Add(square);

                foreach (Square chokepoint in chokepoints)
                {
                    foreach (List<Square> path in paths)
                    {
                        bool doesPathContainChokepoint = false;
                        foreach (Square square in path)
                        {
                            if (chokepoint == square)
                            {
                                doesPathContainChokepoint = true;
                            }
                        }

                        if (doesPathContainChokepoint == false)
                        {
                            chokepoints.Remove(chokepoint);
                        }
                    }
                }

                foreach (Square square in chokepoints)
                    foreach (Piece piece in player.Pieces)
                        if (VerifyMove(piece, square))
                            return true;
            }
        }

        return false;
    }

    bool IsCheckmate()
    {
        for (int playerIndex = 0; playerIndex < 2; playerIndex++)
        {
            Player player = Players[playerIndex];
            if (IsSquareInCheck(player, player.King.MySquare))
                if (AreKingSurroundingsInCheck(player))
                    if (!CanCaptureAttackingPiece(player))
                        if (!CanBlockAttackingPiece(player))
                            return true;
                        else
                            Debug.Log("Can block attacking piece");
                    else
                        Debug.Log("Can capture attacking piece");
                else
                    Debug.Log("Can move King");
            else
                Debug.Log("King is not in check");
        }
        return false;
    }

    bool IsStalemate()
    {
        Player player = GetActivePlayer();
        foreach (Piece piece in player.Pieces)
        {
            if (!piece.WasCaptured)
            {
                foreach (Move move in piece.Moves)
                {
                    int sCol = piece.MySquare.name[0] - 97;
                    int sRow = piece.MySquare.name[1] - 49;

                    int eCol = sCol + move.DeltaCol;
                    int eRow = sRow + move.DeltaRow;

                    if (eRow >= 0 && eRow <= 7 && eCol >= 0 && eCol <= 7)
                    {
                        if (VerifyMove(piece, Board[eRow, eCol]))
                        {
                            Debug.Log("Can move " + piece + " to " + Board[eRow, eCol]);
                            return false;
                        }
                    }
                }
            }
        }
        return true;
    }

    void EndTurn(string sMoveNot)
    {
        int playerIndex = 0;
        while (!Players[playerIndex].IsActive)
            playerIndex++;

        int newPlayerIndex;
        if (playerIndex == Constants.NUMBER_OF_PLAYERS - 1)
            newPlayerIndex = 0;
        else
            newPlayerIndex = playerIndex + 1;

        Players[playerIndex].ToggleActive();
        Players[newPlayerIndex].ToggleActive();

        MainCamera.transform.parent = Players[newPlayerIndex].gameObject.transform;

        if (MyViewController.Axes == RotationAxes.TOP_DOWN)
        {
            if (newPlayerIndex == 0)
            {
                MainCamera.transform.localPosition = Constants.TOP_CAM_POS_W;
                MainCamera.transform.localEulerAngles = Constants.TOP_CAM_ROT_W;
            }
            else
            {
                MainCamera.transform.localPosition = Constants.TOP_CAM_POS_B;
                MainCamera.transform.localEulerAngles = Constants.TOP_CAM_ROT_B;
            }
        }
        else
        {
            MainCamera.transform.localPosition = Constants.LOCAL_CAM_POS;
            if (newPlayerIndex == 0)
                MainCamera.transform.localEulerAngles = Constants.LOCAL_CAM_ROT_W;
            else
                MainCamera.transform.localEulerAngles = Constants.LOCAL_CAM_ROT_B;
        }

        MyGameScene.AddNewMoveLabel(sMoveNot);
    }

    void ChangeView()
    {      
        Player activePlayer = GetActivePlayer();
        MyViewController.ToggleViews();
        if (MyViewController.Axes == RotationAxes.TOP_DOWN)
        {
            if(activePlayer.name.Contains("White"))
            {
                MainCamera.transform.localPosition = Constants.TOP_CAM_POS_W;
                MainCamera.transform.localEulerAngles = Constants.TOP_CAM_ROT_W;
            }
            else
            {
                MainCamera.transform.localPosition = Constants.TOP_CAM_POS_B;
                MainCamera.transform.localEulerAngles = Constants.TOP_CAM_ROT_B;
            }
        }
        else
        {
            MainCamera.transform.localPosition = Constants.LOCAL_CAM_POS;
            if(activePlayer.name.Contains("White"))
                MainCamera.transform.localEulerAngles = Constants.LOCAL_CAM_ROT_W;
            else
                MainCamera.transform.localEulerAngles = Constants.LOCAL_CAM_ROT_B;
        }
    }

    public void PauseGame()
    {
        MySessionState = SessionState.PAUSED;
        Cursor.lockState = CursorLockMode.None;
    }

    public void ResumeGame()
    {
        MySessionState = SessionState.PLAYING;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
