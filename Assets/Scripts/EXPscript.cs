using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EXPscript : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float timebeforedestoryed;
    [SerializeField] bool ismedal = false;
    GameObject Player;


    public void setandshowexp( Vector3 pos)
    {
        Instantiate(gameObject, pos, transform.rotation);
    }
    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }
    private void Update()
    {
        rb.velocity = Vector2.up * 2;
        Destroy(gameObject, timebeforedestoryed);
        if(ismedal)
        {
            rb.velocity = Vector2.up * 3;
            Vector2 position = this.transform.position;
            position.x = Player.transform.position.x - 0.2f;
            transform.position = position;
        }
    }


}
