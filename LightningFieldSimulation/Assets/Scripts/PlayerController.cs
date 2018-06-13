using UnityEngine;
using System.Collections;
//using UnityOSC;

public class PlayerController : MonoBehaviour 
{	
	public float speed;
	public GUIText countText;
	public GUIText winText;
	private int count;
	private int numberOfGameObjects;
	
	void Start()
	{
		count = 0;
		SetCountText();
		winText.text = "";
		numberOfGameObjects = GameObject.FindGameObjectsWithTag("PickUp").Length;
		
		//original osc lines here
	}
	
	void FixedUpdate ()
	{
		float moveHorizontal = Input.GetAxis("Horizontal");
		float moveVertical = Input.GetAxis("Vertical");
		
		Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
		
		GetComponent<Rigidbody>().AddForce (movement * speed * Time.deltaTime);
	}
	
	void OnTriggerEnter(Collider other) 
	{
		if(other.gameObject.tag == "PickUp")
		{
			other.gameObject.SetActive(false);
			count = count + 1;
			SetCountText();
		}
	}
	
	void SetCountText ()
	{
		countText.text = "Count: " + count.ToString();
		if(count >= numberOfGameObjects)
		{
			winText.text = "YOU'RE WINNER!"; //if there is a disease that deserves to be propagated, it's this one -> if this actually happened, I would be a monster. But it would be glorious.
		}
	}
}
