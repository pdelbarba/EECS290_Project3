using UnityEngine;
using System.Collections;

public class MasterScript : MonoBehaviour {
	
	public static GameObject[,] tiles;
	static GameObject selected;
	static int[] selected_ij;
	static GameObject selectSquare;
	public const int GREENTEAM = 1;
	public const int REDTEAM = 2;
	
	// Use this for initialization
	void Start () {
		selected = null;
		tiles = new GameObject[50,50];
		//Dont spawn any random objects in the first 10 rows on each side
		for (var i = 0; i<50; i++) {
			for (var j = 10; j<40; j++) {
				var rand = Random.value;
				if (rand<0.05) {
					GameObject tree = (GameObject)Object.Instantiate(Resources.Load("BigTree",typeof(GameObject)), tileLocation(i,j), Quaternion.AngleAxis(90, Vector3.right));
					tiles[i,j]=tree;
				}
			}
		}
		for (var i = 20; i<30; i++) {
			for (var j = 5; j<10; j++) {
				GameObject tank = (GameObject)Object.Instantiate (Resources.Load ("Tank",typeof(GameObject)), tileLocation (i,j), Quaternion.identity);
				tank.transform.localScale = new Vector3(3.0f, 3.0f, 3.0f);
				tank.transform.Rotate(Vector3.up * 180);
				tank.transform.Rotate(Vector3.left * 90);
				GameObject teamSquare = (GameObject)Object.Instantiate(Resources.Load ("Tank1",typeof(GameObject)), tileLocation (i,j), Quaternion.identity);
				teamSquare.transform.localScale = new Vector3(15, 15, 1);
				var ts = (TankScript)tank.GetComponent (typeof(TankScript));
				ts.teamId = GREENTEAM;
				ts.teamSquare = teamSquare;
				tiles[i,j]=tank;
			}
		}
		for (var i = 20; i<30; i++) {
			for (var j = 40; j<45; j++) {
				GameObject tank = (GameObject)Object.Instantiate (Resources.Load ("Tank",typeof(GameObject)), tileLocation (i,j), Quaternion.identity);
				tank.transform.localScale = new Vector3(3.0f, 3.0f, 3.0f);
				tank.transform.Rotate(Vector3.left * -90);
				GameObject teamSquare = (GameObject)Object.Instantiate(Resources.Load ("Tank2",typeof(GameObject)), tileLocation (i,j), Quaternion.identity);
				teamSquare.transform.localScale = new Vector3(15, 15, 1);
				var ts = (TankScript)tank.GetComponent (typeof(TankScript));
				ts.teamId = REDTEAM;
				ts.teamSquare = teamSquare;
				tiles[i,j]=tank;
			}
		}
	}
	
	public static int[] getIJ(Vector3 v) {
		//Do some raycasting with the vector to get the world position
		Ray ray = Camera.main.ScreenPointToRay(v);
		RaycastHit hit;
		Physics.Raycast(ray, out hit);
		var i = Mathf.FloorToInt((hit.point.x + 500)/20);
		var j = Mathf.FloorToInt((hit.point.y + 500)/20);
		//Best way I could find to return multiple things
		var returnarray = new int[2];
		returnarray[0]=i;
		returnarray[1]=j;
		return returnarray;
	}
	
	public static GameObject getTile(Vector3 v) {
		var tile_ij = getIJ (v);
		return tiles[tile_ij[0],tile_ij[1]];
	}
	
	public static Vector3 tileLocation(int i, int j) {
		return new Vector3((i*20-500)+10,(j*20-500)+10,1);
	}
	
	void Update () {
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
			if (selected != null) {
				Destroy (selectSquare);
			}
			var tank = getTile(Input.mousePosition); // Should retrieve information from array
			selected = tank;
			selected_ij = getIJ (Input.mousePosition);
			TankScript ts = (TankScript)selected.GetComponent(typeof(TankScript));
			selectSquare = (GameObject)Object.Instantiate(Resources.Load ("SelectSquare",typeof(GameObject)), 
				tileLocation (selected_ij[0],selected_ij[1]), Quaternion.identity);
			selectSquare.transform.localScale = new Vector3(ts.range*20, ts.range*20,1);
		}
		if (Input.GetMouseButtonDown(1)) {
			var ij = getIJ(Input.mousePosition);
			if (selected != null) {
				tiles[selected_ij[0],selected_ij[1]]=null;
				TankScript ts = (TankScript)selected.GetComponent(typeof(TankScript));
				ts.MoveTank (ij[0],ij[1]);
				tiles[ij[0],ij[1]]=selected;
				selected = null;
				selected_ij = null;
				Destroy (selectSquare);
			}	
		}
	}
}
