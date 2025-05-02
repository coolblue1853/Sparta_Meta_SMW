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
    private string savePath => Application.persistentDataPath + "/ChangePlayer.json";
    public Animator[] targetAnimators;
    public string[] animatorAddresses;
    private Action onEndGameSave;
    private Action onMoveSceneSave;
    private void OnEnable()
    {
        animatorAddresses = DataManager.instance.Load(targetAnimators, savePath);
    }

    public void Save()
    {
        DataManager.instance.Save(animatorAddresses, savePath);
    }
}
