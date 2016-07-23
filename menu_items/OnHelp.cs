using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class OnHelp : MonoBehaviour {
	public Text GameRecord;
	public GameObject panel;
	public GameObject Background;
	public GameObject Ok_button;
	public GameObject singlePlayer;
	public GameObject multiPlayer;
	public OnSettings Setting;
	public OnFb FB;
	public OnCup cup;
	string Mancala_Rules;
	// Use this for initialization
	void Start () {
		Setting =  FindObjectOfType (typeof(OnSettings))as OnSettings;
		FB =  FindObjectOfType (typeof(OnFb))as OnFb;
		cup =  FindObjectOfType (typeof(OnCup))as OnCup;
	}

	// Update is called once per frame
	void Update () {

	}

	void OnMouseDown () {
		Mancala_Rules = "1. Select any of your bowls at your turn to play."+
			"Turn ends when you drop the last seed in hand in an empty bowl\n\n2."+
			"If the last seed in hand is droped in a non empty bowl, you take all"+
			"the seeds in the bowl and continue playing your turn\n\n3. Capture"+
			"seeds when your bowl gets back upto 4 seeds. Capture opponent seed"+
			"when your last seed in hand completes their bowl to four seeds\n\n"+
			"4. To win, capture all opponents seeds in less than 6 rounds or the"+
			" player with more seeds captured after the 6th round wins";
		GameRecord.text = Mancala_Rules;
		GameRecord.fontSize = 25;
		Background.SetActive (true);
		singlePlayer.SetActive (false);
		multiPlayer.SetActive (false);
		FB.gameObject.SetActive (false);
		Setting.gameObject.SetActive (false);
		cup.gameObject.SetActive (false);
		panel.SetActive (true);
		Ok_button.GetComponent<SpriteRenderer>().enabled = true;
	}

}
