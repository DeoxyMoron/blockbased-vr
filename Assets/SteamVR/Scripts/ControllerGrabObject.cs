using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerGrabObject : MonoBehaviour {

	public BlockManager blockmanager;

	private SteamVR_TrackedObject trackedObj;
	// 1
	private GameObject collidingObject;
	// 2
	private GameObject objectInHand;

	private SteamVR_Controller.Device Controller
	{
	    get { return SteamVR_Controller.Input((int)trackedObj.index); }
	}

	void Awake()
	{
	    trackedObj = GetComponent<SteamVR_TrackedObject>();
			blockmanager = GetComponent<BlockManager>();
	}

	private void SetCollidingObject(Collider col)
	{
    // 1
    if (collidingObject || !col.GetComponent<Rigidbody>())
    {
        return;
    }
    // 2
    collidingObject = col.gameObject;
	}

	// 1
	public void OnTriggerEnter(Collider other)
	{
	    SetCollidingObject(other);
	}

	// 2
	public void OnTriggerStay(Collider other)
	{
	    SetCollidingObject(other);
	}

	// 3
	public void OnTriggerExit(Collider other)
	{
	    if (!collidingObject)
	    {
	        return;
	    }

	    collidingObject = null;
	}

	private void GrabObject()
	{
	    // 1
	    objectInHand = collidingObject;
	    collidingObject = null;
	    // 2
	    var joint = AddFixedJoint();
	    joint.connectedBody = objectInHand.GetComponent<Rigidbody>();

			//Check if holding block
			if (objectInHand.tag == "Block"){

				//EXECUTE BLOCK FINDING OTHER BLOCKS
				objectInHand.GetComponent<Codeblock_Model>().setGrabbed(true);
			}

	}

	// 3
	private FixedJoint AddFixedJoint()
	{
	    FixedJoint fx = gameObject.AddComponent<FixedJoint>();
	    fx.breakForce = 20000;
	    fx.breakTorque = 20000;
	    return fx;
	}
	private void ReleaseObject()
	{
	    // 1
	    if (GetComponent<FixedJoint>())
	    {
	        // 2
	        GetComponent<FixedJoint>().connectedBody = null;
	        Destroy(GetComponent<FixedJoint>());
	        // 3
	        objectInHand.GetComponent<Rigidbody>().velocity = Controller.velocity;
	        objectInHand.GetComponent<Rigidbody>().angularVelocity = Controller.angularVelocity;
	    }
	    // 4
			objectInHand.GetComponent<Codeblock_Model>().setGrabbed(false);
	    objectInHand = null;


	}
	// Update is called once per frame
	void Update () {
		// 1
		if (Controller.GetHairTriggerDown())
		{
		    if (collidingObject)
		    {
		        GrabObject();
		    }
		}

		// 2
		if (Controller.GetHairTriggerUp())
		{
		    if (objectInHand)
		    {
		        ReleaseObject();
		    }
		}
		if(Controller.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad))
		{
			//Spawns Blocks
			Vector2 touchpad = Controller.GetAxis();
			if (touchpad.y > 0.6f){
				Debug.Log("Up");
				if (collidingObject){

				}
				else {
					blockmanager.SpawnMoveForward(1);
				}

			} else if (touchpad.y < -0.6f){
				Debug.Log("Down");
				if (collidingObject){
					if (collidingObject.name != "Green Flag Block"){
						Destroy(collidingObject);
					}

				}
			} else if (touchpad.x>0.6f){
				Debug.Log("Right");
				if (collidingObject){
					collidingObject.GetComponent<BlockInfo>().increment();
				}
				else {
					blockmanager.SpawnTurnRight(90);
				}

			} else if (touchpad.x<-0.6f){
				Debug.Log("Left");
				if (collidingObject){
					collidingObject.GetComponent<BlockInfo>().decrement();
				}
				else {
					blockmanager.SpawnTurnLeft(90);
				}

			}
		}


	}
}
