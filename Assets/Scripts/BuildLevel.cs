using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildLevel : MonoBehaviour
{
	public Transform[] parents;
	public float interval = 0.1f;
	public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
		player.SetActive(false);
		foreach(Transform parent in parents) {
			foreach(Transform child in parent) {
				child.gameObject.SetActive(false);
			}
		}
		StartCoroutine(Build());
    }

	IEnumerator Build() {
		foreach(Transform parent in parents) {
			foreach(Transform child in parent) {
				child.gameObject.SetActive(true);
				yield return new WaitForSeconds(interval);
			}
		}
		player.SetActive(true);
	}
}
