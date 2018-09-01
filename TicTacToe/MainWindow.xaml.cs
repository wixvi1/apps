using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;


namespace TicTacToe
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region PrivateMembers

        private MarkType[] _results;// array of enum type

        private bool _playerOneTurn;// variable for player's turn

        private bool _gameEnded;// variable to check if game is ended
        #endregion

        #region Constructor
        /// <summary>
        /// Default constructor
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            NewGame();// creating new game
        }

        #endregion

        /// <summary>
        /// Start new game, clear all values!
        /// </summary>
        private void NewGame()
        {
            // blank array of free cells
            _results = new MarkType[9];

            for (var i = 0; i < _results.Length; i++) 
                _results[i] = MarkType.Free;
           // Make sure its player 1 turn
            _playerOneTurn = true;
            //Itterate every button on the grid
            Container.Children.Cast<Button>().ToList().ForEach(button =>
            {
                button.Content = string.Empty;
                button.Background = Brushes.White;
                button.Foreground = Brushes.Blue;
            });

            _gameEnded = false;
        }
        /// <summary>
        /// Handles the button click event
        /// </summary>
        /// <param name="sender"> Button that was clicked</param>
        /// <param name="e">The events of the click</param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (_gameEnded)
            {
                NewGame();
                return;
            }

            var button = (Button)sender;
            var column = Grid.GetColumn(button);
            var row = Grid.GetRow(button);

            var index = column + (row * 3);//3 is amount of columns

            // Dont do anything if the cell alrady has a value in it
            if (_results[index] != MarkType.Free)
                return;

            // set the cell value 
            _results[index] = _playerOneTurn ? MarkType.Cross : MarkType.Nought;

            //Set buttons content
            button.Content = _playerOneTurn ? "X" : "O";

            if (!_playerOneTurn)
                button.Foreground = Brushes.Green;
            // Toggle the players turn
            _playerOneTurn ^= true; // flips the value: if it's true - it becomes false, if it's false - it becomes true

            //Check for a winner

            CheckForWinner();
        }
        // Check if there is a winner !
        private void CheckForWinner()
        {
            //Row 0
            if (_results[0] != MarkType.Free && (_results[0] & _results[1] & _results[2]) == _results[0])
            {
                _gameEnded = true;

                Button0_0.Background = Button1_0.Background = Button2_0.Background = Brushes.Indigo;
            }

            //Row 1
            if (_results[3] != MarkType.Free && (_results[3] & _results[4] & _results[5]) == _results[3])
            {
                _gameEnded = true;

                Button0_1.Background = Button1_1.Background = Button2_1.Background = Brushes.Indigo;
            }

            //Row 2
            if (_results[6] != MarkType.Free && (_results[6] & _results[7] & _results[8]) == _results[6])
            {
                _gameEnded = true;

                Button0_2.Background = Button1_2.Background = Button2_2.Background = Brushes.Indigo;
            }

            //Column 0
            if (_results[0] != MarkType.Free && (_results[0] & _results[3] & _results[6]) == _results[0])
            {
                _gameEnded = true;

                Button0_0.Background = Button0_1.Background = Button0_2.Background = Brushes.Indigo;
            }

            //Column 1
            if (_results[1] != MarkType.Free && (_results[1] & _results[4] & _results[7]) == _results[1])
            {
                _gameEnded = true;

                Button1_0.Background = Button1_1.Background = Button1_2.Background = Brushes.Indigo;
            }

            //Column 2
            if (_results[2] != MarkType.Free && (_results[2] & _results[5] & _results[8]) == _results[2])
            {
                _gameEnded = true;

                Button2_0.Background = Button2_1.Background = Button2_2.Background = Brushes.Indigo;
            }

            //Cross 1
            if (_results[0] != MarkType.Free && (_results[0] & _results[4] & _results[8]) == _results[0])
            {
                _gameEnded = true;

                Button0_0.Background = Button1_1.Background = Button2_2.Background = Brushes.Indigo;
            }

            //Cross 2
            if (_results[2] != MarkType.Free && (_results[2] & _results[4] & _results[6]) == _results[2])
            {
                _gameEnded = true;

                Button2_0.Background = Button1_1.Background = Button0_2.Background = Brushes.Indigo;
            }

            if (!_gameEnded)// if it is a draw then all becomes orange
            {
                if (!_results.Any(f => f == MarkType.Free))
                {
                    _gameEnded = true;

                    Container.Children.Cast<Button>().ToList()
                        .ForEach(button =>  button.Background = Brushes.Orange);
                }
            }
        }
    }
}
