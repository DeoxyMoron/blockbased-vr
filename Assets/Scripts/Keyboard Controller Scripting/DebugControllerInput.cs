using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugControllerInput : MonoBehaviour {

    private BlockManager blockmanager;

    //Object Collision
    private GameObject collidingObject;
    private GameObject objectInHand;

    // Use this for initialization
    void Awake () {
		blockmanager = GetComponent<BlockManager>();
	}

    private void SetCollidingObject(Collider col) {
        // 1
        if (collidingObject || !col.GetComponent<Rigidbody>()) {
            return;
        }
        // 2
        collidingObject = col.gameObject;
    }
    public void OnTriggerEnter(Collider other) {
        SetCollidingObject(other);
    }

    // 2
    public void OnTriggerStay(Collider other) {
        SetCollidingObject(other);
    }

    // 3
    public void OnTriggerExit(Collider other) {
        if (!collidingObject) {
            return;
        }

        collidingObject = null;
    }

    private void GrabObject() {
        // 1
        objectInHand = collidingObject;
        collidingObject = null;
        // 2
        var joint = AddFixedJoint();
        joint.connectedBody = objectInHand.GetComponent<Rigidbody>();

        //Check if holding block
        if (objectInHand.tag == "Block") {

            //EXECUTE BLOCK FINDING OTHER BLOCKS
            objectInHand.GetComponent<Codeblock_Model>().setGrabbed(true);
        }

    }

    // 3
    private FixedJoint AddFixedJoint() {
        FixedJoint fx = gameObject.AddComponent<FixedJoint>();
        fx.breakForce = 20000;
        fx.breakTorque = 20000;
        return fx;
    }


    private void ReleaseObject() {
        // 1
        if (GetComponent<FixedJoint>()) {
            // 2
            GetComponent<FixedJoint>().connectedBody = null;
            Destroy(GetComponent<FixedJoint>());
            // 3 - Sets object velocity to the controller's velocity
            objectInHand.GetComponent<Rigidbody>().velocity = GetComponent<Rigidbody>().velocity;
            objectInHand.GetComponent<Rigidbody>().angularVelocity = GetComponent<Rigidbody>().angularVelocity;
        }
        // 4
        objectInHand.GetComponent<Codeblock_Model>().setGrabbed(false);
        objectInHand = null;
    }


    void Update () {
        
        // Input mapping for object grabbing.

        if (Input.GetKeyDown("space")) {
            if (collidingObject) {
                GrabObject();
            }
        }

        if (Input.GetKeyUp("space")) {
            if (objectInHand) {
                ReleaseObject();
            }
        }


        // Input mapping for block creation
   
        if (Input.GetKeyDown("1")) {
            //Debug.Log("Up");
            if (collidingObject) {

            }
            else {
                //Create a move forward motion block.
                blockmanager.SpawnMoveForward(1);
            }
        }

        else if (Input.GetKeyDown(KeyCode.Backspace)) {
            //Debug.Log("Down");
            if (collidingObject) {
                if (collidingObject.name != "Green Flag Block") {
                    Destroy(collidingObject);
                }
            }
        }

        else if (Input.GetKeyDown("2")) {
            //Debug.Log("Right");
            if (collidingObject) {
                collidingObject.GetComponent<BlockInfo>().increment();
            }
            else {
                blockmanager.SpawnTurnRight(90);
            }
        }

        else if (Input.GetKeyDown("3")) {
            //Debug.Log("Left");
            if (collidingObject) {
                collidingObject.GetComponent<BlockInfo>().decrement();
            }
            else {
                blockmanager.SpawnTurnLeft(90);
            }
        }

        //Alternate inputs for increasing and decreasing parameter values:
        else if (Input.GetKeyDown("=")) {
            if (collidingObject) {
                collidingObject.GetComponent<BlockInfo>().increment();
            }
        }

        else if (Input.GetKeyDown("-")) {
            if (collidingObject) {
                collidingObject.GetComponent<BlockInfo>().decrement();
            }
        }
    }
}
