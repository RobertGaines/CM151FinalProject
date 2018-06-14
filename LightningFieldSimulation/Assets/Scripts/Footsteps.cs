using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityOSC;
using UnityEngine.UI;

public class Footsteps : MonoBehaviour {

	public float sumWalkDist = 0f;
	public float distPerStep = 0.1f;
	public Vector3 prevPos;
	public Vector3 currentPos;
	public float prevYPos;
	public float currentYPos;
	
	// Use this for initialization
	void Start () {
		prevPos = this.transform.position;
		currentPos = prevPos;
		prevYPos = this.transform.position.y;
		currentYPos = prevYPos;
	}

	
	// Update is called once per frame
	void Update () {
		currentPos = new Vector3(this.transform.position.x, 
														 0, 
								 this.transform.position.z);
		currentYPos = this.transform.position.y;		
		
		if (Vector3.Distance(prevPos, currentPos) > 0.03 && Mathf.Abs(currentYPos - prevYPos) < 0.01f ) {
			sumWalkDist += (1f/16f);
		}
		prevPos = currentPos;
		prevYPos = currentYPos;
		
		if (sumWalkDist > distPerStep) {
			OSCHandler.Instance.SendMessageToClient("pd", "/unity/footstep", "supaCrunchy");
			sumWalkDist = 0;
			print("one possibly small step");
		}
	}
}
