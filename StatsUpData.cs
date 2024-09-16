using UnityEngine;
using System.IO;
using UnityEngine.AddressableAssets;

[CreateAssetMenu(fileName = "StatsUpData", menuName = "Data/StatsUpData")]
public class StatsUpData : DataSaver
{
    public StatUpData[] stats;
    override public void Save()
    {
        string path = Application.persistentDataPath + "/StatsUpData.json";
         string json = JsonUtility.ToJson(this);
        File.WriteAllText(path, json);
    }
    override public void Load()
    {
        string path = Application.persistentDataPath + "/StatsUpData.json";
     string streamingAssetsPath = Path.Combine(Application.streamingAssetsPath, "StatsUpData.json");
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
    public void ResetPrices()
    {
        foreach(StatUpData data in stats){
            data.SilverPrice=100;
            data.GoldPrice=5;
        }
    }
    override public void SaveForBuild()
    {
        string sourcePath  = Application.persistentDataPath + "/StatsUpData.json";
        string targetPath = Path.Combine(Application.streamingAssetsPath, "StatsUpData.json");
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
public class StatUpData
{
    public StatTypes statType;
    public AssetReference spriteReference;
    public Sprite statImage;
    public string statName;
    public int SilverPrice=0;
    public int GoldPrice=0;

}
