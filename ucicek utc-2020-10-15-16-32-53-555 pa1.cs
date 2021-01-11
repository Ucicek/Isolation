// Feel free to use/modify this code as a start to pa1

using System;
using static System.Console;

namespace PA1._2
{
    static class Program
    {
        static string[] letters = { "a","b","c","d","e","f","g","h","i","j","k","l",
                "m","n","o","p","q","r","s","t","u","v","w","x","y","z"};
        // 0 : empty, 1 : A, 2 : B, -1 : X
        static int[,] board;
        static string NameA, NameB; // player names

        static int rows, cols;
        
        static int pawnRA, pawnCA;
        static int pawnRB, pawnCB;
        static int remRB = -1;

        static bool turnOfA = true;
        static bool gameInProgress = true;

        static void Main()
        {
            
            initializeGameBoard();
            
            while (gameInProgress)
            {
                DrawGameBoard();
                PerformPlayerMove();
            }
            WriteLine();
        }


        static void initializeGameBoard()
        {
            Write("Enter your name (Deafult --> Player A): ");
            NameA = ReadLine();
            if (NameA.Length == 0) NameA = "Player A";
            Write("Enter your name (Deafult --> Player B): ");
            NameB = ReadLine();
            if (NameB.Length == 0) NameB = "Player B";

            Write("Enter the desired number of rows (Deafult --> 6):");
            string rowStr = ReadLine();
            rows = 6;
            if (rowStr.Length > 0)
                rows = int.Parse(rowStr);
            if (rows < 4 || rows > 26)
            {
                WriteLine("Number of rows should be less 26 and greater than 4");
                Write("Enter the desired number of rows (Deafult --> 6):");
                rows = int.Parse(ReadLine());
            }

            Write("Enter the desired number of columns (Deafult --> 8):");
            string colStr = ReadLine();
            cols = 8;
            if (colStr.Length > 0)
                cols = int.Parse(colStr);

            if (cols < 4 || cols > 26)
            {
                WriteLine("Number of columns should be less 26 and greater than 4");
                Write("Enter the desired number of columns (Deafult --> 8):");
                cols = int.Parse(ReadLine());
            }

            board = new int[rows, cols];
            for (int i = 0; rows > i; i++)
            {
                for (int j = 0; cols > j; j++)
                {
                    board[i, j] = 0;
                }
            }

            pawnRA = 0;
            Write("Enter the desired column of the platform {0} :", NameA);
            pawnCA = int.Parse(ReadLine())-1;

            pawnRB = rows - 1;
            Write("Enter the desired column of the platform {0} :", NameB);
            pawnCB = int.Parse(ReadLine())-1;
            

            board[pawnRA, pawnCA] = 1; // set A location
            board[pawnRB, pawnCB] = 2; // set B location
        }

