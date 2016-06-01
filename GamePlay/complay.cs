using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class complay : MonoBehaviour {
	public Texture2D comptextureN;
	public Texture2D comptextureH;


	// Use this for initialization
	void Start () {

	}

	void OnMouseOver(){
		GetComponent<GUITexture>().texture = comptextureH;
		}

	void OnMouseExit(){
		GetComponent<GUITexture>().texture = comptextureN;
		}

	void OnMouseDown () {
		Debug.Log("here");
		GameSceneManager.selection = "2 computers";
		SceneManager.LoadScene ("Scene1");

	}


}
