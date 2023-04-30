using System;
using System.Collections.Generic;
using UnityEngine;

public interface IBaseTowerBehaviour
{
    public float GetRange();
    public float GetAtack();
    public void UpgradeTower();
    public List<UpgradeSystem> GetUpgradeSystem();
}

