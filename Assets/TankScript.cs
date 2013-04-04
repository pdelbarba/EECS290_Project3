using UnityEngine;
using System.Collections;

public class TankScript : MonoBehaviour {
	public GameObject teamSquare;
	public int teamId;
	public int range = 10;
	public int health = 10;
	public int gunRange = 16;
	public GameObject healthR;
	public GameObject healthG;
	
	// Use this for initialization
	void Start () {
		range = 10;
		health = 10;
		gunRange = 16;
		//Vector3 location = MasterScript.tileLocation(i,j);
		//healthG.transform.position = location;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void hit() {
		health -= 2;
		healthG.transform.localScale = new Vector3(health, 2, 1);
	}
	
	public void destroy() {
		Destroy(healthG);
		Destroy(healthR);
		Destroy(teamSquare);
	}
			
	public void MoveTank(int i, int j) {
		Vector3 location = MasterScript.tileLocation(i,j);
		this.transform.position = location;
		teamSquare.transform.position = location;
		//healthG.transform.position = location;
		healthR.transform.position = new Vector3(location.x, location.y, 10);
		healthG.transform.position = new Vector3(location.x, location.y, 10.1f);

	}
}
