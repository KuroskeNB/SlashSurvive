using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class PrefabHandler : MonoBehaviour
{
     public void LoadPrefabAndAssign(ItemData itemData, GameObject caller)
    {
        if (itemData.prefabReference != null)
        {
            // Проверяем, был ли префаб уже загружен
            if (itemData.prefabReference.OperationHandle.IsValid())
            {
                // Префаб уже загружен, просто присваиваем его
                itemData.itemPrefab = itemData.prefabReference.OperationHandle.Result as GameObject;
                if(caller.GetComponent<UiHeroesInventory>())
                caller.GetComponent<UiHeroesInventory>().SpawnPuppet();
                Debug.Log($"Prefab was already loaded and assigned: {itemData.itemPrefab.name}");
            }
            else
            {
                // Если префаб еще не загружен, загружаем его
                var handle = itemData.prefabReference.LoadAssetAsync<GameObject>();

                handle.Completed += (AsyncOperationHandle<GameObject> asyncHandle) =>
                {
                    if (asyncHandle.Status == AsyncOperationStatus.Succeeded)
                    {
                        // Присваиваем загруженный префаб в itemPrefab
                        itemData.itemPrefab = asyncHandle.Result;
                        if(caller.GetComponent<UiHeroesInventory>())
                        caller.GetComponent<UiHeroesInventory>().SpawnPuppet();
                        Debug.Log($"Prefab loaded and assigned: {itemData.itemPrefab.name}");
                    }
                    else
                    {
                        Debug.LogError("Failed to load prefab.");
                    }
                };
            }
        }
        else
        {
            Debug.LogError("Prefab reference is null.");
        }
    }

    void OnPrefabInstantiated(AsyncOperationHandle<GameObject> handle)
    {
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            GameObject prefabInstance = handle.Result;
            // Дополнительные действия с загруженным префабом
        }
        else
        {
            Debug.LogError("Failed to instantiate prefab");
        }
    }
}