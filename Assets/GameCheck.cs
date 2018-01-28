using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCheck : MonoBehaviour {

	// Use this for initialization
	void Start () {		
		/* Checks if the game has been played before if not
		 * it will set the default SFX and Master Volumes */		
		if (PlayerPrefsManager.GetGameCheck() != 1)
		{
			PlayerPrefsManager.SetMasterVolume(0.35f);
			PlayerPrefsManager.SetSFXVolume(0.75f);
			PlayerPrefsManager.SetGameCheck(1);
		}
	}
}
