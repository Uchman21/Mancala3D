using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class OnSettings : MonoBehaviour {
	public Dropdown Difficulty;
	public Dropdown sound;
	public Button Ok_button;
	public GameObject Background;
	public GameObject singlePlayer;
	public GameObject multiPlayer;

	// Use this for initialization
	void Start () {
	
	}

	public void OnMouseDown () {
		Difficulty.gameObject.SetActive (true);
		sound.gameObject.SetActive (true);
		Ok_button.gameObject.SetActive (true);
		Background.SetActive (true);
		singlePlayer.SetActive (false);
		multiPlayer.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
