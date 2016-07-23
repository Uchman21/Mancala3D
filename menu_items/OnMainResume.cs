using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class OnMainResume : MonoBehaviour {
	public GameObject panel;
	public GameObject Background;
	public GameObject singlePlayer;
	public GameObject multiPlayer;
	public Dropdown Difficulty;
	public Dropdown sound;
	public OnHelp Help;
	public OnSettings Setting;
	public OnFb FB;
	public OnCup cup;
	public Scrollbar scrollbar;
	// Use this for initialization
	void Start () {
		Help =  FindObjectOfType (typeof(OnHelp))as OnHelp;
		Setting =  FindObjectOfType (typeof(OnSettings))as OnSettings;
		FB =  FindObjectOfType (typeof(OnFb))as OnFb;
		cup =  FindObjectOfType (typeof(OnCup))as OnCup;
	}

	void OnMouseDown(){
		Background.SetActive (false);
		Difficulty.gameObject.SetActive (false);
		sound.image.enabled = false;//sound.gameObject.SetActive (false);
		scrollbar.value = 0.5f;
		panel.SetActive (false);
		gameObject.GetComponent<SpriteRenderer>().enabled = false;
		singlePlayer.SetActive (true);
		multiPlayer.SetActive (true);
		FB.gameObject.SetActive (true);
		Setting.gameObject.SetActive (true);
		cup.gameObject.SetActive (true);
		Help.gameObject.SetActive (true);
	}
	// Update is called once per frame
	void Update () {
	
	}
}
