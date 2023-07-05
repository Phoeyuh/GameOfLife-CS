/*
     Phoeyuh
     The Game Of Life - c# version
*/

using System;

namespace GameOfLifeV2
{
    class Program
    {
        /*
             - For instructions, please read the text displayed at the program's startup.

             [FUNCTIONALITY]

             - To apply the rules, the surrounding squares (maximum 8) are counted for each block,
               and based on that, the rules are applied.

             - Each block can have only one possible evolution.

             - To avoid incorrect state updates, the calculation is performed on one matrix,
               and the result is applied to a separate matrix. At the end of the calculations,
               the results in the second matrix are transferred back to the first matrix, and this process repeats.

             - Anything outside the boundaries of the matrix is considered an empty block.

             [CHANGES FROM THE BASE VERSION]

             - Improved presentation of the value input mode, which is now manual instead of random.
             - Added a guide screen (instructions).
             - Made minor code improvements.

         */


        static bool ProssimoIns = true;     // Variable that manages block input
        static bool Prosegui = true;       // Variable that manages game evolution


        // Generic
        static void StampaMatrice(int[,] mat)
        {
            for (int i = 0; i < mat.GetLength(0); i++)
            {
                for (int x = 0; x < mat.GetLength(1); x++)
                {
                    if (mat[i, x] == 2)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write(" ■");

                    }
                    if (mat[i, x] == 1)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write(" ■");
                    }
                    if (mat[i, x] == 0)
                    {
                        Console.ResetColor();
                        Console.Write(" □");
                    }
                }
                Console.WriteLine();
            }
        }                  // Print the matrix
        static void StampaTitolo(string parola, string colore)
        {
            //color selection
            switch (colore)
            {
                case "DarkBlue":
                    Console.ForegroundColor = ConsoleColor.DarkBlue; break;
                case "DarkCyan":
                    Console.ForegroundColor = ConsoleColor.DarkCyan; break;
                case "DarkGray":
                    Console.ForegroundColor = ConsoleColor.DarkGray; break;
                case "DarkGreen":
                    Console.ForegroundColor = ConsoleColor.DarkGreen; break;
                case "DarkMagenta":
                    Console.ForegroundColor = ConsoleColor.DarkMagenta; break;
                case "DarkRed":
                    Console.ForegroundColor = ConsoleColor.DarkRed; break;
                case "DarkYellow":
                    Console.ForegroundColor = ConsoleColor.DarkYellow; break;
                case "Cyan":
                    Console.ForegroundColor = ConsoleColor.Cyan; break;
                case "Gray":
                    Console.ForegroundColor = ConsoleColor.Gray; break;
                case "Green":
                    Console.ForegroundColor = ConsoleColor.Green; break;
                case "Magenta":
                    Console.ForegroundColor = ConsoleColor.Magenta; break;
                case "Red":
                    Console.ForegroundColor = ConsoleColor.Red; break;
                case "Yellow":
                    Console.ForegroundColor = ConsoleColor.Yellow; break;
                case "Blue":
                    Console.ForegroundColor = ConsoleColor.Blue; break;
                case "White":
                    Console.ResetColor(); break;
                default:
                    Console.ForegroundColor = ConsoleColor.Yellow; break;

            }

            //line 1
            Console.Write(" ");
            for (int i = 0; i < parola.Length; i++)
                Console.Write("-");
            Console.WriteLine();
            //text
            Console.WriteLine($"|{parola}|");
            //line2
            Console.Write(" ");
            for (int i = 0; i < parola.Length; i++)
                Console.Write("-");
        } // Print a string as a title
        static void StampaIstruzioni()
        {
            StampaTitolo("INSTRUCTIONS", "Green");
            Console.WriteLine("\n");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("To move the cursor:");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("[W] ↑ [A] ← [S] ↓ [D] →");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("To store a new block:");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("[WW] ↑ [AA] ← [SS] ↓ [DD] →");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("type 'p' to start the evolution");
            Console.WriteLine();
            Console.ResetColor();
            Console.ReadKey();
            Console.Clear();
        }                         // Print the instructions

        // Value input [1]
        static void InizializzaMatrice(int[,] mat)
        {
            for (int i = 0; i < mat.GetLength(0); i++)
                for (int x = 0; x < mat.GetLength(1); x++)
                    mat[i, x] = 0;
            Random r = new Random();
            mat[r.Next(0, mat.GetLength(1)), r.Next(0, mat.GetLength(0))] = 2;
        }             // Set all matrix values to 0 and one random value to 2 (cursor)
        static int[] TrovaCursore(int[,] mat)                     // gives the cursor location in the matrix
        {
            int[] array = new int[2];
            for (int i = 0; i < mat.GetLength(0); i++)
            {
                for (int x = 0; x < mat.GetLength(1); x++)
                {
                    if (mat[i, x] == 2)
                    {
                        array[0] = i;
                        array[1] = x;
                        return array;
                    }
                }
            }
            return array;
        }
        static void MuoviCursone(int[,] mat, string scelta)
        {
            // Track the coordinates of the cursor
            int[] coordinate = new int[2];
            coordinate = TrovaCursore(mat);
            int x = coordinate[1], y = coordinate[0];

            //moves the cursor
            try
            {
                switch (scelta)
                {
                    //forward
                    case "w":
                        mat[y - 1, x] = 2;
                        mat[y, x] = 0;
                        break;
                    case "ww":
                        mat[y - 1, x] = 2;
                        mat[y, x] = 1;
                        break;
                    //backward
                    case "s":
                        mat[y + 1, x] = 2;
                        mat[y, x] = 0;
                        break;
                    case "ss":
                        mat[y + 1, x] = 2;
                        mat[y, x] = 1;
                        break;
                    //left
                    case "a":
                        mat[y, x - 1] = 2;
                        mat[y, x] = 0;
                        break;
                    case "aa":
                        mat[y, x - 1] = 2;
                        mat[y, x] = 1;
                        break;
                    //right
                    case "d":
                        mat[y, x + 1] = 2;
                        mat[y, x] = 0;
                        break;
                    case "dd":
                        mat[y, x + 1] = 2;
                        mat[y, x] = 1;
                        break;
                    //end
                    case "p":
                        ProssimoIns = false; break;
                    default:
                        break;
                }
            }
            catch (IndexOutOfRangeException)
            {
                Console.Clear();
            }
        }    // new cursor location
        static void FaseDiInserimento(int[,] mat)                 // Insert user values into the matrix
        {
            do
            {
                try
                {
                    StampaMatrice(mat);
                    Console.ResetColor();
                    Console.Write("\n--> ");
                    string scelta = Console.ReadLine();
                    MuoviCursone(mat, scelta);
                }
                catch (FormatException)
                {
                    Console.Clear();
                }
                Console.Clear();

            } while (ProssimoIns);
        }
        static void RimuoviCursore(int[,] mat)                    // removes the cursor
        {
            for (int i = 0; i < mat.GetLength(0); i++)
                for (int x = 0; x < mat.GetLength(1); x++)
                    if (mat[i,x] == 2)
                        mat[i, x] = 0;
        }

        // Game state evolution [2]
        static void CopiaMatrice(int[,] mat1, int[,] mat2) // Copy mat1 to mat2
        {
            for (int i = 0; i < mat1.GetLength(0); i++)
                for (int x = 0; x < mat1.GetLength(1); x++)
                    mat2[i, x] = mat1[i, x];
        }
        static void EvolviStato(int[,] mat)
        {
            int[,] mat2 = new int[mat.GetLength(0), mat.GetLength(1)]; // Support matrix
            // Copy the original matrix to the secondary one
            CopiaMatrice(mat, mat2);

            // Count how many filled squares surround each individual block
            for (int i = 0; i < mat.GetLength(0); i++)
            {
                for (int x = 0; x < mat.GetLength(1); x++)
                {
                    int conta = 0;
                    //Up
                    if (i + 1 < mat.GetLength(0) && mat[i + 1, x] == 1) // Avoid going beyond the matrix boundaries, consider 0 for values outside
                        conta++;
                    //Down
                    if (i - 1 >= 0 && mat[i - 1, x] == 1)
                        conta++;
                    //Right
                    if (x + 1 < mat.GetLength(1) && mat[i, x + 1] == 1)
                        conta++;
                    //Left
                    if (x - 1 >= 0 && mat[i, x - 1] == 1)
                        conta++;
                    //upper diag 1 [\]
                    if (i - 1 >= 0 && x - 1 >= 0 && mat[i - 1, x - 1] == 1)
                        conta++;
                    //lower diagonal 1 [\]
                    if (i + 1 < mat.GetLength(0) && x + 1 < mat.GetLength(1) && mat[i + 1, x + 1] == 1)
                        conta++;
                    //upper diag 2 [/]
                    if (i - 1 >= 0 && x + 1 < mat.GetLength(1) && mat[i - 1, x + 1] == 1)
                        conta++;
                    //lower diagonal 2 [/]
                    if (i + 1 < mat.GetLength(0) && x - 1 >= 0 && mat[i + 1, x - 1] == 1)
                        conta++;

                    //------------------------------------EXECUTION OF RULES-----------------------------------------//

                    bool flag = true; // Only one condition per cell can occur, avoiding additional checks

                    // RULE 1: The cell dies if 0 / 1 neighbor
                    if (flag && mat[i, x] == 1 && (conta == 0 || conta == 1))
                    {
                        mat2[i, x] = 0;// The cell dies of loneliness
                        flag = false;
                    }

                    // RULE 2: The cell dies if it has 4 or more neighbors
                    if (flag && mat[i, x] == 1 && conta >= 4)
                    {
                        mat2[i, x] = 0; // The cell dies due to overpopulation
                        flag = false;
                    }

                    // RULE 3: The cell survives if it has 2 or 3 neighbors
                    if (flag == true && mat[i, x] == 1 && (conta == 2 || conta == 3))
                        flag = false;    // The cell remains unchanged

                    // RULE 4: A dead cell revives if it has 3 active neighbors
                    if (flag && mat[i, x] == 0 && conta == 3)
                    {
                        mat2[i, x] = 1; // If it has 3 active neighbors, it becomes active
                        flag = false;
                    }
                }
            }
            // Reassign the values of the second matrix to the first matrix
            CopiaMatrice(mat2, mat);
        }

        static void Main(string[] args)
        {
            int[,] mat = new int[15, 15]; // Change field size here

            //Introduction
            StampaIstruzioni();

            //First input phase
            InizializzaMatrice(mat);
            FaseDiInserimento(mat);
            RimuoviCursore(mat);

            //Game execution phase
            StampaMatrice(mat);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("[CLICK TO START]");
            Console.ReadKey();
            Console.Clear();
            do
            {
                EvolviStato(mat);
                StampaMatrice(mat);
                Console.ReadKey();
                Console.Clear();

            } while (Prosegui);
        }
    }
}

