using System;
using System.IO;

namespace AdventOfCode
{
	class Program
	{
        const int ROCK_SCORE = 1; // A & X
        const int PAPER_SCORE = 2; // B & Y
        const int SCISSORS_SCORE = 3; // C & Z

        const int DRAW_SCORE = 3;
        const int WIN_SCORE = 6;

        // rock > scissors 
        // scissors > paper
        // paper > rock

        static void Main(string[] args)
		{
            var lines = File.ReadAllLines(@"aoc.txt");

            var score1 = 0;
            var score2 = 0;

            foreach(var line in lines)
            {
                var team1 = line[0];
                var team2 = line[2];

                var newScores = GetPlayerScores(team1, team2, new Tuple<int, int>(score1, score2));
                score1 = newScores.Item1;
                score2 = newScores.Item2;
            }

            Console.WriteLine(score2);
        }

        private static Tuple<int, int> GetPlayerScores(char team1, char team2, Tuple<int, int> currentScores)
        {
            Tuple<int, int> newScores = null;

            if (team1 == 'A') //rock
            {
                if (team2 == 'X') newScores = new Tuple<int, int>(DRAW_SCORE + ROCK_SCORE, DRAW_SCORE + ROCK_SCORE); // rock
                else if (team2 == 'Y') newScores = new Tuple<int, int>(ROCK_SCORE, WIN_SCORE + PAPER_SCORE); // paper
                else newScores = new Tuple<int, int>(WIN_SCORE + ROCK_SCORE, SCISSORS_SCORE); // scissors
            }
            else if (team1 == 'B') // paper
            {
                if (team2 == 'Y') newScores = new Tuple<int, int>(DRAW_SCORE + PAPER_SCORE, DRAW_SCORE + PAPER_SCORE);
                else if (team2 == 'Z') newScores = new Tuple<int, int>(PAPER_SCORE, WIN_SCORE + SCISSORS_SCORE);
                else newScores = new Tuple<int, int>(WIN_SCORE + PAPER_SCORE, ROCK_SCORE);
            }
            else // scissors
            {
                if (team2 == 'Z') newScores = new Tuple<int, int>(DRAW_SCORE + SCISSORS_SCORE, DRAW_SCORE + SCISSORS_SCORE);
                else if (team2 == 'X') newScores = new Tuple<int, int>(SCISSORS_SCORE, WIN_SCORE + ROCK_SCORE);
                else newScores = new Tuple<int, int>(WIN_SCORE + SCISSORS_SCORE, PAPER_SCORE);
            }

            return new Tuple<int, int>(newScores.Item1 + currentScores.Item1, newScores.Item2 + currentScores.Item2);
        }
    }
}
