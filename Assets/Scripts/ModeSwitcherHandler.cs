using System.Collections.Generic;
using UnityEngine;

public class ModeSwitcherHandler: MonoBehaviour
{
    public void OnModeSwitcherActivated(ApplianceModeSwitcher current)
    {
        ApplianceModeSwitcher[] modeSwitchers = FindObjectsByType<ApplianceModeSwitcher>();
        foreach (ApplianceModeSwitcher s in modeSwitchers)
        {
            if (current != s)
            {
                s.DeactivateModeSwitcher();
            }
        }
    }
}