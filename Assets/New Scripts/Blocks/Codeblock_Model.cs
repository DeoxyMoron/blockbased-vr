using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Codeblock_Model : MonoBehaviour {

	//For Highlighting
	private Material material;
	private Color normalColor;
	private Color highlightColor;
	private Color intermediateColor;
	private float highlightDuration;
	private bool highlight;

	//For Snap Detection
	private GameObject closestObject;
	private GameObject parentGameObject;
	public GameObject[] blocks;
	public string tag;
	public float distance;
	public float threshold;

	public bool parentable = false;
	public bool grabbed = false;

	public  float moveSpeed = 15.0f;
	public  float rotateSpeed = 10.0f;
	public bool snapped = false;


//Controller Detection
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

	public void foo(){
		Debug.Log(transform.name);
	}


	void Update () {
		//Notify closest object
		if (grabbed){
			//REFRESH AND UNDO PARENTING
			StopAllCoroutines();
			//Debug.Log(transform.parent.tag);
			if (transform.parent){
				if (transform.parent.tag == "Block"){
					if (GetComponent<FixedJoint>())
					{
							// 2
							GetComponent<FixedJoint>().connectedBody = null;
							Destroy(GetComponent<FixedJoint>());
					}
					//transform.SetParent(transform.parent.parent);

					transform.parent = null;
				}
			}






			//Executes code when in the grabbed state

			//CHECK to see if another block is close,
			// If it is then sets it to parentable
			//But only if you are not currently grabbing a green Flag
			if (transform.name != "Green Flag Block"){
				GetClosestObject();
			}

		}

		if (parentable){
			//Executes code when in the parentable state
			//Debug.Log(transform.name);
			//Debug.Log("parentable");
			material.color = Color.yellow;
		}

		//Update Highlight
		if (highlight) {
			highlightDuration += Time.deltaTime;

			//make exception for when colored through parenting
			if (!parentable){
				material.color = Color.Lerp(normalColor, highlightColor, highlightDuration*4);
			}

		} else{
			highlightDuration += Time.deltaTime;
			material.color = Color.Lerp(intermediateColor, normalColor, highlightDuration*4);
		}
	}

	//Functions for controller highlighting
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
		//Start Script on Controller enter
		if(other.gameObject.tag=="ViveController"){
			//Debug.Log("Controller Enter");
			//Add Highlight
			highlight = true;
			highlightDuration = 0f;
		}
		//Debug.Log("GREEN BARREL: Controller entered");
	}

	public void OnTriggerStay(Collider other)
	{
		SetCollidingObject(other);
		/*
		if(other.tag=="ViveController"){

			Debug.Log("yee");
		}
		*/

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

		//When letting go check to see if the closest object is still parentable
		//If so make it a parentGameObject

		if (!grabbed){
			if (closestObject){
				if (closestObject.GetComponent<Codeblock_Model>().getParentable()){
					//HAve parent execute code
					//closestObject.GetComponent<MotionBlockTest2>().

					////////SET PARENT
					//Set the parent for the child
					setBlockParent(closestObject);

				}
				closestObject.GetComponent<Codeblock_Model>().setParentable(false);
			}

		}
	}



	public bool getParentable(){
		return parentable;
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


		//Debug.Log(blocks);

		foreach (GameObject obj in blocks){

			//Don't check children for tags
			if (!obj.transform.IsChildOf(transform)){
				//Check if other block is below threshold distance
				distance = Vector3.Distance(obj.transform.position, transform.position);

				//CHECK: object is not self
				if (distance > 0){

					//CHECK: see if it is close enough
					if(distance<threshold){

						//Check for child BLOCKS

						/*
						//CHECK: See if it does not already have children that are blocks?
						if (obj.transform.childCount == 0){
							closestObject = obj; //save parent object
							closestObject.GetComponent<Codeblock_Model>().setParentable(true);
						}*/

						//Check child tags,
						// if there are no children with block tag then set to parentable

						bool hasBlockChild = false;
						foreach (Transform child in obj.transform){
							//Debug.Log(child.tag);
							if (child.tag == "Block"){
								hasBlockChild = true;
							}

						}

						if (!hasBlockChild){
							closestObject = obj; //save parent object
							closestObject.GetComponent<Codeblock_Model>().setParentable(true);
						}

					} else {
						obj.GetComponent<Codeblock_Model>().setParentable(false);
					}
				}
			}
		}
	}

	public void setBlockParent(GameObject obj){
		parentGameObject = obj;
		transform.SetParent(parentGameObject.transform);
		StartCoroutine(InstallPart_Instant(parentGameObject));
	}

	//from old
	public void setParent(GameObject parentGameObject){
		transform.SetParent(parentGameObject.transform);
		StartCoroutine(InstallPart_Instant(parentGameObject));
		snapped = true;
	}

	private FixedJoint AddFixedJoint()
	{
			FixedJoint fx = gameObject.AddComponent<FixedJoint>();
			fx.breakForce = 20000;
			fx.breakTorque = 20000;
			return fx;
	}
	IEnumerator InstallPart(GameObject parentGameObject)
	{
	  while (transform.localPosition != Vector3.down || transform.localRotation != Quaternion.identity)
	  {
	      transform.localPosition = Vector3.MoveTowards(transform.localPosition, Vector3.down, Time.deltaTime * moveSpeed);
	      transform.localRotation = Quaternion.RotateTowards(transform.localRotation, Quaternion.identity, Time.deltaTime * rotateSpeed);
	      yield return new WaitForEndOfFrame();
	  }

		var joint = AddFixedJoint();
		joint.connectedBody = parentGameObject.GetComponent<Rigidbody>();
	}


	IEnumerator InstallPart_Instant(GameObject parentGameObject)
	{

		transform.localPosition = Vector3.down;
		transform.localRotation = Quaternion.identity;
		transform.localScale = new Vector3(1,1,1);
		yield return new WaitForEndOfFrame();
		//transform.localPosition = transform.position;

		var joint = AddFixedJoint();
		joint.connectedBody = parentGameObject.GetComponent<Rigidbody>();
	}
}