        static int getCharIndex(string ch) // to 
        {
            int index = -1;
            for (int i = 0; i < letters.Length; i++)
            {
                if (ch == letters[i])
                {
                    index = i;
                    break;
                }
            }
            return index;
        }
        static void PerformPlayerMove()
        {
            bool inValid = false;
            while (!inValid)
            {
                if (turnOfA == true)
                    Write(NameA);
                else
                    Write(NameB);
                Write(" Enter your move [abcd], redisplay[r], or you may quit the game [q] :");
                string response = ReadLine();
                if (response == "q")
                {
                    gameInProgress = false;
                    return;
                }
                if (response == "r") return;

                if (response.Length != 4)
                {
                    WriteLine(" The move must contain 4 coordinate letters");
                }

                if (response.Length == 4)
                {
                    if (turnOfA == true)
                    {

                        int t_pawnRA = getCharIndex(response.Substring(0, 1));
                        int t_pawnCA = int.Parse(response.Substring(1, 1)) - 1; // 5 -> 4. index
                        int t_remRA = getCharIndex(response.Substring(2, 1));
                        int t_remCA = int.Parse(response.Substring(3, 1))-1; // 3 -> 2. index

                        if(t_pawnRA == -1 || t_remRA == -1)
                        {
                            WriteLine("Please enter valid row address");
                            return;
                        }

                        if((t_pawnRA-pawnRA) > 1 || (t_pawnRA - pawnRA) < -1)
                        {
                            WriteLine("Please enter valid location");
                            return;
                        }

                        if ((t_pawnCA - pawnCA) > 1 || (t_pawnCA - pawnCA) < -1)
                        {
                            WriteLine("Please enter valid location");
                            return;
                        }

                        if(board[t_pawnRA, t_pawnCA] != 0 || board[t_remRA, t_remCA] != 0)
                        {
                            WriteLine("Please enter valid location");
                            return;
                        }

                       
                        board[t_pawnRA, t_pawnCA] = 1; // set A location
                        board[t_remRA, t_remCA] = -1; // remove pawn
                        board[pawnRA, pawnCA] = 0; // set old location empty

                        pawnRA = t_pawnRA;
                        pawnCA = t_pawnCA;

                        turnOfA = false;
                        inValid = true;
                    }
                    else
                    {
                        int t_pawnRB = getCharIndex(response.Substring(0, 1));
                        int t_pawnCB = int.Parse(response.Substring(1, 1)) - 1; // 5 -> 4. indeks
                        int t_remRB = getCharIndex(response.Substring(2, 1));
                        int t_remCB = int.Parse(response.Substring(3, 1))-1; // 3 -> 2. indeks

                        if (pawnRB == -1 || remRB == -1)
                        {
                            WriteLine("Please enter valid row address");
                            return;
                        }
                        if ((t_pawnRB - pawnRB) > 1 || (t_pawnRB - pawnRB) < -1)
                        {
                            WriteLine("Please enter valid location");
                            return;
                        }

                        if ((t_pawnCB - pawnCB) > 1 || (t_pawnCB - pawnCB) < -1)
                        {
                            WriteLine("Please enter valid location");
                            return;
                        }

                        if (board[t_pawnRB, t_pawnCB] != 0 || board[t_remRB, t_remCB] != 0)
                        {
                            WriteLine("Please enter valid location");
                            return;

                        }

                        board[t_pawnRB, t_pawnCB] = 2; // set A location
                        board[t_remRB, t_remCB] = -1; // remove pawn
                        board[pawnRB, pawnCB] = 0; // set old location empty

                        pawnRB = t_pawnRB;
                        pawnCB = t_pawnCB;

                        turnOfA = true;
                        inValid = true; // break loop
                    }

                }

            }
        }




