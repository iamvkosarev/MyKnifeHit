namespace KnifeHit.Core {

    [System.Serializable]
    public class ProgressData
    {
        public int recordScroreOfKnives;
        public int resultingNumOfApples;

        public ProgressData(ProgressData progressData)
        {
            this.recordScroreOfKnives = progressData.recordScroreOfKnives;
            this.resultingNumOfApples = progressData.resultingNumOfApples;
        }

        public ProgressData()
        {
            this.recordScroreOfKnives = 0;
            this.resultingNumOfApples = 0;
        }
    }
}
