using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define
{
    public enum State
    {
        Die,
        Moving,
        Idle,
        Skill,
        Jump,
    }
    public enum MiniGameState
    {
        None,
        Init,
        InGame,
        End,
    }
    public enum Dir
    {
        None,
        Up,
        Down,
        Left,
        Right,
        LeftDown,
        RightDown,
        LeftUp,
        LeftUpDown,
    }
    public enum DialogueKey
    {
        Close,
        Click,
        FailBest,
        SuccBest,
     
    }
}
