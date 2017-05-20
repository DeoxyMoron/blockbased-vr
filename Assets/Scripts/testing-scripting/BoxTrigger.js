#pragma strict

var snap_to = false;

var curr_box_collision : Collider;

function Start () {
	
}

function Update () {
	
}

function OnTriggerEnter(col : Collider){
	//Debug.Log(col.gameObject.name);
	//Debug.Log(this.gameObject.name);

	curr_box_collision = col;
}

function OnTriggerExit(col : Collider){
	curr_box_collision = null;
}

function getSnapTo() : boolean{
	return curr_box_collision;
}

