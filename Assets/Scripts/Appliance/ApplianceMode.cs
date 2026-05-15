using System;
using UnityEngine;

[Serializable]
public class ApplianceMode
{
    public String Name { get => name; }
    public double PowerDraw { get => powerDraw; }
    public double Time { get => time; }

    [SerializeField]
    private String name;
    [SerializeField]
    private double powerDraw;
    [SerializeField]
    private double time;

    public ApplianceMode(String name, double draw, double time)
    {
        this.name = name;
        this.powerDraw = draw;
        this.time = time;
    }
}