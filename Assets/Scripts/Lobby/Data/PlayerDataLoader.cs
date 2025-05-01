using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;


public class PlayerDataLoader : MonoBehaviour
{
    public static void LoadAnimator(PlayerData data, System.Action onComplete = null)
    {
        Addressables.LoadAssetAsync<RuntimeAnimatorController>(data.animatorAddress)
            .Completed += (handle) =>
            {
                if (handle.Status == AsyncOperationStatus.Succeeded)
                {
                    data.animatorController = handle.Result;
                }
                else
                {
                    Debug.LogError("애니메이터 로드 실패: " + data.animatorAddress);
                }

                onComplete?.Invoke();
            };
    }
}


