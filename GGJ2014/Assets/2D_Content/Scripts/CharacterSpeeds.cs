using UnityEngine;
using System.Collections;

public class CharacterSpeeds : MonoBehaviour
{

	public bool jump = false;				// Condition for whether the player should jump.	
	//public AudioClip[] jumpClips;			// Array of clips for when the player jumps.
	public float jumpForce = 1000f;			// Amount of force added when the player jumps.
	private bool grounded = false;			// Whether or not the player is grounded.
	private Animator anim;					// Reference to the player's animator component.
	public bool gamestarted = false;
	public float speedUp = 10;
	public float slowDown = 1;
	private bool isFastHit = false;
	private bool isSlowHit = false;
	public float normalSpeed = 5;
	private float currentSpeed;
	private bool damaged = false;
	public float bounceBackForce = -1000f;


	void Awake()
	{
		anim = GetComponent<Animator>();
	}

	//the moment our character hits the ground we set the grounded to true 
	void OnCollisionEnter2D(Collision2D hit)
	{
		grounded = true;
	}

	//the game start but we set the value of gamestarted to false to set our character s
	void Start()
	{
		gamestarted = false;
		currentSpeed = normalSpeed;
		anim.SetTrigger ("standing");
	}

	void Update()
	{

		if (Input.GetButtonDown ("Jump")) {
			jump = true;
			grounded = false;
		}
		if (Input.GetButtonDown ("Damage")) {
			damaged = true;
		}
		if (Input.GetButtonDown ("Start")) {		
			// If the jump button is pressed and the player is grounded and the character is running forward then the player should jump.					if ((grounded == true) && (gamestarted == true)) {
			jump = false;
			grounded = false;
			gamestarted = true;
			anim.SetTrigger("run");
			// Play a random jump audio clip.
			//int i = Random.Range(0, jumpClips.Length);
			//AudioSource.PlayClipAtPoint(jumpClips[i], transform.position);
		}
			// if the game is set now to start the character will start to run forward in the FixedUpdate
		if (gamestarted == true) {
			if (Input.GetButtonDown ("SlowDown")) {
				isSlowHit = true;
			}
			else if (Input.GetButtonDown ("SpeedUp")) {
				isFastHit = true;
			}
		}
	}

	//everything in the physics we set in the fixupdate 
	void FixedUpdate ()
	{
		// if game is started we move character forward...
		if (gamestarted == true) 
		{
			if (isFastHit == true) 
			{
				currentSpeed = speedUp;
				rigidbody2D.velocity = new Vector2(speedUp, rigidbody2D.velocity.y);
				isFastHit = false;
			}
			else if (isSlowHit == true)
			{
				currentSpeed = slowDown;
				rigidbody2D.velocity = new Vector2(slowDown, rigidbody2D.velocity.y);
				isSlowHit = false;
			}
			rigidbody2D.velocity = new Vector2(currentSpeed, rigidbody2D.velocity.y);
		
			if (damaged == true) {
				anim.SetTrigger("damage");
				rigidbody2D.AddForce(new Vector2(-100000f, bounceBackForce));
				damaged = false;
			}
			// If jump is set to true we are now adding quickly aforce to push the character up
			if(jump == true)
			{
				anim.SetTrigger ("jump");
				// Add a vertical force to the player.
				rigidbody2D.AddForce(new Vector2(0f, jumpForce));
	
				// We set to false otherwise the ridig2D addforce would keep adding force
				jump = false;
				grounded = true;
			}
		}
	}
}
