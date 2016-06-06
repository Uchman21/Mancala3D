using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Networking;
using System.Text.RegularExpressions;

public class GameSelection :  NetworkBehaviour {
	private Player plays;
	private int Winner;
	int steps=0;
	public int Complayer;
	//[SyncVar]
	public static int []A=new int[12];
	public static int []SeedsWon=new int[2];
	public static int[]phouse = new int[2];
	//[SyncVar]
	public bool mouseclicked = false;
	//[SyncVar]
	private int Numconnection = 0;
	public static int turns= 1;
	public Vector3 hitpoint;

	public bool VisibleHand;
	public int realbox;
	private movehand1 hand;
	private Playerhand phand;
	string posnumber;
	//[SyncVar]
	int posnumint =0;
	private GameObject[] Cstones;
	public int choice = 0;
	public Camera[] mainCamera;
	public bool engage=false;
	private GUIS info;
	SetUp setup;
	Vector3 temp;
	//NetworkView networkView;

	IEnumerator reset(int NumHouseP1, int NumHouseP2, bool isInitial){
		hand.h = 1;
		hand.i = 0;
		hand.j = 5;
		engage = true;

		plays.NewGame(ref A,ref SeedsWon);
		if (isInitial == true && GameSceneManager.selection == "2 players") {
			if (isLocalPlayer) {
				hand.movement (true);
			} else {
				hand.movement (false);
			}
		} else {
			hand.movement (true);
		}
		yield return new WaitForSeconds (2f);
		phouse [0] = NumHouseP1;
		phouse [1] = NumHouseP2;
		engage = false;
		yield return null;
	}
	// Use this for initialization
	void Start() {
		plays= FindObjectOfType(typeof(Player))as Player;
		hand = FindObjectOfType (typeof(movehand1))as movehand1;
		phand = FindObjectOfType (typeof(Playerhand))as Playerhand;
		setup = FindObjectOfType (typeof(SetUp))as SetUp;
		StartCoroutine (reset (6,6, true));
		info = FindObjectOfType (typeof(GUIS))as GUIS;
		StartCoroutine (gamer2players ());
	}



	IEnumerator ShowMessage (string message, float delay) {
		info.GetComponent<GUIText>().text = message;
		info.GetComponent<GUIText>().enabled = true;
		yield return new WaitForSeconds(delay);
		info.GetComponent<GUIText>().enabled = false;
	}


	IEnumerator WhoPlaysFirst2Players(int player){
		if (player == 0) {
			if (plays.CanPlay (0, ref A, ref phouse) == true) {
				steps += 1;
				if (turns == 1) {
					setup.turn.text = "Player 1";
				} else {
					setup.turn.text = "Player 2";
				}
				if (plays.AddGame (A) != 0) {
					while (mouseclicked==false) {
						yield return new WaitForSeconds (0.3f);
					}
					engage = true;
					mouseclicked = false;
					yield return StartCoroutine (plays.GamePlay (turns%2, posnumint - 1, A, SeedsWon, phouse));
					engage = false;
				}
			}
			if (plays.CanPlay (1, ref A, ref phouse) == true) {
				if (turns == 1) {
					setup.turn.text = "Player 1";
				} else {
					setup.turn.text = "Player 2";
				}
				if (plays.AddGame (A) != 0) {
					while (plays.addrequests>0) {
						yield return new WaitForSeconds (0.3f);
					}
					if (plays.AddGame (A) != 0) {
						while (mouseclicked==false) {
							yield return new WaitForSeconds (0.3f);
						}
						engage = true;
						mouseclicked = false;
						yield return StartCoroutine (plays.GamePlay (turns%2, posnumint - 1, A, SeedsWon, phouse));
						engage = false;
					}
				}
			}
		} else {
			if (plays.CanPlay (1, ref A, ref phouse) == true) {
				steps += 1;
				Debug.Log ("in else");
				if (turns == 1) {
					setup.turn.text = "Player 1";
				} else {
					setup.turn.text = "Player 2";
				}
				if (plays.AddGame (A) != 0) {
					while (mouseclicked==false) {
						yield return new WaitForSeconds (0.3f);
					}
					mouseclicked = false;
					engage = true;
					yield return StartCoroutine (plays.GamePlay (turns%2, posnumint, A, SeedsWon, phouse));
					engage = false;
				}
			}
			if (plays.CanPlay (0, ref A, ref phouse) == true) {
				if (turns == 1) {
					setup.turn.text = "Player 1";
				} else {
					setup.turn.text = "Player 2";
				}
				if (plays.AddGame (A) != 0) {
					while (plays.addrequests>0) {
						yield return new WaitForSeconds (0.3f);
					}
					if (plays.AddGame (A) != 0) {
						while (mouseclicked==false) {
							yield return new WaitForSeconds (0.3f);
						}
						mouseclicked = false;
						engage = true;
						yield return StartCoroutine (plays.GamePlay (turns%2, posnumint - 1, A, SeedsWon, phouse));
						engage = false;
					}
				}
			}
		}
	}


