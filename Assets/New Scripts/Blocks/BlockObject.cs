using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockObject : MonoBehaviour {

	//Base class
	//handles movement and snapping

	public string opcode ;

	public float moveSpeed = 15.0f;
	public float rotateSpeed = 10.0f;
	public bool snapped = false;

	public void foo(){
		Debug.Log(transform.name);
	}

	public void setParent(GameObject parentGameObject){
		transform.SetParent(parentGameObject.transform);
		StartCoroutine(InstallPart(parentGameObject));
		snapped = true;
	}

	private FixedJoint AddFixedJoint()
	{
			FixedJoint fx = gameObject.AddComponent<FixedJoint>();
			fx.breakForce = 20000;
			fx.breakTorque = 20000;
			return fx;
	}
	IEnumerator InstallPart(GameObject parentGameObject)
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
