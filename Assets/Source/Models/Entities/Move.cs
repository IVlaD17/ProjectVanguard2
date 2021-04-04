namespace Assets.Source.Models.Entities
{
    public class Move
    {
        public string MyName { get; private set; }
        public int DeltaRow { get; private set; }
        public int DeltaCol { get; private set; }

        public Move(string myName, int deltaRow, int deltaCol)
        {
            MyName = myName;
            DeltaRow = deltaRow;
            DeltaCol = deltaCol;
        }
    }
}
