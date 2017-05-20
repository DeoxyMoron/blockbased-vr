#pragma strict

var myInt : int;


function Start ()
{
    myInt = MultiplyByTwo(myInt);
    Debug.Log (myInt);
}


function MultiplyByTwo (number : int) : int
{
    var ret : int;
    ret = number * 2;
    return ret;
}
