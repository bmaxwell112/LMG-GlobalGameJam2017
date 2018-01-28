using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressTrackerUI : MonoBehaviour {

    public Slider progressDisplay;
    public float dropRate;
    public int dropAmount, dropDelay;

    private PlayerController player;
    private bool valueChange;
    private float value;

	// Use this for initialization
	void Start () {
        player = FindObjectOfType<PlayerController>();
        progressDisplay.maxValue = player.escapeNumber;
	}
	
	// Update is called once per frame
	void Update () {
        value = progressDisplay.value;
        if (progressDisplay.value != player.buttonPressed)
        {            
            progressDisplay.value = player.buttonPressed;                       
            if (progressDisplay.value <= player.buttonPressed && progressDisplay.value >= value)
            {
                CancelInvoke();
                valueChange = true;
                Invoke("TransmissionDrop", dropDelay);
                print("test3");
            }            
        }        
	}

    void TransmissionDrop()
    {        
        if (player.buttonPressed >= 0)
        {            
            player.buttonPressed -= dropAmount;
            Invoke("TransmissionDrop", dropRate);            
        }
    }
}