        static void DrawGameBoard()
        {
            const string h = "\u2500"; // horizontal line
            const string v = "\u2502"; // vertical line
            const string tl = "\u250c"; // top left corner
            const string tr = "\u2510"; // top right corner
            const string bl = "\u2514"; // bottom left corner
            const string br = "\u2518"; // bottom right corner
            const string vr = "\u251c"; // vertical join from right
            const string vl = "\u2524"; // vertical join from left
            const string hb = "\u252c"; // horizontal join from below
            const string ha = "\u2534"; // horizontal join from above
            const string hv = "\u253c"; // horizontal vertical cross
                                        //const string sp = " ";      // space
                                        //const string pa = "A";      // pawn A
                                        //const string pb = "B";      // pawn B
            const string bb = "\u25a0"; // block
                                        //const string fb = "\u2588"; // left half block
                                        //const string lh = "\u258c"; // left half block
                                        //const string rh = "\u2590"; // right half block



            // Draw the top board boundary.
            Write("   ");
            for (int c = 1; c <= board.GetLength(1); c++)
            {
                if (c == 1) Write(tl);
                Write(" {0} ", c);
                if (c == board.GetLength(1)) Write("{0}", tr);
                else Write("{0}", hb);

            }
            WriteLine();
            Write("   ");
            for (int c = 0; c < board.GetLength(1); c++)
            {
                if (c == 0) Write(tl);
                Write("{0}{0}{0}", h);
                if (c == board.GetLength(1) - 1) Write("{0}", tr);
                else Write("{0}", hb);
            }
            WriteLine();

            // Draw the board rows.
            
            for (int r = 0; r < board.GetLength(0); r++)
            {
                Write(" {0} ", letters[r]);

                // Draw the row contents.
                for (int c = 0; c < board.GetLength(1); c++)
                {
                    if (c == 0) Write(v);
                    if (board[r, c] == 0) 
                        Write("{0}{1}"," "+ bb + " ", v);
                    else if(board[r, c] == 1) 
                        Write("{0}{1}", " A ", v);
                    else
                        if(board[r,c] == 2)
                        Write("{0}{1}", " B ", v);
                    else
                        Write("{0}{1}", "   ", v);
                }
                WriteLine();

                // Draw the boundary after the row.
                if (r != board.GetLength(0) - 1)
                {
                    Write("   ");
                    for (int c = 0; c < board.GetLength(1); c++)
                    {
                        if (c == 0) Write(vr);
                        Write("{0}{0}{0}", h);
                        if (c == board.GetLength(1) - 1) Write("{0}", vl);
                        else Write("{0}", hv);
                    }
                    WriteLine();
                }
                else
                {
                    Write("   ");
                    for (int c = 0; c < board.GetLength(1); c++)
                    {
                        if (c == 0) Write(bl);
                        Write("{0}{0}{0}", h);
                        if (c == board.GetLength(1) - 1) Write("{0}", br);
                        else Write("{0}", ha);
                    }
                    WriteLine();
                }
            }
            
        }
    }
}

// Graded by: Louis

/* Initialization (10)
 * + 2/2: Player's name initialization and default behaviour
 * + 1/2: Number of rows on board initialization, default behaviour, and condition check
 * + 1/2: Number of columns on board initialization, default behaviour, and condition check
 * + 0.5/2: Player A's starting platform initialization, default behaviour, and condition check
 * + 0.5/2: Player B's starting platform initialization, default behaviour, and condition check
 * Comments:
 * - If the first row/column check fails, the subsequent checks fail to recognize a default (blank)
 *   output and throw an exception. In fact, the subsequent checks will accept any input, even ones
 *   that are outside boundaries.
 * - Same issues with the platform positions; you're not checking that they are within boundaries
 *   i.e. on the board. You also did not implement a default platform position.
 * - Why is the user not able to input a row for the platform position?
 *
 * Board state display (10)
 * + 2/2: Each player displays correctly on the board and are distinguishable from one another
 * + 0/2: Each platform displays correctly on the board
 * + 2/2: Removed tiles display correctly on the board
 * + 2/2: Remaining (unremoved) tiles display correctly on the board
 * + 2/2: Rows and columns display nicely with no broken lines, and labels align with rows/columns
 * Comments:
 * - I can't tell the platform apart from unremoved tiles.
 *
 * Input Parsing (10)
 * + 2/2: Correctly parsing the input in the form of "abcd", where "ab" is the player movement tile and "cd" is the tile to remove
 * + 1/2: Detects if the player movement is valid, and updates the player position if so
 * + 1/2: Detects if the tile to remove is valid, and updates the board to reflect it if so
 * + 2/2: Passes turn to the other player only if the movement and removal are both valid and been updated to the game
 * + 2/2: Prompts the SAME user without passing the turn if invalid input is given, and no changes are updated to the game
 * Comments:
 * - I have been trying to move Player B for a while now, and every time your game tells me it is an
 *   invalid row address.
 *
 * Style (6)
 * + 1/1: Reasonable variable names i.e. no "temp", etc.
 * + 1/1: Reasonable variable types i.e. string for name, char/int for row and column, etc.
 * + 2/2: Good commenting
 * + 2/2: Good consistent indentation and spacing
 * Comments:
 * - Good comments, but most of your comments are explaining single lines of code.
 *   Would be nice to see comments that describe a chunk of code as a whole.
 *   Also, some of your comments are more note-to-self than aimed at other programmers.
 */

// Grade: 27/36