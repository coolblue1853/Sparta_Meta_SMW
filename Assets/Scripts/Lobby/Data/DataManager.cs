using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    public void Save(string[] animatorAddresses, string savePath)
    {
        var dataList = new AnimatorSaveDataList();

        foreach (var addr in animatorAddresses)
            dataList.Animators.Add(new AnimatorSaveData { AnimatorAddress = addr });

        string json = JsonUtility.ToJson(dataList, true);
        File.WriteAllText(savePath, json);
    }

    public string[] Load(Animator[] targetAnimators, string savePath)
    {
        if (!File.Exists(savePath))
        {
            Debug.LogWarning("���� ������ �����ϴ�.");
            return null;
        }

        string json = File.ReadAllText(savePath);
        var dataList = JsonUtility.FromJson<AnimatorSaveDataList>(json);

        if (dataList.Animators.Count != targetAnimators.Length)
        {
            Debug.LogError("Animator ���� ����ġ");
            return null;
        }

        for (int i = 0; i < dataList.Animators.Count; i++)
        {
            string addr = dataList.Animators[i].AnimatorAddress;
            var animator = targetAnimators[i];

            Addressables.LoadAssetAsync<RuntimeAnimatorController>(addr).Completed += handle =>
            {
                if (handle.Status == AsyncOperationStatus.Succeeded)
                    animator.runtimeAnimatorController = handle.Result;
                else
                    Debug.LogError($"Animator {i} �ε� ����: {addr}");
            };
        }

        // �ּҸ� �����ؼ� ��ȯ
        return dataList.Animators.Select(a => a.AnimatorAddress).ToArray();
    }
}
