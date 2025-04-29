using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : BaseController
{
    protected override void Init()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        if (_rigidbody == null)
            Debug.Log("rb2D�� �����ϴ�!");


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

    // �÷��̾� ���� - ���� �� ��ũ��Ʈ�� ���� ���
    void OnMove(InputValue inputValue)
    {
        Vector2 input = inputValue.Get<Vector2>();

        if (input != Vector2.zero)
        {
            _state = Define.State.Moving;
        }
        else
        {
            _state = Define.State.Idle;
            _rigidbody.velocity = Vector3.zero;
        }

        _moveDir = input;
    }

    // ���� �Է�
    void OnJump(InputValue inputValue)
    {
        if (_isGrounded)
        {
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

            if(_height <= 0f)
            {
                _height = 0f;
                _verticalSpeed = 0f;
                _isGrounded = true;
            }
        }

        _spriteRenderer.transform.localPosition = new Vector3(0, _height, 0);
    }

}

