using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GenerateBalls : MonoBehaviour {
	
	
	public GameObject TheStickyBall;
	public GameObject Ball;
	public float ballsPerSecond;
	public int movementSpeed;
	public int startDistance; // how far away should we generate the balls?
	
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
		ballList = new List<GameObject>();
		waitTime = 1/ballsPerSecond; // the time we should wait before generating another ball.
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
			GameObject ball = (GameObject)Instantiate(Ball, transform.position + offset, Ball.transform.rotation) as GameObject;
			
			lastTime = currentTime; // set the lastTime = currentTime;
			Vector3 movementDirection = TheStickyBall.transform.position - ball.transform.position;
			ball.rigidbody.AddForce(movementSpeed*movementDirection);
			ballList.Add(ball);
		}
		
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
