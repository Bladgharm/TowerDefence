using UnityEngine;
using VRTK;

public class WeaponReloadPart : VRTK_InteractableObject
{
    private ConfigurableJoint _joint;
    private TestWeapon _weaponComponent;

    private Vector3 _baseOffset;

    protected override void Awake()
    {
        base.Awake();
        _joint = GetComponent<ConfigurableJoint>();
        _baseOffset = transform.localPosition;
    }

    private void Start()
    {
        _weaponComponent = GetComponentInParent<TestWeapon>();
    }

    protected override void Update()
    {
        base.Update();
        if (IsGrabbed() && _joint != null)
        {
            if (transform.localPosition.y < _baseOffset.y - _joint.linearLimit.limit)
            {
                Debug.Log("Reload weapon");
                if (_weaponComponent != null)
                {
                    _weaponComponent.ReloadWeapon();
                }
            }
        }
    }
}
