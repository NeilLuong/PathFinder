using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;

/// References:
/// https://stackoverflow.com/questions/276808/how-to-populate-a-wpf-grid-based-on-a-2-dimensional-array
/// https://clementmihailescu.github.io/Pathfinding-Visualizer/#
namespace PathFinding
{
    public class GridViewModel : INotifyPropertyChanged
    {
        public List<List<CellViewModel>> Cells { get; set; }
        public StartCellViewModel StartCell { get; set; }
        public EndCellViewModel EndCell { get; set; }

        public ICommand StartAlgorithmCommand { get; }

        public GridViewModel()
        {
            Cells = new List<List<CellViewModel>>();
            PopulateCells();

            StartAlgorithmCommand = new RelayCommand(() => ExecuteDijkstra());
        }

        private void PopulateCells()
        {
            int rows = 24;
            int columns = 39;

            for (int i = 0; i < rows; i++)
            {
                var row = new List<CellViewModel>();

                for (int j = 0; j < columns; j++)
                {
                    if (i == rows / 2 && j == 4)
                    {
                        StartCell = new StartCellViewModel(i, j);
                        row.Add(StartCell);
                    }
                    else if (i == rows / 2 && j == columns - 4)
                    {
                        EndCell = new EndCellViewModel(i, j);
                        row.Add(EndCell);
                    }
                    else
                    {
                        row.Add(new CellViewModel(i, j));
                    }
                }

                Cells.Add(row);
            }
        }

        public async void AnimateShortestPath(List<CellViewModel> nodesInShortestPathOrder)
        {
            for (int i = 0; i < nodesInShortestPathOrder.Count; i++)
            {
                await Task.Delay(50);  // This delays the loop by 50ms for each iteration
                nodesInShortestPathOrder[i].SetBackgroundColor(nodesInShortestPathOrder[i].ShortestPathColor);
            }
        }

        public async void AnimateDijkstra(List<CellViewModel> visitedNodesInOrder, List<CellViewModel> nodesInShortestPathOrder)
        {
            for (int i = 0; i < visitedNodesInOrder.Count; i++)
            {
                await Task.Delay(10);  // This delays the loop by 10ms for each iteration
                visitedNodesInOrder[i].SetBackgroundColor(visitedNodesInOrder[i].VisitedColor);
            }

            AnimateShortestPath(nodesInShortestPathOrder);
        }

        private void ExecuteDijkstra()
        {
            var visitedNodesInOrder = Dijkstra.PerformDijkstra(Cells, StartCell, EndCell);
            var nodesInShortestPathOrder = Dijkstra.GetNodesInShortestPathOrder(EndCell);
            AnimateDijkstra(visitedNodesInOrder, nodesInShortestPathOrder);
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
