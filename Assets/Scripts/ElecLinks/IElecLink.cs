using UnityEngine;
public interface IElecLink
{
    public double TotalPowerDraw();
    public void Shutdown();
    public void OnChildPowerDrawChange();

    public GameObject GetParent();
}