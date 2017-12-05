using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScriptTest : MonoBehaviour {
	public TurtleMovement turtle;
	public BlockManager blockmanager;
	public GameObject[] blocks;
	public string[] instructions;

	GameObject greenFlag;

	// Use this for initialization

	void Awake(){
			//turtle = GetComponent<TurtleMovement>();

	}
	void Start () {
			//string[] instructions = ["asf","asdf"]
			blockmanager = GetComponent<BlockManager>();
	}

	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.R)){
			//Find Green Flag and look for block children
			 greenFlag = GameObject.Find("Green Flag");
			 	//Debug.Log(greenFlag.transform.childCount);
				foreach (Transform child in greenFlag.transform){
					if (child.tag == "Block"){
						Debug.Log(child.name);
					}
				}
		    // Put whatever you want in the brackets.
				//for (int i = 0; i < greenFlag.transform.childCount; i++){

				//}
				//public Component[] childBlocks = GetComponentsInChildren<

			 //TextMesh t = testblock.AddComponent(	)
		}

		if(Input.GetKeyDown(KeyCode.B)){
			turtle.startTurnTest();
		}

		if(Input.GetKeyDown(KeyCode.J)){
			turtle.foobar("wow","wee");
		}

		if(Input.GetKeyDown(KeyCode.Y)){
			//Step 5 increment
			greenFlag = GameObject.Find("Green Flag");
			BlockInfo[] infoChildren;
			infoChildren = greenFlag.GetComponentsInChildren<BlockInfo>();
			foreach (BlockInfo b in infoChildren){
				b.increment();
			}
			//Debug.Log(infoChildren[0]);
		}
				if(Input.GetKeyDown(KeyCode.G))
		{
				//Step 3: Read Blocks and Run Sequence
				Codeblock_Model[] blockChildren;
				greenFlag = GameObject.Find("Green Flag");
				blockChildren = greenFlag.GetComponentsInChildren<Codeblock_Model>();
				Debug.Log(blockChildren);
				foreach (Codeblock_Model c in blockChildren){
					Debug.Log(c.name);
					//HOW TO GET THE INFO FROM THE MODEL
				}

				//Check all block info children of green flag and save list
				List<string> opcodeList = new List<string>();
				List<string> paramList = new List<string>();

				BlockInfo[] infoChildren;
				infoChildren = greenFlag.GetComponentsInChildren<BlockInfo>();

				foreach (BlockInfo b in infoChildren){

					turtle.addStep(b.opcode,b.param);

					//Debug.Log(b.opcode);
					//opcodeList.Add(b.opcode);
					//paramList.Add(b.getParam());
					//Debug.Log(b.param);
					//HOW TO GET THE INFO FROM THE MODEL
				}

				turtle.runSequence();
				//foreach (string b in opcodeList){
				//	Debug.Log(b);
				//}
				//turtle.startClassicRoutine();
				//turtle.readInstructions(opcodeList, paramList);
		}

		if(Input.GetKeyDown(KeyCode.F)){
			//Step 2 Parent BLocks
			blocks = GameObject.FindGameObjectsWithTag("Block");
			foreach(GameObject block in blocks){
				Debug.Log(block.name);
			}

			//blocks[2].GetComponent<Codeblock>().foo();
			blocks[1].GetComponent<Codeblock_Model>().setParent(blocks[0]);
			blocks[2].GetComponent<Codeblock_Model>().setParent(blocks[1]);
			blocks[3].GetComponent<Codeblock_Model>().setParent(blocks[2]);
		}

		if(Input.GetKeyDown(KeyCode.H)){
			//Step 1 Add blocks
			blockmanager.foo();

			blockmanager.SpawnMoveForward(5);
			blockmanager.SpawnTurnLeft(180);
			blockmanager.SpawnTurnRight(180);
			//blocks[1].GetComponentsInChildren<Transform>();
		}

		if(Input.GetKeyDown(KeyCode.R)){

		}

	}
}
