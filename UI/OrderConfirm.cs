using UnityEngine;
using UnityEngine.UI;

public class OrderConfirm : MonoBehaviour
{
    public CurrencyData currencyData;
    private Image orderImage;
    public Button buyButton;
    public Button silverButton;
    private Text orderText;
    private InventoryItem currentOrder;
    // Start is called before the first frame update
    void Update()
    {
        //mouse
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = Input.mousePosition;

            // Проверяем, находится ли нажатие мыши внутри области Image
            if (!RectTransformUtility.RectangleContainsScreenPoint(GetComponent<Image>().rectTransform, mousePosition, Camera.main))
            {
                // Если нажатие вне области, уничтожаем объект
                Destroy(gameObject);
            }
        }
        if(currentOrder.data.IsUnlocked)
        Destroy(gameObject);
        //touch
        // if (Input.touchCount > 0)
        // {
        //     foreach (Touch touch in Input.touches)
        //     {
        //         // Проверяем, если касание началось (нажатие)
        //         if (touch.phase == TouchPhase.Began)
        //         {
        //             // Проверяем, находится ли касание внутри области Image
        //             if (!RectTransformUtility.RectangleContainsScreenPoint(GetComponent<Image>().rectTransform, touch.position, Camera.main))
        //             {
        //                 // Если касание вне области, уничтожаем объект
        //                 Destroy(gameObject);
        //             }
        //         }
        //     }
        // }
    }

    private void OnDisable() {
        Destroy(gameObject);
    }
    public void SetOrder(InventoryItem order)
    {
        if(buyButton)
        buyButton.onClick.AddListener(BuyByGold);
        if(silverButton)
        silverButton.onClick.AddListener(BuyBySilver);
        orderImage=transform.GetChild(0).GetComponentInChildren<Image>();
        if(orderImage)
        Debug.Log("orderImage");
        orderText=transform.GetChild(3).GetComponentInChildren<Text>();
        if(orderText)
        Debug.Log("orderText");

        currentOrder=order;
        if(orderImage)
        orderImage.sprite=order.data.itemPrefab.GetComponent<SpriteRenderer>().sprite;
     switch(order.data.priceType)
     {
        case EPriceType.Rewards:
        buyButton.gameObject.SetActive(false);
        silverButton.gameObject.SetActive(false);
        orderText.text="For Rewards";
        break;
        case EPriceType.Gold:
        silverButton.gameObject.SetActive(false);
        orderText.text=order.data.goldPrice + " of Gold";
        break;
        case EPriceType.GoldAndSilver:
        orderText.text=order.data.goldPrice.ToString() + " of Gold \n or \n" + order.data.silverPrice.ToString() + " of Silver";
        break;
     }
    }
    void OrderSucces()
    {
     currentOrder.data.IsUnlocked=true;
     currentOrder.UpdateData();
    }
    void BuyBySilver()
    {
       currencyData.silverCoins.coinCount-=currentOrder.data.silverPrice;
       OrderSucces();
    }
    void BuyByGold()
    {
        currencyData.goldCoins.coinCount-=currentOrder.data.goldPrice;
        OrderSucces();
     Debug.Log("buy by gold");
    }

}
