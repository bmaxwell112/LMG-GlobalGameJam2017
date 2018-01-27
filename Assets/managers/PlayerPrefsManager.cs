/*	
==========================================================
	Manages the Player Prefs(stored data)
==========================================================
This script contains functions that can be called by any
other scripts to set or get stored game data such as levels
and players unlocked, high scores, and setting preferences.
*/

using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerPrefsManager : MonoBehaviour {

	const string GAME_CHECK = "game_check";
	const string MASTER_VOLUME_KEY = "master_volume";
	const string SFX_VOLUME_KEY = "SFX_volume";

	public static void SetGameCheck(int playedBool)
	{
		PlayerPrefs.SetInt(GAME_CHECK, playedBool);
	}
	public static int GetGameCheck()
	{
		return PlayerPrefs.GetInt(GAME_CHECK);
	}

	public static void SetMasterVolume(float volume){
		if(volume >= 0f && volume <= 1f){
			PlayerPrefs.SetFloat (MASTER_VOLUME_KEY, volume);
		} else {
			Debug.LogError("Master volume out of range");
		}
	}	
	public static float GetMasterVolume(){
		return PlayerPrefs.GetFloat (MASTER_VOLUME_KEY);
	}

	public static void SetSFXVolume(float volume)
	{
		if (volume >= 0f && volume <= 1f)
		{
			PlayerPrefs.SetFloat(SFX_VOLUME_KEY, volume);
		}
		else
		{
			Debug.LogError("SFX volume out of range");
		}
	}
	public static float GetSFXVolume()
	{
		return PlayerPrefs.GetFloat(SFX_VOLUME_KEY);
	}
}
