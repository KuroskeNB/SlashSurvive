using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class SpriteHandler : MonoBehaviour
{
   public void LoadPrefabAndAssign(StatUpData statUpData)
    {
        if (statUpData.spriteReference != null)
        {
            // Проверяем, был ли префаб уже загружен
            if (statUpData.spriteReference.OperationHandle.IsValid())
            {
                // Префаб уже загружен, просто присваиваем его
                var loadedObject = statUpData.spriteReference.OperationHandle.Result as Sprite;
                AssignSprite(loadedObject,ref statUpData.statImage);
            }
            else
            {
                // Если префаб еще не загружен, загружаем его
                var handle = statUpData.spriteReference.LoadAssetAsync<Sprite>();

                handle.Completed += (AsyncOperationHandle<Sprite> asyncHandle) =>
                {
                    if (asyncHandle.Status == AsyncOperationStatus.Succeeded)
                    {
                        // Присваиваем загруженный префаб в statImage
                        AssignSprite(asyncHandle.Result,ref statUpData.statImage);
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
    public void LoadPrefabAndAssign(RewardInfo statUpData)
    {
        if (statUpData.spriteReference != null)
        {
            Debug.Log("load asset");
            // Проверяем, был ли префаб уже загружен
            if (statUpData.spriteReference.OperationHandle.IsValid())
            {
                // Префаб уже загружен, просто присваиваем его
                var loadedObject = statUpData.spriteReference.OperationHandle.Result as Sprite;
                AssignSprite(loadedObject, ref statUpData.RewardImage);
            }
            else
            {
                // Если префаб еще не загружен, загружаем его
                var handle = statUpData.spriteReference.LoadAssetAsync<Sprite>();

                handle.Completed += (AsyncOperationHandle<Sprite> asyncHandle) =>
                {
                    if (asyncHandle.Status == AsyncOperationStatus.Succeeded)
                    {
                        // Присваиваем загруженный префаб в statImage
                        AssignSprite(asyncHandle.Result, ref statUpData.RewardImage);
                    }
                    else
                    {
                        Debug.Log("Failed to load prefab.");
                    }
                };
            }
        }
        else
        {
            Debug.Log("Prefab reference is null.");
        }
    }

    private void AssignSprite(Sprite loadedObject,ref Sprite yourSprite)
    {
        if (loadedObject != null)
        {
            var spriteRenderer = loadedObject;
            if (spriteRenderer != null)
            {
                yourSprite = spriteRenderer;
                Debug.Log($"Prefab loaded and assigned: {yourSprite.name}");
            }
            else
            {
                Debug.Log($"Loaded GameObject '{loadedObject.name}' does not have a SpriteRenderer component.");
            }
        }
        else
        {
            Debug.Log("Loaded GameObject is null.");
        }
    }
}
