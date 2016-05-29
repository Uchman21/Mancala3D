using UnityEngine;
using System.Collections;

public class PlayerHole1 : MonoBehaviour {
/*	private bool mouseclicked = false;
	//private Player1 auth;
	private int box;
	public int turn=-1;
	public int realbox;
	public GameObject[] holes= new GameObject[6];*/

	void Start () {
	//auth= FindObjectOfType(typeof(Player1))as Player1;
	}
	
	/*IEnumerator OnMouseDown () {
		box=GetInstanceID();
		Debug.Log ("hole clicked = "+box);
		Debug.Log("turn"+turn);
		if(turn==1){
		yield return StartCoroutine(getty ());
			mouseclicked=true;}
		else Debug.Log("Not your turn");

	}

	int getblock()
	{

			if(box==holes[0].GetInstanceID())return 0;
			else if (box==holes[1].GetInstanceID())return 1;
			else if (box==holes[2].GetInstanceID())return 2;
			else if (box==holes[3].GetInstanceID())return 3;
			else if (box==holes[4].GetInstanceID())return 4;
			else if (box==holes[5].GetInstanceID())return 5;
			else return -1;

	}
	
	IEnumerator getty ()
	{
		realbox=getblock();
		yield return null;

	}

	public IEnumerator Getblock()
	{

		while (mouseclicked==false)yield return new WaitForSeconds(0.5f);
		Debug.Log("out");
		mouseclicked=false;
	}*/

}
