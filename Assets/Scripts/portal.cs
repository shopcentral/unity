using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
//using generaterStock;

public class portal : MonoBehaviour {
	public generaterStock generate;
	public string shopname;
	void OnCollisionEnter (Collision col)
	{
		StartCoroutine (generate.LoadFromWeb (shopname));
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
