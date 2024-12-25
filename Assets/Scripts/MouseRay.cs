using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseRay : MonoBehaviour
{
    public static bool targetfound = false;
    public static RaycastHit2D hit;
    public static Ray ray;
    [SerializeField] Animator anim;
    [SerializeField] GameObject Flag;
    [SerializeField] HeroMovement heromovement;
    [SerializeField] CastleHealth casle;
    private void Start()
    {
        
    }
    private void Update()
    {
        if(!hit)
        {
            HeroMovement.animationtimem = 0.3f;
        }
        if(Input.GetMouseButtonDown(1))
        {
            if(casle.GateDestroyed == false)
            {
                CastRay();
            }
            
        }
    }
    void CastRay()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);

        if(hit)
        {
            HeroMovement.animationtimem = 0.3f;
            if (hit.transform.tag == "Enemy")
            {
                HeroMovement.moveallow = false;
                Flag.SetActive(false);

                if(!HeroMovement.Target)
                {
                    HeroMovement.Target = hit.collider.gameObject;
                    targetfound = true;
                    HeroMovement.Chase = true;
                    anim.SetBool("Idle", false);
                    anim.SetBool("Walk", true);
                }
                else if(HeroMovement.Target)
                {
                    if(HeroMovement.Target != hit.collider.gameObject)
                    {
                        anim.SetBool("Walk", true);
                    }
                    HeroMovement.attackMode = false;
                    anim.SetBool("Attack", false);
                    HeroMovement.Target = null;
                    targetfound = true;
                    HeroMovement.Target = hit.collider.gameObject;
                    HeroMovement.Chase = true;
                }
            }
        }
    }
}
