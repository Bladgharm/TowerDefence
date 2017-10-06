using UnityEngine;

public class WeaponProjectile : MonoBehaviour
{
    [SerializeField]
    private TrailRenderer _trail;

    private float _moveSpeed = 0f;
    private Vector3 _movementVector;

    private float _trailLenght = 0.1f;

    public void Init(float movementSpeed, Vector3 movementVector)
    {
        _moveSpeed = movementSpeed;
        _movementVector = movementVector;
    }

    private void Update()
    {
        transform.Translate(transform.TransformDirection(_movementVector) * _moveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Hit "+other.name);
    }
}