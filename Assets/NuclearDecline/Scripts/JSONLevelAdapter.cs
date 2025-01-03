using UnityEngine;

namespace NuclearDecline.Gameplay
{

    public class JSONLevelAdapter 
    {
        private readonly string jsonFileName = "levels";

        public LevelsStorage GenerateLevelsStorageFromJSON()
        {
            TextAsset jsonFile = Resources.Load<TextAsset>(jsonFileName);

            LevelsStorage levelsStorage = JsonUtility.FromJson<LevelsStorage>(jsonFile.text);

            return levelsStorage;
        }
    }

    [System.Serializable]
    public class LevelsStorage
    {
        public LevelInfo[] Levels; 
    }

    [System.Serializable]
    public class LevelInfo
    {
        public HolderInfo[] HolderInfo; 
    }

    [System.Serializable]
    public class HolderInfo
    {
        public ItemInfo[] ItemsInfo;
    }

    [System.Serializable]
    public class ItemInfo
    {
        public int Type; 
    }
}