using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class OnCup : MonoBehaviour {
	public Text GameRecord;
	public GameObject panel;
	public GameObject Background;
	public GameObject Ok_button;
	public GameObject singlePlayer;
	public GameObject multiPlayer;
	public OnHelp Help;
	public OnSettings Setting;
	public OnFb FB;
	public Dropdown Difficulty;
	public Dropdown sound;
	// Use this for initialization
	void Start () {
		Help =  FindObjectOfType (typeof(OnHelp))as OnHelp;
		Setting =  FindObjectOfType (typeof(OnSettings))as OnSettings;
		FB =  FindObjectOfType (typeof(OnFb))as OnFb;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnMouseDown () {
		GameRecord.text = "SINGLE-PLAYER RECORD : \n" +
			"W : " + PlayerPrefs.GetInt ("SW", 0) + " - " + 
			" D : " + PlayerPrefs.GetInt ("SD", 0) +  " - " + " L : " + PlayerPrefs.GetInt ("SL", 0) + 
			"\nMULTIPLAYER RECORD : \n" + "W : " + PlayerPrefs.GetInt ("MW", 0) +  " - " + " D : " + 
			PlayerPrefs.GetInt ("MD", 0) +  " - " + " L : " + PlayerPrefs.GetInt ("ML", 0)+
			"\nRANK LEVEL : \n"+ (PlayerPrefs.GetInt ("MW", 0) + PlayerPrefs.GetInt ("MW", 0))/10;
		
		GameRecord.fontSize = 25;
		Background.SetActive (true);
		singlePlayer.SetActive (false);
		multiPlayer.SetActive (false);
		panel.SetActive (true);
		Help.gameObject.SetActive (false);
		FB.gameObject.SetActive (false);
		Setting.gameObject.SetActive (false);
		Ok_button.GetComponent<SpriteRenderer>().enabled = true;
	}



}
