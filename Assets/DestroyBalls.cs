using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DestroyBalls : MonoBehaviour {
	
	GenerateBalls generateBallsScript;
	// Use this for initialization
	void Start () {
	
		generateBallsScript = this.GetComponent<GenerateBalls>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		List<GameObject> toDestroy = new List<GameObject>(); // list of all balls we need to destory
		List<GameObject> toUpdate = new List<GameObject>(); // list of all balls we need to update references on
		
		// get all the dead balls
		foreach(GameObject ball in generateBallsScript.getBallList())
		{
			if(ball.GetComponent<BallScript>().canRotate == true) // we only care about the balls that have stopped moving, and that we can now rotate around
			{
				if(ball.GetComponent<BallScript>().isDead)
				{
					toDestroy.Add(ball);
				}
			}
		}
		
		//remove these balls from the BallManager's ballList
		foreach(GameObject ballToDestroy in toDestroy)
		{
			generateBallsScript.removeBall(ballToDestroy);	
		}
		
		//compile a list of balls that need their lists updated
		foreach(GameObject ballToDestroy in toDestroy)
		{
			foreach(GameObject touchingBall in ballToDestroy.GetComponent<BallScript>().touchingList) // for everything this ball is touching
			{
				if(!toDestroy.Contains(touchingBall)) // if this ball isn't one of the balls we're destroying
				{
					toUpdate.Add(touchingBall); // add it to the list of items we need to update the references on
				}
			}	
		}
		
		//remove all the references
		foreach(GameObject ballToUpdate in toUpdate)
		{
			foreach(GameObject ballToDestroy in toDestroy)
			{
				if(ballToUpdate.GetComponent<BallScript>().touchingList.Contains(ballToDestroy))
				{
					ballToUpdate.GetComponent<BallScript>().touchingList.Remove(ballToDestroy);
				}
				
				// We might have to do this for sameColorList as well
				/*
				if(ballToUpdate.GetComponent<BallScript>().sameColorList.Contains(ballToDestroy))
				{
					ballToUpdate.GetComponent<BallScript>().sameColorList.Remove(ballToDestroy);
				}
				*/
			}
		}
		
		// delete the balls
		List<GameObject> deleteList = toDestroy;
		for(int i = 0 ; i < toDestroy.Count; i++) // increment through to Destroy
		{
			Object.Destroy(deleteList[i]);	//delete from deleteList
		}
		
		
	}
}
