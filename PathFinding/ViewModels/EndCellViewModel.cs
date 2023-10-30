using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace PathFinding
{
    public class EndCellViewModel : CellViewModel
    {
        public EndCellViewModel()
        {
            SetDefault();
        }

        public EndCellViewModel(int row, int col)
        {
            Row = row;
            Col = col;
            SetDefault();
        }

        public void SetDefault()
        {
            BackgroundColor = Brushes.Red;
            VisitedColor = Brushes.Red;
            ShortestPathColor = Brushes.Red;
        }
    }
}
