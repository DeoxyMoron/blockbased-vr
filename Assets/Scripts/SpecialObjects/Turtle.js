#pragma strict

public var start_pos : Vector3;
public var speed : float = 1;

function Start () {

/*
	//test script
	yield new WaitForSeconds(3);
	yield glideForward(3,speed);
	yield turnLeft(90,speed);
	yield glideForward(3,speed);
	yield turnLeft(90,speed);
	yield glideForward(3,speed);
	yield turnLeft(90,speed);
	yield glideForward(3,speed);
	yield turnLeft(90,speed);
	//moveForward(1);
	*/
}

function Update () {

}

function moveForward(scalar : float){
	transform.Translate(Vector3.forward * scalar);
}

function glideForward(distance : float, duration: float){

	//Glides a certain distance forward
	var start_pos : Vector3 = transform.position;
	var end_pos : Vector3 = start_pos + transform.forward * distance;

	var t : float = 0.0;

	while (t < duration){
		transform.position = Vector3.Lerp(start_pos, end_pos, (t/duration));
		t += Time.deltaTime;
		yield;
	}
	transform.position = end_pos;
}

function turnRight(deg : float, duration : float){
	var start_ori : Vector3 = transform.eulerAngles;
	var end_ori : Quaternion = Quaternion.Euler(start_ori + new Vector3(0,deg,0));



	var t : float = 0.0;

	while (t < duration){
		transform.rotation = Quaternion.Slerp(Quaternion.Euler(start_ori), end_ori, (t/duration));
		t += Time.deltaTime;
		yield;
	}

	transform.rotation = end_ori;
}

function turnLeft(deg: float, duration: float){
	yield turnRight(-deg, duration);
}

function routine(){
	//Routine that goes around the island
	//yield new WaitForSeconds(3);
	yield glideForward(3,speed);
	yield turnLeft(90,speed);
	yield glideForward(3,speed);
	yield turnLeft(90,speed);
	yield glideForward(3,speed);
	yield turnLeft(90,speed);
	yield glideForward(3,speed);
	yield turnLeft(90,speed);
	//moveForward(1);

}
