using UnityEngine;
using System.Collections;

public class MasterScript : MonoBehaviour {
	
	GameObject[,] tiles;
	
	// Use this for initialization
	void Start () {
		tiles = new GameObject[50,50];
		//Dont spawn any random objects in the first 10 rows on each side
		for (var i = 0; i<50; i++) {
			for (var j = 10; j<40; j++) {
				var rand = Random.value;
				if (rand<0.05) {
					GameObject tree = (GameObject)Object.Instantiate(Resources.Load("Tree",typeof(GameObject)), tileLocation(i,j), Quaternion.identity);
					tree.transform.localScale = new Vector3(15,15,1);
				}
			}
		}
	}
	
	GameObject getTile(Vector3 v) {
		//Do some raycasting with the vector to get the world position
		Ray ray = Camera.main.ScreenPointToRay(v);
		RaycastHit hit;
		Physics.Raycast(ray, out hit);
		Debug.Log (hit.point);
		Debug.Log (hit.point.x);
		Debug.Log (hit.point.z);
		return null;
		//Debug.Log (tiles[v.x,v.y]);
		//return tiles[v.x,v.y];
	}
	
	Vector3 tileLocation(int i, int j) {
		return new Vector3((i*20-500)+10,(j*20-500)+10,1);
	}
	
	void FixedUpdate () {
		if (Input.GetKey (KeyCode.DownArrow)) {
			Camera.main.transform.Translate(new Vector3(0,-10,0));
		} else if (Input.GetKey (KeyCode.UpArrow)) {
			Camera.main.transform.Translate(new Vector3(0,10,0));
		} else if (Input.GetKey (KeyCode.LeftArrow)) {
			Camera.main.transform.Translate(new Vector3(-10,0,0));
		} else if (Input.GetKey (KeyCode.RightArrow)) {
			Camera.main.transform.Translate(new Vector3(10,0,0));
		}
		
		//Select tanks with Left Mouse Button, Action with Right
		if (Input.GetMouseButtonDown(0)) {
			var tank = getTile(Input.mousePosition); // Should retrieve information from array
			
		}
	}
}
