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

        private MarkType[] mResults;

        private bool mPlayer1Turn;

        private bool mGameEnded;
        #endregion

        #region Constructor
        /// <summary>
        /// Default constructor
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            NewGame();
        }

        #endregion

        /// <summary>
        /// Start new game, clear all values
        /// </summary>
        private void NewGame()
        {
            // blank array of free cells
            mResults = new MarkType[9];

            for (var i = 0; i < mResults.Length; i++)
                mResults[i] = MarkType.Free;
           // Make sure its player 1 turn
            mPlayer1Turn = true;
            //Itterate every button on the grid
            Container.Children.Cast<Button>().ToList().ForEach(button =>
            {
                button.Content = string.Empty;
                button.Background = Brushes.White;
                button.Foreground = Brushes.Blue;
            });

            mGameEnded = false;
        }
        /// <summary>
        /// Handles the button click event
        /// </summary>
        /// <param name="sender"> Button that was clicked</param>
        /// <param name="e">The events of the click</param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (mGameEnded)
            {
                NewGame();
                return;
            }

            var button = (Button)sender;

            var column = Grid.GetColumn(button);
            var row = Grid.GetRow(button);

            var index = column + (row * 3);
            // Dont do anything if the cell alrady has a value in it
            if (mResults[index] != MarkType.Free)
                return;

            // set the cell value 
            mResults[index] = mPlayer1Turn ? MarkType.Cross : MarkType.Nought;

            //Set buttons content
            button.Content = mPlayer1Turn ? "X" : "O";

            if (!mPlayer1Turn)
                button.Foreground = Brushes.Green;
            // Toggle the players turn
            mPlayer1Turn ^= true;

            //Check for a winner

            CheckForWinner();
        }
        // Check if there is a winner
        private void CheckForWinner()
        {
            //Row 0
            if (mResults[0] != MarkType.Free && (mResults[0] & mResults[1] & mResults[2]) == mResults[0])
            {
                mGameEnded = true;

                Button0_0.Background = Button1_0.Background = Button2_0.Background = Brushes.Indigo;
            }

            //Row 1
            if (mResults[3] != MarkType.Free && (mResults[3] & mResults[4] & mResults[5]) == mResults[3])
            {
                mGameEnded = true;

                Button0_1.Background = Button1_1.Background = Button2_1.Background = Brushes.Indigo;
            }

            //Row 2
            if (mResults[6] != MarkType.Free && (mResults[6] & mResults[7] & mResults[8]) == mResults[6])
            {
                mGameEnded = true;

                Button0_2.Background = Button1_2.Background = Button2_2.Background = Brushes.Indigo;
            }

            //Column 0
            if (mResults[0] != MarkType.Free && (mResults[0] & mResults[3] & mResults[6]) == mResults[0])
            {
                mGameEnded = true;

                Button0_0.Background = Button0_1.Background = Button0_2.Background = Brushes.Indigo;
            }

            //Column 1
            if (mResults[1] != MarkType.Free && (mResults[1] & mResults[4] & mResults[7]) == mResults[1])
            {
                mGameEnded = true;

                Button1_0.Background = Button1_1.Background = Button1_2.Background = Brushes.Indigo;
            }

            //Column 2
            if (mResults[2] != MarkType.Free && (mResults[2] & mResults[5] & mResults[8]) == mResults[2])
            {
                mGameEnded = true;

                Button2_0.Background = Button2_1.Background = Button2_2.Background = Brushes.Indigo;
            }

            //Cross 1
            if (mResults[0] != MarkType.Free && (mResults[0] & mResults[4] & mResults[8]) == mResults[0])
            {
                mGameEnded = true;

                Button0_0.Background = Button1_1.Background = Button2_2.Background = Brushes.Indigo;
            }

            //Cross 2
            if (mResults[2] != MarkType.Free && (mResults[2] & mResults[4] & mResults[6]) == mResults[2])
            {
                mGameEnded = true;

                Button2_0.Background = Button1_1.Background = Button0_2.Background = Brushes.Indigo;
            }

            if (!mGameEnded)
            {
                if (!mResults.Any(f => f == MarkType.Free))
                {
                    mGameEnded = true;

                    Container.Children.Cast<Button>().ToList()
                        .ForEach(button => { button.Background = Brushes.Orange; });
                }
            }
        }
    }
}
