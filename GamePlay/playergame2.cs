using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class playergame2 : MonoBehaviour {
	public Texture2D pg2textureN;
	public Texture2D pg2textureH;
	
	
	
	// Use this for initialization
	void Start () {

	}
	
	void OnMouseOver(){
		GetComponent<GUITexture>().texture = pg2textureH;
	}
	
	void OnMouseExit(){
		GetComponent<GUITexture>().texture = pg2textureN;
	}
	
	void OnMouseDown () {
		Debug.Log("here");
		GameSceneManager.selection = "2 players";
		SceneManager.LoadScene("Scene1");
		
	}
}
