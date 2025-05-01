using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class SlotDataManager : MonoBehaviour
{
    public SlotData _slotData;
    public RuntimeAnimatorController[] changePlayerNpcs;
    public string[] changePlayerAdress;

    public ChangePlayerNpc[] changePlayerNpcArr;
    // Start is called before the first frame update
    private void Awake()
    {
        var loader = gameObject.AddComponent<SlotDataLoader>();
        loader.LoadAnimators(_slotData, () =>
        {
            changePlayerNpcs = _slotData.animatorControllerList.ToArray();
            changePlayerAdress = _slotData.animatorAddressList.ToArray();
            SetSlotAnimator();
        });
        LobbyScene.EndGame += SaveSlotData;
    }
    
    void SetSlotAnimator()
    {
        for (int i = 0; i < changePlayerNpcs.Length; i++)
        {
            changePlayerNpcArr[i].ChangeAnimator(changePlayerNpcs[i], changePlayerAdress[i]);
        }
    }
    public void SaveSlotData()
    {
        SlotData data = _slotData;
        _slotData.animatorAddressList = changePlayerAdress.ToList();

        if (data != null)
        {
            string path = "Assets/Resources/Scriptable/Lobby/SlotData.asset";

            if (AssetDatabase.LoadAssetAtPath<SlotData>(path) == null)
            {
                AssetDatabase.CreateAsset(data, path);
            }
            else
            {
                EditorUtility.SetDirty(data); // 데이터 변경을 Unity에 알림
            }

            AssetDatabase.SaveAssets(); // 변경 사항 저장
        }
        else
        {
            Debug.LogError("SlotData is null");
        }

    }
}
