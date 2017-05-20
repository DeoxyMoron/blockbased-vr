#pragma strict

public var detectionRange: float;
public var proximityCheck : boolean;
public var controller : Transform;



function Start(){
	controller = GameObject.Find("Controller1").transform;
	
}

function Update () {

	proximityCheck = false;
	if (Vector3.Distance( controller.position, transform.position) <= detectionRange){
		proximityCheck = true;
	}
}
