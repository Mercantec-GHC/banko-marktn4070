using System;
using System.Collections.Generic;
using System.Numerics;
using System.Threading;

namespace BankoCheater
{
    class Program
    {
        static void Main()
        {
            Console.Clear();

            // Faste navne og plader til spillere
            string[] names = { "LukasB", "Mikkel", "Jonas", "Mathias", "Bente", "Jacob", "Bob", "Peter", "Ole", "Margrethe", "Ella", "Freja", "William", "Karl", "Frida", "Luna", "Emil", "Oscar", "Ida", "Oliver" };
            Dictionary<string, int[,]> player_boards = new Dictionary<string, int[,]>();

            int count_names = 0;

            foreach (string name in names)
            {
                count_names++;
                // Genererer boardet for den nuværende spiller
                int[,] board = Generate_board(name);
                player_boards[name] = board;

                Console.WriteLine($"\n{count_names} - {name}'s spilleplade:");
                Print_board(board);
            }

            while (true)
            {
                Console.Write("\nIndtast et tal mellem 1 og 90 for at krydse det af (eller skriv 'exit' for at afslutte): ");
                string board_input = Console.ReadLine();

                if (board_input.ToLower() == "exit")
                {
                    Console.WriteLine("Programmet afsluttes.");
                    Thread.Sleep(3000);
                    break;
                }
                else if (int.TryParse(board_input, out int drawn_number) && drawn_number >= 1 && drawn_number <= 90)
                {
                    count_names = 0;

                    foreach (var player in player_boards.Keys)
                    {
                        count_names++;
                        Mark_drawn_number(player_boards[player], drawn_number);
                        Console.WriteLine($"\n{count_names} - {player}'s spilleplade efter at der er trukket nr. {drawn_number}:");
                        Print_board(player_boards[player]);
                    }
                    Console.WriteLine("\nSpillere med en række");

                    bool found_row_marked = false; // Flag til at holde styr på, om vi har fundet nogen
                    int player_count = 1; // Tæller for spillerens nummer

                    foreach (var player in player_boards.Keys)
                    {
                        // Tjek hvor mange rækker spilleren har fyldt
                        int marked_rows = Count_marked_rows(player_boards[player]);

                        if (marked_rows == 1)
                        {
                            found_row_marked = true; // Sæt flag til true, hvis vi finder en spiller med en fyldt række
                            Console.WriteLine($"Spiller nr. {player_count} ({player}) har fyldt én række!");
                        }
                        else if (marked_rows == 2)
                        {
                            found_row_marked = true; // Sæt flag til true, hvis vi finder en spiller med to fyldte rækker
                            Console.WriteLine($"Spiller nr. {player_count} ({player}) har fyldt to rækker!");
                        }
                        else if (marked_rows >= 3)
                        {
                            found_row_marked = true; // Sæt flag til true, hvis vi finder en spiller med tre fyldte rækker
                            Console.WriteLine($"Spiller nr. {player_count} ({player}) har fuld plade!");
                        }
                        player_count++; // Øg tælleren for næste spiller
                    }

                    if (!found_row_marked)
                    {
                        Console.WriteLine("Ingen spillere har fyldt nogen rækker endnu.");
                    }
                }
                else
                {
                    Console.WriteLine("Det indtastede er ikke et gyldigt heltal!");
                }
            }
        }

        static int Count_marked_rows(int[,] board)
        {
            int marked_row_count = 0; // Tæller for fyldte rækker
            for (int row = 0; row < board.GetLength(0); row++)
            {
                bool is_row_marked = true;

                for (int col = 0; col < board.GetLength(1); col++)
                {
                    if (board[row, col] != -1) // Hvis et tal ikke er markeret, er rækken ikke fyldt
                    {
                        is_row_marked = false; // Rækken er ikke fyldt
                        break; // Gå til næste række
                    }
                }

                if (is_row_marked)
                {
                    marked_row_count++; // Tæl en fyldt række
                }
            }

            return marked_row_count; // Returner antallet af fyldte rækker
        }

