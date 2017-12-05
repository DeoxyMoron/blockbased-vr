#pragma strict



function Start () {

}

function Update () {

}


public class Codeblock extends MonoBehaviour{

	public class CodeBlock {
		public var id : String;
		public var opcode : String;
		public var next : String;
		public var topLevel : System.Boolean;
		public var parent : String;
		public var x :int;

		public function CodeBlock(_id : String){
			id = _id;
		}

		public function CodeBlock(_id : String, _opcode : String) {
			id = _id;
			opcode = _opcode;
		}

		public function getID() : String{
			return id;
		}

		public function setParent(p : String){
			parent = p;
		}

		public function getParent() : String{
			return parent;
		}

		public function setOpcode(o : String){
			opcode = o;
		}

		public function getOpcode() : String{
			return opcode;
		}
	}
}

function sayHi(){
	var block1 : CodeBlock = new CodeBlock("green_flag");
	var block2 : CodeBlock = new CodeBlock("motion_gotoxy");

	//Set block 2 as the parent of block 1
	block2.setParent(block1.getID());



	Debug.Log(block1.getParent());
	Debug.Log(block2.getParent());
}
