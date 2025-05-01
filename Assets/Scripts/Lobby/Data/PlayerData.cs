using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Player")]

public class PlayerData : ScriptableObject
{
    public string animatorAddress;
    [HideInInspector]
    public RuntimeAnimatorController animatorController;
}