using UnityEngine;
public interface IElecLink
{
    public double TotalPowerDraw();
    public void Shutdown();
    public void OnChildPowerDrawChange();

    //Called only for LinkRenderer setup, may be removed after changing the behaviour of LinkRenderer for the coordinates of objects it connects
    //TODO: check if it can be removed if it is only used for LinkRenderer
    public GameObject GetParent();
}