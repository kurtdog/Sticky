using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	
	

	private float sampleRate; // samples per second
	
	private Vector3 mouseDownPos;
	private Vector3 mouseUpPos;
	private Vector3 samplePos;
	
	private Vector3 torqueVector; // vector from the mouseDownPos to the currentMousePos or MouseUpPost
	
	public float torque;
	
	// Use this for initialization
	void Start () {
		mouseDownPos = new Vector3(0,0,0);
		mouseUpPos = new Vector3(0,0,0);
		samplePos = new Vector3(0,0,0);
		torque = 0;
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
	
		handleInput();
		
	}
	
	
	void handleInput()
	{
		
		//We only want input to work if they started on the bottom half of th escreen
		if(mouseDownPos.y <= Screen.height/2 ) 
		{
			float xDif = 0;
				
			// L mouse press event
			if(Input.GetMouseButtonDown(0) == true) 
			{
				mouseDownPos = Input.mousePosition;	
			}
			
			// R mouse hold event
			if(Input.GetMouseButton(1) == true) 
			{
				//stop all torque
				torqueVector = new Vector3(0,0,0);
				torque = 0;	
			}
			
			// L mouse hold event
			if(Input.GetMouseButton(0) == true)
			{
				torqueVector = Input.mousePosition - samplePos;
				xDif = Input.mousePosition.x - samplePos.x;
				torque = torqueVector.magnitude*10;	
				if(xDif < 0)
				{
					torque = -torqueVector.magnitude*10;	
				}
			}
			
			//L mouse release event
			if(Input.GetMouseButtonUp(0) == true)
			{
				mouseUpPos = Input.mousePosition;
				torqueVector = mouseUpPos - mouseDownPos;
				torque = torqueVector.magnitude;
				
				xDif = mouseUpPos.x - mouseDownPos.x; // difference in mouse position in the x direction
				if(xDif < 0)
				{
					torque = -torqueVector.magnitude;
				}
			}
		}
		
		samplePos = Input.mousePosition; // get the current mouse position
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
		
		}
	}
	
	

}
