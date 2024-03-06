using System;
using UnityEngine;
using TMPro;
using BreakInfinity;

public class Controller : MonoBehaviour
{
    public static Controller instance;
    private void Awake() => instance = this;

    public Data data;

    [SerializeField] private TMP_Text coinsText;
    [SerializeField] private TMP_Text coinsPerSecondText;
    [SerializeField] private TMP_Text coinClickPowerText;

    public BigDouble ClickPower()
    {
        BigDouble total = 1;
        for (int i = 0; i < data.clickUpgradeLevel.Count; i++)
            total += UpgradesManager.instance.clickUpgradesBasePower[i] * data.clickUpgradeLevel[i];
        return total; 
    }

    public BigDouble CoinsPerSecond()
    {
        BigDouble total = 0;
        for (int i = 0; i < data.productionUpgradeLevel.Count; i++)
            total += UpgradesManager.instance.productionUpgradesBasePower[i] * data.productionUpgradeLevel[i];
        return total; 
    }

    private void Start()
    {
        data = new Data();
        UpgradesManager.instance.StartUpgradeManager();
    }

    private void Update()
    {
        coinsText.text = $"{data.coins:F2} Coins";
        coinsPerSecondText.text = $"{CoinsPerSecond():F2}/s";
        coinClickPowerText.text = "+" + ClickPower() + " Coins";

        data.coins += CoinsPerSecond() * Time.deltaTime;
    }

    public void GenerateCoins()
    {
        data.coins += ClickPower();
    }
}
