using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardFlippingGame
{
    [Serializable]
    internal class GameState
    {
        [Serializable]
        public class CardData
        {
            public string Picture{get;set;}
            public bool Isflipped {get;set;}
            public bool isMatched { get;set;}
        }
        public int SoDongBang { get;set;}
        public int SoCotBang {  get;set;}
        public int SoCoHoi {  get;set;}

        public List<CardData> CardDataList;
    }
}
