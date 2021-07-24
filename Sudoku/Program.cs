using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku
{
    class Program
    {
        // Sudoku sablon po kom sam sastavio algoritam

        //Fill the first row with nine different numbers.
        //Fill the second row which is a shift of the first line by three slots.
        //Fill the third row which is a shift of the second line by three slots.
        //Fill the fourth row which is a shift of the third by one slot.

        //line 1: 8 9 3  2 7 6  4 5 1
        //line 2: 2 7 6  4 5 1  8 9 3 (shift 3)
        //line 3: 4 5 1  8 9 3  2 7 6 (shift 3)

        //line 4: 5 1 8  9 3 2  7 6 4 (shift 1)
        //line 5: 9 3 2  7 6 4  5 1 8 (shift 3)
        //line 6: 7 6 4  5 1 8  9 3 2 (shift 3)

        //line 7: 6 4 5  1 8 9  3 2 7 (shift 1)
        //line 8: 1 8 9  3 2 7  6 4 5 (shift 3)
        //line 9: 3 2 7  6 4 5  1 8 9 (shift 3)
        static void Main(string[] args)
        {
            Stopwatch s = new Stopwatch();
            s.Start();

            int[,] sudoku = ConvertToArray(BreakPattern(ShuffleSudoku(GenerateSudoku())));


            int N = sudoku.GetLength(0);

            if (SolveSudoku(sudoku, N))
            {

                // print solution
                Print(sudoku, N);
            }
            else
            {
                Console.Write("No solution");
            }

            //for (int i = 0; i < sudoku.GetLength(0); i++)
            //{
            //    for (int j = 0; j < sudoku.GetLength(1); j++)
            //    {
            //        Console.Write(sudoku[i,j] + " ");
            //    }
            //    Console.WriteLine();
            //}
            //List<int[]> sudoku = BreakPattern(ShuffleSudoku(GenerateSudoku()));
            //for (int i = 0; i < sudoku.Count; i++)
            //{
            //    for (int j = 0; j < sudoku[i].Length; j++)
            //    {
            //        if (i == 0)
            //        {
            //            Console.Write(sudoku[i][j]);
            //        }
            //        else if (i == 1)
            //        {
            //            Console.Write(sudoku[i][j]);
            //        }
            //        else if (i == 2)
            //        {
            //            Console.Write(sudoku[i][j]);
            //        }
            //        else if (i == 3)
            //        {
            //            Console.Write(sudoku[i][j]);
            //        }
            //        else if (i == 4)
            //        {
            //            Console.Write(sudoku[i][j]);
            //        }
            //        else if (i == 5)
            //        {
            //            Console.Write(sudoku[i][j]);
            //        }
            //        else if (i == 6)
            //        {
            //            Console.Write(sudoku[i][j]);
            //        }
            //        else if (i == 7)
            //        {
            //            Console.Write(sudoku[i][j]);
            //        }
            //        else if (i == 8)
            //        {
            //            Console.Write(sudoku[i][j]);
            //        }
            //        else if (i == 9)
            //        {
            //            Console.Write(sudoku[i][j]);
            //        }
            //    }
            //    Console.WriteLine();
            //}

            Console.WriteLine(s.Elapsed);
            Console.ReadLine();
        }
        static List<int[]> GenerateSudoku()
        {
            List<int[]> sudoku = new List<int[]>(9); // predstavlja sudoku
            
            List<int> numbers = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 }; // predstavlja brojeve po kojima ce nasumicno popuniti prvi red sudokua
            
            int[] row = new int[9]; // row niz predstavlja jedan red u sudoku-u koji popunjava i dodaje u sudoku listu

            Random r = new Random();
            int randomNumber; // predstavlja nasumicni broj koji ce se generisati pomocu Random klase

            for (int rowNumber = 0; rowNumber < 9; rowNumber++) // popunjavanje sudoku redova
            {
                switch (rowNumber)
                {
                    case 0:
                        // popunjava prvi red
                        for (int i = 0; i < 9; i++)
                        {
                            randomNumber = numbers[r.Next(0, numbers.Count)]; // nasumicno bira broj iz numbers liste preko indeksa pomocu Random r
                            numbers.Remove(randomNumber); // brise iz numbers liste broj koji smo vec dodali
                            row[i] = randomNumber; // dodaje nasumicno izabran broj iz number liste u row niz
                        }

                        // dodaje prvi red u sudoku kolekciju
                        sudoku.Add(row);
                        row = new int[9];
                        break;

                    case 1:
                        // popunjava drugi red tako sto ce sve brojeve iz prvog reda pomeriti za tri mesta unazad
                        for (int i = 0; i < 9; i++)
                        {
                            if (i >= 6)
                            {
                                row[i] = sudoku[0][i - 6];
                                continue;
                            }
                            row[i] = sudoku[0][i + 3];                            
                        }

                        sudoku.Add(row);
                        row = new int[9];
                        break;

                    case 2:
                        // popunjava treci red tako sto ce brojeve iz drugog reda pomeriti za tri mesta unazad
                        for (int i = 0; i < 9; i++)
                        {
                            if (i >= 6)
                            {
                                row[i] = sudoku[1][i - 6];
                                continue;
                            }
                            row[i] = sudoku[1][i + 3];
                        }

                        sudoku.Add(row);
                        row = new int[9];
                        break;
                    ///////////////////////prva tri kvadrata 3x3 popunjena = prva tri reda pupunjena
                    case 3:
                        // popunjava cetvrti red tako sto ce brojeve iz treceg reda pomeriti za jedno mesto unazad
                        for (int i = 0; i < 9; i++)
                        {
                            if (i >= 8)
                            {
                                row[i] = sudoku[2][i - 8];
                                continue;
                            }
                            row[i] = sudoku[2][i + 1];
                        }

                        sudoku.Add(row);
                        row = new int[9];
                        break;

                    case 4:
                        // popunjava peti red tako sto ce brojeve iz cetvrtog reda pomeriti za tri mesta unazad
                        for (int i = 0; i < 9; i++)
                        {
                            if (i >= 6)
                            {
                                row[i] = sudoku[3][i - 6];
                                continue;
                            }
                            row[i] = sudoku[3][i + 3];
                        }

                        sudoku.Add(row);
                        row = new int[9];
                        break;

                    case 5:
                        // popunjava sesti red tako sto ce brojeve iz petog reda pomeriti za tri mesta unazad
                        for (int i = 0; i < 9; i++)
                        {
                            if (i >= 6)
                            {
                                row[i] = sudoku[4][i - 6];
                                continue;
                            }
                            row[i] = sudoku[4][i + 3];
                        }

                        sudoku.Add(row);
                        row = new int[9];
                        break;
                    ///////////////////////druga tri kvadrata 3x3 popunjena = 3,4 i 5 red popunjeni
                    case 6:
                        // popunjava sedmi red tako sto ce brojeve iz sestog reda pomeriti za jedno mesto unazad
                        for (int i = 0; i < 9; i++)
                        {
                            if (i >= 8)
                            {
                                row[i] = sudoku[5][i - 8];
                                continue;
                            }
                            row[i] = sudoku[5][i + 1];
                        }

                        sudoku.Add(row);
                        row = new int[9];
                        break;

                    case 7:
                        // popunjava osmi red tako sto ce brojeve iz sedmog reda pomeriti za tri mesta unazad
                        for (int i = 0; i < 9; i++)
                        {
                            if (i >= 6)
                            {
                                row[i] = sudoku[6][i - 6];
                                continue;
                            }
                            row[i] = sudoku[6][i + 3];
                        }

                        sudoku.Add(row);
                        row = new int[9];
                        break;

                    case 8:
                        // popunjava deveti red tako sto ce brojeve iz osmog reda pomeriti za tri mesta unazad
                        for (int i = 0; i < 9; i++)
                        {
                            if (i >= 6)
                            {
                                row[i] = sudoku[7][i - 6];
                                continue;
                            }
                            row[i] = sudoku[7][i + 3];
                        }

                        sudoku.Add(row);
                        row = new int[9];
                        break;
                    ///////////////////////zadnja tri kvadrata 3x3 popunjena = zadnja tri reda popunjena
                    default:
                        break;
                }
            }
            
            return sudoku;
        }

        static List<int[]> ShuffleSudoku(List<int[]> sudokuBoard)
        {
            int[] pattern = new int[3]; // pattern niz predstavlja nasumicno izabran sablon od tri broja koji predstavljaju indekse u sudoku listi po kojima se mesaju redovi,
                                        // za kolone ce se koristiti kao lista od tri broja sa izmesanim redosledom koji treba da se unesu u niz
            
            Random r = new Random(); // predstavlja nasumicni broj pomocu kojeg se nasumicno bira sablon iz shufflePatterns lista

            List<int[]> shuffledSudoku = new List<int[]>(9); // predstavlja sudoku koji se popunjava mesanjem brojeva prosledjenog sudokuBoard-a

            List<int[]> shufflePatternsOne = new List<int[]>() // predstavlja listu sablona za prva tri reda/kolone u sudoku
            {
                new int[] {0, 1, 2},
                new int[] {0, 2, 1},
                new int[] {1, 0, 2},
                new int[] {1, 2, 0},
                new int[] {2, 0, 1},
                new int[] {2, 1, 0},
            };

            List<int[]> shufflePatternsTwo = new List<int[]>() // predstavlja sablon za 3, 4 i 5-ti red/kolonu u sudoku
            {
                new int[] {3, 4, 5},
                new int[] {3, 5, 4},
                new int[] {4, 3, 5},
                new int[] {4, 5, 3},
                new int[] {5, 3, 4},
                new int[] {5, 4, 3},
            };

            List<int[]> shufflePatternsThree = new List<int[]>() // predstavlja sablon za zadnja tri reda/kolone u sudoku
            {
                new int[] {6, 7, 8},
                new int[] {6, 8, 7},
                new int[] {7, 6, 8},
                new int[] {7, 8, 6},
                new int[] {8, 6, 7},
                new int[] {8, 7, 6},
            };


            // mesanje redova u tri koraka gde je svaki korak skup od tri reda koji ce biti izmesani po
            // nasumicno odabranom sablonu iz jedne od shufflePattern lista, ako mesa prva tri reda
            // pattern ce izabrati iz shufflePatternOne, ako mesa 3,4, i 5-ti izabrace iz shufflePatternTwo...
            // mesanje se obavlja u tri koraka zato sto nijedan od devet kvadrata 3x3 u sudoku-u ne smeju 
            // imati duplikate brojeva
            for (int i = 0; i < 3; i++)
            {
                switch (i)
                {
                    case 0:
                        pattern = shufflePatternsOne[r.Next(0, shufflePatternsOne.Count)]; // nasumicno bira sablon iz ShufflePatternOne liste
                        for (int j = 0; j < 3; j++) // popunjava prva tri reda shuffledSudoku liste
                        {
                            shuffledSudoku.Add(sudokuBoard[pattern[j]]); // dodaje red iz prosledjenog List<int[]> sudokuBoard parametra po sablonu pattern niza
                        }
                        break;

                    case 1:
                        pattern = shufflePatternsTwo[r.Next(0, shufflePatternsTwo.Count)]; // nasumicno bira sablon iz ShufflePatternTwo liste
                        for (int j = 0; j < 3; j++) // popunjava 3,4 - 5-ti red shuffledSudoku liste
                        {
                            shuffledSudoku.Add(sudokuBoard[pattern[j]]);
                        }
                        break;

                    case 2:
                        pattern = shufflePatternsThree[r.Next(0, shufflePatternsThree.Count)]; // nasumicno bira sablon iz ShufflePatternThree liste
                        for (int j = 0; j < 3; j++) // popunjava zadnja tri reda shuffledSudoku liste
                        {
                            shuffledSudoku.Add(sudokuBoard[pattern[j]]);
                        }
                        break;

                    default:
                        break;
                }
            }

            int[] columnsPatternOne = shufflePatternsOne[r.Next(0, shufflePatternsOne.Count)]; // sablon za mesanje prve tri kolone
            int[] columnsPatternTwo = shufflePatternsTwo[r.Next(0, shufflePatternsTwo.Count)]; // sablon za mesanje 3,4 i 5-te kolone
            int[] columnsPatternThree = shufflePatternsThree[r.Next(0, shufflePatternsThree.Count)]; // sablon za mesanje zadnje tri kolone
            int index = 0; // index za broj u pattern nizu

            // izmesaj kolone
            for (int j = 0; j < shuffledSudoku.Count; j++)
            {
                for (int k = 0; k < shuffledSudoku.Count; k++)
                {
                    switch (k)
                    {
                        case 0: // dodeljuje nizu pattern izmesane prve tri kolone sudokua po sablonu columnsPatternOne
                            pattern = new int[] {shuffledSudoku[j][columnsPatternOne[0]],
                                shuffledSudoku[j][columnsPatternOne[1]], shuffledSudoku[j][columnsPatternOne[2]] }; // pattern 
                            index = 0;
                            break;
                        case 3: // dodeljuje nizu pattern izmesanu 3,4 i 5-u kolonu sudokua po sablonu columnsPatternTwo
                            pattern = new int[] {shuffledSudoku[j][columnsPatternTwo[0]],
                                shuffledSudoku[j][columnsPatternTwo[1]], shuffledSudoku[j][columnsPatternTwo[2]] };
                            index = 0;
                            break;
                        case 6: // dodeljuje nizu pattern izmesane zadnje tri kolone sudokua po sablonu columnsPatternThree
                            pattern = new int[] {shuffledSudoku[j][columnsPatternThree[0]],
                                shuffledSudoku[j][columnsPatternThree[1]], shuffledSudoku[j][columnsPatternThree[2]] };
                            index = 0;
                            break;
                        default:
                            break;
                    }

                    if (k <= 2)
                    {
                        shuffledSudoku[j][k] = pattern[index++]; // dodeljuje izmesane vrednosti za prve tri kolone iz pattern niza po sablonu columnsPatternOne
                    }
                    else if (k <= 5)
                    {
                        shuffledSudoku[j][k] = pattern[index++]; // dodeljuje izmesane vrednosti za 3,4 i 5-u kolonu iz pattern niza po sablonu columnsPatternTwo
                    }
                    else
                    {
                        shuffledSudoku[j][k] = pattern[index++]; // dodeljuje izmesane vrednosti za zadnje tri kolone iz pattern niza po sablonu columnsPatternThree
                    }
                }                
            }
            return shuffledSudoku;
        }

        static List<int[]> BreakPattern(List<int[]> sudoku) // postavlja vrednosti svih brojeva u sudoku redu na 0 sem tri nasumicno izabrana broja
        {
            Random r = new Random();

            List<int> n = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            int numberOne;
            int numberTwo;
            int numberThree;

            for (int i = 0; i < sudoku.Count; i++)
            {
                numberOne = r.Next(1, n.Count);
                n.Remove(numberOne);
                numberTwo = r.Next(1, n.Count);
                n.Remove(numberTwo);
                numberThree = r.Next(1, n.Count);
                n.Remove(numberThree);

                for (int j = 0; j < sudoku[i].Length; j++)
                {
                    if (sudoku[i][j] != numberOne && sudoku[i][j] != numberTwo && sudoku[i][j] != numberThree)
                    {
                        sudoku[i][j] = 0; // ako sudoku broj nije jednak ni jednom od nasumicno izabranih brojeva postavi njegovu vrednost na 0
                    }
                }

                if (n.Count == 0)
                {
                    n = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
                }
            }

            return sudoku;
        }

        static int[,] ConvertToArray(List<int[]> sudokuBreaked) // konvertuje sudoku iz BreakPattern metode u niz da bi mogao da ga prosledi SolveSudoku metodi
        {
            int[,] sudoku = new int[9,9];

            for (int i = 0; i < sudoku.GetLength(0); i++)
            {
                for (int j = 0; j < sudoku.GetLength(1); j++)
                {
                    sudoku[i, j] = sudokuBreaked[i][j];
                }
            }

            return sudoku;
        }







        // backtracking algoritam


        public static bool SolveSudoku(int[,] board,
                                           int n)
        {
            int row = -1;
            int col = -1;
            bool isEmpty = true;
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (board[i, j] == 0)
                    {
                        row = i;
                        col = j;

                        // We still have some remaining
                        // missing values in Sudoku
                        isEmpty = false;
                        break;
                    }
                }
                if (!isEmpty)
                {
                    break;
                }
            }

            // no empty space left
            if (isEmpty)
            {
                return true;
            }

            // else for each-row backtrack
            for (int num = 1; num <= n; num++)
            {
                if (IsSafe(board, row, col, num))
                {
                    board[row, col] = num;
                    if (SolveSudoku(board, n))
                    {

                        // Print(board, n);
                        return true;
                    }
                    else
                    {

                        // Replace it
                        board[row, col] = 0;
                    }
                }
            }
            return false;
        }
        public static bool IsSafe(int[,] board,
                              int row, int col,
                              int num)
        {

            // Row has the unique (row-clash)
            for (int d = 0; d < board.GetLength(0); d++)
            {

                // Check if the number
                // we are trying to
                // place is already present in
                // that row, return false;
                if (board[row, d] == num)
                {
                    return false;
                }
            }

            // Column has the unique numbers (column-clash)
            for (int r = 0; r < board.GetLength(0); r++)
            {

                // Check if the number 
                // we are trying to
                // place is already present in
                // that column, return false;
                if (board[r, col] == num)
                {
                    return false;
                }
            }

            // corresponding square has
            // unique number (box-clash)
            int sqrt = (int)Math.Sqrt(board.GetLength(0));
            int boxRowStart = row - row % sqrt;
            int boxColStart = col - col % sqrt;

            for (int r = boxRowStart;
                 r < boxRowStart + sqrt; r++)
            {
                for (int d = boxColStart;
                     d < boxColStart + sqrt; d++)
                {
                    if (board[r, d] == num)
                    {
                        return false;
                    }
                }
            }

            // if there is no clash, it's safe
            return true;
        }
        public static void Print(int[,] board, int N)
        {

            // We got the answer, just print it
            for (int r = 0; r < N; r++)
            {
                for (int d = 0; d < N; d++)
                {
                    Console.Write(board[r, d]);
                    Console.Write(" ");
                }
                Console.Write("\n");

                if ((r + 1) % (int)Math.Sqrt(N) == 0)
                {
                    Console.Write("");
                }
            }
        }
    }
}
