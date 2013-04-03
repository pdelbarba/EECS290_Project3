using UnityEngine;
using System.Collections;

public class MasterScript : MonoBehaviour {
	
	GameObject[,] tiles;
	GameObject selected;
	int[] selected_ij;
	
	// Use this for initialization
	void Start () {
		selected = null;
		tiles = new GameObject[50,50];
		//Dont spawn any random objects in the first 10 rows on each side
		for (var i = 0; i<50; i++) {
			for (var j = 10; j<40; j++) {
				var rand = Random.value;
				if (rand<0.05) {
					GameObject tree = (GameObject)Object.Instantiate(Resources.Load("Tree",typeof(GameObject)), tileLocation(i,j), Quaternion.identity);
					tiles[i,j]=tree;
				}
			}
		}
		for (var i = 20; i<30; i++) {
			for (var j = 5; j<10; j++) {
				GameObject tank = (GameObject)Object.Instantiate (Resources.Load ("Tank1",typeof(GameObject)), tileLocation (i,j), Quaternion.identity);
				tiles[i,j]=tank;
			}
		}
		for (var i = 20; i<30; i++) {
			for (var j = 40; j<45; j++) {
				GameObject tank = (GameObject)Object.Instantiate (Resources.Load ("Tank2",typeof(GameObject)), tileLocation (i,j), Quaternion.identity);
				tiles[i,j]=tank;
			}
		}
	}
	
	int[] getIJ(Vector3 v) {
		//Do some raycasting with the vector to get the world position
		Ray ray = Camera.main.ScreenPointToRay(v);
		RaycastHit hit;
		Physics.Raycast(ray, out hit);
		Debug.Log (hit.point);
		Debug.Log (hit.point.x);
		Debug.Log (hit.point.y);
		var i = Mathf.FloorToInt((hit.point.x + 500)/20);
		var j = Mathf.FloorToInt((hit.point.y + 500)/20);
		var returnarray = new int[2];
		returnarray[0]=i;
		returnarray[1]=j;
		return returnarray;
	}
	
	GameObject getTile(Vector3 v) {
		var tile_ij = getIJ (v);
		return tiles[tile_ij[0],tile_ij[1]];
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
			selected = tank;
			selected_ij = getIJ (Input.mousePosition);
			Debug.Log (tank);
		}
		if (Input.GetMouseButtonDown(1)) {
			var ij = getIJ(Input.mousePosition);
			if (selected != null) {
				tiles[selected_ij[0],selected_ij[1]]=null;
				tiles[ij[0],ij[1]]=selected;
				selected.transform.position = tileLocation (ij[0],ij[1]);
			}	
		}
	}
}
