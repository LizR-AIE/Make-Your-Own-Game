using UnityEngine;
using System.Collections;

public class PinkWorm : MonoBehaviour {

	Rigidbody2D rigidBody;
	Animator animator;

	void OnTriggerEnter2D(Collider2D other) {
		
		if (other.tag.ToString() == "Player") {
			this.gameObject.SetActive(false);
		}
	}

	// Use this for initialization
	void Start () {
		rigidBody = (Rigidbody2D)GetComponent (typeof(Rigidbody2D));
		animator = (Animator)GetComponent (typeof(Animator));

		rigidBody.AddForce(new Vector2(-2.0f,0.0f));
	}
	
	// Update is called once per frame
	void Update () {
		if (Mathf.Abs (rigidBody.velocity.x) < 3) {
			if (rigidBody.velocity.x < 0.0f) {
				rigidBody.AddForce (new Vector2 (-4.0f, 0.0f));
			} else {
				rigidBody.AddForce (new Vector2 (4.0f, 0.0f));
			}
		}

		animator.SetFloat ("Speed", Mathf.Abs (rigidBody.velocity.x));
	}
}
