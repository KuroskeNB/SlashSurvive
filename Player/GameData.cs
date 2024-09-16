using System.IO;
using UnityEngine;

public class GameData : MonoBehaviour
{
    public int level;
    public string SkinPath;
    public void SaveData()
    {
        // Сохранение данных с помощью PlayerPrefs
        PlayerPrefs.SetInt("PlayerLevel", level);
        PlayerPrefs.SetString("PlayerSkin", SkinPath);
        PlayerPrefs.Save(); // Сохраняет изменения на диск
    }

    // Update is called once per frame
    public void LoadData()
    {
      level = PlayerPrefs.GetInt("PlayerLevel");
      SkinPath=PlayerPrefs.GetString("PlayerSkin");
    }
}
