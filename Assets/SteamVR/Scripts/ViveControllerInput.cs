using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViveControllerInput : MonoBehaviour {


	//public BlockManager blockmanager;

	// 1
	private SteamVR_TrackedObject trackedObj;
	// 2
	private SteamVR_Controller.Device Controller
	{
	    get { return SteamVR_Controller.Input((int)trackedObj.index); }
	}
	void Awake()
	{
	    trackedObj = GetComponent<SteamVR_TrackedObject>();
			//blockmanager = GetComponent<BlockManager>();
	}
	// Update is called once per frame
	void Update () {
		// 1
		if (Controller.GetAxis() != Vector2.zero)
		{
		    //Debug.Log(gameObject.name + Controller.GetAxis());
		}

		// 2
		if (Controller.GetHairTriggerDown())
		{

		    //Debug.Log(gameObject.name + " Trigger Press");
		}

		// 3
		if (Controller.GetHairTriggerUp())
		{
		    //Debug.Log(gameObject.name + " Trigger Release");
		}

		// 4
		if (Controller.GetPressDown(SteamVR_Controller.ButtonMask.Grip))
		{
		    Debug.Log(gameObject.name + " Grip Press");
		}

		// 5
		if (Controller.GetPressUp(SteamVR_Controller.ButtonMask.Grip))
		{
		    Debug.Log(gameObject.name + " Grip Release");
		}

		//6
		/*
		if(Controller.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad)){

			//Spawns Blocks

			Vector2 touchpad = Controller.GetAxis();
			if (touchpad.y > 0.6f){
				Debug.Log("Up");
				blockmanager.SpawnMoveForward(1);
			} else if (touchpad.y < -0.6f){
				Debug.Log("Down");
				//Delete?
			} else if (touchpad.x>0.6f){
				Debug.Log("Right");
				blockmanager.SpawnTurnRight(90);
			} else if (touchpad.x<-0.6f){
				Debug.Log("Left");
				blockmanager.SpawnTurnLeft(90);
			}
		}
		*/

	}
}
