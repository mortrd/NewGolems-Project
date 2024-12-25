using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CameraControll : MonoBehaviour
{
    [SerializeField] EnemyBody body;
    [SerializeField] UpradeManager manager;
    [SerializeField] GameObject Farm;
    [SerializeField] GameObject Battlefield;
    [SerializeField] GameObject[] farmMenu;
    bool GoToFarm = false;
    bool GoToBattlefield = false;
    public bool Infarm = false;
    bool InBattle = false;
    bool Moving = false;
    [SerializeField] GameObject tooltip;
    public bool battlebuff = true;
    [SerializeField] GameObject ECO;
    [SerializeField] GameObject DMG;
    [SerializeField] GameObject secondcam;
    private void Update()
    {
        if(Input.GetKey(KeyCode.A) && !Infarm &&!Moving)
        {
            GoToFarm = true;
            Moving = true;
        }
        if(Input.GetKey(KeyCode.D) && !InBattle && !Moving)
        {
            GoToBattlefield = true;
            Moving=true;
        }
    }
    private void FixedUpdate()
    {
        if(GoToFarm && !Infarm)
        {
            transform.Translate(new Vector2(-1, 0));
        }
        if(GoToBattlefield && !InBattle)
        {
            transform.Translate(new Vector2(1, 0));
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Farm")
        {
            onfarmbuff();
            farmMenu[0].SetActive(true);
            secondcam.SetActive(true);
            GoToFarm =false;
            Moving = false;
            Infarm = true;
        }
        if (collision.gameObject.name == "BattleField")
        {
            onbattlebuff();
            secondcam.SetActive(false);
            GoToBattlefield = false;
            Moving = false;
            InBattle = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Farm")
        {
            
            tooltip.SetActive(false);
            Infarm = false;
            farmMenu[0].SetActive(false);
            farmMenu[1].SetActive(false);
            farmMenu[2].SetActive(false);
        }
        if (collision.gameObject.name == "BattleField")
        {
            InBattle = false;
        }
    }
    void onfarmbuff()
    {
        DMG.SetActive(false);
        ECO.SetActive(true);
        battlebuff = false;
        if(UpradeManager.HeroActive == false)
        {
            manager.Minerspeed += 0.97f;
            manager.miningspeed -= 0.35f;
        }
    }
    void onbattlebuff()
    {
        DMG.SetActive(true);
        ECO.SetActive(false);
        battlebuff = true;
        if(UpradeManager.HeroActive == false)
        {
            manager.Minerspeed -= 0.97f;
            manager.miningspeed += 0.35f;
        }
    }
}
