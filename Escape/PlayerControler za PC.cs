using UnityEngine;
using System.Collections;

public class PlayerControler : MonoBehaviour {

	public float speed = 6.0F;
	public float jumpSpeed = 8.0F;
	public float gravity = 20.0F;
	private Vector3 moveDirection = Vector3.zero;
	public GameObject Door;
	public GameObject NextLevelButton;
	private float hInput = 0;
	
	void Main ()
	{
		// Preventing mobile devices going in to sleep mode 
		Screen.sleepTimeout = SleepTimeout.NeverSleep;
	}
	
	void Update()
	{
		if (Input.GetKey("escape"))
			Application.Quit();
		Move (Input.GetAxisRaw("Horizontal"));

		#if UNITY_ANDROID
		if (Input.GetKeyDown(KeyCode.Escape))
			Application.Quit(); 
//		Move (hInput);
		#endif
	}

	void Move (float horizontal)
	{
		CharacterController controller = GetComponent<CharacterController>();
		if (controller.isGrounded) {
			moveDirection = new Vector3(horizontal, 0, 0);
			moveDirection = transform.TransformDirection(moveDirection);
			moveDirection *= speed;
			if (Input.GetButton("Jump"))
				Jump();
		}
		moveDirection.y -= gravity * Time.deltaTime;
		controller.Move(moveDirection * Time.deltaTime);
	}

	public void Jump()
	{
		moveDirection.y = jumpSpeed;
	}

	public void StartMoving(float horizonal)
	{
		hInput = horizonal;
	}
	
	void  OnTriggerEnter(Collider other) 
	{
		if (other.gameObject.tag == "Key")
		{
			Door.SetActive(true);
			other.gameObject.SetActive (false);
		}
		if (other.gameObject.tag == "Door")
		{
			NextLevelButton.SetActive(true);
			speed=0;
		}
	}

}		