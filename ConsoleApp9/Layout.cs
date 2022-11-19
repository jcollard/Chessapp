using Microsoft.Win32.SafeHandles;
using System.Diagnostics.Metrics;
using System.Drawing;

namespace Chess;

public class PrintBoard
{
    public int turn = 3;
    public string[,] BoardLayout = new string[8, 8];

    public List<string> deadpieces { get; set; }

    public List<string> pieces { get; set; }

    public PrintBoard()
    {

        pieces = new List<string>();
        deadpieces = new List<string>();


    }



    string[] side1pieces = { "r1", "n1", "b1", "k1", "q1", "b2", "n2", "r2" };
    string[] side2pieces = { "R1", "N1", "B1", "Q1", "K1", "B2", "N2", "R2" };





    public void initialize()
    {
        for (int i = 0; i <= 7; i++)
        {
            for (int j = 0; j <= 7; j++)
            {
                if (i == 0) { BoardLayout[i, j] = side1pieces[j]; pieces.Add(side2pieces[j]); }

                else if (i == 7) { BoardLayout[i, j] = side2pieces[j]; pieces.Add(side1pieces[j]); }

                else if (i == 6) { BoardLayout[i, j] = $"P{j + 1}"; pieces.Add($"P{j + 1}"); }

                else if (i == 1) { BoardLayout[i, j] = $"p{j + 1}"; pieces.Add($"p{j + 1}"); }

                else { BoardLayout[i, j] = "  "; }


            }
        }
    }

    public void Print()
    {

        Console.WriteLine("Legend: Use capital letters if you're green, lowercase if you're blue");
        Console.WriteLine("R = Rook, N = Knight, P = Pawn, K = King, B = Bishop, Q = Queen");
        Console.WriteLine();

        int count = 1;
        int row = 1;

        foreach (string i in BoardLayout)
        {

            if (i.ToUpper() == i && !i.Contains(' ') && i != "XX")
            {
                Console.ForegroundColor = ConsoleColor.Green;
                if (count == 8)
                {


                    Console.WriteLine("|  {0}  |", i); count = 1;
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.Write($"{row}");
                    Console.ResetColor();
                    row++;

                    void printnum()
                    {
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine($"{row - 1}");
                        Console.ResetColor();
                    }
                    Console.ForegroundColor = ConsoleColor.DarkMagenta;

                    Console.Write($"-----------------------------------------------------------------");
                    Console.ResetColor();

                    printnum();

                }
                else
                {
                    Console.Write("|  {0}  |", i);
                    count++;
                }
                Console.ResetColor();

            }
            else if (i == "XX" && count != 8)
            {

                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("|  {0}  |", i); Console.ResetColor();
                count++;
            }
            else if (i.ToUpper() != i && i != "XX")
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                if (count == 8)
                {


                    Console.WriteLine("|  {0}  |", i); count = 1;
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.Write($"{row}");
                    Console.ResetColor();
                    row++;

                    void printnum()
                    {
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine($"{row - 1}");
                        Console.ResetColor();
                    }
                    Console.ForegroundColor = ConsoleColor.DarkMagenta;

                    Console.Write($"-----------------------------------------------------------------");
                    Console.ResetColor();

                    printnum();

                }
                else
                {
                    Console.Write("|  {0}  |", i);
                    count++;
                }
                Console.ResetColor();

            }
            else
            {
                if (count == 8)
                {

                    if (i == "XX")
                    {
                        Console.ForegroundColor = ConsoleColor.Red; count = 1;
                        Console.WriteLine("|  {0}  |", i); Console.ResetColor(); Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.Write($"{row}");
                        Console.ResetColor();
                        row++;
                    }




                    else
                    {
                        Console.WriteLine("|  {0}  |", i); count = 1;
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.Write($"{row}");
                        Console.ResetColor();
                        row++;
                    }

                    void printnum()
                    {
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine($"{row - 1}");
                        Console.ResetColor();
                    }
                    Console.ForegroundColor = ConsoleColor.DarkMagenta;

                    Console.Write($"-----------------------------------------------------------------");
                    Console.ResetColor();

                    printnum();

                }


                else
                {
                    Console.Write("|  {0}  |", i);
                    count++;
                }
            }

        }



        string[] Columns = { "A", "B", "C", "D", "E", "F", "G", "H" };
        foreach (string letter in Columns)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;

            Console.Write("   {0}    ", letter);
            Console.ResetColor();


        }



        Console.WriteLine();
        Console.WriteLine("-------------------------------------------------------------------------");
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"List of Pieces Captured: {string.Join(", ", deadpieces)}");
        Console.ResetColor();

        Console.WriteLine("-------------------------------------------------------------------------");

        if (turn % 2 == 0)
        {
            Console.ForegroundColor = ConsoleColor.Green;

            Console.WriteLine("It's Green's turn");
            Console.ResetColor();
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Cyan;

            Console.WriteLine("It's Blue's turn");
            Console.ResetColor();
        }

        if (deadpieces.Contains("K1"))
        {
            Console.WriteLine("Game over! Blue wins!");
        }
        if (deadpieces.Contains("k1"))
        {
            Console.WriteLine("Game over! Green wins");
        }

    }







}
