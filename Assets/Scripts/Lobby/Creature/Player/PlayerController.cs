using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class PlayerController : BaseController
{
    [SerializeField] private PlayerData _playerData; 

    [SerializeField]
    private bool _justDirMove = false;

    [SerializeField]
    private GameObject _cusor;
    private Vector2 _cusorOffset = new Vector2(-0.1f, -0.25f);

    protected override void Init()
    {
        PlayerDataLoader.LoadAnimator(_playerData, () =>
        {
            // �ε� �Ϸ� �� ó�� (��: ĳ���Ϳ� ����)
           _animator.runtimeAnimatorController = _playerData.animatorController;
        });
        LobbyScene.EndGame += SavePlayerData;

        _rigidbody = GetComponent<Rigidbody2D>();
        if (_rigidbody == null)
            Debug.Log("rb2D�� �����ϴ�!");
    }

    public void SetAnimator(RuntimeAnimatorController controller, string adress, out string outAdress)
    {
        outAdress = _playerData.animatorAddress;
        _animator.runtimeAnimatorController = controller;
        _playerData.animatorAddress = adress;
    }


    // ������ ����, ���� ��ũ��Ʈ �и� �ʿ�
    public void SavePlayerData()
    {
        PlayerData data = _playerData;
        // PlayerData�� �������� �����ϱ� ����, �� �ν��Ͻ��� ���� �� ����
        if (data != null)
        {
            string path = "Assets/Resources/Scriptable/Lobby/PlayerData.asset";

            if (AssetDatabase.LoadAssetAtPath<PlayerData>(path) == null)
            {
                AssetDatabase.CreateAsset(data, path);
            }
            else
            {
                EditorUtility.SetDirty(data); // ������ ������ Unity�� �˸�
            }

            AssetDatabase.SaveAssets(); // ���� ���� ����
        }
        else
        {
            Debug.LogError("PlayerData is null");
        }
    }



    // �÷��̾� ����
    protected override void UpdateIdle()
    {
        base.UpdateIdle();
    }

    // �÷��̾� �̵�
    protected override void UpdateMoving()
    {
        base.UpdateMoving();

        // ���⿡ ���� �̵� ����
        _rigidbody.velocity = _moveDir * _movePower;
    }

    void OnAttack(InputValue inputValue)
    {
        _isAttacking = true;
    }


    // �÷��̾� ���� - ���� �� ��ũ��Ʈ�� ���� ���
    void OnMove(InputValue inputValue)
    {
        Vector2 input = inputValue.Get<Vector2>();
        if (input != Vector2.zero )
        {
            ChangeCusor(input);
            _attackDir = input;
            if (input.x > 0)
                _spriteRenderer.flipX = false;
            if (input.x < 0)
                _spriteRenderer.flipX = true;

            if (!_justDirMove)
                State = Define.State.Moving;
        }
        else
        {
            SetIdle();
        }

        _moveDir = input;
    }

    void ChangeCusor(Vector2 input)
    {
        Vector2 offeset = _cusorOffset;
        if (input.x < 0)
            offeset.x = Mathf.Abs(_cusorOffset.x);

        _cusor.transform.localPosition = input + offeset;
        
        float angle = Mathf.Atan2(input.y, input.x) * Mathf.Rad2Deg;
        _cusor.transform.rotation = Quaternion.Euler(0, 0, angle);
    }


    // ���� �Է�
    void OnJump(InputValue inputValue)
    {
        if (_isGrounded)
        {
            State = Define.State.Jump;
            _verticalSpeed = _jumpPower;
            _isGrounded = false;
            UpdateJump();
        }
    }

    protected override void UpdateJump()
    {
        if (!_isGrounded)
        {
            _verticalSpeed -= _gravity* Time.deltaTime;
            _height += _verticalSpeed * Time.deltaTime;

            if (_height <= 0.01f) // ������ ��
            {
                _height = 0f;
                _verticalSpeed = 0f;
                _isGrounded = true;
            }
        }

        _spriteRenderer.transform.localPosition = new Vector3(0, _height, 0);
    }

    // ����Ʈ�� �̿��� ���� ��ȯ
    void OnShift(InputValue inputValue)
    {
        float input = inputValue.Get<float> ();

        if (input == 1.0f)
        {
            SetIdle();
            _justDirMove = true;
        }
        else
        {
            _justDirMove = false;
            if (_moveDir != Vector2.zero)
            {
                State = Define.State.Moving;
            }
        }

    }

    void SetIdle()
    {
        State = Define.State.Idle;
        _rigidbody.velocity = Vector3.zero;
    }

}

