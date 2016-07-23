using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class OnSettings : MonoBehaviour {
	public Dropdown Difficulty;
	public Dropdown sound;
	public GameObject Ok_button;
	public GameObject Background;
	public GameObject singlePlayer;
	public GameObject multiPlayer;
	public Dropdown sound_dropdown;
	public OnHelp Help;
	public OnFb FB;
	public OnCup cup;

	// Use this for initialization
	void Start () {
		Help =  FindObjectOfType (typeof(OnHelp))as OnHelp;
		FB =  FindObjectOfType (typeof(OnFb))as OnFb;
		cup =  FindObjectOfType (typeof(OnCup))as OnCup;
		AudioSource audioSource = UnityEngine.Object.FindObjectOfType<AudioSource>();
		sound_dropdown.value = PlayerPrefs.GetInt ("MancalaSound", 1);
		if (PlayerPrefs.GetInt ("MancalaSound", 1) == 1) {
			audioSource.UnPause();
		} else {
			audioSource.Pause();
		}
	}

	public void OnMouseDown () {
		Difficulty.gameObject.SetActive (true);
		sound.image.enabled = true;//gameObject.SetActive (true);
		Ok_button.GetComponent<SpriteRenderer>().enabled = true;
		Background.SetActive (true);
		FB.gameObject.SetActive (false);
		Help.gameObject.SetActive (false);
		cup.gameObject.SetActive (false);
		singlePlayer.SetActive (false);
		multiPlayer.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
