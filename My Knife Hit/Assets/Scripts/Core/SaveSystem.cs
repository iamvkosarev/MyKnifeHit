using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;


namespace KnifeHit.Core
{
    public static class SaveSystem
    {
        #region Progress Data

        public static void SaveProgress(ProgressData progressData)
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();

            if (!Directory.Exists(Application.persistentDataPath + "/saves"))
            {
                Directory.CreateDirectory(Application.persistentDataPath + "/saves");
            }

            string path = Application.persistentDataPath + "/saves/" + "progress.save";

            FileStream stream = new FileStream(path, FileMode.Create);

            ProgressData data = new ProgressData(progressData);

            binaryFormatter.Serialize(stream, data);

            stream.Close();
        }

        public static ProgressData LoadProgress()
        {
            string path = Application.persistentDataPath + "/saves/" + "progress.save";
            if (File.Exists(path))
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                FileStream stream = new FileStream(path, FileMode.Open);
                ProgressData data = binaryFormatter.Deserialize(stream) as ProgressData;

                stream.Close();

                return data;
            }
            else
            {
                ProgressData progressData = new ProgressData();
                SaveProgress(progressData);
                return progressData;
            }
        }

        #endregion
    }
}