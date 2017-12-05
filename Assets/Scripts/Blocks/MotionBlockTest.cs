using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotionBlockTest : MonoBehaviour {
	public  float moveSpeed = 40.0f;
  public  float rotateSpeed = 90.0f;

	public bool snapped = false;
	public float distance; // distance between enemies and players public Transform target;
	// Use this for initialization

	GameObject parentGameObject;
	void Start () {
		//Debug.Log(FindWhoIsAbove(transform));
		parentGameObject = GameObject.FindWithTag("CodingBoard");
		Debug.Log(parentGameObject.tag);

	}

	void setParent(){
		transform.SetParent(parentGameObject.transform);
		StartCoroutine(InstallPart());
		snapped = true;
	}

	// Update is called once per frame
	void Update () {
		if (!snapped){
			distance = Vector3.Distance(parentGameObject.transform.position, transform.position);
			if(distance<.3){
				setParent();
			}
		}



	}

	//returns the transform who is above the other
	//3rd optional param to define up, in case you're in a world where up changes
	/*
	public static Transform FindWhoIsAbove(Transform a, Transform b, Vector3 up = Vector3.up)
	{
	    return (Vector3.Dot(b.position - a.position, up) <= 0) ? a : b;
	}
*/
	private FixedJoint AddFixedJoint()
	{
			FixedJoint fx = gameObject.AddComponent<FixedJoint>();
			fx.breakForce = 20000;
			fx.breakTorque = 20000;
			return fx;
	}
	IEnumerator InstallPart()
	{
	  while (transform.localPosition != Vector3.down || transform.localRotation != Quaternion.identity)
	  {
	      transform.localPosition = Vector3.MoveTowards(transform.localPosition, Vector3.down, Time.deltaTime * moveSpeed);
	      transform.localRotation = Quaternion.RotateTowards(transform.localRotation, Quaternion.identity, Time.deltaTime * rotateSpeed);
	      yield return new WaitForEndOfFrame();
	  }

		var joint = AddFixedJoint();
		joint.connectedBody = parentGameObject.GetComponent<Rigidbody>();
	}

}
