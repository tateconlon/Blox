using UnityEngine;
using System.Collections;

//	The last collider is the layerController and it's Edge collider which placed at the bottom border of the box, _ .
//This is used so that when the character approaches the block from below then it would trigger the
//character's platform mask to change.  It is at the bottom of the box as since the character controller casts
//rays from its FEET, then there is no weird bouncing issue as with BoxColliders as the ray will not see the collider
//until it is over the line, which means the character is in the box.  This makes the change instantaneous and awesome.

//HOW TO:
//	1.Set BlockColorer layer to MouseClickLayer
//  2.Make BlockColorer collider the area mouse would click
//  3.Make ParentBlock have edge colliders (MUST BE PERFECT, ZOOM IN AS FAR AS CAN GO!) for everything except bottom
//	4.Set ParentBlock layer to whatever colour prefab it represents (will be changed at runtime in game)
//	5.Make ParentBlock the child of the BlockColorer
//	6.Make LayerController have edge collider on the bottom of block.  Also MUST BE PERFECT
//	7.Set LayerController Layer to be LayerCollider
//  8.Set public variables accordingly.


public class LayerController : MonoBehaviour {

	public BlockColorer _block;

	// Use this for initialization
	void Start () {
	}

	//Changes the platform masks of the character upon entering the square from below
	void OnTriggerEnter2D(Collider2D coll)
	{
		Debug.Log ("Collider enter");
		ChangeLayer(coll);
	}

	//Changes the platform masks of the character after entering the square from below
	void OnTriggerStay2D(Collider2D coll)
	{
		Debug.Log ("Collider stay");
		ChangeLayer(coll);

	}

	//The ChooseLayer method returns the colour layers that are not the background, as these are needed for 
	//the character to collide with.  Method is private, but is called using ChangeLayer
	//A LayerMask is essentially an 32-bit integer, where each bit represents a layer (32 layer, index 0)
	//A multitude of layers can be selected, so FFFFFFFF is all layers.  The numbers 8192, 16384 and 32768
	//represent the 12, 13 and 14th bit of the layers Red, Blue and Green respectively (indexing starts at 0)
	//Red is layer 13, therefore bit 12.
	LayerMask ChooseLayer()
	{	if(_block!=null){
		if(Input.GetKey (KeyCode.DownArrow))
		{
				return 0;
		}
		if(_block._currColour == (int)BlockColorer.Colours.Red)
			return 32768 + 16384;
		else if(_block._currColour == (int)BlockColorer.Colours.Blue)
			return 8192 + 32768;
		else if(_block._currColour == (int)BlockColorer.Colours.Green)
			return 8192 + 16384;
		else
		{
			Debug.LogError("ChooseLayer error");
			return -1;
		}
		}
		Debug.LogError("ChooseLayer error");
		return -1;
	}

	//Changes to chacter's platform masks to be opposite of that to the background
	public void ChangeLayer(CharacterController2D controller)
	{
		controller.platformMask = (LayerMask)ChooseLayer ();
	}

	//Changes to chacter's platform masks to be opposite of that to the background
	public void ChangeLayer(Collider2D coll)
	{
		CharacterController2D controller = coll.GetComponent<CharacterController2D>();
		controller.platformMask = (LayerMask)ChooseLayer ();
	}
}
