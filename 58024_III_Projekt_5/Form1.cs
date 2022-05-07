using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace _58024_III_Projekt_5
{
    public partial class Form1 : Form
    {
        // to distinguish between X and O
        Boolean checker = false;
        // to check if there is a winer
        Boolean isThereAWinner = false;
        // once I know how to create the board on a fly this is going to be used to 
        // set customized board size
        int boardSize = 10;
        // and also the winning condition ought to be customizable
        int winningCondition = 6;
        // because there is no reason to create edges of the board each time a button is clicked
        // we make a lists for them at the initialization of the board
        List<int> F_upperEdge = new List<int>();
        List<int> F_bottomEdge = new List<int>();
        List<int> B_upperEdge = new List<int>();
        List<int> B_bottomEdge = new List<int>();

        public Form1()
        {
            InitializeComponent();
            CreateEdges();
        }
        private void CreateEdges()
        {
            // to create two lists of numbers of buttons lying on the edges (upper and lower)
            // of the board, respectively for forward and backward diagonals.
            for (int i = 1; i <= boardSize; i++)
            {
                F_bottomEdge.Add(boardSize * (boardSize - 1) + i); // bottom
                F_bottomEdge.Add(boardSize * (i - 1) + 1);                  // left
                F_upperEdge.Add(i);                                                  // upper
                F_upperEdge.Add(i * boardSize);                             // right

                B_bottomEdge.Add(boardSize * (boardSize - 1) + i); // bottom
                B_upperEdge.Add(boardSize * (i - 1) + 1);                   // left
                B_upperEdge.Add(i);                                                  // upper
                B_bottomEdge.Add(i * boardSize);                            // right
            }
        }
        private void ButtonClick(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            
            if (checker == false)
            {
                btn.Text = "O";
                btn.Enabled = false;
                nowToMove.Text = "X";
                Score(btn);
                checker = true;
            }
            else
            {
                btn.Text = "X";
                btn.Enabled = false;
                nowToMove.Text = "O";
                Score(btn);
                checker = false;
            }
        }
        private void Score(Button but)
        {
            Counting(GetHorizontal(but));
            Counting(GetVertical(but));
            Counting(GetForwardDiagonal(but));
            Counting(GetBackwardDiagonal(but));
            CheckingForDraw();
        }
        private void Counting(List<Control> list)
        {
            if (checker) CountXs(list);
            else CountOs(list);
        }
        private void CountXs(List<Control> list)
        {
            int count = 0;
            if (list.Count >= winningCondition)
            {
                for(int i = 0; i < list.Count; i++)
                {
                    Control c = list[i];
                    if (c.Text == "X")
                    {
                        count++;
                        if (count == winningCondition)
                        {
                            score_X.Text = (int.Parse(score_X.Text) + 1).ToString();
                            MessageBox.Show("Gratuluacje! Gracz X zwyciężył!");
                            foreach (Button button in this.panel1.Controls) { button.Enabled = false; }
                            isThereAWinner = true;
                        }
                    }
                    else count = 0;
                }
            }
        }
        private void CountOs(List<Control> list)
        {
            int count = 0;
            if (list.Count >= winningCondition)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    Control c = list[i];
                    if (c.Text == "O")
                    {
                        count++;
                        if (count == winningCondition)
                        {
                            score_O.Text = (int.Parse(score_O.Text) + 1).ToString();
                            MessageBox.Show("Gratuluacje! Gracz O zwyciężył!");
                            foreach (Button button in this.panel1.Controls) { button.Enabled = false; }
                            isThereAWinner = true;
                        }
                    }
                    else count = 0;
                }
            }
        }
        private List<Control> GetHorizontal(Button but)
        {
            // to create a list of Buttons lying on the same row as 'but'
            List<int> horizontals = new List<int>();
            int buttonPicked = int.Parse(but.Name.Substring(4));
            int rowNumber = buttonPicked / boardSize;
            for (int i =1; i  <= boardSize; i ++)
            {
                horizontals.Add((rowNumber*boardSize)+i );
            }
            return new List<Control>(GetMatches(horizontals));
        }
        private List<Control> GetVertical(Button but)
        {
            // to create a list of Buttons lying on the same column as 'but'
            List<int> vertical = new List<int>();
            int buttonPicked = int.Parse(but.Name.Substring((4)));
            int columnNumber = buttonPicked % boardSize;
            for (int i = 0; i  < boardSize; i ++)
            {
                vertical.Add((i *boardSize)+columnNumber);
            }
            return new List<Control>(GetMatches(vertical));
        }
        private List<Control> GetForwardDiagonal(Button but)
        {
            List<int> numbersOnDiagonal = new List<int>();
            int buttonPicked = int.Parse(but.Name.Substring(4));
            int count = 0;
            int toBeAdded = buttonPicked;
            while (!F_bottomEdge.Contains(toBeAdded))
            {
                toBeAdded = buttonPicked + (count * boardSize) - (count * 1);
                numbersOnDiagonal.Add(toBeAdded);
                count++;
            }
            count = 0;
            toBeAdded = buttonPicked;
            while (!F_upperEdge.Contains(toBeAdded))
            {
                toBeAdded = buttonPicked - (count * boardSize) + (count * 1);
                numbersOnDiagonal.Add(toBeAdded);
                count++;
            }
            numbersOnDiagonal = numbersOnDiagonal.Distinct().ToList();
            numbersOnDiagonal.Sort();
            return new List<Control> (GetMatches(numbersOnDiagonal));
        }
        private List<Control> GetBackwardDiagonal(Button but)
        {
            List<int> numbersOnDiagonal = new List<int>();
            int buttonPicked = int.Parse(but.Name.Substring(4));
            int count = 0;
            int toBeAdded = buttonPicked;
            while (!B_bottomEdge.Contains(toBeAdded))
            {
                toBeAdded = buttonPicked + (count * boardSize) + (count * 1);
                numbersOnDiagonal.Add(toBeAdded);
                count++;
            }

            count = 0;
            toBeAdded = buttonPicked;

            while (!B_upperEdge.Contains(toBeAdded))
            {
                toBeAdded = buttonPicked - (count * boardSize) - (count * 1);
                numbersOnDiagonal.Add(toBeAdded);
                count++;
            }
            numbersOnDiagonal = numbersOnDiagonal.Distinct().ToList();
            numbersOnDiagonal.Sort();
            return new List<Control> (GetMatches(numbersOnDiagonal));
        }
        private List<Control> GetMatches(List<int> list)
        {
            // to extract Buttons based on their Name using list of their numbers
            List<Control> matchedButtons = new List<Control>();
            foreach(int name in list)
            {
                Control[] tobeAdded = panel1.Controls.Find("btn_" + name.ToString(), false);
                matchedButtons.Add(tobeAdded[0]);
            }
            return matchedButtons;
        }
        private void CheckingForDraw()
        {
            if (!isThereAWinner)
            {
                int disabled = 0;
                // we gonna count the disabled buttons
                foreach (Button button in this.panel1.Controls)
                {
                    if (!button.Enabled)
                    {
                        disabled++;
                        // and if all of them are disabled, it's a draw
                        if (disabled == 100)
                        {
                            MessageBox.Show("Brawo zuchy, zacięta to była batalia, ale wynik pozostaje nierozstrzygnięty!", "REMIS");
                        }
                    }
                }
            }
        }
        private void cls_btn_Click(object sender, EventArgs e)
        {
            DialogResult exit = MessageBox.Show("Czy na pewno chcesz zakończyć?", "A może jeszcze partyjka...", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (exit == DialogResult.Yes)
            {
                Close();
            }
            else
            {
                MessageBox.Show("Powodzenia!", "Kółko i krzyżyk");
                NewGame();
            }
        }
        private void restart_btn_Click(object sender, EventArgs e)
        {
            foreach (Button button in this.panel1.Controls)
            {
                    button.Text = "";
                    button.Enabled = true;
            }
            isThereAWinner = false;
        }
        private void new_btn_Click(object sender, EventArgs e)
        {
            NewGame();
        }
        private void NewGame()
        {
            foreach (Button button in this.panel1.Controls)
            {
                button.Text = "";
                button.Enabled = true;
            }
            score_O.Text = "0";
            score_X.Text = "0";
            isThereAWinner = false;
        }
        
    }
}
