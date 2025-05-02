using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RogueNpc : DialogueNpc
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
         if (collision.CompareTag("PlayerRange"))
        {
            var result = LobbyScene.Instance.ShowRougeResult;
            if (result != -1)
            {
                if(result == 0)
                    ShowTextBox(Define.DialogueKey.FailBest.ToString());
                else if (result == 1)
                    ShowTextBox(Define.DialogueKey.SuccBest.ToString());
            }
            else
            {
                LobbyScene.Instance.ShowRougeResult = -1;
                ShowTextBox(Define.DialogueKey.Close.ToString());
            }
        }
    }
}
