using UnityEngine;

public class ResourceController : MonoBehaviour
{
    [SerializeField] private float healthChangeDelay = .5f;

    private BaseController baseController;
    private StatHandler statHandler;

    private float timeSinceLastChange = float.MaxValue;

    public float CurrentHealth { get; private set; }
    public float MaxHealth { get; private set; }
    public float MoveSpeed { get; private set; }

    private void Awake()
    {
        statHandler = GetComponent<StatHandler>();
        baseController = GetComponent<BaseController>();
        MaxHealth = statHandler.Health;
        MoveSpeed = statHandler.Speed;
    }

    private void Start()
    {
        ResetResource();
    }

    private void Update()
    {
        if (timeSinceLastChange < healthChangeDelay)
        {
            timeSinceLastChange += Time.deltaTime;
            if (timeSinceLastChange >= healthChangeDelay)
            {
             //   animationHandler.InvincibilityEnd();
            }
        }
    }
    public void ResetResource()
    {
        CurrentHealth = statHandler.Health;
        MoveSpeed = statHandler.Speed;
    }
    public bool ChangeHealth(float change)
    {
        if (change == 0 || timeSinceLastChange < healthChangeDelay)
        {
            return false;
        }

        timeSinceLastChange = 0f;
        CurrentHealth += change;
        CurrentHealth = CurrentHealth > MaxHealth ? MaxHealth : CurrentHealth;
        CurrentHealth = CurrentHealth < 0 ? 0 : CurrentHealth;


        if (change < 0)
        {
            //danimationHandler.Damage();

        }

        if (CurrentHealth <= 0f)
        {
            baseController.State = Define.State.Die;
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