	public IEnumerator gamer2players()
	{

		int whoplaysfirst = 0;
		while(hand.waiting==true){yield return new WaitForSeconds(0.3f);}
		hand.waiting = true;
		int rounds = 1;
		int i=0;
		setup.round.text = "Round: "+rounds.ToString ();
		setup.player1house.text = "(" + phouse [0].ToString () + ")";
		setup.player2house.text = "(" + phouse [1].ToString () + ")";
		while(phouse[0]!= 0 && phouse[1] != 0)
		{
			yield return StartCoroutine (WhoPlaysFirst2Players(whoplaysfirst));
			Debug.Log ("total" + (SeedsWon [0] + SeedsWon [1]));
			if(plays.CheckGame(0,ref SeedsWon) != 2){
				if (plays.CheckGame(0,ref SeedsWon) == 10){
					SeedsWon[0] += 4;
					SeedsWon[1] += 4;
				}
				int houseplayer1 = SeedsWon[0]/4;
				i = 0;
				Cstones = GameObject.FindGameObjectsWithTag("stone"+(13));
				while(i<Cstones.Length){    //clear the hole
					Destroy(Cstones[i].gameObject);
					i+=1;
				}
				i=0;
				Cstones = GameObject.FindGameObjectsWithTag("stone"+(14));
				while(i<Cstones.Length){    //clear the hole
					Destroy(Cstones[i].gameObject);
					i+=1;
				}
				Debug.Log ("in check");
				yield return StartCoroutine (reset(houseplayer1, 12 - houseplayer1, false));
				rounds++;
				setup.player1Seed.text = "X 00";
				setup.player2Seed.text = "X 00";
				setup.round.text = "Round: "+rounds.ToString ();
				setup.player1house.text = "(" + phouse [0].ToString () + ")";
				setup.player2house.text = "(" + phouse [1].ToString () + ")";
				whoplaysfirst = (whoplaysfirst+1)%2;
			}

		}

		if (choice == 0) {
			if (plays.CheckGame (0, ref SeedsWon) == 1) {
				setup.EndGame ("YOU WON!", GameSceneManager.choice);
				PlayerPrefs.SetInt ("MW", PlayerPrefs.GetInt ("MW", 0) + 1);
			} else if (plays.CheckGame (0, ref SeedsWon) == 0) {
				setup.EndGame ("YOU LOSE!", GameSceneManager.choice);
				PlayerPrefs.SetInt ("ML", PlayerPrefs.GetInt ("ML", 0) + 1);
			} else {
				setup.EndGame ("DRAW!", GameSceneManager.choice);
				PlayerPrefs.SetInt ("MD", PlayerPrefs.GetInt ("MD", 0) + 1);
			}
		} else {
			if (plays.CheckGame (0, ref SeedsWon) == 1) {
				setup.EndGame ("YOU LOSE!", GameSceneManager.choice);
				PlayerPrefs.SetInt ("ML", PlayerPrefs.GetInt ("ML", 0) + 1);
			} else if (plays.CheckGame (0, ref SeedsWon) == 0) {
				setup.EndGame ("YOU WON!", GameSceneManager.choice);
				PlayerPrefs.SetInt ("MW", PlayerPrefs.GetInt ("MW", 0) + 1);
			} else {
				setup.EndGame ("DRAW!", GameSceneManager.choice);
				PlayerPrefs.SetInt ("MD", PlayerPrefs.GetInt ("MD", 0) + 1);
			}
		}
		Debug.Log ("finish");

	}

