using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHit : MonoBehaviour
{
    [SerializeField] Enemy enemy;
    CastleHealth castle;
    public bool hasHit = false;

    private void Start()
    {
        castle = GameObject.FindWithTag("CastleHealth").GetComponent<CastleHealth>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag ==("Gate"))
        {
            StartCoroutine(hit());
        }
    }
    IEnumerator hit()
    {
        hasHit = true;
        castle.TakeDMG(enemy.enemyDmgdeal);
        yield return new WaitForSeconds(0.7f);
        hasHit = false;
    }
}
