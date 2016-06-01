using UnityEngine;
using System.Collections;

public class Exit : MonoBehaviour {

	public Texture2D exittextureN;
	public Texture2D exittextureH;
	
	
	
	// Use this for initialization
	void Start () {
		
	}
	
	void OnMouseOver(){
		GetComponent<GUITexture>().texture = exittextureH;
	}
	
	void OnMouseExit(){
		GetComponent<GUITexture>().texture = exittextureN;
	}
	
	void OnMouseDown () {
		Application.Quit ();
		
	}
}
