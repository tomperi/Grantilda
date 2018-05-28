using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : MonoBehaviour
{

    public GameObject spawner;
    public GameObject projectilePrefab;
    public GameObject projectile;
    public FrameManager frameManager;
    public SpawnProjectileDirection spawnDirection;
    public StoneTrigger recentStoneTrigger;
    public bool projectileIsAlive;
    public bool stoneIsAlive;
    
    // Use this for initialization
    void Start()
    {
        projectileIsAlive = false;
        stoneIsAlive = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T)) // Cheat Code!
        {
            DestroyFloatingStone();
        }
    }

    public void SpawnProjectile(StoneTrigger stoneTrigger)
    {
        if (!projectileIsAlive && stoneIsAlive)
        {
            recentStoneTrigger = stoneTrigger;
            StartCoroutine(SpawnProjectileWithDelay());
        }
    }

    public void EnableNewProjectilesOnDeath()
    {
        projectileIsAlive = false;
        recentStoneTrigger.ResetTrigger();
    }

    public void DestroyFloatingStone()
    {
        stoneIsAlive = false;
        projectileIsAlive = false;
        spawner.GetComponent<FloatingStoneController>().PlayDeathAndDestory();
    }

    IEnumerator SpawnProjectileWithDelay()
    {
        projectileIsAlive = true;
        yield return new WaitForSeconds(1f);
        projectile = Instantiate(projectilePrefab);
        int direction = spawnDirection.Equals(SpawnProjectileDirection.Right) ? 1 : -1;
        projectile.transform.position = new Vector3(spawner.transform.position.x + 5 * direction, spawner.transform.position.y + 3, spawner.transform.position.z);
        projectile.GetComponent<ProjectileController>().projectileManager = this;
        projectile.SetActive(true);
    }
    


}
