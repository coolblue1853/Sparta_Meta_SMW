using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    Animator _animator = null;
    Rigidbody2D _rigidbody = null;

    public float FlapForce = 6f;
    public float ForwardSpeed = 3f;

    bool _isFlap = false;

    public bool GodMode = false;
    MiniGameScene _gameScene;

    void Start()
    {
        _rigidbody = transform.GetComponent<Rigidbody2D>();
        _gameScene = MiniGameScene.Instance;
    }

    public void FixedUpdate()
    {
        if (_gameScene.State != Define.MiniGameState.InGame)
            return;

        Vector3 velocity = _rigidbody.velocity;
        velocity.x = ForwardSpeed;

        if (_isFlap)
        {
            velocity.y += FlapForce;
            _isFlap = false;
        }

        _rigidbody.velocity = velocity;

        float angle = Mathf.Clamp((_rigidbody.velocity.y * 10f), -90, 90);
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    void OnClick()
    {
        if(_gameScene.State == Define.MiniGameState.Init)
        {
            _gameScene.GameStart();
        }
        else if (_gameScene.State == Define.MiniGameState.InGame)
        {
            _isFlap = true;
        }
        else if (_gameScene.State == Define.MiniGameState.End && _gameScene.IsCanGoLobby)
        {
            _gameScene.GoLobby();
        }
    }


    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Obstacle") || collision.transform.CompareTag("BackGround"))
        {
            if (GodMode)
                return;

            if (_gameScene.State == Define.MiniGameState.End)
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