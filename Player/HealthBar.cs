using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public PlayerData playerData;
    private Image image;
    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        playerData=GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerData>();
    }

    // Update is called once per frame
    void Update()
    {
        if(playerData)
       image.fillAmount = playerData.CurrentHealth/playerData.MaxHealth; 
    }
}
