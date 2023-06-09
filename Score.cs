using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UssGame
{
    class Score
    {
        private int score;
        private List<int> scoreList;

        //public ScoreMng()
        //{
        //    score = 0;
        //    scoreList = new List<int>();
        //    ReadFile();
        //}

        public void AddScore(int p)
        {
            score += p;
        }

        public void SaveScore()
        {
            scoreList.Add(score);
            scoreList.Sort();
            scoreList.Reverse();
            using (StreamWriter to_file = new StreamWriter("scores.txt"))
            {
                foreach (int score in scoreList)
                {
                    to_file.WriteLine(score);
                }
            }
        }

        private void ReadFile()
        {
            using (StreamReader from_file = new StreamReader("Scores.txt"))
            {
                string line = from_file.ReadLine();
                if (int.TryParse(line, out int score))
                    {
                        scoreList.Add(score);
                    }
            }
        }
    }
}

