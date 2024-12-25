using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBody : MonoBehaviour
{
    public static float HeroUltybuff = 1;
    public static float HeroPassive = 1;
    [SerializeField] Color frostbite;
    [SerializeField] Color Burning;
    [SerializeField] Enemy enemy;
    [SerializeField] AimAndShoot aim;
    [SerializeField] SpriteRenderer GolemSprite;
    UpradeManager Upgrade;
    public static float FireDps = 0.85f;
    bool takingfireDMG = false;
    float dpstime = 1.75f;
    float timerfordps = 0;
    bool takingfrostDMG = false;
    [SerializeField] GameObject FireParticle;
    [SerializeField] GameObject FrostParticle;
    CameraControll camera;
    float camerabuff = 1.46f;



    private void FixedUpdate()
    {
        
        if(takingfireDMG)
        {
            enemy.TakeDMG(FireDps);
            GolemSprite.color = Burning;
            timerfordps += Time.deltaTime;
            if(timerfordps >= dpstime)
            {
                GolemSprite.color = Color.white;
                takingfireDMG = false;
                timerfordps = 0;
            }
        }
        if (takingfrostDMG)
        {
            GolemSprite.color = frostbite;
            enemy.Frost = 0.3f;
            timerfordps += Time.deltaTime;
            if (timerfordps >= dpstime)
            {
                GolemSprite.color = Color.white;
                takingfrostDMG = false;
                enemy.Frost = 1;

            }
        }
    }
    private void Start()
    {
        camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraControll>();
        aim = GameObject.FindGameObjectWithTag("Aim").GetComponent<AimAndShoot>();
        Upgrade = GameObject.FindGameObjectWithTag("Manager").GetComponent<UpradeManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == ("Arrow") )
        {
            enemy.LastDmg = 0;
            if(camera.battlebuff == true)
            {
                StartCoroutine(enemy.Hurt());
                if(UpradeManager.HeroActive == false)
                {
                    enemy.TakeDMG(Upgrade.DMGtoTake * camerabuff);
                }
                else
                {
                    enemy.TakeDMG(Upgrade.DMGtoTake * enemy.HeroPasiveDMG * HeroUltybuff);
                }
            }
            if (camera.battlebuff == false)
            {
                StartCoroutine(enemy.Hurt());
                enemy.TakeDMG(Upgrade.DMGtoTake * enemy.HeroPasiveDMG * HeroUltybuff);
            }


        }
        if(collision.gameObject.layer == (8))
        {
            Instantiate(FireParticle,transform.position,transform.rotation);
            takingfireDMG = true;
            timerfordps = 0;
        }
        if (collision.gameObject.layer == (9))
        {
            StartCoroutine(Frost());
        }
    }
    
    IEnumerator Frost()
    {
        Instantiate(FrostParticle, transform.position, Quaternion.identity);
        takingfrostDMG = true;
        timerfordps = 0;
        yield return new WaitForSeconds(1.75f);

    }

}
