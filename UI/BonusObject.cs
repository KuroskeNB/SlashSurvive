using UnityEngine;
using UnityEngine.UI;

public class BonusObject : MonoBehaviour
{
    public Image bonusImage;
    public Text timerText;
    public Text buttonText;
    private Bonus bonus;
    [SerializeField]
    private BonusesData bonuses;
    [SerializeField]
    private CurrencyData currency;
    public bool IsUnlocked=true;
    public bool IsRecieved=false;
    public Button bonusButton;
    // Start is called before the first frame update
    void Start()
    {
       bonusButton.onClick.AddListener(OnCLicked);
    }
    public void SetBonus()
    {
        Bonus tempbonus=bonuses.bonuses[Random.Range(0,bonuses.bonuses.Length)];
     bonus=tempbonus;
     if(bonusImage)
     bonusImage.sprite=tempbonus.sprite;
    }

    // Update is called once per frame
    void Update()
    {
        if(IsUnlocked)
        {
         buttonText.text = "Recieve bonus \n   for video";
        }
        else{
            buttonText.text = "You recieved \n  the reward";
        }
    }
    void OnCLicked()
    {
      if(IsUnlocked)
      {
      Debug.Log("bonus recieved");
      IsUnlocked=false;
      IsRecieved=true;
      RecieveBonus();
      }
    }
    
    void RecieveBonus()
    {
     switch(bonus.type)
     {
        case EBonusTypes.Silver:
        currency.silverCoins.coinCount+=bonus.Count;
        break;
        case EBonusTypes.Gold:
        currency.goldCoins.coinCount+=bonus.Count;
        break;
     }
    }
}
