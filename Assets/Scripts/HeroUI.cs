using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroUI : MonoBehaviour
{
    [SerializeField] GameObject Player;
    [SerializeField] float xoffset;
    [SerializeField] float reversexoffest;
    [SerializeField] float yoffset;
    [SerializeField] float reverseyoffest;

    private void Update()
    {
        transform.position = new Vector2(Player.transform.position.x + xoffset, Player.transform.position.y + yoffset);

        if(Player.transform.localScale.x < 0 )
        {
            transform.position = new Vector2(Player.transform.position.x + reversexoffest, Player.transform.position.y + reverseyoffest);
            
        }
    }
}
