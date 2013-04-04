using UnityEngine;
using System.Collections;

public class TankScript : MonoBehaviour {
	public GameObject teamSquare;
	public int teamId;
	public int range = 10;
	public int health = 10;
	public int gunRange = 15;
	public GameObject healthR;
	public GameObject healthG;
	
	// Use this for initialization
	void Start () {
		range = 10;
		health = 10;
		gunRange = 15;
		//Vector3 location = MasterScript.tileLocation(i,j);
		//healthG.transform.position = location;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void MoveTank(int i, int j) {
		Vector3 location = MasterScript.tileLocation(i,j);
		this.transform.position = location;
		teamSquare.transform.position = location;
		//healthG.transform.position = location;
		healthR.transform.position = location;
		healthG.transform.position = new Vector3(location.x, location.y, 10.1f);

	}
}
