using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class UiHeroesInventory : MonoBehaviour
{
    public PrefabHandler prefabHandler;
    public UnityEngine.UI.Button[] contentButtons;
    public GameObject heroContent;
    public GameObject effectContent;
    public GameObject weaponContent;
    public ScrollView scrollView;
    public ScrollRect scrollRect;
    public GameObject puppet;
    private GameObject tempPuppet;
    public GridLayoutGroup grid;
    public InventoryItem itemObject;
    public HeroesData heroesData;
    public EffectsData effectData;
    public WeaponData weaponsData;
    private bool puppetIsSpawned=false;
    private string currentPrefabName;
    // Start is called before the first frame update
    void Start()
    {
        prefabHandler=GetComponent<PrefabHandler>();
        heroesData.Load();
        effectData.Load();
        weaponsData.Load();
        //scrollRect.GetComponentInChildren<ScrollRect>();
        //grid=GetComponent<GridLayoutGroup>();
        foreach(ItemData item in heroesData.heroData)
        {
            prefabHandler.LoadPrefabAndAssign(item,gameObject);
        AddHeroToGrid(item);
        }
        foreach(ItemData item in effectData.effectsData)
        {
            prefabHandler.LoadPrefabAndAssign(item,gameObject);
            AddEffectToGrid(item);
        }
        foreach(ItemData item in weaponsData.weaponData)
        {
            prefabHandler.LoadPrefabAndAssign(item,gameObject);
            AddWeaponToGrid(item);
        }
        for(int i=0;i<heroesData.heroData.Length;i++)
        {
            if(heroesData.heroData[i].itemName==PlayerPrefs.GetString("PlayerPrefab"))
            {
                if(heroesData.heroData[i].itemPrefab)
                {
                    if(tempPuppet)
            Destroy(tempPuppet);
                     currentPrefabName=heroesData.heroData[i].itemName;
                    Debug.LogError("prefab instantiated");
                tempPuppet=Instantiate(heroesData.heroData[i].itemPrefab,puppet.transform.position,puppet.transform.rotation,puppet.transform);
                Destroy(tempPuppet.GetComponent<PlayerController>());
                }
            }
        }
        gameObject.SetActive(false);
    }
    private void OnEnable() {
        effectContent.SetActive(false);
        weaponContent.SetActive(false);
        heroContent.SetActive(true);
    }
    private void OnDestroy() {
        Debug.Log("save heroesdata");
        heroesData.Save();
        effectData.Save();
        weaponsData.Save();
    }
    public void UpdateHeroItems()
    {
        foreach(InventoryItem item in heroContent.GetComponentsInChildren<InventoryItem>())
        {
            item.UpdateData();
        }
        foreach(InventoryItem item in weaponContent.GetComponentsInChildren<InventoryItem>())
        {
            item.UpdateData();
        }
        foreach(InventoryItem item in effectContent.GetComponentsInChildren<InventoryItem>())
        {
            item.UpdateData();
        }
    }
    // Update is called once per frame
    void Update()
    {
        // if(!puppetIsSpawned)
        // {
        //     Debug.LogError("puppetIsSpawned false");
        //     for(int i=0;i<heroesData.heroData.Length;i++)
        // {
        //     if(heroesData.heroData[i].itemName==PlayerPrefs.GetString("PlayerPrefab"))
        //     {
        //         currentPrefabName=heroesData.heroData[i].itemName;
        //         Debug.Log("prefab found");
        //         if(heroesData.heroData[i].itemPrefab)
        //         {
        //         tempPuppet=Instantiate(heroesData.heroData[i].itemPrefab,puppet.transform.position,puppet.transform.rotation,puppet.transform);
        //         Destroy(tempPuppet.GetComponent<PlayerController>());
        //         puppetIsSpawned=true;
        //         }
        //     }
        // }
        // }

        if(PlayerPrefs.GetString("PlayerPrefab")!=currentPrefabName)
        {
            SpawnPuppet();
        }

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
    public void SpawnPuppet()
    {
        if(tempPuppet)
            Destroy(tempPuppet);
            for(int i=0;i<heroesData.heroData.Length;i++)
        {
            if(heroesData.heroData[i].itemName==PlayerPrefs.GetString("PlayerPrefab"))
            {
                if(heroesData.heroData[i].itemPrefab)
                {
                     currentPrefabName=heroesData.heroData[i].itemName;
                tempPuppet=Instantiate(heroesData.heroData[i].itemPrefab,puppet.transform.position,puppet.transform.rotation,puppet.transform);
                Destroy(tempPuppet.GetComponent<PlayerController>());
                }
            }
        }
    }

    void AddHeroToGrid(ItemData data)
    {
        if (itemObject != null && grid != null)
        {
            // Создаем новый объект из префаба
            InventoryItem newItem = Instantiate(itemObject);

            // Устанавливаем его родителем объект с GridLayoutGroup
            newItem.transform.SetParent(grid.transform, false);
            
            // Можно настроить новый объект, если нужно
            // Например, установить текст на кнопке или изображение
            newItem.LoadItem(data);
        }
        else
        {
            Debug.LogError("Item prefab or Grid Layout Group is not set.");
        }
    }
    void AddWeaponToGrid(ItemData data)
    {
        if (itemObject != null && grid != null)
        {
            // Создаем новый объект из префаба
            InventoryItem newItem = Instantiate(itemObject);

            // Устанавливаем его родителем объект с GridLayoutGroup
            newItem.transform.SetParent(weaponContent.transform, false);
            
            // Можно настроить новый объект, если нужно
            // Например, установить текст на кнопке или изображение
            newItem.LoadItem(data);
        }
        else
        {
            Debug.LogError("Item prefab or Grid Layout Group is not set.");
        }
    }
    void AddEffectToGrid(ItemData data)
    {
        if (itemObject != null && grid != null)
        {
            // Создаем новый объект из префаба
            InventoryItem newItem = Instantiate(itemObject);

            // Устанавливаем его родителем объект с GridLayoutGroup
            newItem.transform.SetParent(effectContent.transform, false);
            
            // Можно настроить новый объект, если нужно
            // Например, установить текст на кнопке или изображение
            newItem.LoadItem(data);
        }
        else
        {
            Debug.LogError("Item prefab or Grid Layout Group is not set.");
        }
    }
}
