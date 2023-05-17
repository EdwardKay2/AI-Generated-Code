using System;
using System.Collections.Generic;

class ScrabbleGame
{
    static Dictionary<char, int> letterScores = new Dictionary<char, int>()
    {
        {'A', 1}, {'B', 3}, {'C', 3}, {'D', 2}, {'E', 1}, {'F', 4}, {'G', 2}, {'H', 4},
        {'I', 1}, {'J', 8}, {'K', 5}, {'L', 1}, {'M', 3}, {'N', 1}, {'O', 1}, {'P', 3},
        {'Q', 10}, {'R', 1}, {'S', 1}, {'T', 1}, {'U', 1}, {'V', 4}, {'W', 4}, {'X', 8},
        {'Y', 4}, {'Z', 10}
    };

    static char[,] board = new char[15, 15]; // 15x15 game board
    static bool[,] occupied = new bool[15, 15]; // keeps track of occupied positions

    static void InitializeBoard()
    {
        for (int row = 0; row < 15; row++)
        {
            for (int col = 0; col < 15; col++)
            {
                board[row, col] = '-';
                occupied[row, col] = false;
            }
        }
    }

    static void PrintBoard()
    {
        Console.WriteLine("  0 1 2 3 4 5 6 7 8 9 10 11 12 13 14");
        for (int row = 0; row < 15; row++)
        {
            Console.Write(row.ToString("D2") + " ");
            for (int col = 0; col < 15; col++)
            {
                Console.Write(board[row, col] + " ");
            }
            Console.WriteLine();
        }
    }

    static int CalculateScore(string word)
    {
        int score = 0;
        foreach (char letter in word.ToUpper())
        {
            if (letterScores.ContainsKey(letter))
            {
                score += letterScores[letter];
            }
        }
        return score;
    }

    static void PlaceWordOnBoard(string word, int startRow, int startCol, bool isHorizontal)
    {
        int row = startRow;
        int col = startCol;

        foreach (char letter in word.ToUpper())
        {
            board[row, col] = letter;
            occupied[row, col] = true;

            if (isHorizontal)
            {
                col++;
            }
            else
            {
                row++;
            }
        }
    }

    static bool IsValidMove(string word, int startRow, int startCol, bool isHorizontal)
    {
        int row = startRow;
        int col = startCol;

        foreach (char letter in word.ToUpper())
        {
            if (row >= 15 || col >= 15 || occupied[row, col])
            {
                return false;
            }

            if (isHorizontal)
            {
                col++;
            }
            else
            {
                row++;
            }
        }

        return true;
    }

    static void Main()
    {
        Console.WriteLine("Welcome to Scrabble!");

        InitializeBoard();

        string[] players = { "Player 1", "Player 2" };
        int currentPlayer = 0;

        while (true)
        {
            Console.WriteLine("\n" + players[currentPlayer] + ", it's your turn.");
            Console.WriteLine("Enter a word to place on the board (or 'exit' to quit):");
            string input = Console.ReadLine();

            if (input.ToLower() == "exit")
            {
                break;
            }

            Console.WriteLine("Enter the starting row coordinate (0-14):");
            int startRow = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter the starting column coordinate (0-14):");
            int startCol = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter the direction (0 for horizontal, 1 for vertical):");
            bool isHorizontal = int.Parse(Console.ReadLine()) == 0;

            if (IsValidMove(input, startRow, startCol, isHorizontal))
            {
                int score = CalculateScore(input);
                PlaceWordOnBoard(input, startRow, startCol, isHorizontal);
                PrintBoard();
                Console.WriteLine("Score for '{0}': {1}", input, score);

                currentPlayer = (currentPlayer + 1) % 2; // Switch to the next player
            }
            else
            {
                Console.WriteLine("Invalid move. Try again.");
            }
        }
    }
}