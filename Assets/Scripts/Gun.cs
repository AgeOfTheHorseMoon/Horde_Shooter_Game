using CodeMonkey.Utils;
using System;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private GunDataSO _gunDataSO;
    [SerializeField] private LayerMask _enemyLayer;

    private Transform _projectileSpawnPosition;
    private GameObject _projectilePrefab;
    private GameObject _gunPrefab;
    private float _projectilesPerMinute = 100;
    private int _damage = 10;
    private float _range = 3;
    private float _projectileSpeed;
    private float _projectileLifeTime;

    private SpriteRenderer _gunSprite;
    private bool _flippedLeft;

    private float _nextShot = 0;

    private Transform _target;

    private void Start()
    {
        //if (_gunDataSO == null && gameObject.activeInHierarchy)
        //{
        //    gameObject.SetActive(false);
        //}
    }

    private void OnEnable()
    {
        if (_gunDataSO != null)
        {
            InitializeTheGun();
        }
    }

    public void EquipGun(GunDataSO gunDataSO)
    {
        //if (!gameObject.activeInHierarchy)
        //{
        //    gameObject.SetActive(true);
        //}
        _gunDataSO = gunDataSO;
        InitializeTheGun();
    }

    private void InitializeTheGun()
    {
        // Gun Attributes
        _damage = _gunDataSO.Damage;
        _range = _gunDataSO.Range;

        // Prefab and Transform Initializing
        _gunPrefab = Instantiate(_gunDataSO.GunPrefab, this.transform, false);
        _gunSprite = _gunPrefab.transform.Find("Graphic").GetComponent<SpriteRenderer>();
        _projectilePrefab = _gunDataSO.ProjectilePrefab;
        _projectileSpawnPosition = _gunPrefab.transform.Find("Projectile Spawn Position"); // Use the same in prefabs
        
        // Projectile Attributes
        _projectilesPerMinute = _gunDataSO.ProjectilesPerMinute;
        _projectileSpeed = _gunDataSO.ProjectileSpeed;
        _projectileLifeTime = _gunDataSO.ProjectileLifeTime;

        //Debug.Log("Damage: " + _damage +
        //        "\nRange: " + _range +
        //        "\nProjectilesPerMinute: " + _projectilesPerMinute +
        //        "\nProjectileSpawnPointPos: " + _projectileSpawnPosition +
        //        "\nProjectile Lifetime: " + _projectileLifeTime + " Speed: " + _projectileSpeed
        //    );
    }

    private void Update()
    {
        if (_gunDataSO != null)
        {
            CheckAndShoot();    
        }
    }

    private void CheckAndShoot()
    {
        if (_target == null)
        {
            FindTarget();
        }
        else
        {
            CheckDistance();
            LookAtTarget();
        }
    }

    private void LookAtTarget()
    {
        Vector3 lookDirection = (_target.position - transform.position).normalized;
        Vector3 angle = new Vector3(0, 0, UtilsClass.GetAngleFromVector(lookDirection));
        if (angle.z > 90 && angle.z < 270)
        {   // if in left semicircle
            _flippedLeft = true;
            _gunSprite.flipY = _flippedLeft;
        }
        else
        {   // Flip Right
            _flippedLeft = false;
            _gunSprite.flipY = _flippedLeft;
        }
        transform.eulerAngles = angle;
    }

    private void CheckDistance()
    {
        if ((_target.position - transform.position).magnitude > _range)
        {
            FindTarget();
        }
        else
        {
            Shoot();
        }
    }

    private void FindTarget()
    {
        // Search for a target
        Collider2D targetCandidate = Physics2D.OverlapCircle(transform.position, _range, _enemyLayer);
        if (targetCandidate != null)
        {
            if (targetCandidate.TryGetComponent(out Transform enemyTransform))
            {
                _target = enemyTransform;
            }
        }
    }

    private void Shoot()
    {
        if (Time.time >= _nextShot)
        {
            _nextShot = Time.time + (60 / _projectilesPerMinute);

            Projectile projectile = Instantiate(_projectilePrefab, _projectileSpawnPosition.position, Quaternion.identity).GetComponent<Projectile>();
            Vector3 shootDirection = (_target.position - transform.position).normalized;
            projectile.Setup(shootDirection, _damage, _projectileSpeed, _projectileLifeTime); // Change

        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, _range);
    }
}