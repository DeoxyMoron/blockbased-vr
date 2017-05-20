#pragma strict

public var blockPrefab : Transform;
public var controller : Transform;


function Update () 
{
	if(Input.GetKeyDown(KeyCode.G))
	{
		var blockInstance : Transform;
		blockInstance = Instantiate(blockPrefab, controller.position - Vector3(0,.3,0), Quaternion.identity);
	}
}
