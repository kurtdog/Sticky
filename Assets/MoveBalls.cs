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
				float torque = playerController.torque/2;
				
				//Rigidbody body = ball.rigidbody;
				
				//ball.rigidbody.AddTorque(StickyBall.transform.up*playerController.torque);
				ball.transform.RotateAround(StickyBall.transform.position,Vector3.up,torque*Time.fixedDeltaTime);
			}
			
		}
	}
}
