using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroFoot : MonoBehaviour
{
    [SerializeField] Animator anim;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "HeroStop")
        {
            anim.SetBool("Walk", false);
            anim.SetBool("Idle", true);
            HeroMovement.moveallow = false;
        }
        
    }
}
