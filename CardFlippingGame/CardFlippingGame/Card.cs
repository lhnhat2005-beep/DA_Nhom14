using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace CardFlippingGame
{
    internal class Card
    {
        public string Picture { get; private set; } //MatTruoc

        public string BackPicture { get; set; } //MatSau
        public bool IsFlipped { get; private set; } //TrangThaiLat
        public bool IsMatched { get; set; } //TrangThaiGhep
        public Card(string picture,string backpicture)
        {
            Picture = picture;
            BackPicture = backpicture;
            IsMatched = false;
            IsFlipped = false;
        }
        public void FlipCard() // LatThe
        {
            IsFlipped = !IsFlipped;
        }
    }
}
