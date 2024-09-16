using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "Data/LevelData")]
public class LevelData : ScriptableObject
{
    public Level[] LevelsData;
    // Добавьте другие поля, необходимые для уровня
}

[System.Serializable]
public class WaveData
{
    public GameObject enemy;
    public int enemyPerWave=0;
}

[System.Serializable]
public class Level
{
    public WaveData[] WavesData;
}