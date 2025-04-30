using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    Animator animator = null;
    Rigidbody2D _rigidbody = null;

    public float flapForce = 6f;
    public float forwardSpeed = 3f;

    bool isFlap = false;

    public bool godMode = false;

    MiniGameScene _gameScene;

    void Start()
    {
        _rigidbody = transform.GetComponent<Rigidbody2D>();
        _gameScene = MiniGameScene.instance;
        /*
        animator = transform.GetComponentInChildren<Animator>();


        if (animator == null)
        {
            Debug.LogError("Not Founded Animator");
        }

        if (_rigidbody == null)
        {
            Debug.LogError("Not Founded Rigidbody");
        }
        */
    }

    public void FixedUpdate()
    {
        if (_gameScene._state != Define.MiniGameState.InGame)
            return;

        Vector3 velocity = _rigidbody.velocity;
        velocity.x = forwardSpeed;

        if (isFlap)
        {
            velocity.y += flapForce;
            isFlap = false;
        }

        _rigidbody.velocity = velocity;

        float angle = Mathf.Clamp((_rigidbody.velocity.y * 10f), -90, 90);
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    void OnClick()
    {
        if(_gameScene._state == Define.MiniGameState.Init)
        {
            _gameScene.GameStart();
        }
        else if (_gameScene._state == Define.MiniGameState.InGame)
        {
            isFlap = true;
        }
        else if (_gameScene._state == Define.MiniGameState.End && _gameScene.isCanGoLobby)
        {
            _gameScene.GoLobby();
        }
    }


    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Obstacle") || collision.transform.CompareTag("BackGround"))
        {
            if (godMode)
                return;

            if (_gameScene._state == Define.MiniGameState.End)
                return;

            _gameScene.GameEnd();
            //  animator.SetInteger("IsDie", 1);

        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Hole"))
        {
            _gameScene.AddScore(1);
        }
    }
}