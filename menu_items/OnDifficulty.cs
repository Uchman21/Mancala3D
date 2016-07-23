using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems; 
using UnityEngine.UI; 
using System.Collections.Generic;

public class OnDifficulty : MonoBehaviour {
	// Use this for initialization
	void Start () {
	}


	public void OnValueChange(int value){
		if (value == 0) {
			PlayerPrefs.SetInt ("MancalaLevel", 1);
		} else if (value == 1) {
			PlayerPrefs.SetInt ("MancalaLevel", 2);
		} else if (value == 2) {
			PlayerPrefs.SetInt ("MancalaLevel", 3);
		} else if (value == 3) {
			PlayerPrefs.SetInt ("MancalaLevel", 4);
		} else {
			PlayerPrefs.SetInt ("MancalaLevel", 5);
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
