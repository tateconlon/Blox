using UnityEngine;
using System.Collections;


//  How everything works:  3 different colliders, all seperated and connected through public variables
//  The blockBorderChild is merely the collider that the hero stands on and collides sideways on, an upside down U.
// It is this way so that the character can jump through the bottom, but land on the sides.  TODO: This gives potential to 
//do the breakthrough stunt, where this collider can be deactivated and character turned to zero gravity.
//  This blockBorderChile layer is controlled through the BlockColourer script when the BlockColourer collider(slightly smaller) is clicked.  This is so that if the
//character is merely touching the block on the corner and it is clicked, it doesn't count as the character 
//touching that block.  This is important as if the character is INSIDE the block and it is clicked, the
//character's platform mask must change to reflect this without any bumping interference that may occur.
//  This covers the outer collider(BlockBorderChild used for character to stand on and whose layer is changed
//through BlockColourerScript) and the the inner "mouse" collider to detect mouse clicks as well as if the 
//character is inside the block while it's clicked.
//
//	The last collider is the layerController and it's Edge collider which placed NEAR at the bottom border of the box, 
//parallel to the bottom of the box - .  This is used so that when the character approaches the block 
//from below then it would trigger the
//character's platform mask to change, but NOT RIGHT AWAY.  It is near the bottom of the box as since the character controller casts
//rays from its FEET, then there is no weird bouncing issue as with BoxColliders as the ray will not see the collider
//until it is over the line, which means the character is in the box.  But leave enough space.

//HOW TO:
//	1.Set BlockColorer layer to MouseClickLayer
//  2.Make BlockColorer collider the area mouse would click
//  3.Make ParentBlock have edge colliders (MUST BE PERFECT, ZOOM IN AS FAR AS CAN GO!) for everything except bottom
//	4.Set ParentBlock layer to whatever colour prefab it represents (will be changed at runtime in game)
//	5.Make ParentBlock the child of the BlockColorer
//	6.Make LayerController have edge collider a bit off the bottom of the block, leave enough jump space(test)
//	7.Set LayerController Layer to be LayerCollider
//  8.Set public variables accordingly.


public class BlockColorer : MonoBehaviour {

	bool _heroIn;

	public enum Colours	//To keep track of colour integers better
	{	
		Green,
		Blue,
		Red
	}

	public Colours startColour;
	public int _currColour;		//Defined by Colours enum
	public GameObject blockBorderChild;

	CharacterController2D _controller;

	private delegate void ColourerDelegate();

	public LayerController _layerController;

	// Use this for initialization
	void Start () {
		GetComponent<SpriteRenderer>().color = Color.white;	//Sets base colour to white for colour transformations
		ColourerDelegate[] colourerDelegates = {TurnGreen, TurnBlue, TurnRed};
		colourerDelegates[(int)startColour]();
		_heroIn = false;
	}

	//Changes the colour of the block and
	//Changes the platform mask of the character upon clicking if the character is in the block
	void Click()
	{
		NextColourOnClick();
		if(_heroIn)
		{
			_layerController.ChangeLayer(_controller);
		}
	}

	//If the mouse is clicked on the block, it will change the colour upon enter
	void OnMouseDown()
	{
		Click();
	}

	//If the mouse is dragged into the block, it will change the colour upon enter
	void OnMouseEnter()
	{
		if(Input.GetMouseButton(0))
			Click();
	}

	//Determines the next colour after a click
	private void NextColourOnClick(){
		if(_currColour == (int)Colours.Green)
			TurnBlue();
		else if(_currColour == (int)Colours.Blue)
			TurnRed();
		else if(_currColour == (int)Colours.Red)
			TurnGreen();
	}

	//Applies Green Attributes
	private void TurnGreen(){
		blockBorderChild.layer = LayerMask.NameToLayer("Green");
		gameObject.renderer.material.color = Color.green;
		_currColour = (int)Colours.Green;
	}

	//Applies Blue Attributes
	private void TurnBlue(){
		blockBorderChild.layer = LayerMask.NameToLayer("Blue");
		gameObject.renderer.material.color = Color.blue;
		_currColour = (int)Colours.Blue;
	}

	//Applies Red Attributes
	private void TurnRed(){
		blockBorderChild.layer = LayerMask.NameToLayer("Red");
		gameObject.renderer.material.color = Color.red;
		_currColour = (int)Colours.Red;
	}

	//Upon character enter, stores the characterController2D for the OnMouseClick method
	void OnTriggerEnter2D(Collider2D coll)
	{
		_heroIn = true;
		_controller = coll.GetComponent<CharacterController2D>();
	}

	//Upon character exit, resets the characterController2D to nullfor the OnMouseClick method
	void OnTriggerExit2D(Collider2D coll)
	{
		_controller= null;
		_heroIn = false;
	}


}
