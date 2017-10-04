using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {
	public float moveSpeed,rotateSpeed;

	protected CharacterController controller;
    protected Rigidbody rigidbody;

	float horAxis,verAxis,xAxis,yAxis;
	//Vector3 moveVector;

    private Vector3 _inputAxises;
    [SerializeField]
    private bool _useRigidbody = false;

    public bool UseRigidbody
    {
        get { return _useRigidbody; }
    }

    void Start () {
		controller = GetComponent<CharacterController> ();
	    rigidbody = GetComponent<Rigidbody>();
	}

	void Update ()
    {
        if(!_useRigidbody)
		    ChangePositionCore ();
	}

    void FixedUpdate()
    {
        if (_useRigidbody)
            ChangePositionCore();
    }

    void ChangePositionCore(){
		//moveVector = Vector3.zero;
		Rotate ();
		Move ();

        //moveVector = transform.TransformVector(moveVector);
        //controller.Move(moveVector * moveSpeed * Time.deltaTime);
    }

	void Move()
    {
		//horAxis = Input.GetAxis ("Horizontal");
		//verAxis = Input.GetAxis ("Vertical");
        
        _inputAxises = new Vector3(Input.GetAxis("Horizontal"),0, Input.GetAxis("Vertical"));

        if (Math.Abs(_inputAxises.magnitude) > 0)
        {
            if (!_useRigidbody)
            {
                controller.Move(_inputAxises * moveSpeed * Time.deltaTime);
            }
            else
            {
                rigidbody.MovePosition(transform.position + transform.TransformVector(_inputAxises * moveSpeed * Time.fixedDeltaTime));
            }
        }

  //      if (horAxis != 0){
		//	moveVector += Vector3.right * horAxis;
		//}

		//if(verAxis != 0){
		//	moveVector += Vector3.forward * verAxis;
		//}
	}

	void Rotate()
    {
		xAxis = Input.GetAxisRaw ("Mouse X");
		yAxis = Input.GetAxisRaw ("Mouse Y");

	    var timeConst = _useRigidbody ? Time.fixedDeltaTime : Time.deltaTime;


        if (xAxis !=0 ){
			transform.rotation = transform.rotation*Quaternion.Euler(0, xAxis * rotateSpeed * timeConst, 0);
		}

		if(yAxis !=0 ){
			Camera.main.transform.rotation = Camera.main.transform.rotation*Quaternion.Euler(-yAxis*rotateSpeed* timeConst, 0,0);
		}
	}
}
