#pragma strict

public var grabbable : boolean;
public var controller : Transform;
public var grabbed : boolean;

function Start() 
{
	controller = GameObject.Find("Controller1").transform;
	grabbed = false;
}

function Update() 
{
	if(Input.GetKeyDown(KeyCode.F) && getGrabbable()){

		if (getGrabbed()){
			setGrabbed(false);
		} else{
			setGrabbed(true);
		}
		

	}

	if(Input.GetKeyDown(KeyCode.V)){
		setGrabbed(false);
		Debug.Log(GetComponentInChildren(BoxTrigger).getSnapTo());


	}
}

function setGrabbable(state : boolean)
{
	grabbable = state;
}

function getGrabbable() : boolean
{
	return grabbable;
}

function setGrabbed(state: boolean)
{	
	grabbed = state;
	//this.transform.position = controller.position;
	if (state){
		this.transform.parent = controller;
	} else {
		this.transform.parent = null;
	}
}

function getGrabbed() : boolean
{
	return grabbed;
}


function setColor(c : Color){
	//Debug.Log(c);
	//this.GetComponentInChildren(Renderer).material.color = c;

	for (var r : Renderer in GetComponentsInChildren(Renderer)){
		//Debug.Log(r.material);
		r.material.color = c;
	}


}