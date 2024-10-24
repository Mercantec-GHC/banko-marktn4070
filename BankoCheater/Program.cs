using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace BankoCheater
{
    class Program
    {
        static void Main()
        {
            Console.Clear();
            string input;
            string[] names = { "Sophia", "Andrew", "Emma", "Logan" };

            // Opret en dictionary til at gemme spillerens plader
            Dictionary<string, int[,]> player_boards = new Dictionary<string, int[,]>();

            // Generer plader for hver spiller
            foreach (string name in names)
            {
                player_boards[name] = GenerateNumbers();
                Console.WriteLine("\n" + name + "'s spilleplade:");
                PrintBoard(player_boards[name]);
            }

            // Spilleren kan indtaste numre for at "markere" dem
            while (true)
            {
                Console.Write("\n" + "Indtast et tal mellem 1 0g 90 for at krydse det af (eller skriv 'exit' for at afslutte): ");
                input = Console.ReadLine();

                // Tjek om det indtastede er et tal 
                if (Regex.IsMatch(input.ToLower(), @"^\d+$")) // Matcher kun heltal
                {
                    int drawn_number = Convert.ToInt32(input);

                    if (drawn_number < 1 || drawn_number > 90)
                    {
                        Console.WriteLine("Det indtastede tal er ikke imellem 1 og 90!");
                        continue; // Genstart loopet
                    }
                    else
                    {
                        // Opdater hver plade, hvis nummeret findes
                        foreach (var player_board in player_boards.Keys)
                        {
                            MarkDrawnNumber(player_boards[player_board], drawn_number);
                            Console.WriteLine("\n" + player_board + "'s spilleplade efter at der er trukket nr. " + drawn_number);
                            PrintBoard(player_boards[player_board]);
                        }
                    }
                }
                // Tjek om brugeren vil afslutte programmet
                else if (input.ToLower() == "exit")
                {
                    Console.WriteLine("Programmet afsluttes.");

                    Thread.Sleep(3000); // Vent 3 sekunder

                    break; // Afslut løkken og programmet
                }
                // Tjek om det indtastede ikke er et tal 
                else
                {
                    Console.WriteLine("Det indtastede er ikke et heltal!");
                    continue; // Genstart loopet
                }
            }
        }

        static int[,] GenerateNumbers()
        {
            Random randNum = new Random();
            int[,] numbers = new int[3, 9]; // 3 rækker og 9 kolonner

            for (int i = 0; i < 9; i++)
            {
                // Bestem interval for hver kolonne
                int Min = 1 + i * 10; // Min for hver kolonne
                int Max = Min + 9;    // Max for hver kolonne

                List<int> columnNumbers = new List<int>();

                while (columnNumbers.Count < 3) // Sørg for at generere 3 unikke numre
                {
                    int number = randNum.Next(Min, Max + 1); // Generer et tal inden for intervallet
                    if (!columnNumbers.Contains(number)) // Undgå duplikerede numre
                    {
                        columnNumbers.Add(number); // Tilføj nummeret til listen
                    }
                }

                // Tildel værdierne fra listen til den rigtige kolonne i arrayet
                for (int j = 0; j < 3; j++)
                {
                    numbers[j, i] = columnNumbers[j];
                }
            }

            // Sorter kolonnerne i stigende rækkefølge
            for (int i = 0; i < 9; i++)
            {
                int[] column = { numbers[0, i], numbers[1, i], numbers[2, i] }; // Hent kolonne
                Array.Sort(column); // Sorter i stigende rækkefølge

                // Gem den sorterede kolonne tilbage i arrayet
                for (int j = 0; j < 3; j++)
                {
                    numbers[j, i] = column[j]; // Tildel de sorterede tal
                }
            }

            // Erstat 4 tilfældige tal med "Blank" (-1)
            RemoveNumbers(numbers);

            return numbers; // Returner den genererede 2D-array
        }

        static void RemoveNumbers(int[,] numbers)
        {
            Random rand = new Random();
            int[] columnCount = new int[9]; // Array til at tælle, hvor mange tal der er tilbage i hver kolonne

            // Sæt alle tællere til 3, fordi vi starter med 3 tal pr. kolonne
            for (int i = 0; i < 9; i++)
            {
                columnCount[i] = 3;
            }

            // Gå igennem hver række for at erstatte 4 tilfældige tal med "Blank"
            for (int row = 0; row < 3; row++)
            {
                // Opret en liste med alle kolonneindekser
                List<int> availableColumns = new List<int>(Enumerable.Range(0, 9));

                int removedCount = 0;

                while (removedCount < 4)
                {
                    int randomIndex = rand.Next(availableColumns.Count); // Vælg en tilfældig kolonne

                    int col = availableColumns[randomIndex];

                    // Fjern kun et tal fra kolonnen, hvis der er flere end ét tal tilbage
                    if (columnCount[col] > 1)
                    {
                        numbers[row, col] = -1; // Erstat tallet med -1 (som repræsenterer "Blank")
                        columnCount[col]--; // Reducer antallet af tal i den valgte kolonne
                        removedCount++;
                    }

                    availableColumns.RemoveAt(randomIndex); // Fjern kolonnen fra listen efter fjernelse
                }
            }
        }

        static void PrintBoard(int[,] board)
        {
            for (int i = 0; i < 3; i++) // For hver række
            {
                for (int j = 0; j < 9; j++) // For hver kolonne
                {
                    if (board[i, j] == -1)
                    {
                        Console.Write("-\t"); // Udskriv "-" for "Blank"
                    }
                    else if (board[i, j] == -2)
                    {
                        Console.Write("X\t"); // Udskriv "-" for "Blank"
                    }
                    else
                    {
                        Console.Write(board[i, j].ToString() + "\t"); // Udskriv tallet
                    }
                }
                Console.WriteLine();
            }
        }

        static void MarkDrawnNumber(int[,] board, int drawn_number)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (board[i, j] == drawn_number)
                    {
                        board[i, j] = -2; // Erstat tallet med -1 (som repræsenterer "X")
                    }
                }
            }
        }
    }
}
