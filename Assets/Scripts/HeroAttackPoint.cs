using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroAttackPoint : MonoBehaviour
{
    [SerializeField] Animator anim;
    
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            HeroMovement.attackMode = false;
            anim.SetBool("Attack", false);
            anim.SetBool("Walk", false);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
         if(collision.gameObject.layer == 6 && HeroMovement.Chase && collision.gameObject == HeroMovement.Target)
         {
             HeroMovement.attackMode = true;
            anim.SetBool("Attack", true);
            anim.SetBool("Walk", false);
         }
    }
}
