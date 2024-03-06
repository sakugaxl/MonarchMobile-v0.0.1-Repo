using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using BreakInfinity;

public class Data
{
    public BigDouble coins;

    public List<int> clickUpgradeLevel;
    public List<int> productionUpgradeLevel;

    public Data()
    {
        coins = 0;

        clickUpgradeLevel = new int[6].ToList();
        productionUpgradeLevel = new int[6].ToList();
    }
}
