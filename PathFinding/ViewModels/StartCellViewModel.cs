using System.Windows.Media;

namespace PathFinding
{
    public class StartCellViewModel : CellViewModel
    {
        public StartCellViewModel()
        {
            SetDefault();
        }

        public StartCellViewModel(int row, int col)
        {
            Row = row;
            Col = col;
            SetDefault();
        }

        public void SetDefault()
        {
            BackgroundColor = Brushes.Green;
            VisitedColor = Brushes.Green;
            ShortestPathColor = Brushes.Green;
        }
    }
}
