#pragma strict

public var verticalSpeed : float;
public var horizontalSpeed : float;


function Update ()
{
    Movement();
}


function Movement ()
{
    var verticalMovement : float = Input.GetAxis("Vertical") * verticalSpeed * Time.deltaTime;
    var horizontalMovement : float = Input.GetAxis("Horizontal") * horizontalSpeed * Time.deltaTime;
    
    transform.Translate(Vector3.up * verticalMovement);
    transform.Translate(Vector3.right * horizontalMovement);

	//Debug.Log(transform.position);
}