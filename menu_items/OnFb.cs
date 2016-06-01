using UnityEngine;
using System.Collections;

public class OnFb : MonoBehaviour {
	public string AppID = "1754695884814244";
	public string Link = "https://play.google.com/store/apps/details?id=com.ballwall.afap";
	// The picture's URL and the picture must be at least 200px by 200px.
	public string Picture = "http://images.clipartpanda.com/smiley-face-clip-art-thumbs-up-large-Smiley-Face-making-Thumbs-Up-0-16636.png";

	// The name of your app or game.
	public string Name = "Mancala3D";

	// The caption of your game or app.
	public string Caption;

	// The description of your game or app.
	public string Description = "Enjoy this free game! Challenge yourself and your friends in this great African game.";


	void Awake() {
		Caption = "My Record in Mancala3D Game is "+(PlayerPrefs.GetInt ("SW", 0) + PlayerPrefs.GetInt ("MW", 0)).ToString()+
			" Wins, "+(PlayerPrefs.GetInt ("SD", 0) + PlayerPrefs.GetInt ("MD", 0)).ToString()+" Draws and "+
			(PlayerPrefs.GetInt ("SL", 0) + PlayerPrefs.GetInt ("ML", 0)).ToString()+" Loses. Can u beat it?";
		
	}
	// Use this for initialization
	void Start () {
	
	}
	public void FacebookShare(){
		Application.OpenURL("https://www.facebook.com/dialog/feed?"+
			"app_id="+AppID+
			"&link="+Link+
			"&picture="+Picture+
			"&name="+SpaceHere(Name)+
			"&caption="+SpaceHere(Caption)+
			"&description="+SpaceHere(Description)+
			"&redirect_uri=https://facebook.com/");
	}

	string SpaceHere (string val) {
		return val.Replace(" ", "%20"); // %20 is only used for space
	}

	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseDown() {
		FacebookShare ();
	}
}
