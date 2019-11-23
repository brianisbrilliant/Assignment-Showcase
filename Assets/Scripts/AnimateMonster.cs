using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AnimateMonster : MonoBehaviour
{
	public Animator anim;
	public float closeToPlayer = 5;
	NavMeshAgent agent;
	Transform player;

    // Start is called before the first frame update
    void Start()
    {
		agent = this.GetComponent<NavMeshAgent>();
        player = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetFloat("Speed", agent.velocity.magnitude);
		float distance = Vector3.Distance(this.transform.position, player.transform.position);
		if(distance < closeToPlayer) {
			anim.SetBool("PlayerIsNear", true);
		} else {
			anim.SetBool("PlayerIsNear", false);
		}
    }

	void OnAnimatorMove() {
		agent.velocity = anim.deltaPosition / Time.deltaTime;
	}
}
