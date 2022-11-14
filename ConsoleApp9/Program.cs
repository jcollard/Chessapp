﻿namespace ConsoleApp9
{
    public class Program
    {
        static void Main(string[] args)
        {

             static void ClearCurrentConsoleLine()
            {
                int currentLineCursor = Console.CursorTop;
                Console.SetCursorPosition(0, Console.CursorTop);
                Console.Write(new string(' ', Console.WindowWidth));
                Console.SetCursorPosition(0, currentLineCursor);
            }

            PrintBoard changes = new PrintBoard();
            int turn = 4;

            string[,] BoardLayout = new string[8, 8];

            string[] Rows = { "1", "2", "3", "4", "5", "6", "7", "8" };
            string[] Columns = { "A", "B", "C", "D", "E", "F", "G", "H" };
            List<string> AddressList = new List<string>();

            int[] arr = new int[2];
            //indexes for comparison and logic 
            int[] indexselect(string select)
            {
                int[] array = new int[2];
                for (int i = 0; i <= 7; i++)
                {

                    for (int j = 0; j <= 7; j++)
                    {


                        if (changes.BoardLayout[i, j] == select) { array[0] = i; array[1] = j; }

                    }

                }
                return array;
            }
            int[] indextile(string tile)
            {
                int[] array = new int[2];
                for (int i = 0; i <= 7; i++)
                {

                    for (int j = 0; j <= 7; j++)
                    {


                        if (BoardLayout[i, j] == tile) { array[0] = i; array[1] = j; }

                    }

                }
                return array;
            }

            changes.initialize(); //starting board
            changes.Print();
            void fillboardaddresses()
            {
                for (int i = 0; i <= 7; i++)
                {
                    for (int j = 0; j <= 7; j++)
                    {
                        BoardLayout[i, j] = Columns[j] + Rows[i];
                        AddressList.Add(BoardLayout[i, j]);
                    }
                }

            } //fill tile addresses
            fillboardaddresses();

            bool kinglogic(int[] selectindex, int[] tileindex)
            {
                int selecti = selectindex[0];
                int tilei = tileindex[0];

                int selectj = selectindex[1];
                int tilej = tileindex[1];
                if (changes.BoardLayout[tilei, tilej] == changes.BoardLayout[tilei, tilej].ToUpper() && changes.BoardLayout[selecti, selectj] == changes.BoardLayout[selecti, selectj].ToUpper() && !changes.BoardLayout[tilei, tilej].Contains(' ')) { return false; }
                if (changes.BoardLayout[tilei, tilej] == changes.BoardLayout[tilei, tilej].ToLower() && changes.BoardLayout[selecti, selectj] == changes.BoardLayout[selecti, selectj].ToLower() && !changes.BoardLayout[tilei, tilej].Contains(' ')) { return false; }
              
                if(Math.Abs(selecti-tilei)!=1 && Math.Abs(selectj-tilej)!=1) { return false; }
                else if (Math.Abs(selecti - tilei) != 1 && Math.Abs(selecti - tilei) != 0) { return false; }
                else if (Math.Abs(selectj - tilej) == 0 && Math.Abs(selectj - tilej) == 1) { return false; }
                else return true;










            }


            bool pawnlogic(int[] selectindex, int[] tileindex)
            {
                int selecti = selectindex[0];
                int tilei = tileindex[0];

                int selectj = selectindex[1];
                int tilej = tileindex[1];

                char player = changes.BoardLayout[selecti, selectj][0];
                if (changes.BoardLayout[tilei, tilej] == changes.BoardLayout[tilei, tilej].ToUpper() && changes.BoardLayout[selecti, selectj] == changes.BoardLayout[selecti, selectj].ToUpper() && !changes.BoardLayout[tilei, tilej].Contains(' ')) { return false; }
                if (changes.BoardLayout[tilei, tilej] == changes.BoardLayout[tilei, tilej].ToLower() && changes.BoardLayout[selecti, selectj] == changes.BoardLayout[selecti, selectj].ToLower() && !changes.BoardLayout[tilei, tilej].Contains(' ')) { return false; }

                if (char.ToUpper(player) == player)
                {
                    if (selecti - tilei == 2 && selecti == 6 && changes.BoardLayout[tilei, tilej].Contains(' ') && changes.BoardLayout[tilei + 1, tilej].Contains(' '))
                    {
                        return true;
                    }
                    if (selecti - tilei == 1 && tilej == selectj && changes.BoardLayout[tilei, tilej].Contains(' ')) { return true; }
                    if (selecti - tilei == 1 && Math.Abs(selectj - tilej) == 1 && changes.pieces.Contains(changes.BoardLayout[tilei, tilej])) { return true; }
                    else return false;

                }
                else
                {
                    if (selecti - tilei == -2 && selecti == 1 && changes.BoardLayout[tilei, tilej].Contains(' ') && changes.BoardLayout[tilei - 1, tilej].Contains(' ')&&selectj-tilej==0)
                    {
                        return true;
                    }
                    if (selecti - tilei == -1 && tilej == selectj && changes.BoardLayout[tilei, tilej].Contains(' ')) { return true; }
                    if (selecti - tilei == -1 && Math.Abs(selectj - tilej) == 1 && changes.pieces.Contains(changes.BoardLayout[tilei, tilej])) { return true; }
                    else return false;
                }


            }
            bool knightlogic(int[] selectindex, int[] tileindex)
            {
                int selecti = selectindex[0];
                int tilei = tileindex[0];

                int selectj = selectindex[1];
                int tilej = tileindex[1];

                char player = changes.BoardLayout[selecti, selectj][0];

                if (changes.BoardLayout[tilei, tilej] == changes.BoardLayout[tilei,tilej].ToUpper()&&  changes.BoardLayout[selecti, selectj] == changes.BoardLayout[selecti, selectj].ToUpper()&&!changes.BoardLayout[tilei, tilej].Contains(' ')) { return false; }
                if (changes.BoardLayout[tilei, tilej] == changes.BoardLayout[tilei, tilej].ToLower() && changes.BoardLayout[selecti, selectj] == changes.BoardLayout[selecti, selectj].ToLower() && !changes.BoardLayout[tilei, tilej].Contains(' ')) { return false; }

                if (Math.Abs(selecti - tilei) == Math.Abs(selectj - tilej)) 
                {
                    return false;
                }

                if(Math.Abs(selecti - tilei)!=1 && Math.Abs(selectj - tilej) != 1) { return false; }
                if (Math.Abs(selectj - tilej) != 2 && Math.Abs(selecti - tilei) != 2) { return false; }

                return true;


            }
            bool bishoplogic(int[] selectindex, int[] tileindex)
            {



                int selecti = selectindex[0];
                int tilei = tileindex[0];

                int selectj = selectindex[1];
                int tilej = tileindex[1];
                if (changes.BoardLayout[tilei, tilej] == changes.BoardLayout[tilei, tilej].ToUpper() && changes.BoardLayout[selecti, selectj] == changes.BoardLayout[selecti, selectj].ToUpper() && !changes.BoardLayout[tilei, tilej].Contains(' ')) { return false; }
                if (changes.BoardLayout[tilei, tilej] == changes.BoardLayout[tilei, tilej].ToLower() && changes.BoardLayout[selecti, selectj] == changes.BoardLayout[selecti, selectj].ToLower() && !changes.BoardLayout[tilei, tilej].Contains(' ')) { return false; }
                if (!(Math.Abs(selecti - tilei) == Math.Abs(selectj - tilej))) { return false; }
             

               
                    for (int i = selecti, j = selectj; j>0 ||j<7||i>0||i<7;)
                    {
                        if (i == tilei && j == tilej) { break; }

                        if (selecti > tilei) { i--; }
                        else { i++; }
                        if (selectj > tilej) { j--; }
                        else { j++; }

                        if (changes.pieces.Contains(changes.BoardLayout[i, j]) && changes.BoardLayout[i, j] != changes.BoardLayout[tilei, tilej]) { return false; }


                    }
                


                 return true;
                


            }
            bool rooklogic(int[] selectindex, int[] tileindex)
        
            {

                int selecti = selectindex[0];
                int tilei = tileindex[0];

                int selectj = selectindex[1];
                int tilej = tileindex[1];
                if (changes.BoardLayout[tilei, tilej] == changes.BoardLayout[tilei, tilej].ToUpper() && changes.BoardLayout[selecti, selectj] == changes.BoardLayout[selecti, selectj].ToUpper() && !changes.BoardLayout[tilei, tilej].Contains(' ')) { return false; }
                if (changes.BoardLayout[tilei, tilej] == changes.BoardLayout[tilei, tilej].ToLower() && changes.BoardLayout[selecti, selectj] == changes.BoardLayout[selecti, selectj].ToLower() && !changes.BoardLayout[tilei, tilej].Contains(' ')) { return false; }


                int diff = Math.Abs(selecti - tilei) - Math.Abs(selectj - tilej);


                if (Math.Abs(diff) != Math.Abs(selecti - tilei) && Math.Abs(diff) != Math.Abs(selectj - tilej)) { return false; }


                if(Math.Abs(selecti - tilei)!=0) {

                    for (int i = selecti, j = selectj; i > 0 || i < 7;)
                    {
                        if (i == tilei) { break; }

                        if (selecti > tilei) { i--; }
                        else { i++; }
                        

                        if (changes.pieces.Contains(changes.BoardLayout[i, j]) && changes.BoardLayout[i, tilej] != changes.BoardLayout[tilei, tilej]) { return false; }


                    }


                }
                if (Math.Abs(selectj - tilej) != 0)
                {

                    for (int i = selecti, j = selectj; j > 0 || j < 7;)
                    {
                        if (j == tilej) { break; }

                        if (selectj > tilej) { j--; }
                        else { j++; }


                        if (changes.pieces.Contains(changes.BoardLayout[i, j]) && changes.BoardLayout[tilei, j] != changes.BoardLayout[tilei, tilej]) { return false; }


                    }


                }






                return true;



            }
            bool queenlogic(int[] selectindex, int[] tileindex)
            {
                int selecti = selectindex[0];
                int tilei = tileindex[0];

                int selectj = selectindex[1];
                int tilej = tileindex[1];
                if (changes.BoardLayout[tilei, tilej] == changes.BoardLayout[tilei, tilej].ToUpper() && changes.BoardLayout[selecti, selectj] == changes.BoardLayout[selecti, selectj].ToUpper() && !changes.BoardLayout[tilei, tilej].Contains(' ')) { return false; }
                if (changes.BoardLayout[tilei, tilej] == changes.BoardLayout[tilei, tilej].ToLower() && changes.BoardLayout[selecti, selectj] == changes.BoardLayout[selecti, selectj].ToLower() && !changes.BoardLayout[tilei, tilej].Contains(' ')) { return false; }

                int diff = Math.Abs(selecti - tilei) - Math.Abs(selectj - tilej);

                if (!(Math.Abs(selecti - tilei) == Math.Abs(selectj - tilej))&& Math.Abs(diff) != Math.Abs(selecti - tilei) && Math.Abs(diff) != Math.Abs(selectj - tilej)) { return false; }


                if (Math.Abs(selecti - tilei) == Math.Abs(selectj - tilej))
                {

                    for (int i = selecti, j = selectj; j > 0 || j < 7 || i > 0 || i < 7;)
                    {
                        if (i == tilei && j == tilej) { break; }

                        if (selecti > tilei) { i--; }
                        else { i++; }
                        if (selectj > tilej) { j--; }
                        else { j++; }

                        if (changes.pieces.Contains(changes.BoardLayout[i, j]) && changes.BoardLayout[i, j] != changes.BoardLayout[tilei, tilej]) { return false; }


                    }



                    return true;



                }
                else if (Math.Abs(diff) == Math.Abs(selecti - tilei) || Math.Abs(diff) == Math.Abs(selectj - tilej))
                {


                    if (0 != Math.Abs(selecti - tilei) && 0 != Math.Abs(selectj - tilej)) { return false; }


                    if (Math.Abs(selecti - tilei) != 0)
                    {

                        for (int i = selecti, j = selectj; i > 0 || i < 7;)
                        {
                            if (i == tilei) { break; }

                            if (selecti > tilei) { i--; }
                            else { i++; }


                            if (changes.pieces.Contains(changes.BoardLayout[i, j]) && changes.BoardLayout[i, tilej] != changes.BoardLayout[tilei, tilej]) { return false; }


                        }


                    }
                    if (Math.Abs(selectj - tilej) != 0)
                    {

                        for (int i = selecti, j = selectj; j > 0 || j < 7;)
                        {
                            if (j == tilej) { break; }

                            if (selectj > tilej) { j--; }
                            else { j++; }


                            if (changes.pieces.Contains(changes.BoardLayout[i, j]) && changes.BoardLayout[tilei, j] != changes.BoardLayout[tilei, tilej]) { return false; }


                        }


                    }






                    return true;
                }
                else return false;


            }


            while (true)
            {
                if (changes.deadpieces.Contains("K1")|| changes.deadpieces.Contains("k1"))
                {
                    break;
                }
                Console.WriteLine();

                string select="";

                int[] address = indexselect(select);


                string tile;
                void pieceselect() {
                    while (true)
                    {
                        Console.WriteLine("select piece to move");

                        select = Console.ReadLine();
                        address = indexselect(select);
                        Console.SetCursorPosition(0, Console.CursorTop - 1);
                        ClearCurrentConsoleLine();
                        if (changes.turn % 2 == 0 && select.ToLower() == select) { Console.WriteLine("It's Green's turn, Select piece again. Green uses capital letters"); }
                        else if (changes.turn % 2 != 0 && select.ToUpper() == select) { Console.WriteLine("It's Blue's turn, Select piece again. Blue uses Lowercase letters"); }

                        else if (!changes.pieces.Contains(select)) { Console.WriteLine("Piece does not exist"); }

                        else if (changes.deadpieces.Contains(select)) { Console.WriteLine("Piece does not exist on the board"); }
                        else
                        {
                            break; }

                    }
                }

                pieceselect();

                tileselect();


                void tileselect()
                {
                    while (true)
                    {
                      
                       

                        Console.WriteLine($"Selected Piece: {select} \nPick a tile to move to or type 'BACK' to pick another piece");

                        tile = Console.ReadLine();
                        tile = tile.ToUpper();

                        int[] refadd = indextile(tile);

                        if(tile == "BACK") { pieceselect(); }
                        refadd = indextile(tile);


                        if (!AddressList.Contains(tile)) { Console.WriteLine("Please input correct tile address (Example: A5)"); }

                        else if (select.Contains('k') || select.Contains('K'))
                        {
                            if (!kinglogic(address, refadd))
                            {
                                Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("Invalid Move");
                                Console.ResetColor();
                            }
                            else { break; }
                        }
                        else if (select.Contains('q') || select.Contains('Q'))
                        {
                            if (!queenlogic(address, refadd))
                            {
                                Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("Invalid Move");
                                Console.ResetColor();
                            }
                            else { break; }
                        }

                        else if (select.Contains('P') || select.Contains('p'))
                        {
                            if (!pawnlogic(address, refadd))
                            {
                                Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("Invalid Move");
                                Console.ResetColor();
                            }
                            else { break; }
                        }
                        else if (select.Contains('n') || select.Contains('N'))
                        {
                            if (!knightlogic(address, refadd))
                            {
                                Console.WriteLine("Invalid Move");

                            }
                            else { break; }
                        }
                        else if (select.Contains('R') || select.Contains('r'))
                        {
                            if (!rooklogic(address, refadd))
                            {
                                Console.WriteLine("Invalid Move");

                            }
                            else { break; }
                        }
                        else if (select.Contains('B') || select.Contains('b'))
                        {
                            if (!bishoplogic(address, refadd))
                            {
                                Console.SetCursorPosition(0, Console.CursorTop - 1);
                                ClearCurrentConsoleLine();
                                Console.SetCursorPosition(0, Console.CursorTop - 1);
                                ClearCurrentConsoleLine();
                                Console.WriteLine("Invalid Move");

                            }
                            else { break; }
                        }

                    }
                }











                for (int i = 0; i <= 7; i++)
                    {

                        for (int j = 0; j <= 7; j++)
                        {


                         if (tile == BoardLayout[i, j]) { if (changes.BoardLayout[i, j] != "  ") { changes.deadpieces.Add(changes.BoardLayout[i, j]); } changes.BoardLayout[i, j] = select; }
                        else if (select == changes.BoardLayout[i, j]) { changes.BoardLayout[i, j] = "  "; }

                    }

                }                 //moves pieces



                if (changes.deadpieces.Contains(select)) { changes.deadpieces.Remove(select); } //needed this to fix some bug i forgot why
                changes.turn++;
                Console.Clear();
                changes.Print();
               










            }




















        }
    }
}

















/*
 * 16 white pieces
 * 16 black pieces
 * 64 tiles
 * 8 rows
 * 8 columns
 * movement logic
 * turns logic
 * Player 1 class and player 2 class?
 * 
*/