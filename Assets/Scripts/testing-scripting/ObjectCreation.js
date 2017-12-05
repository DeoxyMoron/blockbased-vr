#pragma strict

public var blockPrefab : Transform;
public var controller : Transform;
public var CodeBuilder : CodeBuilder;
//public var script : Component;

function Update ()
{
	if(Input.GetKeyDown(KeyCode.G))
	{
			createBlock();
	}
}


function createBlock(){
	var blockInstance : Transform;
	blockInstance = Instantiate(blockPrefab, controller.position - Vector3(0,.3,0), Quaternion.identity);
	Debug.Log("Block model created");
	//codeBuilderScript.createBlockObj();
	CodeBuilder.createBlockObj();
	//GetComponent(Codeblock).sayHi();
}
