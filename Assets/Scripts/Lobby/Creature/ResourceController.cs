using UnityEngine;

public class ResourceController : MonoBehaviour
{
    [SerializeField] private float _healthChangeDelay = .5f;

    private BaseController _baseController;
    private StatHandler _statHandler;

    private float _timeSinceLastChange = float.MaxValue;

    public float CurrentHealth { get; private set; }
    public float MaxHealth { get; private set; }
    public float MoveSpeed { get; private set; }

    private void Awake()
    {
        _statHandler = GetComponent<StatHandler>();
        _baseController = GetComponent<BaseController>();
        MaxHealth = _statHandler.Health;
        MoveSpeed = _statHandler.Speed;
    }

    private void Start()
    {
        ResetResource();
    }

    private void Update()
    {
        if (_timeSinceLastChange < _healthChangeDelay)
        {
            _timeSinceLastChange += Time.deltaTime;
        }
    }
    public void ResetResource()
    {
        CurrentHealth = _statHandler.Health;
        MoveSpeed = _statHandler.Speed;
    }
    public bool ChangeHealth(float change)
    {
        if (change == 0 || _timeSinceLastChange < _healthChangeDelay)
        {
            return false;
        }

        _timeSinceLastChange = 0f;
        CurrentHealth += change;
        CurrentHealth = CurrentHealth > MaxHealth ? MaxHealth : CurrentHealth;
        CurrentHealth = CurrentHealth < 0 ? 0 : CurrentHealth;

        if (CurrentHealth <= 0f)
        {
            _baseController.State = Define.State.Die;
        }

        return true;
    }


    public void UpdateHealth(float value)
    {
        MaxHealth += value;
        CurrentHealth += value;
    }
    public void UpdateSpeend(float value)
    {
        MoveSpeed += value;
    }

}