using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] float Speed = 15f;
    AimAndShoot Aim;
    Rigidbody2D rb;
    Vector2 Direction;
    float rotation;
    [SerializeField] GameObject Shootpoint;
    UpradeManager manager;
    Vector3 Diference;
    [SerializeField] Vector3 offset;
    [SerializeField] GameObject[] arrowSFX;


    private void Start()
    {
        int SFXcout = Random.Range(0, 4);
        Instantiate(arrowSFX[SFXcout],transform.position,transform.rotation);
        Destroy(gameObject, 6f);
        Aim = GameObject.FindGameObjectWithTag("Aim").GetComponent<AimAndShoot>();
        rb = GetComponent<Rigidbody2D>();
        Shootpoint = GameObject.FindGameObjectWithTag("ShootPoint");
        Direction = ((new Vector3(25, -6, 0)) - transform.position).normalized;
        manager = GameObject.FindGameObjectWithTag("Manager").GetComponent<UpradeManager>();
        Diference = new Vector3(25, -6, 0) - transform.position;
        rotation = Mathf.Atan2(Diference.y, Diference.x) * Mathf.Rad2Deg;
        if (Aim.Target != null)
        {
            Shootpoint.transform.position = Aim.Target.transform.position;
            Direction = (Shootpoint.transform.position - transform.position) - offset;
            Direction = Direction.normalized;
            Diference = (Shootpoint.transform.position - transform.position) - offset;
            rotation = Mathf.Atan2(Diference.y, Diference.x) * Mathf.Rad2Deg;
        }

    }
    
    private void FixedUpdate()
    {
        rb.velocity = Direction * Speed;
        transform.rotation = Quaternion.Euler(0, 0, rotation );
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == ("Body"))
        {
            Destroy(gameObject);
        }
    }
}
