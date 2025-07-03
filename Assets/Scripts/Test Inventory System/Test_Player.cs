using System;
using UnityEngine;

[
    RequireComponent(typeof(Rigidbody2D)),
    RequireComponent(typeof(Test_PlayerAnimator))
]
public class Test_Player : MonoBehaviour, IDamageable
{
    public static Test_Player Instance { get; private set; }

    // This will become a SO based upgradable stat system later
    [SerializeField] private int _maxHealth = 100;

    [SerializeField] private float _speed = 5f;

    private HealthSystem _healthSystem;

    private Vector2 _playerInputs;

    private Rigidbody2D _rigidbody;
    private Test_PlayerAnimator _animator;

    // Gun Slots
    [SerializeField] private Transform[] availableGunSlots; // May automatically search using transform.find later

    [SerializeField] private GunLoadoutDataSO startingLoadoutData;
    private GunLoadout _gunLoadout;

    private void Awake()
    {
        // Singleton Pattern
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }

        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Test_PlayerAnimator>();
    }
    private void Start()
    {
        // Health System
        _healthSystem = new HealthSystem(_maxHealth);
        _healthSystem.OnDead += UnitKilled;

        // Gun Loadout
        if (startingLoadoutData != null) // decide what to do if loadout not assigned
        {
            _gunLoadout = new GunLoadout(startingLoadoutData, availableGunSlots); // First time loadout equipping
        }
    }

    private void Update()
    {
        InputManagement();
    }

    private void FixedUpdate()
    {
        Movement();
    }

    private void InputManagement()
    {
        _playerInputs = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }

    public Vector2 GetPlayerInputs()
    {
        return _playerInputs;
    }

    private void Movement()
    {
        _rigidbody.velocity = _playerInputs * _speed;
        _animator.Flip(_playerInputs.x);
    }

    public void TakeDamage(int damageAmount)
    {
        _healthSystem.TakeDamage(damageAmount);
    }

    private void UnitKilled(object sender, EventArgs e)
    {
        _healthSystem.OnDead -= UnitKilled;
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }
}