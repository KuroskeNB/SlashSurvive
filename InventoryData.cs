using UnityEngine;
using UnityEngine.AddressableAssets;

[CreateAssetMenu(fileName = "InventoryData", menuName = "Data/InventoryData")]
public class InventoryData : ScriptableObject
{
       public HeroesData heroes;
}

[System.Serializable]
public class ItemData
{
    public AssetReference prefabReference;
    public GameObject itemPrefab;
    public bool IsUnlocked=false;
    public string itemName;
    public EPriceType priceType;
    public int silverPrice=1000;
    public int goldPrice=50;
}

public enum EPriceType
{
    Rewards,
    Gold,
    GoldAndSilver
}