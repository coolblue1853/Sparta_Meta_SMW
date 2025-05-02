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
    private string _savePath => Application.persistentDataPath + "/ChangePlayer.json";
    public Animator[] TargetAnimators;
    public string[] AnimatorAddresses;
    private Action _onEndGameSave;
    private Action _onMoveSceneSave;
    private void OnEnable()
    {
        AnimatorAddresses = DataManager.Instance.Load(TargetAnimators, _savePath);
    }

    public void Save()
    {
        DataManager.Instance.Save(AnimatorAddresses, _savePath);
    }
}
