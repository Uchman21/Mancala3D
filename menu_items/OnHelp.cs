using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class OnHelp : MonoBehaviour {
	public Text GameRecord;
	public GameObject Background;
	public Button Ok_button;
	public GameObject singlePlayer;
	public GameObject multiPlayer;
	string Mancala_Rules;
	// Use this for initialization
	void Start () {
		Mancala_Rules = "1. Select any of your bowls at your turn to play. Turn ends when you drop the last seed in hand in an empty bowl\n\n2. If the last seed in hand is droped in a non empty bowl, you take all the seeds in the bowl and continue playing your turn\n\n3. Capture seeds when your bowl gets back upto 4 seeds. Capture opponent seed when your last seed in hand completes their bowl to four seeds\n\n4. Capture 5 of your opponent's bowls to win";
	}

	// Update is called once per frame
	void Update () {

	}

	void OnMouseDown () {
		GameRecord.text = Mancala_Rules;
		GameRecord.fontSize = 13;
		Background.SetActive (true);
		singlePlayer.SetActive (false);
		multiPlayer.SetActive (false);
		GameRecord.gameObject.SetActive (true);
		Ok_button.gameObject.SetActive (true);
	}

	public void Clear() {
		Background.SetActive (false);
		GameRecord.gameObject.SetActive (false);
		Ok_button.gameObject.SetActive (false);
		singlePlayer.SetActive (true);
		multiPlayer.SetActive (true);
	}
}
