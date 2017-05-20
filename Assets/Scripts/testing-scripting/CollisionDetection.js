#pragma strict

public var saved_color : Color;
public var grabbableBlock : Block;
private var b : GameObject;

function Update () {
	
}

function Start(){
	//Save Default block color
	//Save old color
	saved_color = Color(.221,.435,.941);
}

function OnTriggerEnter(col : Collider){
	if (col.gameObject.tag == "Block"){
		
		//Block
		b = col.gameObject;
		


		//Change color
		b.GetComponent(Block).setColor(Color.green);
		
		//Set Grabbable State
		b.GetComponent(Block).setGrabbable(true);
		

		//Have to do all children because there are multiple 
		//boxes from hacking together a mockup block with notches

		//TODO: move this inside the block class and just have collisionDetection
		//Call some method to change color
		//for(var b : Renderer in col.gameObject.transform){
			
		//	Debug.Log(b);
			
			//saved_color = b.material.color;
			//b.material.color = Color.green;
			//col.gameObject.GetComponent(Block).setGrabbable(true);
			
		//}

		

		//col.gameObject.GetComponent(Block).setGrabbable(true);
		//col.GetComponent(Renderer).material.color = Color.green;
	}
}

function OnTriggerExit(col : Collider){
	if (col.gameObject.tag == "Block"){
		b.GetComponent(Block).setColor(saved_color);
		b.GetComponent(Block).setGrabbable(false);

		//col.gameObject.GetComponent(Block).setGrabbable(false);
		//col.GetComponent(Renderer).material.color = saved_color;
	}
}