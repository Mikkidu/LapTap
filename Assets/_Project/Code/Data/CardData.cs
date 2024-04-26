using System;

namespace AlexDev.LapTap
{
    [Serializable]
    public class CardData
    {
        public int id;
        public bool isOpen = false;
        public bool isDone = false;

        public CardData(int id)
        {
            this.id = id;
        }
    }
}
