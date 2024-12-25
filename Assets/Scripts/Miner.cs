using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class Miner : MonoBehaviour
{
    UpradeManager upgrade;
    Gold gold;
    GameObject MineEntrance;
    GameObject DropPoint;
    [SerializeField] SpriteRenderer sprite;
    float Directionx;
    Rigidbody2D rb;
    bool Empty = true;
    bool moveAllow = true;
    CastleHealth castle;
    [SerializeField] Animator anim;
    [SerializeField] CoidGain coin;



    private void Start()
    {
        MineEntrance = GameObject.FindGameObjectWithTag("Enterance");
        DropPoint = GameObject.FindGameObjectWithTag("Respawn");
        rb = GetComponent<Rigidbody2D>();
        gold = GameObject.FindGameObjectWithTag("Gold").GetComponent<Gold>();
        upgrade = GameObject.FindGameObjectWithTag("Manager").GetComponent<UpradeManager>();
        castle = GameObject.FindGameObjectWithTag("CastleHealth").GetComponent<CastleHealth>();
    }
    private void Update()
    {
        if (moveAllow && Empty && castle.GateDestroyed == false)
        {
            Directionx = MineEntrance.transform.position.x - transform.position.x;
        }
        else if( moveAllow && !Empty && castle.GateDestroyed == false)
        {
            Directionx = DropPoint.transform.position.x - transform.position.x;
        }
        if(castle.GateDestroyed == true)
        {
            anim.SetBool("Idle", true);
            rb.velocity = Vector3.zero;
        }
        
        

    }
    private void FixedUpdate()
    {
        if(moveAllow && castle.GateDestroyed == false)
        {
            rb.velocity = new Vector2(Directionx, 0).normalized * upgrade.Minerspeed;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name ==("Entrance") && Empty)
        {
            StartCoroutine(Mining());
        }
        if(collision.gameObject.name == ("DropPoint"))
        {
            StartCoroutine(DropGold());
        }
    }
    IEnumerator Mining()
    {
        if(castle.GateDestroyed == false)
        {
            moveAllow = false;
            rb.velocity = Vector2.zero;
            Empty = false;
            rb.velocity = Vector2.zero;
            sprite.enabled = false;
            yield return new WaitForSeconds(upgrade.miningspeed);
            sprite.enabled = true;
            moveAllow = true;
            transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
            Directionx = DropPoint.transform.position.x - transform.position.x;
        }


    }
    IEnumerator DropGold()
    {
        if(castle.GateDestroyed == false)
        {
            coin.setandshow(upgrade.mineamountc + 1,this.transform.position);
            gold.AddGold(upgrade.goldammount);
            Empty = true;
            transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
            yield return null;
        }


    }
}
