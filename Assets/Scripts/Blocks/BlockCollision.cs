using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockCollision : MonoBehaviour {

	private GameObject collidingObject;
	// Use this for initialization
	void Start () {
		//Debug.Log("I'm attached to " + gameObject.name);
		//Debug.Log("I am a child of " + transform.parent.gameObject.name);
	}

	private void SetCollidingObject(Collider col)
	{
    // 1
    if (col.tag == "BlockTrigger")
    {
    	collidingObject = col.gameObject;
    }
    // 2

	}

	// 1
	public void OnTriggerEnter(Collider other)
	{
			SetCollidingObject(other);

	}

	// 2
	public void OnTriggerStay(Collider other)
	{

	}

	// 3
	public void OnTriggerExit(Collider other)
	{

	}
	// Update is called once per frame
	void Update () {
		if(collidingObject){
			Debug.Log(collidingObject.tag);
		}
	}
}
