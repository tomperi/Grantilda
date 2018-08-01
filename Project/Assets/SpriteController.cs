using UnityEngine;
using UnityEngine.AI;

public class SpriteController : MonoBehaviour
{
    public GameObject Shadow;

    private NavMeshAgent agent;
    private Animator animator;
    private AudioSource audioSource;
    private bool teleport = false;

    public bool Teleport
    {
        get
        {
            return teleport;
        }
    }

    // Use this for initialization
    void Start ()
	{
	    agent = GetComponentInParent<NavMeshAgent>();
	    animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
	    if (!teleport)
	    {
	        if (agent.velocity.x == 0 && agent.velocity.z == 0)
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
	            else if ((agent.velocity.z > .5 || agent.velocity.z < .5f) && (animator.GetInteger("Direction") == 0))
	            {
	                animator.SetInteger("Direction", -1);
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
        

    }

    public void SetTeleportOut()
    {
        teleport = true;
        Shadow.SetActive(false);
        animator.SetInteger("Direction", 5);
    }

    public void StartTeleportIn()
    {
        agent.isStopped = true;
        Shadow.SetActive(false);
    }

    public void EndTeleportIn()
    {
        agent.isStopped = false;
        Shadow.SetActive(true);
    }

    public void StartShrinkPlayer()
    {
        //agent.isStopped = true;
        animator.SetBool("Shrink", true);
    }

    public void EndShrinkPlayer()
    {
        //agent.isStopped = false;
        animator.SetBool("Shrink", false);
    }
}
