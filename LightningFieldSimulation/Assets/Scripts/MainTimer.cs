using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityOSC;
using UnityEngine.UI;

public class MainTimer : MonoBehaviour {

	public float strikeRange = 500f;
	public Transform bolt;
	public GameObject player;
	
	public Text countText;
	Dictionary<string, ServerLog> servers = new Dictionary<string, ServerLog> ();
	
	// Use this for initialization
	void Start() {
		Application.runInBackground = true;
		
		OSCHandler.Instance.Init();
		//OSCHandler.Instance.CreateClient("PD", IPAddress.Parse("127.0.0.1"), 8000);
		OSCHandler.Instance.SendMessageToClient("pd", "/unity/onboot", "ready");
		
		player = GameObject.Find("Player");
		
		StartCoroutine(StrikeTimer());
	}

	IEnumerator StrikeTimer() {
		yield return new WaitForSeconds(0);
		while (true) {
			Strike();
			float waitTime = Mathf.Pow(Random.Range(2.0f, 3.0f), 2f) - 1; //SQUARE SHORTER TIME INSTEAD
			if (waitTime > 18) {
				waitTime = Random.Range(0.0f, 0.8f);
				//print("DOUBLE STRIKE");
			}
			//print("\tWait time: " + waitTime);
			yield return new WaitForSeconds(waitTime);
		}
	}

	// Update is called once per frame
	void Update() {
	}
	
	void FixedUpdate() {
		//************* Routine for receiving the OSC...
		OSCHandler.Instance.UpdateLogs();
		Dictionary<string, ServerLog> servers = new Dictionary<string, ServerLog>();
		servers = OSCHandler.Instance.Servers;

		foreach (KeyValuePair<string, ServerLog> item in servers) {
			// If we have received at least one packet,
			// show the last received from the log in the Debug console
			if (item.Value.log.Count > 0) {
				int lastPacketIndex = item.Value.packets.Count - 1;

				//get address and data packet
				countText.text = item.Value.packets [lastPacketIndex].Address.ToString ();
				countText.text += item.Value.packets [lastPacketIndex].Data [0].ToString ();

			}
		}
		//*************
	}

	void Strike() {
		//print ("\tI'm striking");
		OSCHandler.Instance.SendMessageToClient("pd", "/unity/onboot", "ready"); //just in case

		Vector3 pos = new Vector3(Random.Range(-strikeRange, strikeRange), 240f, Random.Range(-strikeRange, strikeRange));
		/*GameObject newBolt =*/ //Instantiate(bolt, pos, Quaternion.identity);//(GameObject)Instantiate(Resources.Load("Strike"));
		//newBolt.transform.position = pos;
		pos += new Vector3(250f, 0f, 0f);//REMOVE LATER
		
		float dist = Vector3.Distance(new Vector3(pos.x, 5f, pos.z), player.transform.position);
		double thunderDelay = dist * 4; // max 500 away = 5000 milisec delay
		print("dist to player: " + dist + " -> delay: " + thunderDelay);
		OSCHandler.Instance.SendMessageToClient("pd", "/unity/trigger", (int)thunderDelay);
		
		GenerateBolt(pos);
		
		//app to send to, osc address, actual message
	}
	
	void GenerateBolt(Vector3 initPos) {
		
		int boltSegments = 8 + Random.Range(-6, 7); //top number is exclusive for ints
		float rotAxisMax = 10f; //actually in degrees
		float rotAxisBendMax = 90f;
		//var for previous rotation to factor in
		
		int i = 0;
		Vector3 boltSourcePos = initPos;
		while (i < boltSegments) {
			Transform boltSegment = Instantiate(bolt, boltSourcePos, Quaternion.identity);
			Vector3 rotateAmt = new Vector3(Random.Range(-rotAxisMax, rotAxisMax),
											Random.Range(-rotAxisMax, rotAxisMax),
											Random.Range(-rotAxisMax, rotAxisMax));
			boltSegment.Rotate(rotateAmt);
			
			Vector3 boltSize = boltSegment.GetComponent<Renderer>().bounds.size;
			//boltSegment.position = boltSourcePos + -0.5f*boltSize*boltSegment.rotation.eulerAngles;
			//boltSourcePos = boltSegment.position + 0.5f*boltSize*boltSegment.rotation.eulerAngles;
			boltSegment.position = boltSourcePos + -1f*boltSize;
			boltSourcePos = boltSegment.position;
			i++;
			//initPos += new Vector3(10, 0, 0);
		}
	}
}



















