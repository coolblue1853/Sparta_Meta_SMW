using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Slot")]

public class SlotData : ScriptableObject
{
    public List<string> animatorAddressList;
    [HideInInspector]
    public List<RuntimeAnimatorController> animatorControllerList;
}