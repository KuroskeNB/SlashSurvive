using UnityEngine;
using UnityEngine.UI;

public class UltimateCharge : MonoBehaviour
{
    public PlayerData playerData;
    public Button UltButton;
    private Image image;
    public WaveManager waveManager;
    // Start is called before the first frame update
    void Start()
    {
        image=GetComponent<Image>();
       if(UltButton)
       {
        UltButton.onClick.AddListener(UseUlt);
       UltButton.gameObject.SetActive(false); 
       }
    }

    // Update is called once per frame
    void Update()
    {
        if(!playerData)
        playerData=(GameObject.FindGameObjectWithTag("Player")!=null)?GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerData>():null;
        if(playerData)
        {
       image.fillAmount = playerData.CurrentUltCharge/playerData.MaxUltCharge;
       if(playerData.CurrentUltCharge>=playerData.MaxUltCharge)
       UltButton.gameObject.SetActive(true); 
        }
    }

    void UseUlt()
    {
     StartCoroutine(playerData.GetComponent<PlayerController>().SlowMo());
     UltButton.gameObject.SetActive(false); 
     playerData.CurrentUltCharge=0;
    }
}
