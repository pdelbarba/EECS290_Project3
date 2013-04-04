using UnityEngine;
using System.Collections;

public class FlyJets : MonoBehaviour {
	
	float x;
	float y;
	float z;
	int doabarrelroll = 0;
	// Use this for initialization
	void Start () {
		x = transform.position.x;
		y = transform.position.y;
		z = transform.position.z;
		int i = Random.Range(0, 7);
		if(i == 5)
			doabarrelroll = 2;
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3(x, y, z);
		y += 1;
		transform.Rotate(new Vector3(doabarrelroll, 0, 0));
			
		if(y > 1500)
			y = -800;
	}
}
