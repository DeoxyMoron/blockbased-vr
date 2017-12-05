using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockManager : MonoBehaviour {
	public GameObject motionblock_moveforward;
	public GameObject motionblock_turnRight;
	public GameObject motionblock_turnLeft;
	public GameObject spawnNode;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

	public void foo(){
		Debug.Log("WOWEE");
	}
	public void SpawnMoveForward(int d){
		GameObject clone;
		clone = Instantiate(motionblock_moveforward, spawnNode.transform.position, spawnNode.transform.rotation) as GameObject;
		clone.GetComponent<BlockInfo>().opcode = "move_forward";
		clone.GetComponent<BlockInfo>().setParam(d);
	}
	public void SpawnTurnRight(int d){
		GameObject clone;
		clone = Instantiate(motionblock_turnRight, spawnNode.transform.position, spawnNode.transform.rotation) as GameObject;
		clone.GetComponent<BlockInfo>().opcode = "turn_right";
		clone.GetComponent<BlockInfo>().setParam(d);
	}

	public void SpawnTurnLeft(int d){
		GameObject clone;
		clone = Instantiate(motionblock_turnLeft, spawnNode.transform.position, spawnNode.transform.rotation) as GameObject;
		clone.GetComponent<BlockInfo>().opcode = "turn_left";
		clone.GetComponent<BlockInfo>().setParam(d);
	}
}
