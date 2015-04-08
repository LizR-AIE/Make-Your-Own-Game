using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour 
{
	Rigidbody2D rigidBody;
	Animator animator;
	float movementAmount;
	[SerializeField] float movementSpeed = 1;
	float jumpAmount;
	float groundRadius = 0.1f;
	bool isGrounded = false;
	[SerializeField] LayerMask whatIsGround;

	// Use this for initialization
	void Start () 
	{
		rigidBody = (Rigidbody2D)GetComponent (typeof(Rigidbody2D));
		animator = (Animator)GetComponent (typeof(Animator));
	}
	
	// Update is called once per frame
	void Update () 
	{
		//--------------------------------
		// Player Update
		movementAmount = Input.GetAxis ("Horizontal");
		animator.SetFloat("Speed", Mathf.Abs(movementAmount));

		jumpAmount = Input.GetAxis ("Jump");

		Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, groundRadius, whatIsGround);
		
		isGrounded = false;
		for (int i = 0; i < colliders.Length; i++) {
			if (colliders [i].gameObject != gameObject) {
				isGrounded = true;
			}
		}

		if (isGrounded == false) {
			animator.SetBool ("Jump", true);
		} else {
			animator.SetBool ("Jump", false);
		}

		// End Player Update
		//--------------------------------
	}

	void FixedUpdate()
	{
		if (movementAmount != 0.0f && Mathf.Abs (rigidBody.velocity.x) < 3) {
			rigidBody.AddForce (new Vector2 (movementAmount * movementSpeed, 0.0f));
		}



		if(jumpAmount != 0.0f && isGrounded == true){
			rigidBody.AddForce (new Vector2 (0.0f, 3.0f), ForceMode2D.Impulse);
		}
	}
}
