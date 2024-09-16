using UnityEngine;

[CreateAssetMenu(fileName = "BonusesData", menuName = "Data/BonusesData")]
public class BonusesData : ScriptableObject
{
    public Bonus[] bonuses;
}

[System.Serializable]
public class Bonus
{
    public int Count;
    public EBonusTypes type;
    public Sprite sprite;
}

[System.Serializable]
public enum EBonusTypes
{
    Silver,
    Gold
}