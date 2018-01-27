//	==========================================================
//				  Controls all music in the game
//	==========================================================
//	Script needs to exist on Persistant music GameObject in the 
//	first scene. The script will keep itself loaded as scenes
//	change. 

using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour {

	//	levelMusicChangeArray's size needs to be set to the number
	//	of scenes in the Build Index. Each Element in the array
	//	corresponds to a scene in the build index.
	//	NOTE: In the inspector drag music into the elememnt for
	//	the scene in the array.
	public AudioClip[] levelMusicChangeArray;
	private AudioClip lastLevelMusic;
	
	private AudioSource audioSource;
	
	void Awake(){
		DontDestroyOnLoad (gameObject);

        Debug.Log("Don't Destroy on load " + name);
	}
	// Use this for initialization
	void Start () {
		audioSource = GetComponent<AudioSource>();
		audioSource.volume = PlayerPrefsManager.GetMasterVolume();
	}
		
	public void ChangeVolume (float volume){
		// This function can be accessed elseware to change the volume.
		audioSource.volume = volume;
	}

	void OnEnable()
	{
		//Tell our 'OnLevelFinishedLoading' function to start listening for a scene change as soon as this script is enabled.
		SceneManager.sceneLoaded += OnLevelFinishedLoading;
	}

	void OnDisable()
	{
		//Tell our 'OnLevelFinishedLoading' function to stop listening for a scene change as soon as this script is disabled. Remember to always have an unsubscription for every delegate you subscribe to!
		SceneManager.sceneLoaded -= OnLevelFinishedLoading;
	}

	void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
	{
		int level = SceneManager.GetActiveScene().buildIndex;
		AudioClip thisLevelMusic = levelMusicChangeArray[level];
		
		//	if the level has music and that music is not what is currently playing
		//	it loads the new music
		if (thisLevelMusic && thisLevelMusic != lastLevelMusic)
		{
			audioSource.clip = thisLevelMusic;
			audioSource.loop = true;
			audioSource.Play();
			Debug.Log("playing clip " + thisLevelMusic);
			lastLevelMusic = thisLevelMusic;
		}
	}
}
