/*
    Built by Brian Foster, Fall 2019
    
    This script uses the Sphere Collider trigger on the player
    to detect "NextLevel" and "LastLevel" tags on objects.
    Then it moves the player to the next level and looks for a 
    "PlayerSpawn" object in the scene to move the player to.
    If it doesn't see one, it moves the player to 0,1,0 and hopes
    that the level is built for that.

    This script can also load a specific level if they run into an object tagged as 
    "SpecificLevel" that is named the level that you want the player to go to.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(SphereCollider))]
public class SceneChanger : MonoBehaviour
{
    public float teleportWaitTime = 0.5f;

    void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag("NextLevel")) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            StartCoroutine(TeleportPlayer());
        } else if(other.gameObject.CompareTag("LastLevel")) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
            StartCoroutine(TeleportPlayer());
        } else if(other.gameObject.CompareTag("SpecficLevel")) {
            string levelName = other.gameObject.name;
            SceneManager.LoadScene(levelName);
            StartCoroutine(TeleportPlayer());
        } 
    }

    IEnumerator TeleportPlayer() {
        yield return new WaitForSeconds(teleportWaitTime);
        // make sure the FPSController's rigidbody is set to "Interpolate" and "Continuous".
        if(GameObject.Find("PlayerSpawn") != null) {
            this.transform.position = GameObject.Find("PlayerSpawn").transform.position;
        } else {
            this.transform.position = new Vector3(0,1,0);
        }
    }
}
