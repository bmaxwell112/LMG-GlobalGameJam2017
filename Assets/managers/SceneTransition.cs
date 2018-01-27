using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SceneTransition : MonoBehaviour {
	public float fadeInTime;
	
	private Image fadePanel;
	private Color currentColor = Color.black;
	

	// Use this for initialization
	void Start () {
		fadePanel = GetComponent<Image>();		
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.timeSinceLevelLoad < fadeInTime){
			//fade in
			float alphaChange = Time.deltaTime / fadeInTime;
			currentColor.a -= alphaChange;
			fadePanel.color = currentColor;
		} else {
			// deactivate panel
			gameObject.SetActive (false);
		}
	}
}
