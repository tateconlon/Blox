using UnityEngine;
using System.Collections;

public class LevelChanger : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Alpha0))
		{
			Application.LoadLevel(0);
		}
		else if(Input.GetKeyDown(KeyCode.Alpha1))
		{
			Application.LoadLevel(1);
		}
		else if(Input.GetKeyDown(KeyCode.Alpha2))
		{
			Application.LoadLevel(2);
		}
		else if(Input.GetKeyDown(KeyCode.Alpha3))
		{
			Application.LoadLevel(3);
		}
	}
}
