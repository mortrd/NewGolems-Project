using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minotaur : MonoBehaviour
{
    [SerializeField] EnemyHit hit;
    [SerializeField] Animator anim;
    private void Update()
    {
        if (hit.hasHit)
        {
            anim.SetBool("Wait", true);
            
        }else
        {
            anim.SetBool("Wait", false);
        }
    }
}
