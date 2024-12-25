using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class Enemy : MonoBehaviour
{
    int chanse;
    HeroEXP exp;
    bool notlasthitlimiter = true;
    Gold gold;
    [SerializeField] BoxCollider2D[] boxCollider;
    [SerializeField] GameObject flag;
    [SerializeField] CircleCollider2D circlecollider;
    [SerializeField] HealthBar Health;
    [SerializeField] Animator animator;
    GameObject Player;
    public float CurentHealth;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] GameObject RayPoint;
    [SerializeField] LayerMask GateLayer;
    [SerializeField] CoidGain goldgain;
    [SerializeField] EXPscript EXPgain;
    Spawner spawner;
    CastleHealth Gate;
    public bool AllowMove = true;
    bool Gotgold = false;
    UpradeManager upgrade;
    public float Frost = 1;
    bool Gotcoin = false;
    [SerializeField] GameObject Particle;
    [SerializeField] float enemyHpBuff;
    public int enemyDmgdeal;
    public float enemyspeed;
    public int coinforkill;
    bool hasdied = false;
    [SerializeField] int coingain;
    public int LastDmg = 1;
    bool Limiter = false;
    bool expLimiter = false;
    [SerializeField] float Expyield;
    public float HeroPasiveDMG = 1;
    public bool amourbroken = false;
    [SerializeField] GameObject particle;
    [SerializeField] GameObject MedalGain;
    bool haveshownbrokenarmour = false;
    bool gotmedal = false;
    bool lasthitexplimit = false;
    public GameObject Slowparticle;


    private void Update()
    {
        if(amourbroken && !haveshownbrokenarmour)
        {
            particle.SetActive(true);
            haveshownbrokenarmour = true;
        }
        if(this.gameObject == HeroMovement.Target)
        {
            flag.SetActive(true);
        }
        else if(this.gameObject != HeroMovement.Target)
        {
            flag.SetActive(false);
        }
        if(CurentHealth <= 0)
        {
            

            Death();
        }
        if(Gate.GateDestroyed == true)
        {
            AllowMove = false;
            animator.SetBool("Taunt", true);
        }
        
       
    }
    private void FixedUpdate()
    {
        if(AllowMove == true)
        {
            rb.velocity = Vector2.left * upgrade.enemySpeed * enemyspeed * Frost;
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }
    private void Start()
    {
        chanse = Random.Range(0, 10);
        if(UpradeManager.HeroActive == true)
        {
            exp = GameObject.FindGameObjectWithTag("EXP").GetComponent<HeroEXP>();
        }
        spawner = GameObject.FindGameObjectWithTag("Spawner").GetComponent<Spawner>(); 
        Gate = GameObject.FindGameObjectWithTag("CastleHealth").GetComponent<CastleHealth>();
        gold = GameObject.FindGameObjectWithTag("Gold").GetComponent<Gold>();
        upgrade = GameObject.FindGameObjectWithTag("Manager").GetComponent<UpradeManager>();
        CurentHealth = upgrade.enemyMaxHP + enemyHpBuff;
        Health.slider.maxValue = upgrade.enemyMaxHP + enemyHpBuff;
        Health.slider.value = CurentHealth;
        Player = GameObject.FindGameObjectWithTag("Player");
    }
    public void TakeDMG(float DMG)
    {
        CurentHealth -= DMG;

        Health.SetHealth(CurentHealth);
    }

    void Death()
    {
        gameObject.layer = 2;
        if(LastDmg == 1 && !Limiter && HeroMovement.Target == this.gameObject)
        {
            //Hero Last Hit
            EXPgain.setandshowexp(this.transform.position);
            HeroMovement.attackMode = false;
            HeroMovement.HeroKill = true;
            HeroMovement.Chase = false;
            HeroMovement.Target = null;
            Limiter = true;
            
        }
        if (LastDmg != 1 && notlasthitlimiter && HeroMovement.Target == this.gameObject)
        {
            //Tower Last Hit
            HeroMovement.NotLastHit = true;
            HeroMovement.Chase = false;
            HeroMovement.attackMode = false;
            notlasthitlimiter = false;
            HeroMovement.anim.SetBool("Walk", false);
            HeroMovement.Target = null;
        }
        if (!Gotgold)
        {
            gold.AddGold(coingain);
            Gotgold = true;
        }
        
        if (LastDmg == 1 && !gotmedal)
        {
            
            if(chanse >= 5)
            {
                Instantiate(MedalGain, Player.transform.position, transform.rotation);
                upgrade.gainMedal(1);
                gotmedal = true;
            }
        }
        if (!hasdied)
        {
            
            spawner.enemykilled += 1;
            hasdied = true;
        }  
        boxCollider[0].enabled = false;
        boxCollider[1].enabled = false;
        circlecollider.enabled = false;
        AllowMove = false;
        gameObject.tag = "Untagged";
        animator.SetBool("Death", true);
        animator.SetBool("AttackMode", false);
        if(!Gotcoin)
        {
            goldgain.setandshow(coinforkill,this.transform.position);
            Gotcoin = true;
        }
        if(UpradeManager.HeroActive == true)
        {
            if (!expLimiter)
            {
                exp.expGain(Expyield);
                expLimiter = true;
            }
        }
        
        if(LastDmg == 1 && !lasthitexplimit)
        {
            exp.expGain(Expyield * 0.65f);
            lasthitexplimit = true;
        }
        Destroy(gameObject,2f);
    }
    public void HurtRoutine()
    {
        StartCoroutine(Hurt());
    }
    public  IEnumerator Hurt()
    {
        Instantiate(Particle,transform.position,transform.rotation);
        animator.SetBool("Hurt", true);
        yield return new WaitForSeconds(0.01f);
        animator.SetBool("Hurt", false);
    }
}
