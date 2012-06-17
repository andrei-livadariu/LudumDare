using UnityEngine;
using System.Collections;

public class TrackerMonitor : MonoBehaviour {
	public GameObject linkedObject;
	
	private GameController gc;
	private GameObject level;
	private TrackerSingleMarker marker;
	
	// Use this for initialization
	void Start () {
		gc = GameObject.Find( "GameController" ).GetComponent<GameController>();
		level = GameObject.Find( "Level" );
		linkedObject = transform.GetChild( 0 ).gameObject;
		marker = GetComponent<TrackerSingleMarker>();
	}
	
	// Update is called once per frame
	void Update () {
		if( gc.initialized ) {
			if( marker.visible ) {
				StopCoroutine( "Timeout" );
				linkedObject.SetActiveRecursively( true );
				Vector3 relativePosition = level.transform.InverseTransformPoint( transform.position );
				
				// Bind the position to the level area
				relativePosition.x = Mathf.Clamp( relativePosition.x, -150f, 150f );
				relativePosition.z = Mathf.Clamp( relativePosition.z, -150f, 150f );
				
				relativePosition.x = Mathf.RoundToInt( ( relativePosition.x + 150f ) / 100 ) * 100f - 150f;
				relativePosition.z = Mathf.RoundToInt( ( relativePosition.z + 150f ) / 100 ) * 100f - 150f;
				
				linkedObject.transform.localPosition = new Vector3(
					relativePosition.x,
					gc.currentHeight * 55f,
					relativePosition.z
				);
			} else {
				StartCoroutine( "Timeout" );
			}
		}
	}
	
	IEnumerator Timeout() {
		yield return new WaitForSeconds( 2.0f );
		linkedObject.SetActiveRecursively( false );
	}
	
	public void OnShow() {
		++gc.totalMarkers;
		if( ! gc.initialized ) {
			if( gc.totalMarkers == 1 ) {
				// Enable the level and align its corner to the new marker
				level.SetActiveRecursively( true );
				level.transform.parent = transform;
				level.transform.localPosition = new Vector3( 150f, -150f, 0f );
				level.transform.localEulerAngles = new Vector3( 90f, 0f, 0f );
			}
			else if( gc.totalMarkers == 2 ) {
				// The level returns to world space
				level.transform.parent = null;
				
				// Move the tracker meshes into the level
				TrackerMonitor[] allTrackers = FindObjectsOfType( typeof( TrackerMonitor ) ) as TrackerMonitor[];
				foreach( TrackerMonitor tracker in allTrackers ) {
					tracker.linkedObject.transform.parent = level.transform;
					tracker.linkedObject.transform.localPosition = new Vector3( 0f, gc.currentHeight * 55f, 0f );
					tracker.linkedObject.transform.localEulerAngles = new Vector3( 0f, 0f, 0f );
				}
				
				gc.initialized = true;
			}
		}
	}
	
	public void OnHide() {
		--gc.totalMarkers;
	}
}
