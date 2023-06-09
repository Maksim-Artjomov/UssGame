using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;
using System.Xml.Linq;

namespace UssGame
{
    public class Player
    {
        public string Name { get; set; }
        public int Score { get; set; }

        public void Draw(int xOffset, int yOffset)
        {
            Console.SetCursorPosition(xOffset, yOffset);
            Console.Write("Player: " + Name);

            Console.SetCursorPosition(xOffset, yOffset + 1);
            Console.Write("Score: " + Score);
        }
    }
}
