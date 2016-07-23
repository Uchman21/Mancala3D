using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems; 
using UnityEngine.UI; 
using System.Collections.Generic;

public class OnSound : MonoBehaviour {
	AudioSource audioSource;
	// Use this for initialization
	void Start () {
		audioSource = UnityEngine.Object.FindObjectOfType<AudioSource>();
	}


	public void OnValueChange(int value){
		if (value == 0) {
			PlayerPrefs.SetInt ("MancalaSound", 0);
			audioSource.Pause();
		} else {
			PlayerPrefs.SetInt ("MancalaSound", 1);
			audioSource.UnPause();
		}
	}

	// Update is called once per frame
	void Update () {
		
	}

	void Awake() 
	{ 
		EventTrigger evtrig = gameObject.AddComponent<EventTrigger>(); 
		EventTrigger.TriggerEvent trigev = new EventTrigger.TriggerEvent(); 
		trigev.AddListener((eventData) => Fix(eventData)); 
		EventTrigger.Entry entry = new EventTrigger.Entry() { callback = trigev, eventID = EventTriggerType.Select }; 
		if (evtrig.triggers == null) 
			evtrig.triggers = new List<EventTrigger.Entry>(); 
		evtrig.triggers.Add(entry); 
	}
	
	public void Fix(BaseEventData eventData) 
	{ 
		// If paused, remove the spawned dropdown 
		if (Time.timeScale == 0f) 
		{ 
			Transform tr = transform.Find("Dropdown List"); 
			if (tr) 
				Destroy(tr.gameObject); 
		} 
	} 


}
