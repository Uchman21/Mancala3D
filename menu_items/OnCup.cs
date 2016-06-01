using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class OnCup : MonoBehaviour {
	public Text GameRecord;
	public GameObject Background;
	public Button Ok_button;
	public GameObject singlePlayer;
	public GameObject multiPlayer;
	public Dropdown Difficulty;
	public Dropdown sound;
	// Use this for initialization
	void Start () {
		GameRecord.text = "Single Game Record : " + "\n" + "W : " + PlayerPrefs.GetInt ("SW", 0) + 
			" D : " + PlayerPrefs.GetInt ("SD", 0) + " L : " + PlayerPrefs.GetInt ("SL", 0) + "\n" +
			"Multiplayer Game Record : " + "\n" + "W : " + PlayerPrefs.GetInt ("MW", 0) + " D : " + 
			PlayerPrefs.GetInt ("MD", 0) + " L : " + PlayerPrefs.GetInt ("ML", 0);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnMouseDown () {
		
		GameRecord.fontSize = 20;
		Background.SetActive (true);
		singlePlayer.SetActive (false);
		multiPlayer.SetActive (false);
		GameRecord.gameObject.SetActive (true);
		Ok_button.gameObject.SetActive (true);
	}

	void Awake() {
		GameRecord.text = "Single Game Record : " + "\n" + "W : " + PlayerPrefs.GetInt ("SW", 0) + 
			" D : " + PlayerPrefs.GetInt ("SD", 0) + " L : " + PlayerPrefs.GetInt ("SL", 0) + "\n" +
			"Multiplayer Game Record : " + "\n" + "W : " + PlayerPrefs.GetInt ("MW", 0) + " D : " + 
			PlayerPrefs.GetInt ("MD", 0) + " L : " + PlayerPrefs.GetInt ("ML", 0);
	}

	public void Clear() {
		Background.SetActive (false);
		Difficulty.gameObject.SetActive (false);
		sound.gameObject.SetActive (false);
		GameRecord.gameObject.SetActive (false);
		Ok_button.gameObject.SetActive (false);
		singlePlayer.SetActive (true);
		multiPlayer.SetActive (true);
	}
}
