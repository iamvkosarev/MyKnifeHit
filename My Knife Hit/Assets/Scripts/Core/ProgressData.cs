namespace KnifeHit.Core 
{

    [System.Serializable]
    public class ProgressData
    {
        public int recordLevelsPassed;
        public int resultingNumOfApples;
        

        public ProgressData(ProgressData progressData)
        {
            this.recordLevelsPassed = progressData.recordLevelsPassed;
            this.resultingNumOfApples = progressData.resultingNumOfApples;
        }

        public ProgressData()
        {
            this.recordLevelsPassed = 0;
            this.resultingNumOfApples = 0;
        }
    }
}
