using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIMenu : MonoBehaviour
{
    public Canvas heroesInventory;
    public Canvas Rewards;
    public Text RecordText;
    public Button InventoryButton;
    public Button RewardsButton;
    public Button PlayButton;
    // Start is called before the first frame update
    void Start()
    {
        //heroesInventory.gameObject.SetActive(false);
        InventoryButton.onClick.AddListener(OnInventoryButtonClicked);
        PlayButton.onClick.AddListener(OnPlayClicked);
        RewardsButton.onClick.AddListener(OnRewardsClicked);

        if(RecordText)
        RecordText.text="Your record is: " + PlayerPrefs.GetInt("MaximalScore");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnInventoryButtonClicked()
    {
        heroesInventory.gameObject.SetActive(true);
        heroesInventory.GetComponent<UiHeroesInventory>().UpdateHeroItems();
    }
    void OnPlayClicked()
    {
     SceneManager.LoadScene("GameScene");
    }
    void OnRewardsClicked()
    {
        Rewards.gameObject.SetActive(true);
    }
}
