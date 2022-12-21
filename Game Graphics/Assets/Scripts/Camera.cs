using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent( typeof(Camera) )]

public class Camera : MonoBehaviour {

	[SerializeField]
	public float moveSpeed = 75;

	[SerializeField]
	public float sprintSpeed = 10;

	[SerializeField]
	public float mouseSensitivity = 1;

	public float dampingCoefficient = 5;

	Vector3 velocity;


	void Start()
	{
		Cursor.lockState = CursorLockMode.Locked;
	}


	void Update()
	{
		velocity = Vector3.Lerp(velocity, Vector3.zero, dampingCoefficient * Time.deltaTime);
		transform.position += velocity * Time.deltaTime;
		velocity += GetAccelerationVector() * Time.deltaTime;
		UpdateLook();
	}


	void UpdateLook()
	{
		Vector2 look = mouseSensitivity * new Vector2(Input.GetAxis("Mouse X"), -Input.GetAxis("Mouse Y"));
		Quaternion r = transform.rotation;
		Quaternion h = Quaternion.AngleAxis(look.x, Vector3.up);
		Quaternion v = Quaternion.AngleAxis(look.y, Vector3.right);
		transform.rotation = h * r * v;
	}


	Vector3 GetAccelerationVector()
	{
		Vector3 moveInput = default;

		void AddMovement(KeyCode key, Vector3 dir)
		{
			if(Input.GetKey(key))
				moveInput += dir;
		}

		AddMovement(KeyCode.W, Vector3.forward);
		AddMovement(KeyCode.S, Vector3.back);
		AddMovement(KeyCode.D, Vector3.right);
		AddMovement(KeyCode.A, Vector3.left);
		AddMovement(KeyCode.Space, Vector3.up);
		AddMovement(KeyCode.LeftControl, Vector3.down);

		Vector3 direction = transform.TransformVector(moveInput.normalized);

		if(Input.GetKey(KeyCode.LeftShift))
			return direction * (moveSpeed * sprintSpeed);
		return direction * moveSpeed;
	}
}
