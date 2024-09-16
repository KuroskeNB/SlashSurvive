using UnityEngine;
using UnityEngine.UI;
public class ChanceButton : MonoBehaviour
{
    private int ChanceTimes=0;
    private Button chanceButton;
    public CurrencyData currencyData;
    public WaveManager waveManager;
    // Start is called before the first frame update
    void Start()
    {
        chanceButton=GetComponent<Button>();
        chanceButton.onClick.AddListener(OnChanceClicked);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

void OnChanceClicked()
{
 if(ChanceTimes==0)
 {

 }
 else
 {

 }
 waveManager.NewChance();
}
}
