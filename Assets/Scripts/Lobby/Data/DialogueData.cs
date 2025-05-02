using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueEntry
{
    public string key;
    [TextArea]
    public string text;
}

[CreateAssetMenu(menuName = "Data/NPCDialogue")]
public class DialogueData : ScriptableObject
{
    public string npcId;
    public List<DialogueEntry> data;
}
