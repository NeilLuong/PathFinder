using System.ComponentModel;
using System.Windows.Input;
using System.Windows.Media;

namespace PathFinding
{
    public class CellViewModel : INotifyPropertyChanged
    {
        public bool IsMouseDown { get; set; } = false;

        public Brush VisitedColor = Brushes.Aqua;
        public Brush ShortestPathColor = Brushes.Yellow;

        // Positional information for each cell
        public int Row { get; set; }
        public int Col { get; set; }

        public int Distance
        {
            get => _distance;
            set
            {
                _distance = value;
                OnPropertyChanged(nameof(Distance));
            }
        }
        private int _distance = int.MaxValue; // By default, distance is set to infinity

        public bool IsVisited
        {
            get => _isVisited;
            set
            {
                _isVisited = value;
                OnPropertyChanged(nameof(IsVisited));
            }
        }
        private bool _isVisited;

        public bool IsWall
        {
            get => _isWall;
            set
            {
                _isWall = value;
                OnPropertyChanged(nameof(IsWall));
            }
        }
        private bool _isWall;

        public CellViewModel PreviousNode
        {
            get => _previousNode;
            set
            {
                _previousNode = value;
                OnPropertyChanged(nameof(PreviousNode));
            }
        }
        private CellViewModel _previousNode;

        public Brush BackgroundColor
        {
            get => _backgroundColor;
            set
            {
                if (_backgroundColor != value)
                {
                    _backgroundColor = value;
                    OnPropertyChanged(nameof(BackgroundColor));
                }
            }
        }
        private Brush _backgroundColor = Brushes.Transparent; // Default color

        public ICommand ChangeColorCommand { get; }

        public CellViewModel()
        {
            ChangeColorCommand = new RelayCommand(SetAsWall);
        }

        public CellViewModel(int row, int col)
        {
            Row = row;
            Col = col;
            ChangeColorCommand = new RelayCommand(SetAsWall);
        }

        public void SetAsWall()
        {
            if (!IsWall)
            {
                IsWall = true;
                BackgroundColor = Brushes.DarkGray;
            }
            else
            {
                IsWall = false;
                BackgroundColor = Brushes.Transparent;
            }
        }

        public void SetBackgroundColor(Brush color)
        {
            BackgroundColor = color;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
