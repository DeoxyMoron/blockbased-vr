using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurtleMovement : MonoBehaviour {

		Coroutine classic;
		Vector3 startPos;
		Vector3 startOri;

		List<string> actionList = new List<string>();
		List<int> paramList= new List<int>();

		private TrailRenderer tr;

		void Start()
		{
			//Vector3 whatever = transform.position + transform.forward * 2;
			startPos = transform.position;
			startOri = transform.eulerAngles;

			tr = GetComponent<TrailRenderer>();

			//List<string> actionList = new List<string>();
			//List<int> paramList = new List<int>();
		}

		/////
		public void addStep(string s, int i){
			//Debug.Log(s);
			//Debug.Log(i);
			actionList.Add(s);
			paramList.Add(i);
		}

		public void clearActions(){
			Debug.Log("s");
			actionList.Clear();
			paramList.Clear();
		}

		public void runSequence(){
			StartCoroutine(TurnTest(actionList, paramList));
		}

		public void printLists(){
			Debug.Log(actionList);
		}

		/////


		///////Testing
		public void foobar(string a, string b){
			Debug.Log(a);
			Debug.Log(b);
		}

		public void startTurnTest(){
			//Reset any old actions
			//reset();
			List<string> testList = new List<string>();

			//testList.Add("turn_right");
			//testList.Add("turn_left");
			//testList.Add("move_forward");
			testList.Add("move_forward");
			testList.Add("turn_left");
			testList.Add("move_forward");
			testList.Add("turn_left");
			testList.Add("move_forward");
			testList.Add("turn_left");


			List<int> testList2 = new List<int>();

			testList2.Add(2);
			testList2.Add(120);
			testList2.Add(2);
			testList2.Add(120);
			testList2.Add(2);
			testList2.Add(120);


			StartCoroutine(TurnTest(testList, testList2));
			//StartCoroutine(TurnTest());
		}

		public IEnumerator TurnTest(List<string> testList, List<int> testList2){
			//StopAllCoroutines();;
			int i = 0;
			foreach (string t in testList){

				if (t=="turn_left"){
					yield return StartCoroutine(TurnLeft(testList2[i], 1));
				}
				else if (t=="turn_right"){
					yield return StartCoroutine(TurnRight(testList2[i], 1));
				}
				else if (t=="move_forward"){
					yield return StartCoroutine(GlideForward(testList2[i], 1));
				}

				i += 1;

			}

			actionList.Clear();
			paramList.Clear();

			//yield return StartCoroutine(TurnLeft(90, 1));
		}

		///////////

		public void startClassicRoutine(){
			//Reset any old actions
			reset();
			classic = StartCoroutine(ClassicRoutine());
		}

		public void reset(){
			StopAllCoroutines();
			transform.position = startPos;
			transform.eulerAngles = startOri;

			//Trailrender
			tr.Clear();
		}



		public IEnumerator ClassicRoutine(){
			//StopAllCoroutines();
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


		/*
				public void readInstructions(string[] steps){
					foreach (string step in steps){
						Debug.Log(step);
					}
				}
			*/
				/*public void readInstructions(List<string> actions, List<string> params){
					Debug.Log(actions);
					//for (int i=0; i<actions.length; i++){
					//	Debug.Log(actions[i]);
					//	Debug.Log(params[i]);
					//}
				}
		*/
}
