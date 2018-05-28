using UnityEngine;
using UnityEngine.AI;

public class SpriteController : MonoBehaviour
{
    private NavMeshAgent agent;

    private Animator animator;
    private AudioSource audioSource;

    // Use this for initialization
    void Start ()
	{
	    agent = GetComponentInParent<NavMeshAgent>();
	    animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
        if (agent.velocity.x == 0)
        {
            if (audioSource != null)
            {
                audioSource.Stop();
            }
            animator.SetInteger("Direction", 0);
        }
        else
        {
            if (audioSource != null && !audioSource.isPlaying)
            {
                audioSource.Play();
            }
            if (agent.velocity.x > .5f)
            {
                animator.SetInteger("Direction", 2);
            }
            else if (agent.velocity.x < -.5f)
            {
                animator.SetInteger("Direction", 1);
            }
            else if (agent.velocity.z > .5f)
            {
                // Move Up
            }
            else if (agent.velocity.z < 0f)
            {
                // Move Down
            }
        }
        

    }

    void LateUpdate()
    {

    }
}
