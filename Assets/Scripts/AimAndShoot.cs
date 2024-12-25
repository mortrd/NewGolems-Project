using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AimAndShoot : MonoBehaviour
{
    [SerializeField] Rigidbody2D Arrowrb;
    [SerializeField] float AimOffset;
    [SerializeField] GameObject[] ArrowPrefab;
    [SerializeField] public Transform Target;
    public float TargetingRange = 10f;
    public float AttackSpeed = 2.25f;
    [SerializeField] float timer = 2;
    [SerializeField] GameObject Gate;
    [SerializeField] UpradeManager upgrade;
    [SerializeField] CastleHealth Castle;



    private void Start()
    {
        InvokeRepeating("FindTarget", 0f, 0.3f);
        AttackSpeed = 2.25f;
        timer = AttackSpeed;
    }
    private void Update()
    {

        if (Target == null)
            return;


        if (this.timer <= this.AttackSpeed)
        {
            this.timer += Time.deltaTime;
        }
        else
        {
            if(Castle.shootAllow)
            {
                Shoot();
                this.timer = 0;
            }
            
        }

    }
    void FindTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float ShortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;
        foreach (GameObject enemy in enemies)
        {
            float distancetoenemy = Vector3.Distance(new Vector3 (Gate.transform.position.x,Gate.transform.position.y-3f,Gate.transform.position.z), enemy.transform.position);
            if (distancetoenemy + AimOffset < ShortestDistance)
            {
                ShortestDistance = distancetoenemy;
                nearestEnemy = enemy;

            }
        }
            if (nearestEnemy != null && ShortestDistance <= TargetingRange)
            {
                Target = nearestEnemy.transform;
            }
            else
            {
                Target = null;
                timer = AttackSpeed;

            }


        
    }
        void Shoot()
        {
        int arrowstate = ArrowState();
        Instantiate(ArrowPrefab[arrowstate], transform.position, transform.rotation);
            
        }

    int ArrowState()
    {
        if(upgrade.fireUpgrade == true)
        {
            return 1;
        }
        else if(upgrade.frostUpgrade == true) 
        {
            return 2;
        }else
        {
            return 0;
        }
    }
    
}

