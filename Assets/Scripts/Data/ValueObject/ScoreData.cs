using System;

namespace Data.ValueObject
{
    [Serializable]

    public class ScoreData
    {
        
        public int StackValue=0;
        public float IncomeValue=1;
        public float IncomeMultiplier=0.05f;
        public float TotalGameScore=0;
        public float TotalLevelScore=0;
        public float StackScore=0;
        public float AtmScore=0;

        }
}