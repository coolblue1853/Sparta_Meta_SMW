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
                Debug.LogError($"�ִϸ����� �ε� ����: {address}");
                slotData.animatorControllerList.Add(null); // ����Ʈ ���� ���߱� ���� null �߰�
            }
        }

        onComplete?.Invoke();
    }
}
