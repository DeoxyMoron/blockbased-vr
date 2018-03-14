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
    private GameObject collidingBlockControl; 

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

    if (col.tag == "BlockSpawn")
    {
        collidingBlockControl = col.gameObject;
        collidingObject = null;
        return;
    }
        // 1
    else if (collidingObject || !col.GetComponent<Rigidbody>())
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
        Controller.TriggerHapticPulse(1000);
        //Debug.Log(collidingObject);
    }

	// 2
	public void OnTriggerStay(Collider other)
	{
	    SetCollidingObject(other);
	}

	// 3
	public void OnTriggerExit(Collider other)
	{

        collidingBlockControl = null;

        if (!collidingObject)
	    {
	        return;
	    }


        collidingObject = null;
        

    }

	private void GrabObject()
	{

        VibrateController(.05f, 1500);
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


    public void VibrateController(float timer, ushort amount)
    {
        StartCoroutine(VibrateControllerContinuous(timer, amount));
    }


    IEnumerator VibrateControllerContinuous(float timer, ushort amount)
    {
        float pulse_length = 0.5f;
        bool saw_tooth_right = true;
        // if true the pulse will happen in a sawtooth pattern like this /|/|/|/|
        // else it will happen opposite like this |\|\|\|\
        while (timer > 0)
        {
            if (saw_tooth_right) { Controller.TriggerHapticPulse((ushort)((pulse_length - (timer % pulse_length)) * (float)amount)); }
            else { Controller.TriggerHapticPulse((ushort)((timer % pulse_length) * (float)amount)); }
            Controller.TriggerHapticPulse(amount);
            timer -= Time.deltaTime;
            yield return null;
        }
        yield break;
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

           if (collidingBlockControl)
            {
                
                if (collidingBlockControl.name== "BlockSpawnMoveForward")
                {
                    blockmanager.SpawnMoveForward(1);

                }

                else if (collidingBlockControl.name == "BlockSpawnTurnRight")
                {
                    blockmanager.SpawnTurnRight(90);
                }

                else if (collidingBlockControl.name == "BlockSpawnTurnLeft")
                {
                    blockmanager.SpawnTurnLeft(90);
                }
                else if (collidingBlockControl.name == "BlockDelete")
                {
                    blockmanager.Clear();
                }

                collidingBlockControl = null;
            }
            collidingBlockControl = null;

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
					//blockmanager.SpawnMoveForward(1);
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
					//blockmanager.SpawnTurnRight(90);
				}

			} else if (touchpad.x<-0.6f){
				Debug.Log("Left");
				if (collidingObject){
					collidingObject.GetComponent<BlockInfo>().decrement();
				}
				else {
					//blockmanager.SpawnTurnLeft(90);
				}

			}
		}


	}
}
