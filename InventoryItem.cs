using UnityEngine;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour
{
    public ItemData data;
    private Button button;
    public OrderConfirm orderIcon;
    public Image currentImage;
    // Start is called before the first frame update
    void Start()
    {
        button=GetComponent<Button>();
        button.onClick.AddListener(OnClicked);
    }

    // Update is called once per frame
    void Update()
    {
        if(data.itemPrefab!=null&&data.itemPrefab.GetComponent<SpriteRenderer>().sprite!=transform.GetChild(0).GetComponent<Image>().sprite)
        {
            transform.GetChild(0).GetComponent<Image>().sprite=data.itemPrefab.GetComponent<SpriteRenderer>().sprite;
        }
    }
    public void LoadItem(ItemData itemData)
{
    data = itemData;
    Debug.Log("children name " + transform.GetChild(0).gameObject.name);

    // Проверка на наличие дочернего объекта и компонентов
    if (transform.childCount > 0 &&
        transform.GetChild(0).GetComponent<Image>() != null &&
        itemData.itemPrefab != null &&
        itemData.itemPrefab.GetComponent<SpriteRenderer>() != null)
    {
        transform.GetChild(0).GetComponent<Image>().sprite = itemData.itemPrefab.GetComponent<SpriteRenderer>().sprite;
    }

    if (data.IsUnlocked)
    {
        if (transform.childCount > 1)
        {
            transform.GetChild(1).gameObject.SetActive(false);
        }
    }
}


    void OnClicked()
    {
       // Debug.Log("clicked on "+ data.itemPrefab.name);

    if(!data.IsUnlocked)
    {
         OrderConfirm tempOrder=Instantiate(orderIcon,new Vector2(0,0),transform.rotation,GameObject.Find("Inventory").transform);
         if(tempOrder)
         {
         Debug.Log("temporder");
         tempOrder.SetOrder(this);
         }
     }
    if(!data.IsUnlocked) return;

    if(data.itemPrefab.CompareTag("Player"))
    {
        ClickedOnSkin();
    }
    else if(data.itemPrefab.CompareTag("Weapon"))
    {
        ClickedOnWeapon();
    }
    else if(data.itemPrefab.CompareTag("Effect"))
    {
        ClickedOnEffect();
    }
    
    }

    void ClickedOnSkin()
    {
    PlayerPrefs.SetString("PlayerPrefab",data.itemName);
    }

    void ClickedOnWeapon()
    {
        //Debug.Log("clicked on "+ data.itemPrefab.name);
    PlayerPrefs.SetString("WeaponPrefab",data.itemPrefab.name);
    }
    void ClickedOnEffect()
    {
    PlayerPrefs.SetString("EffectPrefab",data.itemName);
    }

    public void UpdateData()
    {
        if(data.IsUnlocked)
        transform.GetChild(1).gameObject.SetActive(false);
    }
}
