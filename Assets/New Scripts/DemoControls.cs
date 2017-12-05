using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoControls : MonoBehaviour {
	public TurtleMovement turtle;
	public GameObject[] blocks;
	public GameObject greenFlagBlock;
	private Vector3 startPos;
	private Quaternion startOri;
	// Use this for initialization
	void Start () {
		greenFlagBlock = GameObject.Find("Green Flag Block");
		startPos = greenFlagBlock.transform.position;
		startOri = greenFlagBlock.transform.rotation;
	}

	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.R)){
			turtle.reset();
		}
		if(Input.GetKeyDown("space")){

			greenFlagBlock.transform.position = startPos;
			greenFlagBlock.transform.rotation = startOri;

			turtle.reset();

			blocks = GameObject.FindGameObjectsWithTag("Block");


			//Debug.Log(blocks);

			foreach (GameObject obj in blocks){
				if (obj.name == greenFlagBlock.name){
					Debug.Log("wwwww");
				} else{
					Destroy(obj);
				}
			}
		}
	}
}
