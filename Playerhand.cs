using UnityEngine;
using System.Collections;

public class Playerhand : MonoBehaviour {
	
	public float moveTime = 0.005f;           //Time it will take object to move, in seconds.
	public LayerMask blockingLayer;//Layer on which collision will be checked.
	private Win1 Fill;
	public Rigidbody rb2D; 
	public Rigidbody startp; //The Rigidbody2D component attached to this object.
	private float inverseMoveTime;          //Used to make movement more efficient.
	public float timer=0f;
	public Rigidbody stone;
	public Rigidbody pointloc;
	//public float timer=0f;
	public int count=0;
	private Animator animator; 
	//	public Vector3 SPosition = new Vector3(-39,0,0);
	private Rigidbody myobj;
	public Vector3 targetPosition;
	public Rigidbody points;
	public GameObject nobj;
	public SkinnedMeshRenderer handpoly;
	Vector3 temp;
	public Camera maincamera;
	public static Vector3 orig;
	
	
	//Protected, virtual functions can be overridden by inheriting classes.
	protected virtual void Start ()
	{
		//handpoly.enabled = false;
		orig = rb2D.position;
		inverseMoveTime = 2f / moveTime;
		//By storing the reciprocal of the move time we can use it by multiplying instead of dividing, this is more efficient.
		
	}
	
	//Move returns true if it is able to move and false if not. 
	//Move takes parameters for x direction, y direction and a RaycastHit2D to check collision.
	public IEnumerator Move (float xDir, float yDir, float zDir, int num)
	{
		int counter = 1;
		// Calculate end position based on the direction parameters passed in when calling Move.
		Vector3 end = new Vector3 (xDir, yDir, zDir);
		//If nothing was hit, start SmoothMovement co-routine passing in the Vector2 end as destination
		yield return StartCoroutine (SmoothMovement (end));
		
		points = Instantiate(pointloc,transform.position,transform.rotation)as Rigidbody;
		points.name = "point" + num;
		points.tag = "points";
		while (counter <= 4 && num > 0){
			shoot(1f,num);
			//Debug.Log(Hole[j].myobj.name);
			timer = 0.0f;
			counter= counter + 1;
			yield return new WaitForSeconds(0.3f);		
			
		}

		
	}


	public IEnumerator JMove (float xDir, float yDir, float zDir)
	{
		//handpoly.enabled = true;
		// Calculate end position based on the direction parameters passed in when calling Move.
		Vector3 end = new Vector3 (xDir, yDir, zDir);
		//Vector3 end1 = new Vector3 (xDir, yDir, zDir + 5 );
		//yield return StartCoroutine (SmoothMovement (end1));
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
		
		//Sposition = new Vector3(0.8,0,2);
		myobj = Instantiate(stone,transform.position,transform.rotation)as Rigidbody;
		myobj.velocity =transform.InverseTransformDirection(new Vector3(0,0,h));
		myobj.tag="stone"+num;
		//Debug.Log ("clicked");
		//Destroy(myobj.gameObject,3);
	}


	
	
}