using System.Collections.Generic;

[System.Serializable]
public class AnimatorSaveData
{
    public string AnimatorAddress;
}

[System.Serializable]
public class AnimatorSaveDataList
{
    public List<AnimatorSaveData> Animators = new List<AnimatorSaveData>();
}