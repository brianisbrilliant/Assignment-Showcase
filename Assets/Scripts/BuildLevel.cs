// BF Currently all of the 'if(debug)' code is there because there is considerable extra time when building this project.
// I am interested in the optimization possible on this project.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildLevel : MonoBehaviour
{
	[Tooltip("Different folders for game Objects in the scene go here.")]
	public Transform[] parents;
	[Tooltip("How long between turning each object on?")]
	public float interval = 0.1f;
	[Tooltip("How long do you want the total build time to be?")]
	public float totalLoadTime = 5f;
	[Tooltip("Check this if you want to load based on 'Total Load Time' versus 'interval' time.")]
	public bool loadByTime = true;
	[Tooltip("This script will turn on this object when it is finished building the level.")]
	public GameObject player;
	[Tooltip("Have the 'Load in Camera' animated over the course of one second. This script will slow it to match the build process.")]
	public Animator loadInCamAnimator;
	[Tooltip("Check this to see messages in the console about build timings.")]
	public bool debug = true;

	int totalObjects = 0;

    // Start is called before the first frame update
    void Start()
    {
		if(debug) Debug.Log("Starting BuildLevel at " + Time.realtimeSinceStartup.ToString("0.00"));
		
		player.SetActive(false);
		foreach(Transform parent in parents) {
			foreach(Transform child in parent) {
				child.gameObject.SetActive(false);
				totalObjects += 1;
			}
		}
		
		if(debug) Debug.Log("Total objects = " + totalObjects);
		
		if(loadByTime) {
			interval = totalLoadTime / totalObjects;
		}
		
		StartCoroutine(Build());
		
		if(loadInCamAnimator != null) {
			if(loadByTime) {
				loadInCamAnimator.speed = 1 / totalLoadTime;
			} else {
				loadInCamAnimator.speed = 1 / (interval * totalObjects);
			}
		}
		
		if(debug) Debug.Log("Cam Anim speed = " + loadInCamAnimator.speed);
    }

	IEnumerator Build() {
		foreach(Transform parent in parents) {
			foreach(Transform child in parent) {
				child.gameObject.SetActive(true);
				
				if(debug) Debug.Log("Turned on another object at " + Time.realtimeSinceStartup.ToString("0.00"));
				
				yield return new WaitForSeconds(interval);
			}
		}
		player.SetActive(true);
		
		Debug.Log("Time since startup = " + Time.realtimeSinceStartup.ToString("0.00"));
	}
}
