using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedPoleInteraction : MonoBehaviour {

	//For Highlighting
	private Material material;
	private Color normalColor;
	private Color highlightColor;
	private Color intermediateColor;
	private float highlightDuration;
	private bool highlight;

	public TurtleMovement turtle;

	private GameObject collidingObject;

	// For VR
	private SteamVR_TrackedObject trackedObj;
	// 2
	private SteamVR_Controller.Device Controller
	{
	    get { return SteamVR_Controller.Input((int)trackedObj.index); }
	}
	void Awake()
	{
	    trackedObj = GetComponent<SteamVR_TrackedObject>();
	}

	void Start(){
		material = GetComponent<MeshRenderer>().material;

		normalColor = material.color;
		highlightColor = new Color(
			normalColor.r * 1.5f,
			normalColor.g * 1.5f,
			normalColor.b * 1.5f
		);

		intermediateColor = normalColor;
	}

	private void SetCollidingObject(Collider col)
	{
		// 1
		if (collidingObject || !col.GetComponent<Rigidbody>())
		{
				return;
		}
		// 2
		collidingObject = col.gameObject;
	}

	public void OnTriggerEnter (Collider other){
		SetCollidingObject(other);

		Debug.Log(other.gameObject.tag);

		//Start Script on Controller enter
		if(other.gameObject.tag=="ViveController"){
			Debug.Log("Controller Enter");
			//turtle.startClassicRoutine();
			turtle.reset();

			//Add Highlight
			highlight = true;
			highlightDuration = 0f;

		}
		//Debug.Log("GREEN BARREL: Controller entered");

	}

	// 2
	public void OnTriggerStay(Collider other)
	{
		SetCollidingObject(other);
	}

	public void OnTriggerExit(Collider other)
	{
			if (!collidingObject)
			{
					return;
			}

			collidingObject = null;

			//turn highlight off
			highlight = false;
			highlightDuration = 0f;
			intermediateColor = material.color;
			//highlightDuration = 0f;
	}
	// Update is called once per frame
	void Update () {
		if (highlight) {
			highlightDuration += Time.deltaTime;
			material.color = Color.Lerp(normalColor, highlightColor, highlightDuration*4);
		} else{
			highlightDuration += Time.deltaTime;
			material.color = Color.Lerp(intermediateColor, normalColor, highlightDuration*4);
		}

		/*
		if (Controller.GetHairTriggerDown())
		{
		    if (collidingObject == Controller)
		    {
		        Debug.Log("YEEHAW");
		    }
		}*/
	}
}
