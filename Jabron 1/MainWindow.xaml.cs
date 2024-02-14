using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace TicTacToe
{
    public enum Player
    {
        None,
        X,
        O
    }

    public partial class MainWindow : Window
    {
        private Player currentPlayer;
        private Player[,] board;
        private Button[,] buttons;
        private Random random;

        public MainWindow()
        {
            InitializeComponent();
            InitializeGame();
        }

        private void InitializeGame()
        {
            currentPlayer = Player.X;
            board = new Player[3, 3];
            buttons = new Button[3, 3] { { Button1, Button2, Button3 }, { Button4, Button5, Button6 }, { Button7, Button8, Button9 } };
            random = new Random();

            
            foreach (Button button in buttons)
            {
                button.Content = "";
                button.IsEnabled = true;
            }

            
            StatusText.Text = "Ход игрока X";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;

            
            int row = Grid.GetRow(button);
            int column = Grid.GetColumn(button);

            
            if (board[row, column] == Player.None)
            {
                button.Content = currentPlayer.ToString();
                board[row, column] = currentPlayer;

                
                if (CheckForWin(currentPlayer))
                {
                    StatusText.Text = $"Игрок {currentPlayer} победил!";
                    DisableButtons();
                }
                else if (CheckForDraw())
                {
                    StatusText.Text = "Ничья!";
                    DisableButtons();
                }
                else
                {
                    
                    currentPlayer = (currentPlayer == Player.X) ? Player.O : Player.X;
                    StatusText.Text = $"Ход игрока {currentPlayer}";

                    
                    if (currentPlayer == Player.O)
                    {
                        MakeRobotMove();
                    }
                }
            }
        }

        private bool CheckForWin(Player player)
        {
            
            return
                (board[0, 0] == player && board[0, 1] == player && board[0, 2] == player) ||
                (board[1, 0] == player && board[1, 1] == player && board[1, 2] == player) ||
                (board[2, 0] == player && board[2, 1] == player && board[2, 2] == player) ||
                (board[0, 0] == player && board[1, 0] == player && board[2, 0] == player) ||
                (board[0, 1] == player && board[1, 1] == player && board[2, 1] == player) ||
                (board[0, 2] == player && board[1, 2] == player && board[2, 2] == player) ||
                (board[0, 0] == player && board[1, 1] == player && board[2, 2] == player) ||
                (board[0, 2] == player && board[1, 1] == player && board[2, 0] == player);
        }

        private bool CheckForDraw()
        {
            
            foreach (Player player in board)
            {
                if (player == Player.None)
                    return false;
            }
            return true;
        }

        private void DisableButtons()
        {
            
            foreach (Button button in buttons)
            {
                button.IsEnabled = false;
            }
        }

        private void MakeRobotMove()
        {
            if (currentPlayer == Player.O)
            {
                
                List<Tuple<int, int>> emptyCells = new List<Tuple<int, int>>();

                
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (board[i, j] == Player.None)
                        {
                            emptyCells.Add(new Tuple<int, int>(i, j));
                        }
                    }
                }

                
                if (emptyCells.Count > 0)
                {
                    int randomIndex = random.Next(emptyCells.Count);
                    Tuple<int, int> randomCell = emptyCells[randomIndex];
                    int row = randomCell.Item1;
                    int column = randomCell.Item2;

                    board[row, column] = Player.O;
                    buttons[row, column].Content = Player.O.ToString();

                    
                    if (CheckForWin(Player.O))
                    {
                        StatusText.Text = "Игрок O победил!";
                        DisableButtons();
                    }
                    else if (CheckForDraw())
                    {
                        StatusText.Text = "Ничья!";
                        DisableButtons();
                    }
                    else
                    {
                        
                        currentPlayer = Player.X;
                        StatusText.Text = "Ход игрока X";
                    }
                }
            }
        }

        private void RestartButton_Click(object sender, RoutedEventArgs e)
        {
            InitializeGame();
        }
    }
}