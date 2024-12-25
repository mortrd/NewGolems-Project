using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.UI;

public class HeroSkills : MonoBehaviour
{
    
    public static bool frostattack = false;
    public static bool CriticalAttack = false;
    public static bool HeroPassive = false;
    public static bool UltimateState = false;
    public static bool UltimateStatelvl2 = false;
    public static bool isactive1 = false;
    public static bool isactive2 = false;
    public static bool isactive3 = false;
    public static bool isactive4 = false;

    [Header("Ability 1")]
    [SerializeField] GameObject frostindicater;
    [SerializeField] public Image ability1image;
    [SerializeField] float cooldown1 = 6f;
    public bool isoncooldown1 = false;
    [SerializeField] KeyCode ability1key;
    public GameObject ability1indicater;

    [Header("Ability 2")]
    [SerializeField] GameObject Critindicater;
    [SerializeField] public  Image ability2image;
    [SerializeField] float cooldown2 = 6f;
    public bool isoncooldown2 = false;
    [SerializeField] KeyCode ability2key;
    public GameObject ability2indicater;



    [Header("Ulty")]
    [SerializeField] public Image ability4image;
    [SerializeField] float cooldown4 = 35f;
    public bool isoncooldown4 = false;
    [SerializeField] KeyCode ability4key;
    public GameObject ultyindicater;
    [SerializeField] GameObject ultyparticle;


    private void Start()
    {
        ability1image.fillAmount = 0;
        ability2image.fillAmount = 0;
        ability4image.fillAmount = 0;
    }
    private void Update()
    {
        ability1();
        ability2();
        Ultimate();
    }
    public void ability1()
    {
        if(isactive1 && Input.GetKey(ability1key) && isoncooldown1 == false)
        {
            frostindicater.SetActive(true);
            ability1indicater.SetActive(true);
            frostattack = true;
        }

        if(isoncooldown1)
        {
            ability1image.fillAmount -= 1 / cooldown1 * Time.deltaTime;
            if(ability1image.fillAmount <= 0)
            {
                ability1image.fillAmount = 0;
                isoncooldown1 = false;
            }
        }
    }
    public void ability1clickable()
    {
        if (isactive1 && isoncooldown1 == false)
        {
            frostindicater.SetActive(true);
            ability1indicater.SetActive(true);
            frostattack = true;
        }

        if (isoncooldown1)
        {
            ability1image.fillAmount -= 1 / cooldown1 * Time.deltaTime;
            if (ability1image.fillAmount <= 0)
            {
                ability1image.fillAmount = 0;
                isoncooldown1 = false;
            }
        }
    }
    public void ability2()
    {
        if (isactive2 && Input.GetKey(ability2key) && isoncooldown2 == false)
        {
            ability2indicater.SetActive(true);
            Critindicater.SetActive(true);
            CriticalAttack = true;
        }

        if (isoncooldown2)
        {
            ability2image.fillAmount -= 1 / cooldown2 * Time.deltaTime;
            if (ability2image.fillAmount <= 0)
            {
                ability2image.fillAmount = 0;
                isoncooldown2 = false;
            }
        }
    }
    public void ability2Clickable()
    {
        if (isactive2 && isoncooldown2 == false)
        {
            ability2indicater.SetActive(true);
            Critindicater.SetActive(true);
            CriticalAttack = true;
        }

        if (isoncooldown2)
        {
            ability2image.fillAmount -= 1 / cooldown2 * Time.deltaTime;
            if (ability2image.fillAmount <= 0)
            {
                ability2image.fillAmount = 0;
                isoncooldown2 = false;
            }
        }
    }
    public void passiveability()
    {
        if (isactive3)
        {
            HeroPassive = true;
        }
    }
    public void Ultimate()
    {
        if (isactive4 && Input.GetKey(ability4key) && isoncooldown4 == false)
        {
            ultyparticle.SetActive(true);
            Critindicater.SetActive(true);
            frostindicater.SetActive(true);
            ultyindicater.SetActive(true);
            UltimateState = true;
            frostattack = true;
            CriticalAttack = true;
            if (UltimateStatelvl2)
            {
                EnemyBody.HeroUltybuff = 1.6f;
            }
        }

        if (isoncooldown4)
        {
            ability4image.fillAmount -= 1 / cooldown4 * Time.deltaTime;
            if (ability4image.fillAmount <= 0)
            {
                ability4image.fillAmount = 0;
                isoncooldown4 = false;
            }
        }

    }
    public void UltimateClickable()
    {
        if (isactive4 && isoncooldown4 == false)
        {
            ultyparticle.SetActive(true);
            Critindicater.SetActive(true);
            frostindicater.SetActive(true);
            ultyindicater.SetActive(true);
            UltimateState = true;
            frostattack = true;
            CriticalAttack = true;
            if (UltimateStatelvl2)
            {
                EnemyBody.HeroUltybuff = 1.6f;
            }
            
        }

        if (isoncooldown4)
        {
            ability4image.fillAmount -= 1 / cooldown4 * Time.deltaTime;
            if (ability4image.fillAmount <= 0)
            {
                ability4image.fillAmount = 0;
                isoncooldown4 = false;
            }
        }

    }
}
