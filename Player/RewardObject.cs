using UnityEngine;
using UnityEngine.UI;
public class RewardObject : MonoBehaviour
{
    public RewardInfo data;
    private Button button;
    public CurrencyData currencyData;
    public WeaponData weaponData;
    public EffectsData effectsData;
    public HeroesData heroesData;

    public void LoadReward(RewardInfo newdata)
    {
    data=newdata;
    button=GetComponent<Button>();
    button.transform.GetChild(1).GetComponent<Image>().sprite=data.RewardImage;
    button.transform.GetChild(0).GetComponent<Text>().text="Reward for \n " + data.ScoreRequirements + " score-record";
    button.onClick.AddListener(OnClicked);
    }
    // Start is called before the first frame update
    void Start()
    {
        button.onClick.AddListener(OnClicked);
    }
    void Update()
    {
      if(button.transform.GetChild(1).GetComponent<Image>().sprite!=data.RewardImage)
      button.transform.GetChild(1).GetComponent<Image>().sprite=data.RewardImage;
    }

    void OnClicked()
    {
        if(data.bIsUnlocked) return;
     if(PlayerPrefs.GetInt("MaximalScore")<data.ScoreRequirements) return;
     Debug.Log("recieve reward");   
     switch(data.rewardType)
     {
        case ERewardType.Skin:
        UnlockSkin();
        break;
        case ERewardType.SilverCoins:
        UnlockSilverCoins();
        break;
        case ERewardType.GoldCoins:
        UnlockGoldCoins();
        break;
        case ERewardType.Weapon:
        UnlockWeapon();
        break;
        case ERewardType.Effect:
        UnlockEffect();
        break;
     }
     Destroy(gameObject);
    }
    void UnlockSkin()
    {
     foreach(ItemData item in heroesData.heroData)
     {
        if(item.itemName==data.rewardName)
        {
        item.IsUnlocked=true;
        data.bIsUnlocked=true; 
        }
     }
    }
    void UnlockSilverCoins()
    {
     if(currencyData)
     currencyData.silverCoins.coinCount+=data.rewardCount;
     data.bIsUnlocked=true; 
    }
    void UnlockGoldCoins()
    {
        if(currencyData)
     currencyData.goldCoins.coinCount+=data.rewardCount;
     data.bIsUnlocked=true; 
    }
    void UnlockWeapon()
    {
     foreach(ItemData item in weaponData.weaponData)
     {
        if(item.itemName==data.rewardName)
        {
        item.IsUnlocked=true;
        data.bIsUnlocked=true; 
        }
     }
    }
    void UnlockEffect()
    {
        foreach(ItemData item in effectsData.effectsData)
     {
        if(item.itemName==data.rewardName)
        {
        item.IsUnlocked=true;
        data.bIsUnlocked=true; 
        }
     }

    }

}
