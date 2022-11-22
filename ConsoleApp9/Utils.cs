namespace Chessapp;

public static class Utils
{
    public static void TryClear()
    {
        try
        {
            Console.Clear();
        }
        catch
        {
            Console.WriteLine("\nCLEAR()\n");
        }
    }

    public static string ReadLine()
    {
        string input = Console.ReadLine()!;
        StreamWriter outputFile = File.AppendText("temp_inputs.txt");
        outputFile.WriteLine(input);
        outputFile.Close();
        return input;
    }

    /// <summary>
    /// Given two integers, returns 0 if they are the same, -1 if start is
    /// greater than target, and 1 if target is greater than start.
    /// </summary>
    public static int GetIncrement(int start, int target)
    {
        if (start == target)
        {
            return 0;
        }

        if (start > target)
        {
            return -1;
        }

        return 1;
    }

    public static bool IsOrthogonal((int row, int col) start, (int row , int col) target)
    {
        int rowInc = Utils.GetIncrement(start.row, target.row);
        int colInc = Utils.GetIncrement(start.col, target.col);
        return Math.Abs(rowInc) + Math.Abs(colInc) == 1;
    }

    public static bool IsDiagonal((int row, int col) start, (int row , int col) target)
    {
        return Math.Abs(start.row - target.row) == Math.Abs(start.col - target.col);
    }
}