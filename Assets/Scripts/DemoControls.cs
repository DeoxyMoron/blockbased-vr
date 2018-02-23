using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoControls : MonoBehaviour {
	public TurtleMovement turtle;
	public GameObject[] blocks;
	public GameObject greenFlagBlock;
	private Vector3 startPos;
	private Quaternion startOri;

	void Start () {
		greenFlagBlock = GameObject.Find("Green Flag Block");
		startPos = greenFlagBlock.transform.position;
		startOri = greenFlagBlock.transform.rotation;
	}

	void Update () {
		if(Input.GetKeyDown(KeyCode.R)) {
			turtle.reset();
		}
		if(Input.GetKeyDown(KeyCode.Escape)) {

			greenFlagBlock.transform.position = startPos;
			greenFlagBlock.transform.rotation = startOri;

			turtle.reset();

			blocks = GameObject.FindGameObjectsWithTag("Block");


			//Debug.Log(blocks);

			foreach (GameObject obj in blocks) {
				if (obj.name == greenFlagBlock.name){
					//Debug.Log("wwwww");
				} else {
					Destroy(obj);
				}
			}
		}
	}
}
