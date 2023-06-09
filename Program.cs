using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace UssGame
{
    class Program
    {
        static ConsoleColor snakeColor = ConsoleColor.Green;

        static void Main(string[] args)
        {
            snakeColor = Menu();

            Console.Write("Enter your name: ");
            string n = Console.ReadLine();

            Sound gameOverSound = new Sound("../../../pomer.mp3");
            Sound BGSound = new Sound("../../../fon.mp3");
            BGSound.SetVolume(0.1f);
            BGSound.Play();

            List<Point> poisons = new List<Point>();
            List<Point> mons = new List<Point>();

            while (n.Length < 1)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Please, enter your name.");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write("Enter your name: ");
                n = Console.ReadLine();
            }
            Player player = new Player { Name = n, Score = 0 };

            Console.Clear();

            Console.SetWindowSize(80, 25);

            Walls walls = new Walls(80, 25);
            walls.Draw();

            int speed = 90;

            Point p = new Point(4, 5, '█');
            Snake snake = new Snake(p, 4, Direction.RIGHT);
            Console.ForegroundColor = snakeColor;
            snake.Draw();

            FoodCreator foodCreator = new FoodCreator(80, 25, 'x');
            Console.ForegroundColor = ConsoleColor.Magenta;
            Point food = foodCreator.CreateP();
            food.Draw();

            FoodCreator poisonCreator = new FoodCreator(80, 25, '─');
            Console.ForegroundColor = ConsoleColor.White;
            Point poison = poisonCreator.CreateP();
            poison.Draw();

            FoodCreator monCreator = new FoodCreator(80, 25, '$');
            Console.ForegroundColor = ConsoleColor.Yellow;
            Point mon = monCreator.CreateP();
            mon.Draw();

            poisons.Add(poison);
            mons.Add(mon);

            while (true)
            {
                if (walls.IsHit(snake) || snake.IsHitTail())
                {
                    using (StreamWriter writer = new StreamWriter("../../../Scores.txt", true))
                    {
                        writer.WriteLine(player.Name + ": " + player.Score);
                    }
                    break;
                }
                else if (snake.Poisoned(poisons))
                {
                    speed += 10; ;
                }
                else if (snake.Eat(food))
                {
                    Sound nyam = new Sound("../../../nyam.mp3");
                    nyam.SetVolume(0.2f);
                    nyam.Play();

                    Console.ForegroundColor = ConsoleColor.Magenta;
                    food = foodCreator.CreateP();
                    food.Draw();

                    Console.ForegroundColor = ConsoleColor.White;
                    poisonCreator = new FoodCreator(80, 25, '─');
                    poison = poisonCreator.CreateP();
                    poison.Draw();

                    Console.ForegroundColor = ConsoleColor.Yellow;
                    monCreator = new FoodCreator(80, 25, '$');
                    mon = monCreator.CreateP();
                    mon.Draw();

                    poisons.Add(poison);
                    mons.Add(mon);

                    player.Score++;

                    speed = Speed(player.Score, speed);
                }
                else if (snake.Eat(mon))
                {
                    break;
                }
                else
                {
                    Console.ForegroundColor = snakeColor;
                    snake.Move();
                }
                Thread.Sleep(speed);

                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo key = Console.ReadKey();
                    snake.HandleKey(key.Key);
                }

                // Добавлено: отображение текущей скорости
                Console.SetCursorPosition(0, 0);
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("Current Speed: " + speed);
            }

            BGSound.Stop();
            gameOverSound.Play();
            gameOverSound.SetVolume(0.2f);
            WriteGameOver();
            Console.ReadLine();
        }

        static void WriteGameOver()
        {
            int xOffset = 25;
            int yOffset = 8;
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(xOffset, yOffset++);
            WriteText("************************", xOffset, yOffset++);
            WriteText("Try again!", xOffset + 9, yOffset++);
            yOffset++;
            WriteText("Author: Maksim Artemov", xOffset, yOffset++);
            WriteText("************************", xOffset, yOffset++);
        }

        static void WriteText(string text, int xOffset, int yOffset)
        {
            Console.SetCursorPosition(xOffset, yOffset);
            Console.WriteLine(text);
        }

        static ConsoleColor Menu()
        {
            Console.ForegroundColor = ConsoleColor.White;
            while (true)
            {
                Console.WriteLine("----------\n1 - Play\n2 - Top players\n3 - Snake colour\n----------");
                int v = 0;
                v = int.Parse(Console.ReadLine());

                if (v == 1 || v == 0)
                {
                    break;
                }
                else if (v == 2)
                {
                    string[] lines = File.ReadAllLines("../../../Scores.txt");
                    foreach (string line in lines)
                    {
                        Console.WriteLine(line);
                    }
                }
                else if (v == 3)
                {
                    snakeColor = ChooseSnakeColor();
                }
            }
            return snakeColor;
        }

        static ConsoleColor ChooseSnakeColor()
        {
            int varv;
            ConsoleColor snakeColor;

            while (true)
            {
                Console.WriteLine("Choose snake colour:\n1 - Magenta:\n2 - Yellow:\n3 - White:\n4 - Blue:\n5 - Green");
                varv = int.Parse(Console.ReadLine());

                if (varv >= 1 && varv <= 5)
                {
                    break;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid input. Please try again.");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }

            switch (varv)
            {
                case 1:
                    snakeColor = ConsoleColor.Magenta;
                    break;
                case 2:
                    snakeColor = ConsoleColor.Yellow;
                    break;
                case 3:
                    snakeColor = ConsoleColor.White;
                    break;
                case 4:
                    snakeColor = ConsoleColor.Blue;
                    break;
                case 5:
                    snakeColor = ConsoleColor.Green;
                    break;
                default:
                    snakeColor = ConsoleColor.Magenta;
                    break;
            }

            return snakeColor;
        }

        static int Speed(int score, int speed)
        {
            if (score == 2)
            {
                speed -= 15;
            }
            else if (score == 4)
            {
                speed -= 15;
            }
            else if (score == 6)
            {
                speed -= 15;
            }
            else if (score == 7)
            {
                speed -= 15;
            }
            return speed;
        }
    }
}