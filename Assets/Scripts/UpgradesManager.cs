using System.Collections;
using System.Collections.Generic;
using BreakInfinity;
using UnityEngine;
using UnityEngine.UI;

public class UpgradesManager : MonoBehaviour
{
    public static UpgradesManager instance;
    private void Awake() => instance = this;
    
    public List<Upgrades> clickUpgrades;
    public Upgrades clickUpgradePrefab;

    public List<Upgrades> productionUpgrades;
    public Upgrades productionUpgradesPrefab;

    public ScrollRect clickUpgradesScroll;
    public Transform clickUpgradesPanel;

    public ScrollRect productionUpgradesScroll;
    public Transform productionUpgradesPanel;

    public string[] clickUpgradeNames;
    public string[] productionUpgradeNames;

    public BigDouble[] clickUpgradeBaseCost;
    public BigDouble[] clickUpgradeCostMult;
    public BigDouble[] clickUpgradesBasePower;
    
    public BigDouble[] productionUpgradeBaseCost;
    public BigDouble[] productionUpgradeCostMult;
    public BigDouble[] productionUpgradesBasePower;

    public void StartUpgradeManager()
    {   
        Methods.UpgradeCheck(Controller.instance.data.clickUpgradeLevel, 6);
        clickUpgradeNames = new[] {"Click Power +1", "Click Power +5", "Click Power +10", "Click Power +25", "Click Power +50", "Click Power +100"};
        productionUpgradeNames = new[] 
        {
            "+1 Coins/s", 
            "+5 Coins/s", 
            "+10 Coins/s", 
            "+25 Coins/s", "+50 Coins/s", "+100 Coins/s",
        };

        clickUpgradeBaseCost = new BigDouble[] {10, 50, 100, 1000, 2000, 4000};
        clickUpgradeCostMult = new BigDouble[] {1.25, 1.35, 1.55, 2, 2.5, 3.45};
        clickUpgradesBasePower = new BigDouble[] {1, 5, 10, 25, 50, 100};

        productionUpgradeBaseCost = new BigDouble[] {25, 100, 1000, 10000, 25000, 50000};
        productionUpgradeCostMult = new BigDouble[] {1.5, 1.75, 2, 3, 5.2, 6.8};
        productionUpgradesBasePower = new BigDouble[] {1, 2, 10, 100, 500, 1000};

        for (int i = 0; i < Controller.instance.data.clickUpgradeLevel.Count; i++)
        {
            Upgrades upgrade = Instantiate(clickUpgradePrefab, clickUpgradesPanel);
            upgrade.UpgradeID = i;
            clickUpgrades.Add(upgrade);
        }

        for (int i = 0; i < Controller.instance.data.productionUpgradeLevel.Count; i++)
        {
            Upgrades upgrade = Instantiate(productionUpgradesPrefab, productionUpgradesPanel);
            upgrade.UpgradeID = i;
            productionUpgrades.Add(upgrade);
        }

        clickUpgradesScroll.normalizedPosition = new Vector2(0, 0);
        productionUpgradesScroll.normalizedPosition = new Vector2 (0, 0);

        UpdateUpgradeUI("click");
        UpdateUpgradeUI("production");
    }

    public void UpdateUpgradeUI(string type, int UpgradeID = -1)
    {
        var data = Controller.instance.data;

        switch (type)
        {
            case "click":
                if (UpgradeID == -1)
                    for (int i = 0; i < clickUpgrades.Count; i++) UpdateUI(clickUpgrades, data.clickUpgradeLevel, clickUpgradeNames, i);
                else UpdateUI(clickUpgrades, data.clickUpgradeLevel, clickUpgradeNames, UpgradeID);
                break;            
            case "production":
                if (UpgradeID == -1)
                    for (int i = 0; i < productionUpgrades.Count; i++) UpdateUI(productionUpgrades, data.productionUpgradeLevel, productionUpgradeNames, i);
                else UpdateUI(productionUpgrades, data.productionUpgradeLevel, productionUpgradeNames, UpgradeID);
                break;
        }

        void UpdateUI(List<Upgrades> upgrades, List<int> upgradeLevels, string[] upgradeNames, int ID)
        {
            upgrades[ID].LevelText.text = upgradeLevels[ID].ToString();
            upgrades[ID].CostText.text = $"Cost: {UpgradeCost(type, ID):F2} Coins";
            upgrades[ID].TitleText.text = upgradeNames[ID];
        }
    }

    public BigDouble UpgradeCost(string type, int UpgradeID)
    { 
        var data = Controller.instance.data;
        switch (type)
        {
            case "click":
                return clickUpgradeBaseCost[UpgradeID] 
                                * BigDouble.Pow(clickUpgradeCostMult[UpgradeID], data.clickUpgradeLevel[UpgradeID]);
            case "production":
                return productionUpgradeBaseCost[UpgradeID] 
                                * BigDouble.Pow(productionUpgradeCostMult[UpgradeID], data.productionUpgradeLevel[UpgradeID]);
        }
        return 0;
    }

    public void BuyUpgrade(string type, int UpgradeID)
    {
        var data = Controller.instance.data;

        switch (type)
        {
            case "click": Buy(data.clickUpgradeLevel, UpgradeID);
                break;
            case "production": Buy(data.productionUpgradeLevel, UpgradeID);
                break;
        }

        void Buy(List<int> upgradeLevels, int ID)
        {
            if (data.coins >= UpgradeCost(type, ID))
            {
                data.coins -= UpgradeCost(type, ID);
                upgradeLevels[ID] += 1;
            }

            UpdateUpgradeUI(type, ID);
        }
    }
}
