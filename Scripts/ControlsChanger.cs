using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlsChanger : MonoBehaviour {

	public Text ControlsDescription;
	string CurrentControlScheme;

	// Use this for initialization
	void Start () {
		CurrentControlScheme = PlayerPrefs.GetString("ControlScheme");

		if(CurrentControlScheme == "")
		{
			CurrentControlScheme = "Buttons";
			ControlsDescription.text = CurrentControlScheme;
		}
	}


	public void SwitchControlScheme()
	{
		if (CurrentControlScheme == "Buttons") 
		{
			CurrentControlScheme = "Touch";
		} 
		else if (CurrentControlScheme == "Touch") 
		{
			CurrentControlScheme = "Buttons";
		}

		ControlsDescription.text = CurrentControlScheme;
		PlayerPrefs.SetString ("ControlScheme", CurrentControlScheme);
	}
}
