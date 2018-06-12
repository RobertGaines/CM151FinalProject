using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainTimer : MonoBehaviour {

	public float strikeRange = 500f;
	public Transform bolt;
	
	// Use this for initialization
	void Start() {
		StartCoroutine(StrikeTimer());
	}

	IEnumerator StrikeTimer() {
		yield return new WaitForSeconds(0);
		while (true) {
			Strike();
			yield return new WaitForSeconds(Random.Range(0.0f, 1.0f));
		}
	}

	// Update is called once per frame
	void Update() {

	}

	void Strike() {
		print ("\tI'm striking");

		Vector3 pos = new Vector3(Random.Range(-strikeRange, strikeRange), 80f, Random.Range(-strikeRange, strikeRange));
		/*GameObject newBolt =*/ Instantiate(bolt, pos, Quaternion.identity);//(GameObject)Instantiate(Resources.Load("Strike"));
		//newBolt.transform.position = pos;
	}
}
