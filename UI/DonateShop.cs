using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class DonateShop : MonoBehaviour
{
    public DonateObject obj;
    //private UnityEngine.UI.Button button;
    //public Donate data;
    public GridLayoutGroup grid;
    public ScrollView scrollView;
    public ScrollRect scrollRect;
    public DonateData donateData;
    // Start is called before the first frame update
    void Start()
    {
        foreach(Donate item in donateData.Donates)
        {
        AddItemToGrid(item);
        }
    }

    void AddItemToGrid(Donate data)
    {
        if (obj != null && grid != null)
        {
            // Создаем новый объект из префаба
            DonateObject newItem = Instantiate(obj);

            // Устанавливаем его родителем объект с GridLayoutGroup
            newItem.transform.SetParent(grid.transform, false);
            
            // Можно настроить новый объект, если нужно
            // Например, установить текст на кнопке или изображение
            newItem.LoadDonate(data);
        }
        else
        {
            Debug.LogError("Item prefab or Grid Layout Group is not set.");
        }
    }
}
