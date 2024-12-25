using System.Collections;
using UnityEngine;

public class Hitwave : MonoBehaviour
{
    [SerializeField] Spawner spawner;
    [SerializeField] GameObject Warningpanel;
    [SerializeField] GameObject[] enemiestospawn;
    [SerializeField] int i;
    [SerializeField] float waittime;
    [SerializeField] GameObject Destroyedskulls;
    [SerializeField] GameObject skulls;
    [SerializeField] bool lastwave;
    [SerializeField] AimAndShoot aim;
    [SerializeField] GameObject Victorypanel;
    float counter = 9f;
    [SerializeField] float timebetweenspawn;
    [SerializeField] public bool freePlay = false;
    [SerializeField] CastleHealth castle;




    private void Start()
    {
        StartCoroutine(Warning());
        StartCoroutine(waitandspawn());
    }
    private void Update()
    {
        Destroy(gameObject,45);
        if (lastwave && !freePlay)
        {
            counter -= Time.deltaTime;
        }
        if(counter < 0 && !freePlay)
        {
            if(aim.Target == null && !freePlay && castle.GateDestroyed == false)
            {
                StartCoroutine(Ending());
            }
            
        }
        if(freePlay == true)
        {
            
        }
    }

    IEnumerator waitandspawn()
    {
        spawner.allowtospawn = false;
        yield return new WaitForSeconds(3.3f);
        for (i = enemiestospawn.Length; i > 0; i--)
        {
            Instantiate(enemiestospawn[i - 1], new Vector2(transform.position.x, Random.Range(-13, -1.7f)), transform.rotation);
            transform.Translate(Vector2.right * 2);
            yield return new WaitForSeconds(timebetweenspawn);
        }
        yield return new WaitForSeconds(waittime);
        if(!lastwave && castle.GateDestroyed == false && UpradeManager.HeroActive == true)
        {
            UpradeManager.pauseallow = false;
        }
        skulls.SetActive(false);
        Destroyedskulls.SetActive(true);
        spawner.allowtospawn = true;
    }
    IEnumerator Warning()
    {
        Warningpanel.SetActive(true);
        yield return new WaitForSeconds(3.2f);
        Warningpanel.SetActive(false);
    }
    IEnumerator Ending()
    {
        yield return new WaitForSeconds(3);
        Time.timeScale = 0;
        Victorypanel.SetActive(true);
    }
    public void FreePlayBottun()
    {
        StopAllCoroutines();
        spawner.allowtospawn = true;
        freePlay = true;
        Time.timeScale = 1.0f;
        Victorypanel.SetActive(false);
    }

}
