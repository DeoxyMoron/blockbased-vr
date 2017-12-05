using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockInfo : MonoBehaviour {

	public string opcode;
	public int param;
	public TextMesh textObj;
	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

	public void setOpcode(string op){
		opcode = op;
	}

	public void setParam(int p){
		param = p;
		textObj.text = p.ToString();
	}

	public string getParam(){
		return param.ToString();
	}

	public void increment(){
		if (opcode == "move_forward")
		{
			if (param<10){
				setParam(param+=1);
			}
		}
		else
		{
			//For turning opcodes
			if (param<180){
				//180 max value
				setParam(param+=10);
			}

		}
	}

	public void decrement(){
		if (opcode == "move_forward")
		{
			if (param > -10){
				setParam(param-=1);
			}

		}
		else
		{
				//For turning opcodes
			if (param > -180){
				setParam(param-=10);
			}

		}
	}

	public string getOpcode(){
		return opcode;
	}


}
