using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Upgrades : MonoBehaviour
{
    public int UpgradeID;
    public Image UpgradeButton;
    public TMP_Text LevelText;
    public TMP_Text TitleText;
    public TMP_Text CostText;

    public void BuyClickUpgrade() => UpgradesManager.instance.BuyUpgrade("click", UpgradeID);
    public void BuyProductionUpgrade() => UpgradesManager.instance.BuyUpgrade("production", UpgradeID);
}
