using UnityEngine;
using System.Collections;

public class BGScroller : MonoBehaviour {

 public float scrollSpeed;
 public float tileSizedZ;
 
 private Vector3 startPosition;
	
	void Start () 
	{
		startPosition = transform.position;
	
	}
	
	
	void Update () 
	{
	 float newPosition = Mathf.Repeat (Time.time * scrollSpeed, tileSizedZ); 
	 transform.position = startPosition + Vector3.forward * newPosition;
	}
}
