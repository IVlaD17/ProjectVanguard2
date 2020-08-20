public struct Move
{
    public string MyName;
    public int DeltaRow;
    public int DeltaCol;

    public Move(string myName, int deltaRow, int deltaCol)
    {
        MyName = myName;
        DeltaRow = deltaRow;
        DeltaCol = deltaCol;
    }
}
