using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoidGain : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] TextMeshProUGUI text;

    
    public void setandshow(int amount,Vector2 pos)
    {

        text.text = "+" + amount.ToString();
        Instantiate(gameObject,pos,transform.rotation);
    }
    private void Update()
    {

        rb.velocity = Vector2.up * 2;
        Destroy(gameObject, 1);
    }
}
