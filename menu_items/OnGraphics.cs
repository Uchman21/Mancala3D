using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems; 
using UnityEngine.UI; 
using System.Collections.Generic;

public class OnGraphics : MonoBehaviour {
	// Use this for initialization
	private SetUp setup;
	void Start () {
		setup = FindObjectOfType (typeof(SetUp))as SetUp;
	}
	
	
	public void Beauty(int value){
		int choice = GameSceneManager.choice;
		if (value == 0) {
			Screen.SetResolution (640, 480, true);
			setup.position(choice);
		} else if (value == 1) {
			Screen.SetResolution (1280, 800, true);
			setup.position(choice);
		}else if (value == 2) {
			Screen.SetResolution (1440, 900, true);
			setup.position(choice);
		} else {
			Screen.SetResolution (1600, 900, true);
			setup.position(choice);
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
