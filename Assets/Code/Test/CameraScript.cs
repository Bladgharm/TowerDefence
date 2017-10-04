using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
	public Transform target;
    [SerializeField]
    private bool _useFixedUpdate = false;

    private float _lerpSpeed = 5;

    private void Start()
    {
        var playerComponent = target.GetComponent<PlayerScript>();
        _useFixedUpdate = playerComponent.UseRigidbody;
    }

	void Update()
    {
        if (!_useFixedUpdate)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, target.rotation, _lerpSpeed * Time.deltaTime);
            transform.position = Vector3.Lerp(transform.position, target.position, _lerpSpeed * Time.deltaTime);
        }
		
	}

    void FixedUpdate()
    {
        if (_useFixedUpdate)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, target.rotation, _lerpSpeed * Time.fixedDeltaTime);
            transform.position = Vector3.Lerp(transform.position, target.position, _lerpSpeed * Time.fixedDeltaTime);
        }
    }
}
