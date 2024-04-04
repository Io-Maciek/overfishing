using Godot;
using Overfishing.Scripts.Fishes;
using Overfishing.Statics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

internal class FishLoaderScript : Node
{
    public List<AFish> AvailableFishes;


    public override void _Ready()
    {
        GetAll();
        if (AvailableFishes.Count < 4)
        {
            throw new Exception("Number of fish must be at least 4");
        }
    }

    public void GetAll()
    {
        AvailableFishes = GetDefault();
    }

    List<AFish> GetDefault()
    {
        return new List<AFish>()
        {
            new Guppy(),
            new Nemo(),
            new Lanternfish(),
            new Camo(),
            new AlphaFish(),
        };
    }
}
