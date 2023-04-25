using System;

namespace SaveSystem
{
    [Serializable]
    public class ProgressData
    {
        public int Coins;
        public int Level;

        public ProgressData(Progress progress)
        {
            Coins = progress.Coins;
            Level = progress.Level;
        }
    }
}