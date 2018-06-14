using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnStrike : MonoBehaviour {
	
	public float framesUntilFade;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		framesUntilFade--;
		//gameObject = what this script is attached to
		if (framesUntilFade <= 0 && gameObject.GetComponent<Renderer>().material.color.a <= 0) {
			Destroy(gameObject);
		}
		gameObject.GetComponent<Renderer>().material.color -= new Color(0, 0, 0, 0.01f);
	}
}
