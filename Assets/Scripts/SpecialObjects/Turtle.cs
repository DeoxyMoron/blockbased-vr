using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Turtle : MonoBehaviour {

	void Start()
	{
		//Vector3 whatever = transform.position + transform.forward * 2;


	}

	public void startClassicRoutine(){
		StartCoroutine(ClassicRoutine());
	}

	public IEnumerator ClassicRoutine(){
		for (int i = 0; i<4; i++){
			yield return StartCoroutine(GlideForward(3, 1));
			yield return StartCoroutine(TurnLeft(90, 1));
		}
	}
	public IEnumerator GlideForward(float distance, float timeToMove)
   {
      var currentPos = transform.position;
			var endPos = transform.position + transform.forward * distance;
      var t = 0f;
       while(t < 1)
       {
             t += Time.deltaTime / timeToMove;
             transform.position = Vector3.Lerp(currentPos, endPos, t);
             yield return null;
      }
    }

	public IEnumerator TurnRight(float deg, float duration){
		var start_ori = transform.eulerAngles;
		var end_ori = Quaternion.Euler(start_ori + new Vector3(0,deg,0));



		var t = 0f;

		while (t < duration){
			transform.rotation = Quaternion.Slerp(Quaternion.Euler(start_ori), end_ori, (t/duration));
			t += Time.deltaTime;
			yield return null;
		}

		transform.rotation = end_ori;
	}


	public IEnumerator TurnLeft(float deg, float duration){
			yield return TurnRight(-deg, duration);
	}
//whatever
/*
	float t;
	Vector3 startPosition;
	Vector3 target;
	float duration;

	void Start()
	{
					startPosition = target = transform.position;
	}

	public void SetDestination(Vector3 destination, float time)
	{
				 t = 0;
				 startPosition = transform.position;
				 timeToReachTarget = time;
				 target = destination;
	}
*/

/*
public Vector3 start_pos;
	// Use this for initialization
	void Start () {
		moveForward(1);
		glideForward(5.0f,10.0f);
	}

	public void moveForward(float scalar){
		transform.Translate(Vector3.forward * scalar);
	}

	public void glideForward(float distance, float duration){

		//Glides a certain distance forward
		Vector3 start_pos = transform.position;
		Vector3 end_pos = start_pos + transform.forward * distance;

		float t = 0.0f;

		while (t < duration){
			transform.position = Vector3.Lerp(start_pos, end_pos, (t/duration));
			t += Time.deltaTime;
			//yield return null;
		}
		transform.position = end_pos;
	}
*/
	// Update is called once per frame
	void Update()
	{
		//			t += Time.deltaTime/timeToReachTarget;
			//		transform.position = Vector3.Lerp(startPosition, target, t);
	}
}
