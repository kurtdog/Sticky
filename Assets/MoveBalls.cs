using UnityEngine;
using System.Collections;

public class MoveBalls : MonoBehaviour {
	
	
	public GameObject StickyBall;
	PlayerController playerController;
	GenerateBalls generateBalls;
	
	// Use this for initialization
	void Start () {
		playerController = StickyBall.GetComponent<PlayerController>();
		generateBalls = this.GetComponent<GenerateBalls>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
	
		// rotate all the balls based on the playerController's torque
		foreach(GameObject ball in generateBalls.getBallList())
		{
			if(ball.GetComponent<BallScript>().canRotate)
			{
				Vector3 up = this.transform.up;
				float torque = playerController.torque;
				Rigidbody body = ball.rigidbody;
				
				ball.rigidbody.AddTorque(StickyBall.transform.up*playerController.torque);
			}
			
		}
	}
}
