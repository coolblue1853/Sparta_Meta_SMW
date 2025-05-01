using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DialogueNpc : BaseNpc
{

    [SerializeField] private DialogueData _dialogueData;
    private NpcDialogueController _dialogueController;

    private void Awake()
    {
        _dialogueController = GetComponent<NpcDialogueController>();
    }

    public override void InteractiveNPC()
    {
        base.InteractiveNPC();
        ShowTextBox(Define.DialogueKey.Click.ToString());
    }
    //근처 상호작용 UI
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerRange"))
        {
            ShowTextBox(Define.DialogueKey.Close.ToString());
        }
    }
    public void ShowTextBox(string key)
    {
        _dialogueController.ShowText(GetDialogueByKey(key));
    }
    string GetDialogueByKey(string key)
    {
        return _dialogueData.data.FirstOrDefault(d => d.key == key)?.text;
    }

}
