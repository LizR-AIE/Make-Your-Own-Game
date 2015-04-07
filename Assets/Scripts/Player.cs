using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour 
{
	Rigidbody2D rigidBody;
	Animator animator;
	public float movementSpeed;



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
		if (Input.GetAxis ("Horizontal") != 0.0f) {
			rigidBody.AddForce(new Vector2(Input.GetAxis ("Horizontal") * movementSpeed, 0.0f));
		}

		animator.SetFloat("Speed", Mathf.Abs(Input.GetAxis("Horizontal")));

		if (Input.GetAxis ("Jump") != 0.0f) {
			animator.SetBool ("Jumping", true);
		} else {
			animator.SetBool ("Jumping", false);
		}
		// End Player Update
		//--------------------------------
	}
}
