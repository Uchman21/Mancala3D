using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class movehand1 : MonoBehaviour {

		public float moveTime;           //Time it will take object to move, in seconds.
		public LayerMask blockingLayer;//Layer on which collision will be checked.
		public Rigidbody rb2D; 
		private Vector3 startp = new Vector3(-23.01f,25.6f,-4.35f); //The Rigidbody2D component attached to this object.
		private float inverseMoveTime;          //Used to make movement more efficient.
		public int i =0, j=5;
		public int h = 1;
		public float timer=0f;
		public Rigidbody stone;
		public Rigidbody pointloc;
		public int count=0;
		private Rigidbody myobj;
		public Vector3 targetPosition;
		public Rigidbody points;
		public GameObject [] nobj;
		public bool waiting = true;
		public SkinnedMeshRenderer handpoly;
		public static Vector3 orig;
		public bool render;
		public int completed  = 0;
		
		//Protected, virtual functions can be overridden by inheriting classes.
		protected virtual void Start ()
		{
		orig = rb2D.position;
		moveTime = 0.01f;
		inverseMoveTime = 2f / moveTime; //By storing the reciprocal of the move time we can use it by multiplying instead of dividing, this is more efficient.
		}
		
	public void movement(bool render){
		waiting = true;
		targetPosition = rb2D.transform.position;
		while (h <= 12) {
			if (i < 6) {
				StartCoroutine(Move (startp.x + (i * 9.3f), startp.y, startp.z, h, render));
				i = i+1;
			} else {
				StartCoroutine( Move (startp.x + (j * 9.3f), startp.y, startp.z + 8.5f, h, render));
				j = j - 1;
			}
			h = h + 1;
		}
		StartCoroutine( Move (orig.x , orig.y, orig.z,-1, true));

	}
		
		//Move returns true if it is able to move and false if not. 
		//Move takes parameters for x direction, y direction and a RaycastHit2D to check collision.
	public IEnumerator Move (float xDir, float yDir, float zDir, int num, bool render)
		{
		int counter = 1;
			// Calculate end position based on the direction parameters passed in when calling Move.
		Vector3 end = new Vector3 (xDir, yDir, zDir);
			//If nothing was hit, start SmoothMovement co-routine passing in the Vector2 end as destination

		if (num > 0) {
			if (render == true) {
				points = Instantiate (pointloc, end, pointloc.transform.rotation)as Rigidbody;
				points.tag = "points" + (num);
			} else {
				points = Instantiate (pointloc, new Vector3(0,0,0), pointloc.transform.rotation)as Rigidbody;
				points.tag = "not_local";
			}
		} else if ( num == -1){
			
		}
		while (counter <= 4 && num > 0){
				shootInit (5f, num, end, render);

				timer = 0.0f;
				counter= counter + 1;
				yield return new WaitForSeconds(0.5f);	
				
		
		}
		completed++;
		if (completed == 13) {
			yield return new WaitForSeconds (1.5f);
			waiting = false;
			completed = 0;
		}

		}

	public IEnumerator JMove (float xDir, float yDir, float zDir)
	{
		// Calculate end position based on the direction parameters passed in when calling Move.
		Vector3 end = new Vector3 (xDir, yDir, zDir);
		//If nothing was hit, start SmoothMovement co-routine passing in the Vector2 end as destination
		yield return StartCoroutine (SmoothMovement (end));
		
	}
		
		
		//Co-routine for moving units from one space to next, takes a parameter end to specify where to move to.
		public IEnumerator SmoothMovement (Vector3 end)
		{
			//Calculate the remaining distance to move based on the square magnitude of the difference between current position and end parameter. 
			//Square magnitude is used instead of magnitude because it's computationally cheaper.
		float sqrRemainingDistance = (rb2D.transform.position - end).sqrMagnitude;
			
			//While that distance is greater than a very small amount (Epsilon, almost zero):
			while(sqrRemainingDistance > float.Epsilon)
			{
				//Find a new position proportionally closer to the end, based on the moveTime
				Vector3 newPostion = Vector3.MoveTowards(rb2D.position, end, inverseMoveTime * Time.deltaTime);
				
				//Call MovePosition on attached Rigidbody2D and move it to the calculated position.
				rb2D.MovePosition (newPostion);
				
				//Recalculate the remaining distance after moving.
			sqrRemainingDistance = (rb2D.transform.position - end).sqrMagnitude;
				
				//Return and loop until sqrRemainingDistance is close enough to zero to end the function
				yield return null;
			}
		}
		
		
	public void shoot (float h,int num) {
		
		myobj = Instantiate(stone,transform.position,transform.rotation)as Rigidbody;
		myobj.velocity =transform.InverseTransformDirection(new Vector3(0,-5,h*4));
		myobj.tag="stone"+num;
	}

	public void shootInit (float h,int num, Vector3 position, bool render) {


		if (render == false) {
			myobj = Instantiate(stone,new Vector3(0,0,0),transform.rotation)as Rigidbody;
			myobj.velocity =transform.InverseTransformDirection(new Vector3(0,0,h));
			myobj.tag="stone"+num;
			myobj.GetComponent<MeshRenderer> ().enabled = false;
		} else {
			myobj = Instantiate(stone,position,transform.rotation)as Rigidbody;
			myobj.velocity =transform.InverseTransformDirection(new Vector3(0,0,h));
			myobj.tag="stone"+num;
			myobj.GetComponent<MeshRenderer> ().enabled = true;
		}

	}

	
}