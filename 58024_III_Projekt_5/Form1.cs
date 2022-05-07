using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace _58024_III_Projekt_5
{
    public partial class Form1 : Form
    {
        // to distinguish between X and O
        Boolean GJ_58024_checker = false;
        // to check if there is a winer
        Boolean GJ_58024_isThereAWinner = false;
        // once I know how to create the board on a fly this is going to be used to 
        // set customized board size
        int GJ_58024_boardSize = 10;
        // and also the winning condition ought to be customizable
        int GJ_58024_winningCondition = 6;
        // because there is no reason to create edges of the board each time a button is clicked
        // we make a lists for them at the initialization of the board
        List<int> GJ_58024_F_upperEdge = new List<int>();
        List<int> GJ_58024_F_bottomEdge = new List<int>();
        List<int> GJ_58024_B_upperEdge = new List<int>();
        List<int> GJ_58024_B_bottomEdge = new List<int>();
        public Form1()
        {
            InitializeComponent();
            GJ_58024_CreateEdges();
        }
        private void GJ_58024_CreateEdges()
        {
            // to create two lists of numbers of buttons lying on the edges (upper and lower)
            // of the board, respectively for forward and backward diagonals.
            for (int i = 1; i <= GJ_58024_boardSize; i++)
            {
                GJ_58024_F_bottomEdge.Add(GJ_58024_boardSize * (GJ_58024_boardSize - 1) + i); // bottom
                GJ_58024_F_bottomEdge.Add(GJ_58024_boardSize * (i - 1) + 1);                  // left
                GJ_58024_F_upperEdge.Add(i);                                                  // upper
                GJ_58024_F_upperEdge.Add(i * GJ_58024_boardSize);                             // right

                GJ_58024_B_bottomEdge.Add(GJ_58024_boardSize * (GJ_58024_boardSize - 1) + i); // bottom
                GJ_58024_B_upperEdge.Add(GJ_58024_boardSize * (i - 1) + 1);                   // left
                GJ_58024_B_upperEdge.Add(i);                                                  // upper
                GJ_58024_B_bottomEdge.Add(i * GJ_58024_boardSize);                            // right
            }
        }
        private void GJ_58024_ButtonClick(object GJ_58024_sender, EventArgs GJ_58024_e)
        {
            Button GJ_58024_btn = GJ_58024_sender as Button;
            
            if (GJ_58024_checker == false)
            {
                GJ_58024_btn.Text = "O";
                GJ_58024_btn.Enabled = false;
                GJ_58024_nowToMove.Text = "X";
                GJ_58024_Score(GJ_58024_btn);
                GJ_58024_checker = true;
            }
            else
            {
                GJ_58024_btn.Text = "X";
                GJ_58024_btn.Enabled = false;
                GJ_58024_nowToMove.Text = "O";
                GJ_58024_Score(GJ_58024_btn);
                GJ_58024_checker = false;
            }
        }
        private void GJ_58024_Score(Button GJ_58024_but)
        {
            GJ_58024_Counting(GJ_58024_GetHorizontal(GJ_58024_but));
            GJ_58024_Counting(GJ_58024_GetVertical(GJ_58024_but));
            GJ_58024_Counting(GJ_58024_GetForwardDiagonal(GJ_58024_but));
            GJ_58024_Counting(GJ_58024_GetBackwardDiagonal(GJ_58024_but));
            GJ_58024_CheckingForDraw();
        }
        private void GJ_58024_Counting(List<Control> GJ_58024_list)
        {
            if (GJ_58024_checker) GJ_58024_CountXs(GJ_58024_list);
            else GJ_58024_CountOs(GJ_58024_list);
        }
        private void GJ_58024_CountXs(List<Control> GJ_58024_list)
        {
            int count = 0;
            if (GJ_58024_list.Count >= GJ_58024_winningCondition)
            {
                for(int i = 0; i < GJ_58024_list.Count; i++)
                {
                    Control c = GJ_58024_list[i];
                    if (c.Text == "X")
                    {
                        count++;
                        if (count == GJ_58024_winningCondition)
                        {
                            GJ_58024_score_X.Text = (int.Parse(GJ_58024_score_X.Text) + 1).ToString();
                            MessageBox.Show("Gratuluacje! Gracz X zwyciężył!");
                            foreach (Button GJ_58024_button in this.panel1.Controls) { GJ_58024_button.Enabled = false; }
                            GJ_58024_isThereAWinner = true;
                        }
                    }
                    else count = 0;
                }
            }
        }
        private void GJ_58024_CountOs(List<Control> GJ_58024_list)
        {
            int count = 0;
            if (GJ_58024_list.Count >= GJ_58024_winningCondition)
            {
                for (int i = 0; i < GJ_58024_list.Count; i++)
                {
                    Control c = GJ_58024_list[i];
                    if (c.Text == "O")
                    {
                        count++;
                        if (count == GJ_58024_winningCondition)
                        {
                            GJ_58024_score_O.Text = (int.Parse(GJ_58024_score_O.Text) + 1).ToString();
                            MessageBox.Show("Gratuluacje! Gracz O zwyciężył!");
                            foreach (Button GJ_58024_button in this.panel1.Controls) { GJ_58024_button.Enabled = false; }
                            GJ_58024_isThereAWinner = true;
                        }
                    }
                    else count = 0;
                }
            }
        }
        private List<Control> GJ_58024_GetHorizontal(Button GJ_58024_but)
        {
            // to create a list of Buttons lying on the same row as 'but'
            List<int> GJ_58024_horizontals = new List<int>();
            int buttonPicked = int.Parse(GJ_58024_but.Name.Substring(4));
            int rowNumber = buttonPicked / GJ_58024_boardSize;
            for (int GJ_58024_i =1; GJ_58024_i  <= GJ_58024_boardSize; GJ_58024_i ++)
            {
                GJ_58024_horizontals.Add((rowNumber*GJ_58024_boardSize)+GJ_58024_i );
            }
            return new List<Control>(GJ_58024_GetMatches(GJ_58024_horizontals));
        }
        private List<Control> GJ_58024_GetVertical(Button GJ_58024_but)
        {
            // to create a list of Buttons lying on the same column as 'but'
            List<int> GJ_58024_vertical = new List<int>();
            int GJ_58024_buttonPicked = int.Parse(GJ_58024_but.Name.Substring((4)));
            int GJ_58024_columnNumber = GJ_58024_buttonPicked % GJ_58024_boardSize;
            for (int GJ_58024_i = 0; GJ_58024_i  < GJ_58024_boardSize; GJ_58024_i ++)
            {
                GJ_58024_vertical.Add((GJ_58024_i *GJ_58024_boardSize)+GJ_58024_columnNumber);
            }
            return new List<Control>(GJ_58024_GetMatches(GJ_58024_vertical));
        }
        private List<Control> GJ_58024_GetForwardDiagonal(Button GJ_58024_but)
        {
            List<int> GJ_58024_numbersOnDiagonal = new List<int>();
            int GJ_58024_buttonPicked = int.Parse(GJ_58024_but.Name.Substring(4));
            int GJ_58024_count = 0;
            int GJ_58024_toBeAdded = GJ_58024_buttonPicked;
            while (!GJ_58024_F_bottomEdge.Contains(GJ_58024_toBeAdded))
            {
                GJ_58024_toBeAdded = GJ_58024_buttonPicked + (GJ_58024_count * GJ_58024_boardSize) - (GJ_58024_count * 1);
                GJ_58024_numbersOnDiagonal.Add(GJ_58024_toBeAdded);
                GJ_58024_count++;
            }
            GJ_58024_count = 0;
            GJ_58024_toBeAdded = GJ_58024_buttonPicked;
            while (!GJ_58024_F_upperEdge.Contains(GJ_58024_toBeAdded))
            {
                GJ_58024_toBeAdded = GJ_58024_buttonPicked - (GJ_58024_count * GJ_58024_boardSize) + (GJ_58024_count * 1);
                GJ_58024_numbersOnDiagonal.Add(GJ_58024_toBeAdded);
                GJ_58024_count++;
            }
            GJ_58024_numbersOnDiagonal = GJ_58024_numbersOnDiagonal.Distinct().ToList();
            GJ_58024_numbersOnDiagonal.Sort();
            return new List<Control> (GJ_58024_GetMatches(GJ_58024_numbersOnDiagonal));
        }
        private List<Control> GJ_58024_GetBackwardDiagonal(Button GJ_58024_but)
        {
            List<int> numbersOnDiagonal = new List<int>();
            int buttonPicked = int.Parse(GJ_58024_but.Name.Substring(4));
            int count = 0;
            int toBeAdded = buttonPicked;
            while (!GJ_58024_B_bottomEdge.Contains(toBeAdded))
            {
                toBeAdded = buttonPicked + (count * GJ_58024_boardSize) + (count * 1);
                numbersOnDiagonal.Add(toBeAdded);
                count++;
            }

            count = 0;
            toBeAdded = buttonPicked;

            while (!GJ_58024_B_upperEdge.Contains(toBeAdded))
            {
                toBeAdded = buttonPicked - (count * GJ_58024_boardSize) - (count * 1);
                numbersOnDiagonal.Add(toBeAdded);
                count++;
            }
            numbersOnDiagonal = numbersOnDiagonal.Distinct().ToList();
            numbersOnDiagonal.Sort();
            return new List<Control> (GJ_58024_GetMatches(numbersOnDiagonal));
        }
        private List<Control> GJ_58024_GetMatches(List<int> GJ_58024_list)
        {
            // to extract Buttons based on their Name using list of their numbers
            List<Control> matchedButtons = new List<Control>();
            foreach(int name in GJ_58024_list)
            {
                Control[] tobeAdded = panel1.Controls.Find("btn_" + name.ToString(), false);
                matchedButtons.Add(tobeAdded[0]);
            }
            return matchedButtons;
        }
        private void GJ_58024_CheckingForDraw()
        {
            if (!GJ_58024_isThereAWinner)
            {
                int GJ_58024_disabled = 0;
                // we gonna count the disabled buttons
                foreach (Button GJ_58024_button in this.panel1.Controls)
                {
                    if (!GJ_58024_button.Enabled)
                    {
                        GJ_58024_disabled++;
                        // and if all of them are disabled, it's a draw
                        if (GJ_58024_disabled == 100)
                        {
                            MessageBox.Show("Brawo zuchy, zacięta to była batalia, ale wynik pozostaje nierozstrzygnięty!", "REMIS");
                        }
                    }
                }
            }
        }
        private void GJ_58024_cls_btn_Click(object GJ_58024_sender, EventArgs GJ_58024_e)
        {
            DialogResult GJ_58024_exit = MessageBox.Show("Czy na pewno chcesz zakończyć?", "A może jeszcze partyjka...", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (GJ_58024_exit == DialogResult.Yes)
            {
                Close();
            }
            else
            {
                MessageBox.Show("Powodzenia!", "Kółko i krzyżyk");
                GJ_58024_NewGame();
            }
        }
        private void GJ_58024_restart_btn_Click(object GJ_58024_sender, EventArgs GJ_58024_e)
        {
            foreach (Button GJ_58024_button in this.panel1.Controls)
            {
                    GJ_58024_button.Text = "";
                    GJ_58024_button.Enabled = true;
            }
            GJ_58024_isThereAWinner = false;
        }
        private void GJ_58024_new_btn_Click(object GJ_58024_sender, EventArgs GJ_58024_e)
        {
            GJ_58024_NewGame();
        }
        private void GJ_58024_NewGame()
        {
            foreach (Button GJ_58024_button in this.panel1.Controls)
            {
                GJ_58024_button.Text = "";
                GJ_58024_button.Enabled = true;
            }
            GJ_58024_score_O.Text = "0";
            GJ_58024_score_X.Text = "0";
            GJ_58024_isThereAWinner = false;
        }
        
    }
}
