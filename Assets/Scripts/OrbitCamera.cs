using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitCamera : MonoBehaviour {

	[SerializeField]
	private Transform target;

	public float rotSpeed = 1.5f;

	private float _rotY;
	private float _rotX;
	private Vector3 _offset;

	// Start is called before the first frame update
	void Start() {
		_rotY = transform.eulerAngles.y;
		//These are both global positions, so offset will become a local difference.
		_offset = target.position - transform.position;
		_rotX = 0;
	}
	
	void LateUpdate() {
		float horInput = Input.GetAxis("Horizontal");
		float vertInput = Input.GetAxis("Vertical");

		if (horInput != 0) {
			_rotY += horInput * rotSpeed;
		} else {
			_rotY += Input.GetAxis("Mouse X") * rotSpeed * 3;
		}

		if (vertInput != 0) {
			/*
			if (Mathf.Abs(_rotX) < 1) {
				_rotX = 0;
			} if (_rotX < 0) {
				_rotX++;
			} else if (_rotX > 0) {
				_rotX--;
			}
			*/
		} else if (Input.GetAxis("Mouse Y") != 0) {
			_rotX += Input.GetAxis("Mouse Y") * rotSpeed * Time.deltaTime * 100;
			_rotX = Mathf.Clamp(_rotX, -40, 40);
		}
		
		//Get a Quaternion/Matrix 4 representation of the rotation transform.
		Quaternion rotation = Quaternion.Euler(_rotX, _rotY, 0);

		//The offset point is rotated around its own 0, 0 origin.
		//Then it is applied to the global position.
		transform.position = target.position - (rotation * _offset);

		//This method is awesome.
		transform.LookAt(target);
	}
}