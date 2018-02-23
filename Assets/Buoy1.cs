using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buoy1 : MonoBehaviour {
	public float amplitude;          //Set in Inspector 
	public float speed;                  //Set in Inspector 
	public float tempVal;
	public Vector3 tempPos;
	void Start () 
	{
		tempVal = transform.position.y;
	}
	void Update ()
	{        
		tempPos.y = tempVal + amplitude * Mathf.Sin (speed * Time.time);
		transform.position = tempPos;
	}

}