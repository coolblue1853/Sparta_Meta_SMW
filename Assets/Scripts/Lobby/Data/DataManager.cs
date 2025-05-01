using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class DataManager : MonoBehaviour
{
    public static DataManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    public void Save(List<string> animatorAddresses, string savePath)
    {
        Debug.Log(savePath);
        var dataList = new AnimatorSaveDataList();

        foreach (var addr in animatorAddresses)
            dataList.animators.Add(new AnimatorSaveData { animatorAddress = addr });

        string json = JsonUtility.ToJson(dataList, true);
        File.WriteAllText(savePath, json);
    }

    public void Load( List<Animator> targetAnimators, string savePath)
    {
        if (!File.Exists(savePath))
        {
            Debug.LogWarning("저장 파일이 없습니다.");
            return;
        }

        string json = File.ReadAllText(savePath);
        var dataList = JsonUtility.FromJson<AnimatorSaveDataList>(json);

        if (dataList.animators.Count != targetAnimators.Count)
        {
            Debug.LogError("Animator 개수 불일치");
            return;
        }

        for (int i = 0; i < dataList.animators.Count; i++)
        {
            int index = i;
            string addr = dataList.animators[i].animatorAddress;

            Addressables.LoadAssetAsync<RuntimeAnimatorController>(addr).Completed += handle =>
            {
                if (handle.Status == AsyncOperationStatus.Succeeded)
                {
                    targetAnimators[index].runtimeAnimatorController = handle.Result;
                }
                else
                {
                    Debug.LogError($"Animator {index} 로드 실패: {addr}");
                }
            };
        }
    }
}
