using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class SlotDataLoader : MonoBehaviour
{
    public void LoadAnimators(SlotData slotData, Action onComplete = null)
    {
        slotData.animatorControllerList = new List<RuntimeAnimatorController>();

        StartCoroutine(LoadAllAnimatorsCoroutine(slotData, onComplete));
    }

    private IEnumerator LoadAllAnimatorsCoroutine(SlotData slotData, Action onComplete)
    {
        foreach (var address in slotData.animatorAddressList)
        {
            var handle = Addressables.LoadAssetAsync<RuntimeAnimatorController>(address);
            yield return handle;

            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                slotData.animatorControllerList.Add(handle.Result);
            }
            else
            {
                Debug.LogError($"애니메이터 로드 실패: {address}");
                slotData.animatorControllerList.Add(null); // 리스트 길이 맞추기 위해 null 추가
            }
        }

        onComplete?.Invoke();
    }
}
