using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HeroEXP : MonoBehaviour
{
    [SerializeField] int counterfor1 = 0;
    int counterfor2 = 0;
    int counterfor3 = 0;
    int counterfor4 = 0;
    [SerializeField] HeroMovement hero;
    [SerializeField] Slider expbar;
    [SerializeField] TextMeshProUGUI HeroLvltext;
    bool ultiUnlicked = false;
    public int upgradepoint = 1;
    public static bool haveupgradepoint;
    [SerializeField] GameObject[] ActivationIcons;
    [SerializeField] GameObject[] UpgradeICONS;
    [SerializeField] GameObject[] skillpoints;
    int ability1point = 1;
    int ability2point = 1;
    int ability3point = 1;
    int ability4point = 1;
    [SerializeField] TextMeshProUGUI[] skillpointtext;
    [SerializeField] Button ability1Button;
    [SerializeField] Button ability2Button;
    [SerializeField] Button ability3Button;
    [SerializeField] Button ability4Button;

    private void Start()
    {
        haveupgradepoint = true;
        ActivationIcons[0].SetActive(true);
        ActivationIcons[1].SetActive(true);
        ActivationIcons[2].SetActive(true);
    }
    private void Update()
    {
        if(expbar.value == expbar.maxValue)
        {
            LvlUp();
        }
        if (upgradepoint > 0)
        {
            haveupgradepoint = true;
        }
        else
        {
            haveupgradepoint = false;
        }
        if (upgradepoint <= 0)
        {
            closeallActivationicons();
            closeallUpgradeicons();
        }
        if(haveupgradepoint)
        {
            if (HeroSkills.isactive1 == false)
            {
                ActivationIcons[0].SetActive(true);
            }
            if (HeroSkills.isactive2 == false)
            {
                ActivationIcons[1].SetActive(true);
            }
            if (HeroSkills.isactive3 == false)
            {
                ActivationIcons[2].SetActive(true);
            }
            if (HeroSkills.isactive4 == false && HeroMovement.HeroLVL >= 6 && !ultiUnlicked)
            {
                ActivationIcons[3].SetActive(true);
            }
            //----------------------------------------------
            if (HeroSkills.isactive1 == true && counterfor1 < 3)
            {
                UpgradeICONS[0].SetActive(true);
            }
            else
            {
                UpgradeICONS[0].SetActive(false);
            }
            if (HeroSkills.isactive2 == true && counterfor2 < 3)
            {
                UpgradeICONS[1].SetActive(true);
            }
            else
            {
                UpgradeICONS[1].SetActive(false);
            }
            if (HeroSkills.isactive3 == true && counterfor3 < 3)
            {
                UpgradeICONS[2].SetActive(true);
            }
            else
            {
                UpgradeICONS[2].SetActive(false);
            }
            if (HeroSkills.isactive4 == true && HeroMovement.HeroLVL >= 11 && counterfor4 < 2)
            {
                UpgradeICONS[3].SetActive(true);
            }
            else
            {
                UpgradeICONS[3].SetActive(false);
            }
        }
    }
    public void expGain(float amount)
    {
        expbar.value += amount * UpradeManager.expbonus;
    }
    [ContextMenu("LVLUP")]
    public void LvlUp()
    {
        
        upgradepoint++;
        
        HeroMovement.HeroLVL++;
        hero.DMG += 3;
        HeroLvltext.text = HeroMovement.HeroLVL.ToString();
        expbar.value = 0;
        expbar.maxValue += 10;
        
    }
    public void activateability1()
    {
        skillpoints[0].SetActive(true);
        counterfor1++;
        ActivationIcons[0].SetActive(false);
        upgradepoint--;
        ability1Button.image.color = Color.white;
        HeroSkills.isactive1 = true;
    }
    public void activateability2()
    {
        skillpoints[1].SetActive(true);
        counterfor2++;
        ActivationIcons[1].SetActive(false);
        ability2Button.image.color = Color.white;
        HeroSkills.isactive2 = true;
        upgradepoint--;
    }
    public void activateability3()
    {
        skillpoints[2].SetActive(true);
        counterfor3++;
        ActivationIcons[2].SetActive(false);
        HeroSkills.HeroPassive = true;
        ability3Button.image.color = Color.white;
        HeroSkills.isactive3 = true;
        upgradepoint--;
    }
    public void activateability4()
    {
        if(!ultiUnlicked)
        {
            skillpoints[3].SetActive(true);
            counterfor4++;
            ActivationIcons[3].SetActive(false);
            ability4Button.image.color = Color.white;
            HeroSkills.isactive4 = true;
            upgradepoint--;
            ultiUnlicked = true;
        }
        
    }
    public void upgradeAbility1()
    {
        if (ability1point != 2)
        {
            ability1point++;
            skillpointtext[0].text = ability1point.ToString();
        }
        else
        {
            skillpointtext[0].text = "MAX";
        }
        TooltipSystem.Hide();
        counterfor1++;
        HeroMovement.FrostLVL -= 0.12f;
        upgradepoint--;
    }
    public void upgradeAbility2()
    {
        if (ability2point != 2)
        {
            ability2point++;
            skillpointtext[1].text = ability2point.ToString();
        }
        else
        {
            skillpointtext[1].text = "MAX";
        }
        TooltipSystem.Hide();
        counterfor2++;
        HeroMovement.critlvl += 1;
        upgradepoint--;
    }
    public void upgradeAbility3()
    {
        if(ability3point != 2)
        {
            ability3point++;
            skillpointtext[2].text = ability3point.ToString();
        }
        else
        {
            skillpointtext[2].text = "MAX";
        }
        TooltipSystem.Hide();
        counterfor3++;
        HeroMovement.PassiveDMGbuff += 0.12f;
        upgradepoint--;
    }
    public void upgradeAbility4()
    {
        ability4point++;
        skillpointtext[3].text = "MAX";
        TooltipSystem.Hide();
        HeroMovement.UltTimer += 5;
        counterfor4++;
        HeroSkills.UltimateStatelvl2 = true;
        upgradepoint--;
    }
    private void closeallActivationicons()
    {
        ActivationIcons[0].SetActive(false);
        ActivationIcons[1].SetActive(false);
        ActivationIcons[2].SetActive(false);
        ActivationIcons[3].SetActive(false);
    }
    private void closeallUpgradeicons()
    {
        UpgradeICONS[0].SetActive(false);
        UpgradeICONS[1].SetActive(false);
        UpgradeICONS[2].SetActive(false);
        UpgradeICONS[3].SetActive(false);
    }
}