        static int[,] Generate_board(string name)
        {
            int[] række_1;
            int[] række_2;
            int[] række_3;

            if (name == "LukasB")
            {
                række_1 = new int[] { 40, 50, 62, 70, 80 };
                række_2 = new int[] { 7, 22, 36, 44, 52 };
                række_3 = new int[] { 8, 19, 38, 54, 66 };
            }
            else if (name == "Mikkel")
            {
                række_1 = new int[] { 12, 44, 60, 70, 81 };
                række_2 = new int[] { 4, 37, 45, 55, 72 };
                række_3 = new int[] { 6, 28, 48, 58, 65 };
            }
            else if (name == "Jonas")
            {
                række_1 = new int[] { 2, 41, 51, 70, 87 };
                række_2 = new int[] { 6, 26, 31, 52, 88 };
                række_3 = new int[] { 8, 17, 29, 65, 74 };
            }
            else if (name == "Mathias")
            {
                række_1 = new int[] { 2, 42, 62, 71, 83 };
                række_2 = new int[] { 6, 23, 35, 68, 88 };
                række_3 = new int[] { 7, 18, 24, 55, 69 };
            }
            else if (name == "Bente")
            {
                række_1 = new int[] { 1, 11, 23, 30, 81 };
                række_2 = new int[] { 14, 26, 31, 74, 85 };
                række_3 = new int[] { 4, 34, 46, 59, 68 };
            }
            else if (name == "Jacob")
            {
                række_1 = new int[] { 2, 21, 30, 74, 82 };
                række_2 = new int[] { 22, 32, 43, 56, 67 };
                række_3 = new int[] { 14, 28, 38, 77, 89 };
            }
            else if (name == "Bob")
            {
                række_1 = new int[] { 6, 10, 24, 32, 40 };
                række_2 = new int[] { 7, 35, 41, 56, 73 };
                række_3 = new int[] { 29, 39, 48, 68, 89 };
            }
            else if (name == "Peter")
            {
                række_1 = new int[] { 13, 32, 41, 51, 85 };
                række_2 = new int[] { 15, 22, 42, 74, 86 };
                række_3 = new int[] { 9, 28, 37, 57, 68 };
            }
            else if (name == "Ole")
            {
                række_1 = new int[] { 1, 15, 22, 42, 80 };
                række_2 = new int[] { 6, 33, 43, 72, 84 };
                række_3 = new int[] { 19, 28, 47, 58, 68 };
            }
            else if (name == "Margrethe")
            {
                række_1 = new int[] { 1, 22, 33, 40, 84 };
                række_2 = new int[] { 16, 34, 52, 67, 87 };
                række_3 = new int[] { 6, 28, 59, 68, 79 };
            }
            else if (name == "Ella")
            {
                række_1 = new int[] { 14, 21, 33, 40, 81 };
                række_2 = new int[] { 8, 16, 46, 56, 63 };
                række_3 = new int[] { 9, 18, 39, 58, 77 };
            }
            else if (name == "Freja")
            {
                række_1 = new int[] { 4, 23, 34, 41, 52 };
                række_2 = new int[] { 5, 12, 36, 55, 74 };
                række_3 = new int[] { 38, 48, 58, 66, 89 };
            }
            else if (name == "William")
            {
                række_1 = new int[] { 13, 33, 40, 72, 82 };
                række_2 = new int[] { 2, 18, 35, 53, 83 };
                række_3 = new int[] { 5, 29, 66, 79, 89 };
            }
            else if (name == "Karl")
            {
                række_1 = new int[] { 11, 31, 46, 54, 82 };
                række_2 = new int[] { 5, 47, 55, 63, 84 };
                række_3 = new int[] { 8, 29, 48, 77, 89 };
            }
            else if (name == "Frida")
            {
                række_1 = new int[] { 11, 46, 51, 63, 84 };
                række_2 = new int[] { 6, 24, 47, 55, 67 };
                række_3 = new int[] { 16, 28, 35, 69, 76 };
            }
            else if (name == "Luna")
            {
                række_1 = new int[] { 2, 23, 54, 64, 71 };
                række_2 = new int[] { 5, 18, 41, 56, 67 };
                række_3 = new int[] { 9, 39, 59, 69, 90 };
            }
            else if (name == "Emil")
            {
                række_1 = new int[] { 1, 34, 42, 65, 80 };
                række_2 = new int[] { 15, 26, 54, 66, 81 };
                række_3 = new int[] { 9, 17, 45, 68, 75 };
            }
            else if (name == "Oscar")
            {
                række_1 = new int[] { 2, 10, 40, 50, 84 };
                række_2 = new int[] { 3, 13, 24, 65, 85 };
                række_3 = new int[] { 5, 39, 47, 77, 90 };
            }
            else if (name == "Ida")
            {
                række_1 = new int[] { 23, 30, 45, 51, 71 };
                række_2 = new int[] { 5, 13, 35, 46, 74 };
                række_3 = new int[] { 7, 38, 68, 77, 90 };
            }
            else // Oliver
            {
                række_1 = new int[] { 15, 50, 63, 73, 80 };
                række_2 = new int[] { 8, 25, 32, 47, 65 };
                række_3 = new int[] { 36, 49, 54, 77, 84 };
            }

            // Opretter en todimensional array og fylder den med værdierne fra rækkerne
            int[,] board = new int[3, 5];

            for (int col = 0; col < 5; col++)
            {
                board[0, col] = række_1[col];
                board[1, col] = række_2[col];
                board[2, col] = række_3[col];
            }

            return board;
        }

        static void Print_board(int[,] board)
        {
            for (int row = 0; row < board.GetLength(0); row++)
            {
                for (int col = 0; col < board.GetLength(1); col++)
                {
                    if (board[row, col] == -1)
                    {
                        Console.Write("X  "); // Udskriver X i stedet for -1
                    }
                    else if (board[row, col] >= 1 && board[row, col] <= 9)
                    {
                        Console.Write("0" + board[row, col] + " ");
                    }
                    else
                    {
                        Console.Write(board[row, col] + " ");
                    }
                }
                Console.WriteLine();
            }
        }

        static void Mark_drawn_number(int[,] board, int number)
        {
            for (int row = 0; row < board.GetLength(0); row++)
            {
                for (int col = 0; col < board.GetLength(1); col++)
                {
                    if (board[row, col] == number)
                    {
                        board[row, col] = -1;  // -1 for hver tal som er trukket
                    }
                }
            }
        }
    }
}
