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
            // 로드 완료 후 처리 (예: 캐릭터에 적용)
           _animator.runtimeAnimatorController = _playerData.animatorController;
        });
        LobbyScene.EndGame += SavePlayerData;

        _rigidbody = GetComponent<Rigidbody2D>();
        if (_rigidbody == null)
            Debug.Log("rb2D가 없습니다!");
    }

    public void SetAnimator(RuntimeAnimatorController controller, string adress, out string outAdress)
    {
        outAdress = _playerData.animatorAddress;
        _animator.runtimeAnimatorController = controller;
        _playerData.animatorAddress = adress;
    }


    // 데이터 저장, 추후 스크립트 분리 필요
    public void SavePlayerData()
    {
        PlayerData data = _playerData;
        // PlayerData를 에셋으로 저장하기 전에, 새 인스턴스를 만든 후 저장
        if (data != null)
        {
            string path = "Assets/Resources/Scriptable/Lobby/PlayerData.asset";

            if (AssetDatabase.LoadAssetAtPath<PlayerData>(path) == null)
            {
                AssetDatabase.CreateAsset(data, path);
            }
            else
            {
                EditorUtility.SetDirty(data); // 데이터 변경을 Unity에 알림
            }

            AssetDatabase.SaveAssets(); // 변경 사항 저장
        }
        else
        {
            Debug.LogError("PlayerData is null");
        }
    }



    // 플레이어 정지
    protected override void UpdateIdle()
    {
        base.UpdateIdle();
    }

    // 플레이어 이동
    protected override void UpdateMoving()
    {
        base.UpdateMoving();

        // 방향에 따른 이동 조작
        _rigidbody.velocity = _moveDir * _movePower;
    }

    void OnAttack(InputValue inputValue)
    {
        _isAttacking = true;
    }


    // 플레이어 조작 - 추후 새 스크립트를 팔지 고민
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


    // 점프 입력
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

            if (_height <= 0.01f) // 여유를 줌
            {
                _height = 0f;
                _verticalSpeed = 0f;
                _isGrounded = true;
            }
        }

        _spriteRenderer.transform.localPosition = new Vector3(0, _height, 0);
    }

    // 쉬프트를 이용한 방향 전환
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

