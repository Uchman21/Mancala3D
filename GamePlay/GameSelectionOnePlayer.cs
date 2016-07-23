using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Text.RegularExpressions;

public class GameSelectionOnePlayer :  MonoBehaviour {
	private Player plays;
	private int Winner;
	int steps=0;
	public int Complayer;
	public static int []A=new int[12];
	public static int []SeedsWon=new int[2];
	public static int[]phouse = new int[2];
	public bool mouseclicked = false;
	public static int turns= 1;
	public Vector3 hitpoint;
	public bool isDoubleClick = true;

	public bool VisibleHand;
	public int realbox;
	private movehand1 hand;
	string posnumber;
	private GameObject[] Cstones;
	public int choice = 0;
	public Camera[] mainCamera;
	public bool engage=false;
	SetUp setup;
	Vector3 temp;
	private int level;

	IEnumerator reset(int NumHouseP1, int NumHouseP2){
		hand.h = 1;
		hand.i = 0;
		hand.j = 5;
		phouse [0] = NumHouseP1;
		phouse [1] = NumHouseP2;
		engage = true;
		plays.NewGame(ref A,ref SeedsWon);
		hand.movement (true);
		engage = false;
		yield return null;
	}
	// Use this for initialization
	void Start() {
		Screen.sleepTimeout = SleepTimeout.NeverSleep;
		level = PlayerPrefs.GetInt ("MancalaLevel", 1);
		plays= FindObjectOfType(typeof(Player))as Player;
		hand = FindObjectOfType (typeof(movehand1))as movehand1;
		setup = FindObjectOfType (typeof(SetUp))as SetUp;
		StartCoroutine (reset (6,6));
		StartCoroutine (gamer1player ());
	}




	IEnumerator WhoPlaysFirst1Player(int player){
		if (player == 0) {
			if(plays.CanPlay(0,ref A, ref phouse) == true){
				steps += 1;
				turns = 1;
				setup.turn.text = "You";
				if (plays.AddGame (A) != 0) {
					#if UNITY_ANDROID
						Handheld.Vibrate ();
					#endif
					while (mouseclicked==false) {
						yield return new WaitForSeconds (0.3f);
					}
					mouseclicked = false;
					turns = 2;
					engage =  true;
					yield return StartCoroutine (plays.GamePlay (0, int.Parse (posnumber) - 1));
					engage = false;
				}
			}
			if(plays.CanPlay(1,ref A, ref phouse) == true){
				Complayer = 2;
				//splaying=true;
				setup.turn.text = "Computer";
				if (plays.AddGame (A) != 0) {
					while (plays.addrequests>0) {
						yield return new WaitForSeconds (0.3f);
					}
					engage = true;
					yield return StartCoroutine (plays.Player2 (level));
					engage =  false;
				}
			}
		} else {
			if(plays.CanPlay(1,ref A, ref phouse) == true){
				Complayer = 2;
				setup.turn.text = "Computer";
				if (plays.AddGame (A) != 0) {
					while (plays.addrequests>0) {
						yield return new WaitForSeconds (0.3f);
					}
					engage = true;
					yield return StartCoroutine (plays.Player2 (level));
					engage = false;
				}
			}
			if(plays.CanPlay(0,ref A, ref phouse) == true){
				steps += 1;
				turns = 1;
				setup.turn.text = "You";
				if (plays.AddGame (A) != 0) {
					#if UNITY_ANDROID
						Handheld.Vibrate ();
					#endif
					while (mouseclicked==false) {
						yield return new WaitForSeconds (0.3f);
					}
					mouseclicked = false;
					turns = 2;
					engage = true;
					yield return StartCoroutine (plays.GamePlay (0, int.Parse (posnumber) - 1));
					engage = false;
				}
			}
		}
	}



	public IEnumerator gamer1player()
	{

		int whoplaysfirst = 0;
		int rounds = 1;
		int i=0;
		setup.round.text = "Round: "+rounds.ToString ();
		setup.player1house.text = "(" + phouse [0].ToString () + ")";
		setup.player2house.text = "(" + phouse [1].ToString () + ")";
		while((phouse[0]!= 0 && phouse[1] != 0) && rounds <= 6)
		{
			while(hand.waiting==true){yield return new WaitForSeconds(0.3f);}
			yield return StartCoroutine (WhoPlaysFirst1Player(whoplaysfirst));
			if(plays.CheckGame(0,ref SeedsWon) !=2){
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

				Holes[] ClHoles = FindObjectsOfType(typeof(Holes))as Holes[];
				foreach (Holes ClHole in ClHoles) {
					Destroy (ClHole.gameObject);
				}
				yield return StartCoroutine (reset(houseplayer1, 12 - houseplayer1));
				rounds++;
				setup.player1Seed.text = "X 00";
				setup.player2Seed.text = "X 00";
				setup.round.text = "Round: "+rounds.ToString ();
				setup.player1house.text = "(" + phouse [0].ToString () + ")";
				setup.player2house.text = "(" + phouse [1].ToString () + ")";
				turns = (whoplaysfirst + 1) % 2 + 1;
				whoplaysfirst = (whoplaysfirst+1)%2;
			}

		}

		if ((SeedsWon[0]/4) > 6) {
			setup.EndGame ("YOU WON!", GameSceneManager.choice);
			PlayerPrefs.SetInt ("SW", PlayerPrefs.GetInt ("SW", 0) + 1);
		} else if ((SeedsWon[0]/4) < 6) {
			setup.EndGame ("YOU LOSE!", GameSceneManager.choice);
			PlayerPrefs.SetInt ("SL", PlayerPrefs.GetInt ("SL", 0) + 1);
		} else {
			setup.EndGame ("DRAW!", GameSceneManager.choice);
			PlayerPrefs.SetInt ("SD", PlayerPrefs.GetInt ("SD", 0) + 1);
		}
		Debug.Log ("finish");

	}



	public IEnumerator handleClick(string tagposition, float catchTime){
		if (engage == false) {
			yield return new WaitForSeconds (catchTime);
			int hole = int.Parse (tagposition.Replace ("points", ""));
			Debug.Log (turns);
			if (turns == 1 && isDoubleClick == false) {
				if (hole <= phouse [0] && A [hole - 1] != 0) {
					mouseclicked = true;
					//isDoubleClick = true;
				}
			} else if (turns == 2 && isDoubleClick == false) {
				if (hole > phouse [0] && A [hole - 1] != 0) {
					mouseclicked = true;
					//isDoubleClick = true;
				}
			}
			posnumber = Regex.Replace (tagposition, @"\D", "");
		}
	}




}
