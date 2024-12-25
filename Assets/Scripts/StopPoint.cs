using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopPoint : MonoBehaviour
{
    [SerializeField] Enemy enemy;
    [SerializeField] Animator animator;
    public bool isinattackmode = false;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == ("Gate"))
        {
            isinattackmode = true;
            enemy.AllowMove = false;
            animator.SetBool("AttackMode", true);

        }
    }
}
