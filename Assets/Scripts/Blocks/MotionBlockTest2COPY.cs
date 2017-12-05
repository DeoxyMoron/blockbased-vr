using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotionBlockTest2COPY : MonoBehaviour {



	//For Highlighting
	private Material material;
	private Color normalColor;
	private Color highlightColor;
	private Color intermediateColor;
	private float highlightDuration;
	private bool highlight;

	private GameObject closestObject;
	public GameObject[] blocks;
	public string tag;
	public float distance;
	public float threshold;

	public bool parentable;
	public bool grabbed;

	// Use this for initialization
	void Start () {
		tag = "Block";
		threshold = .4f;
		grabbed = false;

		//Highlighting
		material = GetComponent<MeshRenderer>().material;

		normalColor = material.color;
		highlightColor = new Color(
		normalColor.r * 1.5f,
		normalColor.g * 1.5f,
		normalColor.b * 1.5f
		);

		intermediateColor = normalColor;

		highlight = false;
		highlightDuration = 0f;

	}

	// Update is called once per frame
	void Update () {
		//Notify closest object
		if (grabbed){
			//Executes code when in the grabbed state

			//CHECK to see if another block is close,
			// If it is then sets it to parentable
			GetClosestObject();
		}

		if (parentable){
			//Executes code when in the parentable state
			Debug.Log(transform.name);
			Debug.Log("parentable");
		}

		//Update Highlight
		if (highlight) {
			highlightDuration += Time.deltaTime;
			material.color = Color.green;
			//material.color = Color.Lerp(normalColor, highlightColor, highlightDuration*4);
		} else{
			highlightDuration += Time.deltaTime;
			material.color = Color.Lerp(intermediateColor, normalColor, highlightDuration*4);
		}
	}

	public void setHighlighted(bool b){
		//Handles highlighting
		if (b){
			//Add Highlight
			highlight = true;
			highlightDuration = 0f;
		} else{
			//turn highlight off
			highlight = false;
			highlightDuration = 0f;
			intermediateColor = material.color;
		}
	}

	public void setGrabbed(bool b){
		//Sets as Grabbed
		grabbed = b;

		if (!grabbed){
			if (closestObject){
				closestObject.GetComponent<MotionBlockTest2>().setParentable(false);
			}

		}
	}

	public void setParentable(bool b){

		parentable = b;
		//Makes the block able to accept children
		if (b){
			//Set to highlighted
			setHighlighted(true);
		} else{
			setHighlighted(false);
		}
	}

	public void GetClosestObject()
 	{
    blocks = GameObject.FindGameObjectsWithTag(tag);


		foreach (GameObject obj in blocks){
			/*if(!closestObject)
			{
				 closestObject = obj;
			}*/

			//Check if other block is below threshold distance
			distance = Vector3.Distance(obj.transform.position, transform.position);

			if (distance > 0){
				if(distance<threshold){

					//Debug.Log(obj.name);
					//Debug.Log(distance);

					//Highlight parent object
					closestObject = obj; //save parent object
					closestObject.GetComponent<MotionBlockTest2>().setParentable(true);
				} else {
					obj.GetComponent<MotionBlockTest2>().setParentable(false);
				}
			}


		}
 	}



	public void foobar(){
		Debug.Log(transform.name);
	}
}
