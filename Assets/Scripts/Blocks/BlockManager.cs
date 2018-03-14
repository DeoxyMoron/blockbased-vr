using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockManager : MonoBehaviour {
	public GameObject motionblock_moveforward;
	public GameObject motionblock_turnRight;
	public GameObject motionblock_turnLeft;
	public GameObject spawnNode;

    private GameObject[] blocks;
    private GameObject greenFlagBlock;

    /// <summary>Spawns a "Move Forward" motion block at the spawn node.</summary>
    /// <param name="d"></param>
	public void SpawnMoveForward(int d) {
		GameObject clone;
		clone = Instantiate(motionblock_moveforward, spawnNode.transform.position, spawnNode.transform.rotation) as GameObject;
		clone.GetComponent<BlockInfo>().opcode = "move_forward";
		clone.GetComponent<BlockInfo>().setParam(d);
	}

    /// <summary>
    /// Spawns a "Turn Right" motion block at the spawn node.
    /// </summary>
    /// <param name="d"></param>
	public void SpawnTurnRight(int d) {
		GameObject clone;
		clone = Instantiate(motionblock_turnRight, spawnNode.transform.position, spawnNode.transform.rotation) as GameObject;
		clone.GetComponent<BlockInfo>().opcode = "turn_right";
		clone.GetComponent<BlockInfo>().setParam(d);
	}

    /// <summary>
    /// Spawns a "Turn Left" motion block at the spawn node.
    /// </summary>
    /// <param name="d"></param>
	public void SpawnTurnLeft(int d) {
		GameObject clone;
		clone = Instantiate(motionblock_turnLeft, spawnNode.transform.position, spawnNode.transform.rotation) as GameObject;
		clone.GetComponent<BlockInfo>().opcode = "turn_left";
		clone.GetComponent<BlockInfo>().setParam(d);
	}

    public void Clear()
    {
           
        //Clears all blocks
        blocks = GameObject.FindGameObjectsWithTag("Block");

        greenFlagBlock = GameObject.Find("Green Flag Block");



        //Debug.Log(blocks);

        foreach (GameObject obj in blocks)
        {
            if (obj.name == greenFlagBlock.name)
            {
                //Debug.Log("wwwww");
            }
            else
            {
                Destroy(obj);
            }
        }
    }
}
