using UnityEngine;
using System.Collections;

public class TankScript : MonoBehaviour {
	public GameObject teamSquare;
	public int teamId;
	public int range;

	// Use this for initialization
	void Start () {
		range = 5;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void MoveTank(int i, int j) {
		var location = MasterScript.tileLocation(i,j);
		this.transform.position = location;
		teamSquare.transform.position = location;
	}
}
