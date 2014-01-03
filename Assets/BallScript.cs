using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BallScript : MonoBehaviour {
	
	
	public GameObject TheStickyBall;
	public GameObject CameraObject;
	public GameObject GameManager;
	private int m_id; // the id of this ball
	public float distanceFromCenter;
	public bool isDead;
	public bool canRotate;
	public Color m_color;
	public List<GameObject> touchingList; // I could prolly just have this be a list of GameObjects, then i don't have to deal with ID's and shit
	public List<GameObject> sameColorList; // list of all the balls I'm touching that have the same color as me
	private int lastTouchingSize; // keep track of hte touching list size, we only want to check if we need to delte, if we're now touching more balls
	
	BallScript ballScript;
	GameManager gameManagerScript;
	
	
	
	public int id
	{
		get{return m_id;}
		set{m_id = value;}
		
	}
	
	public Color color
	{
		get{return m_color;}
		set{m_color = value;}
	}
	
	// Use this for initialization
	void Start () {
		touchingList = new List<GameObject>();
		sameColorList = new List<GameObject>();
		lastTouchingSize = 0;
		distanceFromCenter = 0;
		
		gameManagerScript = GameManager.GetComponent<GameManager>();
		
		if(gameManagerScript.possibleColors.Count > 0)
		{
			m_color = gameManagerScript.getPossibleColor(); // pick one of the possible colors at random
			renderer.material.color = m_color;
		}
		else{
			renderer.material.color = Color.white;
		}
		
		
		isDead = false;
		canRotate = false;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		
		rigidbody.velocity = new Vector3(rigidbody.velocity.x,0,rigidbody.velocity.z);

		if(touchingList.Count > lastTouchingSize)
		{
			primeForDeletion();
			lastTouchingSize = touchingList.Count;
		}
	}
	
	void OnTriggerEnter(Collider col) // on colliding with an object
	{

		//Debug.Log("collision");

		if(col.gameObject.GetComponent<BallScript>() != null)
		{
			//stop moving both balls
			rigidbody.velocity = new Vector3(0,0,0);
			rigidbody.angularVelocity = new Vector3(0,0,0);
			col.gameObject.rigidbody.velocity = new Vector3(0,0,0);
			col.gameObject.rigidbody.angularVelocity = new Vector3(0,0,0);
			col.gameObject.GetComponent<BallScript>().canRotate = true;
			
			
			touchingList.Add(col.gameObject); // we're touching htis ball
			col.gameObject.GetComponent<BallScript>().touchingList.Add(this.gameObject); // and it's touching me
		}
		
		//calculate the distance from the sticky ball, if it's a new max distance, update the maxdistance on the camera script
		Vector3 distance = this.transform.position - TheStickyBall.transform.position;
		distanceFromCenter = distance.magnitude;
		if(distanceFromCenter > CameraObject.GetComponent<CameraScript>().maxDistance)
		{
			CameraObject.GetComponent<CameraScript>().maxDistance = distanceFromCenter;
		}
			
	}
	

	
	void primeForDeletion()
	{
		
		foreach(GameObject ball in touchingList)// for every ball in my touching list, 	
		{
			if(ball != null)
			{
				ballScript = ball.GetComponent<BallScript>();
	
				if( ballScript.color == m_color)//if this ball has the same color as me
				{
					if(!sameColorList.Contains(ball)) // add it to the list, as long as it's not already there
					{
						sameColorList.Add(ball);	
					}
					
				}
			}
		}	
		
		
		if(sameColorList.Count > 1) // if there is more than 1 ball touching me that has the same color, 
		{
			isDead = true; // this ball is dead
			
			foreach(GameObject ball in sameColorList)// the balls touching me are dead
			{	
				ball.GetComponent<BallScript>().isDead = true; 
			}
		}
			
		
	}

	

}
