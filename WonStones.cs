using UnityEngine;
using System.Collections;

public class WonStones : MonoBehaviour {

	
	public Rigidbody stone;
	//public float timer=0f;
	public int count=0;
	//	public Vector3 SPosition = new Vector3(-39,0,0);
	public Rigidbody myobj;

	void OnMouseDown () {
		
		Debug.Log ("hole clicked");
	}
	public void shoot (float h) {
		
		//Sposition = new Vector3(0.8,0,2);
		myobj = Instantiate(stone,transform.position,transform.rotation)as Rigidbody;
		myobj.velocity =transform.InverseTransformDirection(new Vector3(0,0,h));
		//Debug.Log ("clicked");
		//Destroy(myobj.gameObject,3);
	}
	public void clear () {
		
		
		Debug.Log ("clear");
		Destroy(myobj.gameObject,3);
	}	
	void Update()
	{ 
		
	}
}
