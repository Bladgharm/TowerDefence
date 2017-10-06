using TMPro;
using UnityEngine;
using VRTK;

public class TestWeapon : VRTK_InteractableObject
{
    [SerializeField]
    private float _fireRate = 0.1f;
    [SerializeField]
    private float _bulletsSpeed = 10f;
    [SerializeField]
    private int _bulletsPerMag = 30;
    [SerializeField]
    private Transform _bulletsSpawnPoint;
    [SerializeField]
    private GameObject _weaponProjectile;
    [SerializeField]
    private TextMeshProUGUI _bulletsCouner;

    private int _currentBulletsCount = 0;
    private float _fireTimer = 0f;

    protected override void Awake()
    {
        base.Awake();
        _currentBulletsCount = _bulletsPerMag;
    }

    protected override void Update()
    {
        base.Update();
        //if (Input.GetMouseButton(0) && _currentBulletsCount > 0)
        //{
        //    Shoot();
        //}

        if (IsUsing() && _currentBulletsCount > 0)
        {
            Shoot();
        }

        _bulletsCouner.text = _currentBulletsCount.ToString();

        if (_fireTimer < _fireRate)
        {
            _fireTimer += Time.deltaTime;
        }
    }

    private void Shoot()
    {
        if (_fireTimer < _fireRate)
        {
            return;
        }

        _fireTimer = 0f;

        var projectile = Instantiate(_weaponProjectile, _bulletsSpawnPoint.position, Quaternion.identity);
        var projectileComponent = projectile.GetComponent<WeaponProjectile>();
        projectileComponent.Init(_bulletsSpeed, transform.forward);
        _currentBulletsCount--;
    }

    public void ReloadWeapon()
    {
        _currentBulletsCount = _bulletsPerMag;
    }
}