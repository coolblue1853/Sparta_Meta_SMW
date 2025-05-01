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
    public List<Animator> targetAnimators;
    public List<string> animatorAddresses;
    private Action onEndGameSave;
    private Action onMoveSceneSave;
    private void OnEnable()
    {
        DataManager.instance.Load(targetAnimators, savePath);
    }

    private void Awake()
    {
     //   AddInvoke();
    }
    private void OnDisable()
    {
     //   DeleteInvoke();
    }
     void AddInvoke()
    {
        onEndGameSave = () => DataManager.instance.Save(animatorAddresses, savePath);
        onMoveSceneSave = () => DataManager.instance.Save(animatorAddresses, savePath);

        LobbyScene.EndGameSave += onEndGameSave;
        LobbyScene.MoveSceneSave += onMoveSceneSave;
    }
    void DeleteInvoke()
    {
        LobbyScene.EndGameSave -= onEndGameSave;
        LobbyScene.MoveSceneSave -= onMoveSceneSave;
    }

    public void Save()
    {
        DataManager.instance.Save(animatorAddresses, savePath);
      //  DeleteInvoke();
      //  AddInvoke();
    }
}
