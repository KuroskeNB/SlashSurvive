using UnityEngine;
using System.IO;
using UnityEngine.AddressableAssets;

[CreateAssetMenu(fileName = "RewardData", menuName = "Data/RewardData")]
public class RewardData : DataSaver
{
    public RewardInfo[] rewardsData;
    override public void Save()
    {
        string path = Application.persistentDataPath + "/RewardData.json";
         string json = JsonUtility.ToJson(this);
        File.WriteAllText(path, json);
    }
    override public void Load()
    {
        string path = Application.persistentDataPath + "/RewardData.json";
    string streamingAssetsPath = Path.Combine(Application.streamingAssetsPath, "RewardData.json");
     if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            JsonUtility.FromJsonOverwrite(json, this);
        }
        else if(File.Exists(streamingAssetsPath))
        {
           File.Copy(streamingAssetsPath, path);
            string json = File.ReadAllText(path);
         JsonUtility.FromJsonOverwrite(json, this);
        }
    }

    override public void SaveForBuild()
    {
        string sourcePath  = Application.persistentDataPath + "/RewardData.json";
        string targetPath = Path.Combine(Application.streamingAssetsPath, "RewardData.json");
        if (File.Exists(sourcePath))
        {
            if (!Directory.Exists(Path.GetDirectoryName(targetPath)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(targetPath));
            }
            File.Copy(sourcePath, targetPath, true);
            Debug.Log($"Файл скопирован из {sourcePath} в {targetPath}");
            
        }
    }

}

[System.Serializable]
public class RewardInfo
{
    public int ScoreRequirements =0;
    public AssetReference spriteReference;
    public Sprite RewardImage;
    
    public ERewardType rewardType;
    public string rewardName;
    public int rewardCount;
    public bool bIsUnlocked=false;
}

public enum ERewardType
{
    Skin,
    Weapon,
    Effect,
    SilverCoins,
    GoldCoins
}