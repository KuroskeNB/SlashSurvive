using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class UIRewards : MonoBehaviour
{
    public ScrollView scrollView;
     public ScrollRect scrollRect;
     public SpriteHandler spriteHandler;
    public VerticalLayoutGroup grid;
    public RewardObject itemObject;
    public RewardData rewardData;
    // Start is called before the first frame update
    void Start()
    {
        spriteHandler=GetComponent<SpriteHandler>();
        rewardData.Load();
        foreach(RewardInfo item in rewardData.rewardsData)
        {
             spriteHandler.LoadPrefabAndAssign(item);
        AddItemToGrid(item);
        }
        gameObject.SetActive(false);
    }
    private void OnDestroy() {
        Debug.Log("save rewardsdata");
        rewardData.Save();
    }

    // Update is called once per frame
   void AddItemToGrid(RewardInfo data)
    {
        if(data.bIsUnlocked) return;
        if (itemObject != null && grid != null && !data.bIsUnlocked)
        {
            // Создаем новый объект из префаба
            RewardObject newItem = Instantiate(itemObject);

            // Устанавливаем его родителем объект с GridLayoutGroup
            newItem.transform.SetParent(grid.transform, false);

            // Можно настроить новый объект, если нужно
            // Например, установить текст на кнопке или изображение
            newItem.LoadReward(data);
        }
        else
        {
            Debug.LogError("Item prefab or Grid Layout Group is not set.");
        }
    }
    void Update()
    {
     if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = Input.mousePosition;

            //Проверяем, находится ли нажатие мыши внутри области Image
            if (!RectTransformUtility.RectangleContainsScreenPoint(scrollRect.GetComponent<RectTransform>(), mousePosition, Camera.main))
            {
                // Если нажатие вне области, уничтожаем объект
                gameObject.SetActive(false);
            }
        }
    }
}
