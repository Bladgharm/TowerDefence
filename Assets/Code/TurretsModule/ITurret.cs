using UnityEngine;

namespace Assets.Code
{
    public interface ITurret
    {
        TurretLevel[] TurretLevels { get;}
        bool CanUpgrade { get;}

        Transform Target { get; }
    }

    [System.Serializable]
    public class TurretLevel
    {
        public float Health;
        public float CurrentHealth;
        public float AttackSpeed;
        public float AttackRange;
    }
}