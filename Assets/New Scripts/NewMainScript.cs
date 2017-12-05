using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewMainScript : MonoBehaviour {

	public BlockManager blockmanager;

	private SteamVR_TrackedObject trackedObj;
	// 2
	private SteamVR_Controller.Device Controller
	{
	    get { return SteamVR_Controller.Input((int)trackedObj.index); }
	}
	void Awake()
	{
	    trackedObj = GetComponent<SteamVR_TrackedObject>();
			blockmanager = GetComponent<BlockManager>();
	}

	void Update(){
		//if (Controller.GetHairTriggerDown()){
		//	Debug.Log("wowza");
		//}
		if(Input.GetKeyDown(KeyCode.H)){
			//Step 1 Add blocks
			blockmanager.SpawnMoveForward(5);
			blockmanager.SpawnTurnLeft(180);
			blockmanager.SpawnTurnRight(180);
			//blocks[1].GetComponentsInChildren<Transform>();
		}
	}

}
