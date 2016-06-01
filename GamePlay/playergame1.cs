using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class playergame1 : MonoBehaviour {
	
	
	// Use this for initialization
	void Start () {
	
	}

	public void OnMouseDown () {
		Debug.Log("here");
		GameSceneManager.selection = "1 player";
		SceneManager.LoadScene("Mancala");
		
	}
}
