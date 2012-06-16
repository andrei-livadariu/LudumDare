using UnityEngine;
using System.Collections;

public class TrackerMonitor : MonoBehaviour {
	
	private GameController gc;
	
	// Use this for initialization
	void Start () {
		gc = GameObject.Find( "GameController" ).GetComponent<GameController>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void OnShow() {
		if( gc.totalMarkers == 0 ) {
			GameObject level = GameObject.Find( "Level" );
			level.transform.parent = transform;
			level.transform.localPosition = new Vector3( 6f, -6f, 0f );
			level.transform.localEulerAngles = new Vector3( 90f, 0f, 0f );
		}
		++gc.totalMarkers;
	}
	
	public void OnHide() {
		
	}
}
