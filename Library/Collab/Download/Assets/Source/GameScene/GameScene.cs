using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameScene : MonoBehaviour
{
    float fAddMoveDCooldown = 0.25f;
    float fAddMoveCooldown;

    RectTransform movesListRT;
    float fMovesListHeight;
    float fMovesListWidth;

    RectTransform moveLabelRT;
    float fMoveLabelHeight;
    float fMoveLabelWidth;

    [Header("Game Panel")]
    public GameObject GamePanel;
    public Text Player1NameLabel;
    public Text Player2NameLabel;
    public Text GameTimeLabel;
    public Text ErrorMessageLabel;
    public Text TurnTimeLabel;

    [Header("Moves Panel")]
    public GameObject MovesPanel;
    public GameObject MovesListView;
    public GameObject MoveLabelPrefab;

    // Start is called before the first frame update
    void Start()
    {
        movesListRT = MovesListView.GetComponent<RectTransform>();
        fMovesListHeight = movesListRT.rect.height;
        fMovesListWidth = movesListRT.rect.width;

        moveLabelRT = MoveLabelPrefab.GetComponent<RectTransform>();
        fMoveLabelHeight = moveLabelRT.rect.height;
        fMoveLabelWidth = moveLabelRT.rect.width;

        fAddMoveCooldown = fAddMoveDCooldown;

        ErrorMessageLabel.text = "";
    }

    // Update is called once per frame
    void Update()
    {

    }

    void AddNewMoveLabel(string move)
    {
        GameObject moveLabelGO = Instantiate(MoveLabelPrefab);
        Text moveLabel = moveLabelGO.GetComponent<Text>();

        moveLabelGO.name = move + "Label";
        moveLabel.text = move;
        
        moveLabelGO.transform.SetParent(MovesListView.transform);

        fMovesListHeight += fMoveLabelHeight;
        movesListRT.sizeDelta = new Vector2(0, fMovesListHeight);
    }
}