	public void getnearest(){
		GameObject closeobj = null;
		for(int i=0;i<12;i++){
			phand.nobj =  GameObject.FindGameObjectWithTag("points"+(i+1)) as GameObject;
			if(!closeobj){
				closeobj = phand.nobj;
			}
			if(Vector3.Distance(phand.targetPosition, phand.nobj.transform.position) <= Vector3.Distance(phand.targetPosition, closeobj.transform.position)){
				closeobj = phand.nobj;
			}
		}
		Debug.Log (phouse [0]);
		Debug.Log (phouse [0]);


		string tagposition = closeobj.tag;
		int hole = int.Parse (tagposition.Replace ("points", ""));
		Debug.Log (hole);
		Debug.Log ( A[hole - 1]);
		Debug.Log (isServer);
		Debug.Log (turns);
		if (turns == 1 && isServer) {
			if (hole <= phouse [0] && A[hole - 1] != 0) {
				if(isLocalPlayer)RpcsyncClick (true, Regex.Replace (tagposition, @"\D", ""), 2);
			}
		} else if (turns == 2 && !isServer) {
			if (hole > phouse [0] && A[hole - 1] != 0) {
				if(isLocalPlayer)CmdsyncClick (true, Regex.Replace (tagposition, @"\D", ""),1);
			}
		}
		posnumber = Regex.Replace(tagposition, @"\D", "");
		posnumint = int.Parse (posnumber);
	}

	void OnMouseDown(){
		Debug.Log ("out");
		Debug.Log (engage);
		Debug.Log ("num player: "+Numconnection);
		if (engage == false && isLocalPlayer && Numconnection == -1 && !GameSceneManager.GameOver && !GameSceneManager.isPaused && !GameSceneManager.isOnOption) {
			Debug.Log ("in");
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition); //Convert mouse position to raycast
			RaycastHit hit; //Create a RaycastHit variable to store the hit data into
			if (Physics.Raycast (ray, out hit, 1000f)) { //If the user clicks the left mouse button and we hit an object with our raycast then
				phand.targetPosition = hit.point; //Store the hit position into the clicked position variable
			}
			getnearest ();
		} 

	}



	[Command]
	void CmdsyncClick(bool state, string posnumber, int Theturn){
		RpcsyncClick (state, posnumber, Theturn);
	}


	[ClientRpc]
	void RpcsyncClick(bool state, string posnumber, int Theturn){
		Debug.Log ("hit");
		posnumint = int.Parse (posnumber);
		turns = Theturn;
		mouseclicked = state;
	}

	[ClientRpc]
	void RpcMoveNotLocalPlayerObject(){
		GameObject[] Players = GameObject.FindGameObjectsWithTag ("Player");
		foreach (GameObject Player in Players) {
			if (!Player.GetComponent<NetworkIdentity> ().isLocalPlayer) {
				Player.transform.position = new Vector3 (0, 0, 0);
			}
		}
		Numconnection = -1;
	}

	void Update ()
	{
		if (Numconnection < 3 && Numconnection != -1) {
			Numconnection = GameSceneManager.Numconnection;
		} else if (Numconnection != -1){
			Debug.Log ("numcon" + Numconnection);
			RpcMoveNotLocalPlayerObject ();
		}
	}
	
}
