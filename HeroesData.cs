using UnityEngine;
using System.IO;

[CreateAssetMenu(fileName = "HeroesData", menuName = "Data/HeroesData")]
public class HeroesData : DataSaver
{
    public ItemData[] heroData;
    override public void Save()
    {
        string path = Application.persistentDataPath + "/HeroesData.json";
         string json = JsonUtility.ToJson(this);
        File.WriteAllText(path, json);
    }
    override public void Load()
    {
        string path = Application.persistentDataPath + "/HeroesData.json";
        string streamingAssetsPath = Path.Combine(Application.streamingAssetsPath, "HeroesData.json");
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
        string sourcePath  = Application.persistentDataPath + "/HeroesData.json";
        string targetPath = Path.Combine(Application.streamingAssetsPath, "HeroesData.json");
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

