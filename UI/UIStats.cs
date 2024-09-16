using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class UIStats : MonoBehaviour
{
    public StatsUpData statsUpData;
    public SpriteHandler spriteHandler;
    public ScrollView scrollView;
    public GridLayoutGroup grid;
    public StatItem itemObject;

    // Start is called before the first frame update
    void Start()
    {
        spriteHandler=GetComponent<SpriteHandler>();
        statsUpData.Load();
       foreach(StatUpData item in statsUpData.stats)
        {
            spriteHandler.LoadPrefabAndAssign(item);
            Debug.Log("load asset");
        AddItemToGrid(item);
        }
        //statsUpData.Load();
    }
    private void OnDestroy() {
        Debug.Log("save statsdata");
        statsUpData.Save();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

     void AddItemToGrid(StatUpData data)
    {
        if (itemObject != null && grid != null)
        {
            // Создаем новый объект из префаба
            StatItem newItem = Instantiate(itemObject);

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
}
