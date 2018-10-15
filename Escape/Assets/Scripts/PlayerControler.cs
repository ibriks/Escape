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
	private int jump = 0;
	
	void Main ()
	{
		// Preventing mobile devices going in to sleep mode 
		Screen.sleepTimeout = SleepTimeout.NeverSleep;
	}
	 
	void Update()
	{

//		if (Input.GetKey("escape"))
//			Application.Quit();
		Move (Input.GetAxis("Horizontal"),jump);

		if (Input.GetKeyDown(KeyCode.Escape))
			Application.Quit(); 
//		Move (hInput,jump);
	}
	
	void Move (float horizontal, int up)
	{
		CharacterController controller = GetComponent<CharacterController>();
		if (controller.isGrounded) {
			moveDirection = new Vector3(horizontal, 0, 0);
			moveDirection = transform.TransformDirection(moveDirection);
			moveDirection *= speed;
			if (Input.GetButton("Jump")) up++;
			if (up != 0)
			{
				moveDirection.y = jumpSpeed;
				up = 0;
			}
		}
		if (moveDirection.y>0)
		{
			moveDirection.x = horizontal;
			moveDirection.x *= speed;
		}
		moveDirection.y -= gravity * Time.deltaTime;
		controller.Move(moveDirection * Time.deltaTime);
	}
	
	public void StartMoving(float horizontal)
	{
		hInput = horizontal;
	}

	public void StartJump(int up)
	{
		Move (hInput, up);
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