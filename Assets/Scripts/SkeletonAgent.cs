using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SkeletonAgent : MonoBehaviour
{
	Animator anim;
	NavMeshAgent agent;
	Transform player;
	float distance;

    // Start is called before the first frame update
    void Start()
    {
		anim = this.GetComponent<Animator>();
		agent = this.GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
		
    }

    // Update is called once per frame
    void Update()
    {
		distance = Vector3.Distance(this.transform.position, player.position);
		if(distance > 3) {
			anim.SetFloat("Velocity", 1);
		} else {
			anim.SetFloat("Velocity", 0);
		}
		agent.destination = player.position;
        
    }

	void OnAnimatorMove() {
		agent.velocity = anim.deltaPosition / Time.deltaTime;
	}
}
