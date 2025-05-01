using System;
using UnityEditor;
using UnityEngine;

public class ChangePlayerNpc : BaseNpc
{

    SlotDataManager _manager;
    [SerializeField] private int _index;

    private string _adress;
    private Animator _animator;

    private void Awake()
    {
        _manager = GetComponentInParent<SlotDataManager>();
        _animator = GetComponent<Animator>();

    }

    public void ChangeAnimator(RuntimeAnimatorController controller, string adress)
    {
        _animator.runtimeAnimatorController = controller;
        _adress = adress;
    }
    
    public override void InteractiveNPC()
    {
        base.InteractiveNPC();
        ChangePlayerAnimator();
    }

    void ChangePlayerAnimator()
    {
        PlayerController player = LobbyScene.Instance._playerPrefab.GetComponent<PlayerController>();
        string adress = _manager.animatorAddresses[_index];
        _manager.animatorAddresses[_index] = player.animatorAddresses[0];
        player.SetAnimator(_animator.runtimeAnimatorController, adress, out RuntimeAnimatorController output);
        _animator.runtimeAnimatorController = output;
        _manager.Save();
    }

}
