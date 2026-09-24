using System;
using UnityEngine;

public class GridCleaner : MonoBehaviour
{
    public event EventHandler OnGridCleaning;
    public void CleanGrid()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("gridElem");
        foreach (GameObject elem in objs)
        {
            if (elem.GetComponent<ApplianceController>() != null)
            {
                elem.GetComponent<ApplianceView>().DestroyModeSwitcher();
            }
            Destroy(elem);
        }
        OnGridCleaning?.Invoke(this, EventArgs.Empty);
    }
}