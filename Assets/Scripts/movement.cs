using UnityEngine;
using System.Collections;

public class movement : MonoBehaviour {
	public GameObject head;
	
	public float speed = 10f;
	// Use this for initialization
	void Start () 
	{

	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetMouseButton (0)) {
			Debug.Log ("Pressed left click.");
			GetComponent<Rigidbody> ().AddForce (1 * speed * head.transform.forward);
		}
		Transform tr;
		//		if (Input.GetAxis("Vertical") > 0)
		//		{
		//			transform.Translate(0f,0f,0.1f);
		//		}
		GetComponent<Rigidbody>().AddForce (Input.GetAxis("Vertical") * speed * transform.forward);

		tr= GetComponent<Transform> ();
		tr.eulerAngles = new Vector3(0,tr.eulerAngles.y,0);

		transform.Rotate (0,Input.GetAxis("Horizontal"),0);
	}
}
