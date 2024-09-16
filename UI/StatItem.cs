using System;
using UnityEngine;
using UnityEngine.UI;

public class StatItem : MonoBehaviour
{
    public CurrencyData currencyData;
    private PlayerData playerData;
    public StatUpData data;
    public Text StatNameText;
    public Button goldButton;
    public Button silverButton;
    public Text StatValue;
    public Text goldPrice;
    public Text silverPrice;
    // Start is called before the first frame update
    void Start()
    {
        //button=GetComponent<Button>();
        if(goldButton)
        goldButton.onClick.AddListener(OnClickedGold);
        if(silverButton)
        silverButton.onClick.AddListener(OnClickedSilver);

        Text[] texts = GetComponentsInChildren<Text>();
        foreach(Text lol in texts)
        {
            if(lol.gameObject.name=="GoldPrice")
            goldPrice=lol;
            if(lol.gameObject.name=="SilverPrice")
            silverPrice=lol;
        }
    }
    void Update()
    {
        if(playerData==null)
        {
        playerData=(GameObject.FindGameObjectWithTag("Player"))?GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerData>():null;
        if(playerData)
        StatValue.text=playerData.GetStatValue(data.statType).ToString();
        }
        if(playerData)
     StatValue.text=playerData.GetStatValue(data.statType).ToString()+ " + " + playerData.GetStatLevelUp(data.statType).ToString();

     if(transform.GetChild(0).GetComponent<Image>().sprite!=data.statImage)
     transform.GetChild(0).GetComponent<Image>().sprite=data.statImage;
    }

    void OnClickedGold()
    {
        if(currencyData.goldCoins.coinCount>data.GoldPrice)
        {
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerData>().UpStats(data.statType);
        currencyData.goldCoins.coinCount-=data.GoldPrice;
        Debug.Log("Buy by gold");
        UpdateItem();
        }
    }
    void OnClickedSilver()
    {
        if(currencyData.silverCoins.coinCount>data.SilverPrice)
        {
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerData>().UpStats(data.statType);
        currencyData.silverCoins.coinCount-=data.SilverPrice;
        Debug.Log("Buy by silver");
        UpdateItem();
        }
    }
    public void LoadItem(StatUpData newdata)
    {
        data=newdata;
        if(newdata.statImage)
     transform.GetChild(0).GetComponent<Image>().sprite=newdata.statImage;
     if(goldPrice&&silverPrice)
     {
     goldPrice.text=newdata.GoldPrice.ToString();
     silverPrice.text=newdata.SilverPrice.ToString();
     }
     if(playerData)
     StatValue.text=playerData.GetStatValue(data.statType).ToString();
     if(StatNameText)
     StatNameText.text = Enum.GetName(typeof(StatTypes),data.statType);
    }
    void UpdateItem()
    {
        data.GoldPrice=data.GoldPrice*2;
        data.SilverPrice=data.SilverPrice*2;
        

        if(goldPrice&&silverPrice)
     {
     goldPrice.text=data.GoldPrice.ToString();
     silverPrice.text=data.SilverPrice.ToString();
     StatValue.text=GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerData>().GetStatValue(data.statType).ToString();
     }
    }
}
