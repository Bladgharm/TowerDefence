using UnityEngine;

namespace Assets.Code
{
    public class TestTurret : MonoBehaviour, ITurret
    {
        [SerializeField]
        private TurretLevel[] _turretLevels;
        [SerializeField]
        private Transform _target;
        [SerializeField]
        private Transform _turretHead;

        private int _currentLevel = 0;

        private float _lastShootTime = 0f;

        public TurretLevel[] TurretLevels
        {
            get
            {
                return _turretLevels;
            }
        }

        public bool CanUpgrade
        {
            get { return _turretLevels != null && _turretLevels.Length > 0 && _currentLevel < _turretLevels.Length-1; }
        }

        public Transform Target
        {
            get { return _target; }
        }

        private void Start()
        {
            Debug.Log("Current turret level:" + (_currentLevel + 1));
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                if (CanUpgrade)
                {
                    _currentLevel++;
                }
                Debug.Log("Current turret level:"+(_currentLevel+1));
            }
            ProcessTurret();
        }

        private void ProcessTurret()
        {
            if (_target != null)
            {
                float distance = Vector3.Distance(transform.position, _target.position);
                if (distance < _turretLevels[_currentLevel].AttackRange)
                {
                    var direction = _target.position - transform.position;
                    var q = Quaternion.LookRotation(direction);
                    _turretHead.localRotation = Quaternion.Lerp(_turretHead.localRotation, q, 10 * Time.deltaTime);
                    Shoot();
                }
                else
                {
                    _turretHead.localRotation = Quaternion.Lerp(_turretHead.localRotation, Quaternion.identity, 10 * Time.deltaTime);
                }
            }
        }

        private void Shoot()
        {
            if (_lastShootTime <= 0)
            {
                _lastShootTime = _turretLevels[_currentLevel].AttackSpeed;
                Debug.Log("Shoot");
            }
            else
            {
                _lastShootTime -= Time.deltaTime;
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, _turretLevels[_currentLevel].AttackRange);
        }
    }
}