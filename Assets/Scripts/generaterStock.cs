using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

//[System.Serializable]
//public struct xyz
//{
// public float x,y,z;
//};
[System.Serializable]
public struct Item
{
	public string Name,shape;
	public int mode;
	public Vector3 pos;
	public Quaternion rot;
	public float scale;
};
[System.Serializable]
public class ItemHolder
{
	public Item[] items;
}
public class generaterStock : MonoBehaviour {

	private string db = "";
	public bool useweb;

//	string GetText(string json="central"){
//		string fileName = "Assets/Resources/"+json+".json";
//		try{
//			StreamReader theFile = new StreamReader(fileName);
//			using (theFile) {
//				db = theFile.ReadToEnd ();
//			}
//		}
//		catch {
//			db = "";
//			print ("catch");
//		}
//		return db;
//	}

	string GetText(string json="central"){
		string fileName = json ;//+".txt";
		//try{
			TextAsset theFile = Resources.Load(fileName) as TextAsset;
			db = theFile.text;
			print("db" +db);

//		}
//		catch {
//			//db = "{}";
//			print ("catch");
//		}
		return db;
	}

	public IEnumerator LoadFromWeb(string shop = "central"){
//		print("space key pressed");
//		foreach(Transform child in transform) {
//			Destroy(child.gameObject);
//		}
		WWW www = new WWW("http://localhost:8080/bourne/"+shop);
		//yield return new WaitUntil(() => www.isDone);
		yield return www;
		LoadStock (www.text);
			
	}

	void LoadStock(string dbtext){ // my own function
		
		GameObject item, addItem; //create item varible
		Vector3 pos, lastspot, diff;
		pos = lastspot = diff = new Vector3 (0, 0, 0);

		foreach(Transform child in transform) {
			Destroy(child.gameObject);
		}
		//change the db to json so unity can read
		ItemHolder stuff = JsonUtility.FromJson<ItemHolder> (dbtext);

		// instantiate the items into the gameplay
		foreach (Item stock in stuff.items) {

			Quaternion rot = stock.rot;
			rot.w = 1;
			Vector3 scale = new Vector3 (1, 1, 1) * (stock.scale + 1);
			addItem = Resources.Load (stock.shape) as GameObject;
			if (addItem) {

				if (stock.mode == 0) {
					pos = stock.pos;
					diff = lastspot - pos;
					lastspot = pos;
				}
				if (stock.mode == 1) {
					pos = pos - diff;
				}
				item = Instantiate (addItem, pos, rot) as GameObject; // place the stock in the game
				item.name =stock.Name;
				item.transform.localScale = scale;
				item.transform.parent = transform;
				//print("inloop "+stock.shape);

			} else
				print ("can't instantiate");

		}
		//Vector3 scale = addItem.transform.localScale;

	}
	public void Loader(string room="central"){
		if (useweb)
			StartCoroutine (LoadFromWeb (room));
		else
			LoadStock(GetText (room)); //LoadStock(GetText (room));
	}	

	// Use this for initialization
	void Start () {
		Loader ("central");
		//LoadStock(GetText()); // function call

	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Space))
			Loader();
		else if (Input.GetKeyDown (KeyCode.F))
			Loader ("furniture");
		else if (Input.GetKeyDown (KeyCode.T))
			Loader ("fruit");
	}
}