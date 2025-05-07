using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class PlayerController : BaseController
{
    PlayerStatHandler _statHandler;
    [SerializeField] private bool _justDirMove = false;
    [SerializeField] private GameObject _cusor;

    private Vector2 _cusorOffset = new Vector2(-0.1f, -0.25f);

    [SerializeField] private string[] animatorAddresses;
    [SerializeField] private Animator[] targetAnimators;
    private Vector3 _ridePivot = new Vector3(0.1f, 0.94f);
    private float _rideSpeed = 2f;

    private string savePath => Application.persistentDataPath + "/Player.json";
    protected override void Init()
    {

        _statHandler = GetComponent<PlayerStatHandler>();
        _rigidbody = GetComponent<Rigidbody2D>();
        if (_rigidbody == null)
            Debug.Log("rb2D가 없습니다!");
    }
    private void OnEnable()
    {

        animatorAddresses = DataManager.instance.Load(targetAnimators, savePath);
    }

    public void SetAnimator(RuntimeAnimatorController controller,string adress,
        out RuntimeAnimatorController output, out string outAdress)
    {
        outAdress = animatorAddresses[0];
        output = _animator.runtimeAnimatorController;
        _animator.runtimeAnimatorController = controller;
        animatorAddresses[0] = adress;
        DataManager.instance.Save(animatorAddresses, savePath);
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
        _rigidbody.velocity = _moveDir * _resourceController.MoveSpeed;
    }

    void OnAttack(InputValue inputValue)
    {
        _isAttacking = true;
    }

    protected override void UpdateJump()
    {
        if (!_isGrounded)
        {
            _verticalSpeed -= _statHandler.Gravity * Time.deltaTime;
            _height += _verticalSpeed * Time.deltaTime;

            if (_height <= 0.02f) // 여유를 줌
            {
                _height = 0f;
                _verticalSpeed = 0f;
                _isGrounded = true;
                State = Define.State.Moving;
            }
        }

        _sprites.transform.localPosition = new Vector3(0, _height, 0);
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
            {
                _mainSpriteRenderer.flipX = false;
                _weaponRnderer.flipX = false;
                _rideSpriteRenderer.flipX = false;
            }

            if (input.x < 0)
            {
                _mainSpriteRenderer.flipX = true;
                _weaponRnderer.flipX = true;
                _rideSpriteRenderer.flipX = true;
            }


            if (!_justDirMove)
                State = Define.State.Moving;
        }
        else
        {
            SetIdle();
        }

        _moveDir = input;
    }

    // 점프 입력
    void OnJump(InputValue inputValue)
    {
        if (_isGrounded)
        {
            State = Define.State.Jump;
            _verticalSpeed = _statHandler.JumpPower;
            _isGrounded = false;
           // UpdateJump();
        }
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
    void OnRide(InputValue inputValue)
    {
        if (_rideSpriteRenderer.gameObject.activeSelf == false)
        {
            _mainSpriteRenderer.transform.localPosition = _ridePivot;
            _weaponPivot.transform.localPosition = _ridePivot;
            _rideSpriteRenderer.gameObject.SetActive(true);
            //이동속도 증감 부분. 추후 개선 필요
            _resourceController.UpdateSpeend(_rideSpeed);
        }
        else
        {
            _mainSpriteRenderer.transform.localPosition = Vector3.zero;
            _weaponPivot.transform.localPosition = Vector3.zero;
            _rideSpriteRenderer.gameObject.SetActive(false);
            _resourceController.UpdateSpeend(-_rideSpeed);
        }
    }

    protected override void UpdateDie()
    {
        _resourceController.ResetResource();
        LobbyScene.Instance.EndLogueGame();
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
    void SetIdle()
    {
        State = Define.State.Idle;
        _rigidbody.velocity = Vector3.zero;
    }
}

