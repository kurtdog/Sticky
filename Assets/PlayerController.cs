using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	
	
	public GameObject StickyBall;
	
	
	private Vector3 mouseDownPos;
	private Vector3 mouseUpPos;
	
	private Vector3 centerToDownPos; // vector from the center of the screen to the mouseDownPos
	private Vector3 torqueVector; // vector from the mouseDownPos to the currentMousePos or MouseUpPost
	
	public float torque;
	
	// Use this for initialization
	void Start () {
		mouseDownPos = new Vector3(0,0,0);
		mouseUpPos = new Vector3(0,0,0);
		centerToDownPos = new Vector3(0,0,0);
		torque = 0;
	}
	
	// Update is called once per frame
	void Update () {
	
		handleInput();
		
		
		// using RHR, if the cross product is negative, we should have negative torque
		if(Vector3.Cross(centerToDownPos,torqueVector).z < 0)
		{
			torque = -torqueVector.magnitude;
			
		}
		else
		{
			torque = torqueVector.magnitude;	
		}
		
		Debug.Log("Torque: " + torque);
	}
	
	void handleInput()
	{
		
		// mouse press event
		if(Input.GetMouseButtonDown(0) == true) 
		{
			mouseDownPos = Input.mousePosition;	
			centerToDownPos = mouseDownPos;  //it's really mouseDown - center, but since center is zero, we don't the -center part
			
		}
		// mouse hold event
		if(Input.GetMouseButton(0) == true)
		{
			torqueVector = Input.mousePosition - mouseDownPos;
			
		}
		
		//mouse release event
		if(Input.GetMouseButtonUp(0) == true)
		{
			mouseUpPos = Input.mousePosition;
			torqueVector = mouseUpPos - mouseDownPos;
		}
		
		
		
	}
}
