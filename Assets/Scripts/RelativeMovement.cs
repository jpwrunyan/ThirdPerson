using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Animator))]
public class RelativeMovement : MonoBehaviour {

	[SerializeField]
	private Transform target;

	public float rotSpeed = 15.0f;
	public float moveSpeed = 6.0f;

	public float jumpSpeed = 15.0f;
	public float gravity = -9.8f;
	public float terminalVelocity = -10.0f;
	public float minFall = -1.5f;

	public float pushForce = 3.0f;

	private float _vertSpeed;

	private CharacterController _charController;
	private ControllerColliderHit _contact;
	private Animator _animator;
	void Start() {
		//Initialize vertical speed to the the minimum falling speed at the start.
		_vertSpeed = minFall;
		_charController = GetComponent<CharacterController>();
		_animator = GetComponent<Animator>();
	}

	private void OnControllerColliderHit(ControllerColliderHit hit) {
		_contact = hit;

		Rigidbody body = hit.collider.attachedRigidbody;
		if (body != null && !body.isKinematic) {
			body.velocity = hit.moveDirection * pushForce;
		}
	}

	void Update() {
		Vector3 movement = Vector3.zero;
		float horInput = Input.GetAxis("Horizontal");
		float vertInput = Input.GetAxis("Vertical");
		if (horInput != 0 || vertInput != 0) {
			Quaternion tmp = target.rotation;

			//Because the camera (target) itself is looking down at this object, 
			//it's necessary to temporarily extract only the y rotation and calculate based on that. 
			//The x and z rotation can change therefore without affecting movement on the x-z plane.
			target.eulerAngles = new Vector3(0, target.eulerAngles.y, 0);

			//Alternatively, we could have perhaps cloned a transformation.

			movement.x = horInput * moveSpeed;
			movement.z = vertInput * moveSpeed;
			movement = Vector3.ClampMagnitude(movement, moveSpeed);
			movement = target.TransformDirection(movement);

			//Restore the other rotations back to the target now that we're done using its y-only rotation.
			target.rotation = tmp;

			Quaternion direction = Quaternion.LookRotation(movement);

			transform.rotation = Quaternion.Lerp(transform.rotation, direction, rotSpeed * Time.deltaTime);
		}

		_animator.SetFloat("speed", movement.sqrMagnitude);

		transform.parent = null;

		bool hitGround = false;
		RaycastHit hit;
		if (_vertSpeed < 0 && Physics.Raycast(transform.position, Vector3.down, out hit)) {
			float check = (_charController.height + _charController.radius) / 1.9f;
			hitGround = hit.distance <= check;
		}
		
		if (hitGround) {
			if (Input.GetButtonDown("Jump")) {
				_vertSpeed = jumpSpeed;
			} else {
				_vertSpeed = minFall;
				_animator.SetBool("jumping", false);
			}
		} else {
			_vertSpeed += gravity * 5 * Time.deltaTime;
			if (_vertSpeed < terminalVelocity) {
				_vertSpeed = terminalVelocity;
			}
			if (_contact != null) {
				_animator.SetBool("jumping", true);	
			}
			if (_charController.isGrounded) {
				if (Vector3.Dot(movement, _contact.normal) < 0) {
					movement = _contact.normal * moveSpeed;
				} else {
					movement += _contact.normal * moveSpeed;
				}
			}
		}

		/*
		if (_charController.isGrounded) {
			if (Input.GetButtonDown("Jump")) {
				_vertSpeed = jumpSpeed;
			} else {
				_vertSpeed = minFall;
			}
		} else {
			_vertSpeed += gravity * 5 * Time.deltaTime;
			if (_vertSpeed < terminalVelocity) {
				_vertSpeed = terminalVelocity;
			}
		}
		*/

		movement.y = _vertSpeed;

		movement *= Time.deltaTime;
		_charController.Move(movement);

	}
}