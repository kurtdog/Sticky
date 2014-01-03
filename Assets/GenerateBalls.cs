using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GenerateBalls : MonoBehaviour {
	
	public GameObject GameManager;
	GameManager gameManagerScript;
	
	public GameObject TheStickyBall;
	public GameObject Ball;
	public int startDistance; // how far away should we generate the balls? // accessed by camera
	
	
	private float currentTime;
	private float lastTime;
	private float waitTime;
	private List<GameObject> ballList;
	
	public List<GameObject> getBallList()
	{
		return this.ballList;	
	}
	
	public void removeBall(GameObject ball)
	{
		ballList.Remove(ball);
	}
	
	// Use this for initialization
	void Start () {
	
		currentTime = 0;
		startDistance = 5;
		ballList = new List<GameObject>();
		gameManagerScript = GameManager.GetComponent<GameManager>();
		waitTime = 1/(float)gameManagerScript.ballsPerSecond; // the time we should wait before generating another ball.
	}
	
	// Update is called once per frame
	void FixedUpdate () {
	
		currentTime += Time.fixedDeltaTime;
		
		
		
		makeBalls(); // generate the balls
		deleteBalls(); // delete balls
		
	}
	
	
	/*void OnGUI() {
		
		GUI.color = Color.white; // set the color
		string time = "Time: " + (int)currentTime;
        GUI.Label(new Rect(10, 10, 100, 20),time); // draw text

    }*/
	
	void makeBalls()
	{
		Vector3 offset = getRandomVector();
		if(currentTime - lastTime > waitTime) // if the time past since generating the last ball is > the time we need to wait
		{
			// make another ball
			GameObject ball = (GameObject)Instantiate(Ball, TheStickyBall.transform.position + offset, Ball.transform.rotation) as GameObject;
			//adjust the size of the ball
			float scaleFactor = gameManagerScript.planetScale* getRandomValue();
			ball.transform.localScale = new Vector3(scaleFactor,scaleFactor,scaleFactor);
			lastTime = currentTime; // set the lastTime = currentTime;
			Vector3 movementDirection = TheStickyBall.transform.position - ball.transform.position;// set the movement direction
			ball.transform.forward = -movementDirection; // set the direciton the ball is facing, for the particle effect
			ball.rigidbody.AddForce(gameManagerScript.ballMovementSpeed*movementDirection); // start moving
			ballList.Add(ball);
		}
		
	}
	
	// get a random value between certain values
	float getRandomValue()
	{
		float rand = Random.value;
		
		while(rand < .75)
		{
			rand = Random.value;
		}
		//Debug.Log("rand: " + rand);
		return rand;
	}
	
	void deleteBalls()
	{
		//if 3 balls of the same color are touching, delete them.
		
	}
	
	Vector3 getRandomVector()
	{
		Vector3 randomOffset = new Vector3(Random.insideUnitCircle.x,0,Random.insideUnitCircle.y);
		randomOffset.Normalize();
		randomOffset = randomOffset*startDistance;
		
		
		return randomOffset;
	}
	
	
}
