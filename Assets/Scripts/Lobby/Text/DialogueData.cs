using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Dialogue/NPCDialogue")]
[System.Serializable]
public class DialogueEntry
{
    public string key;
    [TextArea]
    public string text;
}

public class DialogueData : ScriptableObject
{
    public string npcId;
    public List<DialogueEntry> data;
}