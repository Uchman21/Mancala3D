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
	public static int []A=new int[12];
	public static int []SeedsWon=new int[2];
	public static int[]phouse = new int[2];
	public bool mouseclicked = false;
	public bool isDoubleClick = true;
	private int Numconnection = 0;
	public static int turns= 1;
	public Vector3 hitpoint;

	public bool VisibleHand;
	public int realbox;
	private movehand1 hand;
	string posnumber;
	int posnumint =0;
	private GameObject[] Cstones;
	public int choice = 0;
	public Camera[] mainCamera;
	SetUp setup;
	Vector3 temp;

	IEnumerator reset(int NumHouseP1, int NumHouseP2, bool isInitial){
		hand.h = 1;
		hand.i = 0;
		hand.j = 5;
		GameSceneManager.engage = true;
		phouse [0] = NumHouseP1;
		phouse [1] = NumHouseP2;
		plays.NewGame(ref A,ref SeedsWon);
		if (isInitial == true && GameSceneManager.selection == "2 Players") {
			if (isLocalPlayer) {
				hand.movement (true);
			} else {
				hand.movement (false);
			}
		} else {
			hand.movement (true);
		}
		GameSceneManager.engage = false;
		yield return null;
	}
	// Use this for initialization
	void Start() {
		Screen.sleepTimeout = SleepTimeout.NeverSleep;
		plays= FindObjectOfType(typeof(Player))as Player;
		hand = FindObjectOfType (typeof(movehand1))as movehand1;
		setup = FindObjectOfType (typeof(SetUp))as SetUp;
		StartCoroutine (reset (6,6, true));
		StartCoroutine (gamer2players ());
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
					while (mouseclicked==false && GameSceneManager.timeLeft > 0) {
						yield return new WaitForSeconds (0.3f);
					}
					GameSceneManager.engage = true;
					mouseclicked = false;
					if (GameSceneManager.timeLeft > 0 && posnumint < 13) {
						setup.Timer.enabled = false;
						yield return StartCoroutine (plays.GamePlay (turns % 2, posnumint - 1));
						GameSceneManager.timeLeft = 30;
					}
					GameSceneManager.engage = false;
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
						while (mouseclicked==false && GameSceneManager.timeLeft > 0 ) {
							yield return new WaitForSeconds (0.3f);
						}
						GameSceneManager.engage = true;
						mouseclicked = false;
						if (GameSceneManager.timeLeft > 0 && posnumint < 13) {
							setup.Timer.enabled = false;
							yield return StartCoroutine (plays.GamePlay (turns % 2, posnumint - 1));
							GameSceneManager.timeLeft = 30;
						}
						GameSceneManager.engage = false;
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
					while (mouseclicked==false && GameSceneManager.timeLeft > 0) {
						yield return new WaitForSeconds (0.3f);
					}
					mouseclicked = false;
					GameSceneManager.engage = true;
					if (GameSceneManager.timeLeft > 0 && posnumint < 13) {
						setup.Timer.enabled = false;
						yield return StartCoroutine (plays.GamePlay (turns % 2, posnumint));
						GameSceneManager.timeLeft = 30;
					}
					GameSceneManager.engage = false;
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
						while (mouseclicked==false && GameSceneManager.timeLeft > 0) {
							yield return new WaitForSeconds (0.3f);
						}
						mouseclicked = false;
						GameSceneManager.engage = true;
						if (GameSceneManager.timeLeft > 0 && posnumint < 13) {
							setup.Timer.enabled = false;
							yield return StartCoroutine (plays.GamePlay (turns % 2, posnumint - 1));
							GameSceneManager.timeLeft = 30;
						}
						GameSceneManager.engage = false;
					}
				}
			}
		}
	}


	public IEnumerator gamer2players()
	{

		int whoplaysfirst = 0;
		hand.waiting = true;
		int rounds = 1;
		int i=0;
		setup.round.text = "Round: "+rounds.ToString ();
		setup.player1house.text = "(" + phouse [0].ToString () + ")";
		setup.player2house.text = "(" + phouse [1].ToString () + ")";
		while((phouse[0]!= 0 && phouse[1] != 0) && rounds <= 6)
		{
			while(hand.waiting==true || Numconnection != -1){yield return new WaitForSeconds(0.3f);}
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

				Holes[] ClHoles = FindObjectsOfType(typeof(Holes))as Holes[];
				foreach (Holes ClHole in ClHoles) {
					Destroy (ClHole.gameObject);
				}
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
			if ((SeedsWon[0]/4) > 6) {
				setup.EndGame ("YOU WON!", GameSceneManager.choice);
				PlayerPrefs.SetInt ("MW", PlayerPrefs.GetInt ("MW", 0) + 1);
			} else if ((SeedsWon[0]/4) < 6) {
				setup.EndGame ("YOU LOSE!", GameSceneManager.choice);
				PlayerPrefs.SetInt ("ML", PlayerPrefs.GetInt ("ML", 0) + 1);
			} else {
				setup.EndGame ("DRAW!", GameSceneManager.choice);
				PlayerPrefs.SetInt ("MD", PlayerPrefs.GetInt ("MD", 0) + 1);
			}
		} else {
			if ((SeedsWon[1]/4) > 6) {
				setup.EndGame ("YOU LOSE!", GameSceneManager.choice);
				PlayerPrefs.SetInt ("ML", PlayerPrefs.GetInt ("ML", 0) + 1);
			} else if ((SeedsWon[0]/4) < 6) {
				setup.EndGame ("YOU WON!", GameSceneManager.choice);
				PlayerPrefs.SetInt ("MW", PlayerPrefs.GetInt ("MW", 0) + 1);
			} else {
				setup.EndGame ("DRAW!", GameSceneManager.choice);
				PlayerPrefs.SetInt ("MD", PlayerPrefs.GetInt ("MD", 0) + 1);
			}
		}
		Debug.Log ("finish");

	}

	public IEnumerator handleClick(string tagposition, float catchTime){
		if (GameSceneManager.engage == false) {
			yield return new WaitForSeconds (catchTime);
			int hole = int.Parse (tagposition.Replace ("points", ""));
			Debug.Log ("turn " + turns);
			if (turns == 1 && isServer) {
				if (hole <= phouse [0] && A [hole - 1] != 0) {
					if (isLocalPlayer)
						RpcsyncClick (true, Regex.Replace (tagposition, @"\D", ""), 2);
				}
			} else if (turns == 2 && !isServer) {
				if (hole > phouse [0] && A [hole - 1] != 0) {
					if (isLocalPlayer)
						CmdsyncClick (true, Regex.Replace (tagposition, @"\D", ""), 1);
				}
			}
			posnumber = Regex.Replace (tagposition, @"\D", "");
			posnumint = int.Parse (posnumber);
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
				Player.transform.position = new Vector3 (0, 0, 0);
		}
		Numconnection = -1;
	}

	void Update ()
	{
		if(((turns == 1 && isServer && isLocalPlayer) || (turns == 2 && !isServer && isLocalPlayer)) && Holes.connectioned == true && GameSceneManager.engage == false && hand.waiting==false){
			GameSceneManager.timeLeft -= Time.deltaTime;
			if (setup.Timer.enabled == false) {
				if (isLocalPlayer) {
					#if UNITY_ANDROID
					Handheld.Vibrate ();
					#endif
					setup.Timer.enabled = true;
				}
			}
			if (GameSceneManager.timeLeft < 0) {
				setup.Timer.enabled = false;
				GameSceneManager.timeLeft = 30;
				if (turns == 2 && !isServer && isLocalPlayer) {
					CmdsyncClick (false, "20", 1);
				} else if(turns == 1 && isServer && isLocalPlayer) {
					RpcsyncClick (false, "20", 2);
				}
			} else {
				setup.Timer.text = "TIMER: " + Mathf.RoundToInt(GameSceneManager.timeLeft);
			}
		}
		if (Numconnection < 3 && Numconnection != -1) {
			Numconnection = GameSceneManager.Numconnection;
		} else if (Numconnection != -1) {
			Holes.connectioned = true;
			Debug.Log ("numcon" + Numconnection);
			Numconnection = -1;
		}
	}
	
}
