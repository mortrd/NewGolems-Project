using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject[] hitwave;
    [SerializeField] GameObject[] enemyPrefab;
    [SerializeField] float spawnrate = 3;
    float timer = 0;
    float deadlinetime = 0;
    [SerializeField] float deadline = 45f;
    [SerializeField] UpradeManager manager;
    [SerializeField] int counter = 0;
    float spawnrateDicrease = 0.5f;
    public static int HPbuff = 20;
    int enemytospawn;
    public int enemykilled = 0;
    public bool allowtospawn = true;
    [SerializeField] WaveUI Waveui; 
    public int waveLvl;
    [SerializeField] Hitwave hit;


    private void Update()
    {
        Waveui.setslider(enemykilled);
        if (counter == 3 || counter == 4 || counter == 5)
        {
            enemytospawn = Random.Range(0, 2);
        }
        if (counter == 6 || counter == 7 || counter == 8)
        {
            enemytospawn = Random.Range(0, 3);
        }
        if (counter == 9 || counter == 10 || counter == 11)
        {
            enemytospawn = Random.Range(0, 4);
        }
        if (counter >= 12)
        {
            enemytospawn = Random.Range(0, 5);
        }


        if (timer < spawnrate)
        {
            timer += Time.deltaTime;
        }
        else
        {
            if (allowtospawn)
            {
                Instantiate(enemyPrefab[enemytospawn], new Vector2(transform.position.x, Random.Range(-12.4f, -1.5f)), Quaternion.identity);
                timer = 0;
            }
            
        }
        if(deadlinetime < deadline)
        {
            deadlinetime += Time.deltaTime;
        }
        else
        {
            increaseDificulty();
            deadlinetime = 0;
        }

        if(enemykilled == 20)
        {
            waveLvl = 1;
            allowtospawn = false;
            hitwave[0].SetActive(true);
        }
        if (enemykilled == 45)
        {
            waveLvl = 2;
            allowtospawn = false;
            hitwave[1].SetActive(true);
        }
        if (enemykilled == 70)
        {
            waveLvl = 3;
            allowtospawn = false;
            hitwave[2].SetActive(true);
        }
    }
    [ContextMenu("IncreaseDificulty")]
    void increaseDificulty()
    {
        counter += 1;
        spawnrate -= spawnrateDicrease;
        manager.enemyMaxHP += HPbuff;
        if (counter < 8)
        {
            deadline -= 5f;
            HPbuff += 9;
        }
        if(counter == 9)
        {
            HPbuff += 9;
        }
        if (counter == 13)
        {
            HPbuff += 9;           
        }
        if(counter == 17)
        {
            deadline -= 5;
            HPbuff += 11;
        }
        if(counter >= 23)
        {
            spawnrate = 0.75f;
            HPbuff += 28;
            deadline = 15f;
        }
        
        
    }
}
