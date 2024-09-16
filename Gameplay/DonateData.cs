using UnityEngine;
using System.IO;

[CreateAssetMenu(fileName = "DonateData", menuName = "Data/DonateData")]
public class DonateData : ScriptableObject
{
    public Donate[] Donates;
}

[System.Serializable]
public class Donate
{
    public int GoldCount;
    public float PriceInDollars;
}