using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Win1))]

public class Win2 : MonoBehaviour {
	
//public GameObject[] holes;
	public int count=0;
	public int gencount=0;
	public int j=0;
	private Win1[] Hole;
	private GameObject Gamef;
	public float timer=0f;

void Start()
	{
		//holes = GameObject.FindGameObjectsWithTag("stonefall");
		/*for(int i=0; i<holes.Length;i++)
		{
			Hole[i]=holes[i].GetComponent<Win1>()as Win1;
		}*/
		//Hole=holes.GetComponents<Win1>();
		//Hole= FindObjectsOfType(typeof(Win1))as Win1[];
				
	}
void Update()
	{ 
		/*if(j<12)
		{
			
			//while(count <4){
				timer+=Time.deltaTime;
			if(timer >1f)
				{
					if(count<2){

					Hole[j].shoot(1f);
				//Destroy(Hole[j].myobj.gameObject,2f);
				Hole[j].myobj.tag="stone"+(j+1);
				//Debug.Log(Hole[j].myobj.name);
					timer = 0.0f;
					count= count + 1;
				} 
				if(count==2)
			{
				j+=1;
				count=0;
			}
			}
		}*/

	}
}




