using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class HeroMovement : MonoBehaviour
{
    public static float FrostLVL = 0.65f;
    public static int critlvl = 1;
    public static bool attackMode = false;
    public static bool Chase = false;
    public static bool HeroKill = false;
    public static bool NotLastHit = false;
    public static float PassiveDMGbuff = 1.10f;
    public int DMG = 12;
    public static bool moveallow = false;
    [SerializeField] GameObject critindicater;
    [SerializeField] GameObject frostindicator;
    [SerializeField] GameObject HeroRally;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] GameObject Direction;
    [SerializeField] Camera cam;
    [SerializeField] float xoffset;
    [SerializeField] float yoffset;
    [SerializeField] public float speed;
    [SerializeField] public float timeBTWattack;
    public float StartTimeBTwAttack;
    public Transform Attackpos;
    public float attackRange;
    public static float animationtimem = 0.3f;
    public LayerMask WhatISenemies;
    public static GameObject Target;
    [SerializeField] float ChasexOffset;
    [SerializeField] float reverseChasexOffset;
    [SerializeField] float ChaseyOffset;
    [SerializeField] float reverseChaseyOffset;
    public static Animator anim;
    public static int HeroLVL = 1;
    [SerializeField] GameObject Flag;
    [SerializeField] public static float UltTimer = 10;
    [SerializeField] GameObject crithitParticle;
    [SerializeField] HeroSkills skills;
    [SerializeField] GameObject ultyparticle;
    [SerializeField] GameObject armourbreakparticle;
    [SerializeField] CastleHealth castle;
    [SerializeField] UpradeManager upgrade;
    private void Start()
    {
        upgrade.enemyMaxHP = 222;
        UpradeManager.HeroActive = true;
        StartTimeBTwAttack = 0.471f;
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        if(castle.GateDestroyed)
        {
            anim.SetBool("Dead",true);
        }
        if(HeroSkills.UltimateState)
        {
            if(UltTimer > 0)
            {
                UltTimer -= Time.deltaTime;
            }
            else
            {
                ultyparticle.SetActive(false);
                HeroSkills.frostattack = false;
                HeroSkills.CriticalAttack = false;
                HeroSkills.UltimateState = false;
                critindicater.SetActive(false);
                frostindicator.SetActive(false);
                EnemyBody.HeroUltybuff = 1;
                skills.isoncooldown4 = true;
                skills.ability4image.fillAmount = 1;
                skills.ultyindicater.SetActive(false);
                UltTimer = 10;
            }
        }
        if (NotLastHit)
        {
            anim.SetBool("Idle", true);
            NotLastHit = false;
        }
        if(Chase && Target != null)
        {
            HeroRally.transform.position = new Vector2 (Target.transform.position.x+0.5f,0);
        }
        if(!attackMode)
        {
            anim.SetBool("Attack", false);
        }
        
        if(HeroKill)
        {
            anim.SetBool("Idle", true);
            HeroKill = false;
        }
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            Info();
        }
        if (Input.GetMouseButtonDown(1) && !MouseRay.hit && castle.GateDestroyed == false)
        {
            Vector3 mousedir = cam.ScreenToWorldPoint(Input.mousePosition);
            Chase = false;
            attackMode = false;
            moveallow = true;
            Target = null;
            Direction.transform.position = new Vector3 (mousedir.x, mousedir.y,0);
            anim.SetBool("Walk", true);
            anim.SetBool("Attack", false);
            anim.SetBool("Idle", false);
            flag.timer = .5f;
            Flag.SetActive(true);
        }
        if(transform.position.x > HeroRally.transform.position.x)
        {
            transform.localScale = new Vector3(-1.07f,1.07f, 1.07f);
        }
        else
        {
            transform.localScale = new Vector3(1.07f, 1.07f, 1.07f);
        }
    }
    private void FixedUpdate()
    {
        if (moveallow)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(Direction.transform.position.x + xoffset,Direction.transform.position.y +yoffset), speed);
        }
        if(Chase && Target.transform.position.x > -4)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(Target.transform.position.x + ChasexOffset, Target.transform.position.y + ChaseyOffset), speed);
        }
        else if(Chase && Target.transform.position.x <= -4)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(Target.transform.position.x + reverseChasexOffset, Target.transform.position.y + reverseChaseyOffset), speed);
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(Attackpos.position, attackRange);
    }
    void Info()
    {
        Debug.Log(Target.name);
        if(Chase)
        {
            Debug.Log("Chase True");
        }
        if(!Chase)
        {
            Debug.Log("Chase Flase");
        }
        if(attackMode)
        {
            Debug.Log("AttackMode True");
        }
        if (!attackMode)
        {
            Debug.Log("AttackMode False");
        }
    }
    public void Attack()
    {
        if (attackMode)
        {
                Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(Attackpos.position, attackRange, WhatISenemies);
                for (int i = 0; i < enemiesToDamage.Length; i++)
                {
                enemiesToDamage[i].GetComponent<Enemy>().LastDmg = 1;
                if (HeroSkills.HeroPassive)
                    {
                        enemiesToDamage[i].GetComponent<Enemy>().LastDmg = 1;
                        enemiesToDamage[i].GetComponent<Enemy>().HeroPasiveDMG = PassiveDMGbuff;
                        enemiesToDamage[i].GetComponent<Enemy>().amourbroken = true;
                    }
                    enemiesToDamage[i].GetComponent<Enemy>().LastDmg = 1;
                    enemiesToDamage[i].GetComponent<Enemy>().TakeDMG(DMG * enemiesToDamage[i].GetComponent<Enemy>().HeroPasiveDMG);
                    enemiesToDamage[i].GetComponent<Enemy>().HurtRoutine();
                    enemiesToDamage[i].GetComponent<Enemy>().LastDmg = 1;
                    if (HeroSkills.frostattack)
                    {
                        enemiesToDamage[i].GetComponent<Enemy>().Frost = FrostLVL;
                        enemiesToDamage[i].GetComponent<Enemy>().Slowparticle.SetActive(true);
                    if (!HeroSkills.UltimateState)
                        {
                            frostindicator.SetActive(false);
                            skills.ability1indicater.SetActive(false);
                            HeroSkills.frostattack = false;
                            skills.isoncooldown1 = true;
                            skills.ability1image.fillAmount = 1;
                        }
                    }
                    if (HeroSkills.CriticalAttack)
                    {
                        if (critlvl == 1)
                        {
                            enemiesToDamage[i].GetComponent<Enemy>().TakeDMG(DMG * enemiesToDamage[i].GetComponent<Enemy>().HeroPasiveDMG);
                            Instantiate(crithitParticle, enemiesToDamage[i].transform.position, Quaternion.identity);
                        }
                        else if (critlvl == 2)
                        {
                            enemiesToDamage[i].GetComponent<Enemy>().TakeDMG(DMG * enemiesToDamage[i].GetComponent<Enemy>().HeroPasiveDMG);
                            enemiesToDamage[i].GetComponent<Enemy>().TakeDMG(DMG * enemiesToDamage[i].GetComponent<Enemy>().HeroPasiveDMG);
                            Instantiate(crithitParticle, enemiesToDamage[i].transform.position, Quaternion.identity);
                        }
                        else if (critlvl >= 3)
                        {
                            enemiesToDamage[i].GetComponent<Enemy>().TakeDMG(DMG * enemiesToDamage[i].GetComponent<Enemy>().HeroPasiveDMG);
                            enemiesToDamage[i].GetComponent<Enemy>().TakeDMG(DMG * enemiesToDamage[i].GetComponent<Enemy>().HeroPasiveDMG);
                            enemiesToDamage[i].GetComponent<Enemy>().TakeDMG(DMG * enemiesToDamage[i].GetComponent<Enemy>().HeroPasiveDMG);
                            enemiesToDamage[i].GetComponent<Enemy>().TakeDMG(DMG / 2 * enemiesToDamage[i].GetComponent<Enemy>().HeroPasiveDMG);
                            Instantiate(crithitParticle, enemiesToDamage[i].transform.position, Quaternion.identity);
                        }
                        if (!HeroSkills.UltimateState)
                        {
                            skills.ability2indicater.SetActive(false);
                            skills.isoncooldown2 = true;
                            skills.ability2image.fillAmount = 1;
                            critindicater.SetActive(false);
                            HeroSkills.CriticalAttack = false;
                        }
                    }

                }
        }
    }
}
