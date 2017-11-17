using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class move : MonoBehaviour {

public float restartLevelDelay = 1f;
public Rigidbody2D playerbody;
private int hp = 3;
private Animator animator;
	// Use this for initialization
	void Start () {
		playerbody = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
	}
	private void Restart() {
		SceneManager.LoadScene (0);
	}
	private void OnDisable(){
		// what we store in the game manager when changing levels
		// GameManager.instance.points = points;
	}
	private void checkIfGameOver(){
		if(hp <= 0){
			//GameManager.instance.GameOver()
		}
	}
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D other) {
		//Check if the tag of the trigger collided with is Exit.
            if(other.tag == "Exit")
            {
                //Invoke the Restart function to start the next level with a delay of restartLevelDelay (default 1 second).
                Invoke ("Restart", restartLevelDelay);
                
                //Disable the player object since level is over.
                enabled = false;
            }
    }	
	void FixedUpdate() {
		if(GameManager.instance.doingSetup) return;
		Vector2 vel = new Vector2(0,0);
		if(Input.GetKey(KeyCode.UpArrow)){
			vel.y = 8;		
			animator.SetTrigger("down");
			//playerbody.transform.position += Vector3.up * Time.deltaTime;
			//Debug.Log("up");
			//playerbody.AddForce(transform.up * 10);
		}
		else if(Input.GetKey(KeyCode.DownArrow)){
			animator.SetTrigger("down");
			vel.y = -8;
			//playerbody.transform.position += Vector3.down * Time.deltaTime;
			//playerbody.AddForce(Vector2.down*5);

		}
		if(Input.GetKey(KeyCode.LeftArrow)){
			animator.SetTrigger("left");
			vel.x = -8;
			//playerbody.velocity = new Vector2(-1,0);
			//playerbody.transform.position += Vector3.left * Time.deltaTime;
			//playerbody.AddForce(Vector3.left);

		}
		else if(Input.GetKey(KeyCode.RightArrow)){
			animator.SetTrigger("right");
			vel.x = 8;
			//playerbody.velocity = new Vector2(1,0);
			//playerbody.transform.position += Vector3.right * Time.deltaTime;
			//playerbody.AddForce(Vector3.right);
		}
		if(vel.x == 0 && vel.y == 0){
			animator.SetTrigger("idle");
		}
		
		playerbody.velocity = vel;
	}
}
