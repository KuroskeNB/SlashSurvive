using System.Data.Common;
using UnityEngine;
using UnityEngine.UI;

public class DonateObject : MonoBehaviour
{
    public Button buyButton;
    public Donate donateData;
    public Text donateText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void LoadDonate(Donate data)
    {
      donateData=data;
      if(donateText)
      donateText.text=(data.GoldCount + " Gold Coins\n for " + data.PriceInDollars+"$");
    }
}
