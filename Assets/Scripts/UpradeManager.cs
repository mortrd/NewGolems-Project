using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpradeManager : MonoBehaviour
{
    public static bool HeroActive;
    public static float expbonus = 1;
    public float Minerspeed = 3.5f;
    public float miningspeed;
    public int goldammount = 1;
    public int MedalCount = 0;
    public static bool pauseallow = false;
    [SerializeField] HeroMovement hero;
    [SerializeField] TextMeshProUGUI MedalText;
    [SerializeField] Text CostforDmgtext;
    int CostforDmg = 5;
    [SerializeField] Text CostforSpeedtext;
    int costforspeed = 15;
    [SerializeField] Text CostforArcherstext;
    int costforArchers = 25;
    [SerializeField] Text CostforGatetext;
    int costforGate = 5;
    [SerializeField] Text minercounttext;
    int minercount = 1;
    [SerializeField] Text Costforminerspeedtext;
    int costforminerspeed = 5;
    [SerializeField] Text Costforminingspeedtext;
    int Costforminingspeed = 5;
    [SerializeField] Text CostforGoldAmouttext;
    int CostforGoldamount = 20;
    int counter = 0;
    Gold gold;
    public int DMGtoTake = 9;
    public float enemySpeed = 1.82f;
    public int enemyMaxHP = 70;
    [SerializeField] GameObject[] Shootingpoints;
    EnemyBody enemybody;
    int minerAddcounter = 0;
    public int mineamountc = 0;
    int minerspeedc = 0;
    int mineingspeedc = 0;
    int attspeedc = 0;
    int DMGupgradeC = 0;
    float attspeedtoIncrease = 0.45f;
    [SerializeField] AimAndShoot aimandshoot;
    [SerializeField] CastleHealth castleheath;
    [SerializeField] GameObject miner;
    [SerializeField] GameObject OpenCastleMenu;
    [SerializeField] GameObject closecastleMenu;
    [SerializeField] GameObject castleupgradeMenu;
    [SerializeField] GameObject OpenFarmMenu;
    [SerializeField] GameObject closefarmMenu;
    [SerializeField] GameObject farmupgradeMenu;
    public bool fireUpgrade = false; 
    public bool frostUpgrade = false;
    [SerializeField] GameObject SpecialUpgrades;
    bool UpgradeDone = false;
    [SerializeField] Text[] textforupgrades;
    [SerializeField] GameObject[] speedstodestroy;
    [SerializeField] GameObject[] archerstodesroy;
    [SerializeField] GameObject[] minertodestory;
    [SerializeField] GameObject[] minerspeedtodestroy;
    [SerializeField] GameObject[] miningspeedtodestroy;
    [SerializeField] GameObject[] GoldAmounttodestroy;
    [SerializeField] GameObject tooltip;
    [SerializeField] int[] shopprise;
    [SerializeField] TextMeshProUGUI[] costtext;
    [SerializeField] int[] shopcounter;
    [SerializeField] TextMeshProUGUI[] Shopcountertext;
    [SerializeField] TextMeshProUGUI medalcounttext;
    [SerializeField] GameObject Shop;
    [SerializeField] Animator Heroanim;
    float attackspeedincrease = 1f;
    float speedanim = 1f;
    [SerializeField] GameObject passivebuff;
    [SerializeField] GameObject choisepanel;
    [SerializeField] GameObject Herocharacter;
    int DMGtoUpgrade = 5;
    bool ispause = false;
    [SerializeField] GameObject panel;
    [SerializeField] GameObject Shopbottun;


    private void Start()
    {
        Time.timeScale = 0;
        choisepanel.SetActive(true);
        Minerspeed = 3.5f;
        miningspeed = 2.7f;
        gold = GameObject.FindGameObjectWithTag("Gold").GetComponent<Gold>();
    }
    private void Update()
    {
        if (aimandshoot.Target != null)
        {
            enemybody = GameObject.FindGameObjectWithTag("Body").GetComponent<EnemyBody>();
        }
        if(DMGupgradeC == 4 && !UpgradeDone && HeroActive == false)
        {
            SpecialUpgrades.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.Escape) && pauseallow)
        {
            if (ispause)
            {
                Resume();
            }
            else
            {
                PuaseGame();
            }
        }

    }
    public void PuaseGame()
    {
        panel.SetActive(true);
        Time.timeScale = 0;
        ispause = true;
    }
    public void Resume()
    {
        panel.SetActive(false);
        Time.timeScale = 1f;
        ispause = false;
    }
    public void UpgradeDMG(int cost)
    {
        if (gold.GoldCount >= CostforDmg && !castleheath.GateDestroyed)
        {
            DMGtoTake += DMGtoUpgrade;
            gold.Pay(CostforDmg);
            CostforDmg += 5;
            CostforDmgtext.text = CostforDmg.ToString();
            cost = CostforDmg;
            DMGupgradeC += 1;
            if(fireUpgrade == true)
            {
                EnemyBody.FireDps += 0.21f;
            }
        }

    }
    public void UpgradeAttackSpeed(int cost)
    {
        //Max at lvl 10
        if (gold.GoldCount >= costforspeed && attspeedc < 9 && !castleheath.GateDestroyed)
        {
            aimandshoot.AttackSpeed -= attspeedtoIncrease;
            gold.Pay(costforspeed);
            costforspeed += 10;
            CostforSpeedtext.text = costforspeed.ToString();
            cost = costforspeed;
            attspeedc += 1;
            if (attspeedc < 3)
            {
                attspeedtoIncrease -= 0.14f;

            }
            // Maxed
            if(attspeedc >=9 )
            {
                attspeedtoIncrease -= 0.08f;
                textforupgrades[0].text = "MAX";
            }
        }

    }
    public void UpgradeAddArchers(int cost)
    {
        //max lvl is 8
        if (gold.GoldCount >= costforArchers && !castleheath.GateDestroyed)
        {
            if (counter <= 6)
            {
                Shootingpoints[counter + 1].SetActive(true);
                counter += 1;
                gold.Pay(costforArchers);
                costforArchers += 10;
                CostforArcherstext.text = costforArchers.ToString();
                cost = costforArchers;
            }
            if(counter >= 7)
            {
                textforupgrades[1].text = "MAX";
            }


        }


    }
    public void UpgradeGateHealth(int cost)
    {
        if (gold.GoldCount >= costforGate && !castleheath.GateDestroyed)
        {

            castleheath.MaxHealth += 20f;
            castleheath.slider.maxValue = castleheath.MaxHealth;
            castleheath.CurentHealth = castleheath.MaxHealth;
            castleheath.SetHealth(castleheath.CurentHealth);
            gold.Pay(costforGate);
            costforGate += 5;
            CostforGatetext.text = costforGate.ToString();
            cost = costforGate;
        }

    }
    public void UpgradeAddMiner(int cost)
    {

        cost = 5;
        if (gold.GoldCount >= cost && minerAddcounter < 9 && !castleheath.GateDestroyed)
        {
            gold.Pay(cost);
            Instantiate(miner, new Vector2(-29.18f, -6f), Quaternion.identity);
            minercount += 1;
            minercounttext.text = minercount.ToString();
            minerAddcounter += 1;
            if(minercount >=10)
            {
                textforupgrades[2].text = "MAX";
            }
        }

    }
    public void UpgradeMinerSpeed(int cost)
    {
        // max lvl is 10
        if (gold.GoldCount >= costforminerspeed && minerspeedc < 9 && !castleheath.GateDestroyed)
        {
            Minerspeed += 0.88f;
            gold.Pay(costforminerspeed);
            costforminerspeed += 5;
            Costforminerspeedtext.text = costforminerspeed.ToString();
            cost = costforminerspeed;
            minerspeedc += 1;
            if(minerspeedc >= 9)
            {
                textforupgrades[3].text = "MAX";

            }
        }

    }
    public void UpgradeMinigSpeed(int cost)
    {
        //max lvl is 5
        if (gold.GoldCount >= Costforminingspeed && mineingspeedc < 4 && !castleheath.GateDestroyed)
        {
            miningspeed -= 0.67f;
            gold.Pay(Costforminingspeed);
            Costforminingspeed += 5;
            Costforminingspeedtext.text = Costforminingspeed.ToString();
            cost = Costforminingspeed;
            mineingspeedc += 1;
            if (mineingspeedc >= 4)
            {
                textforupgrades[4].text = "MAX";

            }
        }

    }
    public void UpgradeMineAmmount(int cost)
    {
        // max lvl is 3
        if (gold.GoldCount >= CostforGoldamount && mineamountc < 2 && !castleheath.GateDestroyed)
        {
            goldammount += 1;
            gold.Pay(CostforGoldamount);
            CostforGoldamount += 5;
            CostforGoldAmouttext.text = CostforGoldamount.ToString();
            mineamountc += 1;
            if (mineamountc >= 2)
            {
                textforupgrades[5].text = "MAX";

            }
        }

    }
    public void openCastleUpgradeMenu()
    {
        OpenCastleMenu.SetActive(false);
        castleupgradeMenu.SetActive(true);
        closecastleMenu.SetActive(true);
    }
    public void CloseCastleUpgradeMenu()
    {
        OpenCastleMenu.SetActive(true);
        castleupgradeMenu.SetActive(false);
        closecastleMenu.SetActive(false);
    }
    public void openFarmMenu()
    {
        OpenFarmMenu.SetActive(false);
        farmupgradeMenu.SetActive(true);
        closefarmMenu.SetActive(true);
    }
    public void CloseFarmUpgradeMenu()
    {
        OpenFarmMenu.SetActive(true);
        farmupgradeMenu.SetActive(false);
        closefarmMenu.SetActive(false);
    }
    public void FireUpgrade()
    {
        tooltip.SetActive(false);
        UpgradeDone = true;
        SpecialUpgrades.SetActive(false);
        fireUpgrade = true;
    }
    public void FrostUpgrade()
    {
        tooltip.SetActive(false);
        UpgradeDone = true;
        SpecialUpgrades.SetActive(false);
        frostUpgrade = true;
    }
    public void gainMedal(int amount)
    {
        MedalCount += amount;
        MedalText.text = MedalCount.ToString();
    }
    public void HeroDMGUpgrade()
    {
        if(MedalCount >= shopprise[0])
        {
            MedalCount -= shopprise[0];
            shopcounter[0] += 1;
            shopprise[0] += 1;
            hero.DMG += 3;
        }
        updatecostsandcounter();
    }
    public void HeroAttackspeeupgrade()
    {
        if(MedalCount >= shopprise[1])
        {
            attackspeedincrease += .1f;
            MedalCount -= shopprise[1];
            shopcounter[1] += 1;
            shopprise[1] += 1;
            Heroanim.SetFloat("attackspeed", attackspeedincrease);
        }
        updatecostsandcounter();
    }
    public void HeroMovespeedupgrade()
    {
        if(MedalCount >= shopprise[2])
        {
            speedanim += 0.05f;
            MedalCount -= shopprise[2];
            shopcounter[2] += 1;
            shopprise[2] += 1;
            hero.speed += 0.01f;
            Heroanim.SetFloat("speed", speedanim);
        }
        updatecostsandcounter();
    }
    public void HeroEXPgainupgrade()
    {
        if (MedalCount >= shopprise[3])
        {
            MedalCount -= shopprise[3];
            shopprise[3] += 1;
            expbonus += 0.1f;
            shopcounter[3] += 1;
        }
        
        updatecostsandcounter();
    }
    private void updatecostsandcounter()
    {
        costtext[0].text = shopprise[0].ToString();
        costtext[1].text = shopprise[1].ToString();
        costtext[2].text = shopprise[2].ToString();
        costtext[3].text = shopprise[3].ToString();

        Shopcountertext[0].text = shopcounter[0].ToString();
        Shopcountertext[1].text = shopcounter[1].ToString();
        Shopcountertext[2].text = shopcounter[2].ToString();
        Shopcountertext[3].text = shopcounter[3].ToString();

        medalcounttext.text = MedalCount.ToString(); ;
    }
    public void exitshop()
    {
        pauseallow = true;
        Time.timeScale = 1;
        Shop.SetActive(false);
    }
    public void Herochoise ()
    {
        pauseallow = true;
        Spawner.HPbuff = 21;
        passivebuff.SetActive(false);
        Herocharacter.SetActive(true);
        Time.timeScale = 1;
        choisepanel.SetActive(false);
        Shopbottun.SetActive(true);
    }
    public void castlechouse()
    {
        pauseallow = true;
        passivebuff.SetActive(true);
        DMGtoUpgrade = 7;
        Spawner.HPbuff = 9;
        DMGtoTake += 4;
        HeroActive = false;
        enemyMaxHP = 88;
        Time.timeScale = 1;
        choisepanel.SetActive(false);
    }
    public void openshopMenu()
    {
        Time.timeScale = 0;
        Shop.SetActive(true);
    }
}
