using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueEntry
{
    public string Key;
    [TextArea]
    public string Text;
}

[CreateAssetMenu(menuName = "Data/NPCDialogue")]
public class DialogueData : ScriptableObject
{
    public string NpcId;
    public List<DialogueEntry> Data;
}
