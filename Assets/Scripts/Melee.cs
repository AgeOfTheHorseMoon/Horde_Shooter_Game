using UnityEngine;

public class Melee : MonoBehaviour
{
    //    [SerializeField] float _stabSpeed = 10f;
    //    [SerializeField] float _returnSpeed = 10f;

    //    [SerializeField] bool _isStabbing = false;

    //    [SerializeField] bool _canAttack = true;

    //    Transform _enemy;
    //    Vector3 _initialPosition;

    //    private void Start()
    //    {
    //        _initialPosition = transform.localPosition;
    //        _enemy = FindObjectOfType<Enemy>().transform;
    //    }

    //    private void Update()
    //    {

    //        if (Input.GetButtonDown("Fire2")) // Replace "Fire2" with your input for the sword attack
    //        {
    //            if (_canAttack)
    //            {
    //                PerformStab();
    //            }
    //        }

    //        if (_isStabbing)
    //        {
    //            Stab();
    //        }
    //        else
    //        {
    //            ReturnToInitialPosition();
    //        }

    //        if (!_isStabbing && Vector3.Distance(transform.localPosition, _initialPosition) < 0.01f)
    //        {
    //            _canAttack = true;
    //        }
    //    }

    //    public void Stab()
    //    {

    //        Vector3 targetPosition = _enemy.position;
    //        Vector3 direction = targetPosition - transform.position;
    //        direction.Normalize();

    //        transform.Translate(direction * _stabSpeed * Time.deltaTime);

    //        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
    //        {
    //            _isStabbing = false;
    //        }
    //    }

    //    public void PerformStab()
    //    {
    //        _isStabbing = true;
    //        _canAttack = false;
    //    }

    //    private void ReturnToInitialPosition()
    //    {
    //        transform.localPosition = Vector3.MoveTowards(transform.localPosition, _initialPosition, _returnSpeed * Time.deltaTime);
    //    }
}
