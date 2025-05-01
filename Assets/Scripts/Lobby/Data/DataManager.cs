using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

    public void Save(string[] animatorAddresses, string savePath)
    {
        var dataList = new AnimatorSaveDataList();

        foreach (var addr in animatorAddresses)
            dataList.animators.Add(new AnimatorSaveData { animatorAddress = addr });

        string json = JsonUtility.ToJson(dataList, true);
        File.WriteAllText(savePath, json);
    }

    public string[] Load(Animator[] targetAnimators, string savePath)
    {
        if (!File.Exists(savePath))
        {
            Debug.LogWarning("저장 파일이 없습니다.");
            return null;
        }

        string json = File.ReadAllText(savePath);
        var dataList = JsonUtility.FromJson<AnimatorSaveDataList>(json);

        if (dataList.animators.Count != targetAnimators.Length)
        {
            Debug.LogError("Animator 개수 불일치");
            return null;
        }

        for (int i = 0; i < dataList.animators.Count; i++)
        {
            string addr = dataList.animators[i].animatorAddress;
            var animator = targetAnimators[i];

            Addressables.LoadAssetAsync<RuntimeAnimatorController>(addr).Completed += handle =>
            {
                if (handle.Status == AsyncOperationStatus.Succeeded)
                    animator.runtimeAnimatorController = handle.Result;
                else
                    Debug.LogError($"Animator {i} 로드 실패: {addr}");
            };
        }

        // 주소만 추출해서 반환
        return dataList.animators.Select(a => a.animatorAddress).ToArray();
    }
}
