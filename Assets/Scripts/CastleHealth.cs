using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CastleHealth : MonoBehaviour
{
    public Slider slider;
    public float CurentHealth;
    public float MaxHealth = 100;
    [SerializeField] GameObject Spawner;
    [SerializeField] GameObject retrymenu;
    public bool GateDestroyed = false;
    [SerializeField] GameObject[] Castles;
    [SerializeField] GameObject DeathParticle;
    public bool shootAllow = true;

    private void Start()
    {
        MaxHealth = CurentHealth = 100;
        SetMaxHealth(MaxHealth);
    }
    
    public void SetHealth(float health)
    {
        slider.value = CurentHealth;
    }

    public void SetMaxHealth(float health)
    {
        slider.maxValue = MaxHealth;
        slider.value = MaxHealth;
    }
    public void TakeDMG(float DMG)
    {
        CurentHealth -= DMG;
        SetHealth(CurentHealth);
    }
    private void Update()
    {
        
        if (CurentHealth <= 0)
        {
            GateDestory();
        }
    }
    void GateDestory()
    {
        Castles[0].gameObject.SetActive(false);
        Castles[1].gameObject.SetActive(true);
        GateDestroyed = true;
        DeathParticle.SetActive(true);
        shootAllow = false;
        Spawner.SetActive(false);
        retrymenu.SetActive(true);

    }
    public void Restart()
    {
        SceneManager.LoadScene(1);
    }


}
