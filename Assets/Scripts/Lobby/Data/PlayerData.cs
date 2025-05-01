using System.Collections.Generic;

[System.Serializable]
public class AnimatorSaveData
{
    public string animatorAddress;
}

[System.Serializable]
public class AnimatorSaveDataList
{
    public List<AnimatorSaveData> animators = new List<AnimatorSaveData>();
}