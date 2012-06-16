using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {
	
	// Tiles start at (150, 150, 0)
	// Movement on the X and Z axis is done in increments of -100
	// Movement on the Y axis is done in increments of 55
	
	// The order of the axis is Y, X, Z (height, then 2d position)
	public GameObject[,,] level = new GameObject[16,4,4];
	public int currentHeight = 0;
	public int totalMarkers = 0;
	public bool initialized = false;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
