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
        var _chagneAnimator = _animator.runtimeAnimatorController;
        RuntimeAnimatorController temp = _chagneAnimator;
        _chagneAnimator = player._animator.runtimeAnimatorController;


        player.SetAnimator(temp, _adress, out string outAdress);
        _animator.runtimeAnimatorController = _chagneAnimator;
        _manager.changePlayerAdress[_index] = outAdress;
    }

}
