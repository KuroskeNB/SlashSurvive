using UnityEngine;
using System.IO;

[CreateAssetMenu(fileName = "CurrencyData", menuName = "Data/CurrencyData")]
public class CurrencyData : DataSaver
{
    public Coin silverCoins;
    public Coin goldCoins;
   override public void Save()
    {
        string path = Application.persistentDataPath + "/CurrencyData.json";
         string json = JsonUtility.ToJson(this);
        File.WriteAllText(path, json);
    }
    override public void Load()
    {
        string path = Application.persistentDataPath + "/CurrencyData.json";
    string streamingAssetsPath = Path.Combine(Application.streamingAssetsPath, "CurrencyData.json");
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
        string sourcePath  = Application.persistentDataPath + "/CurrencyData.json";
        string targetPath = Path.Combine(Application.streamingAssetsPath, "CurrencyData.json");
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
public enum ECurrency
{
 SilverCoins,
 GoldCoins
}
[System.Serializable]
public class Coin
{
    public ECurrency currencyType;
    public int coinCount;
    public Sprite currencySprite;
}
[System.Serializable]
public class PriceData
{
    public int SilverPrice=0;
    public int GoldPrice=0;
}
