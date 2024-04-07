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
/*        GetAll();
        AvailableFishes.AddRange(AddBonusFish());

        if (AvailableFishes.Count < 4)
        {
            throw new Exception("Number of fish must be at least 4");
        }*/
    }

    public void LoadFish()
    {
        GetAll();
        AvailableFishes.AddRange(AddBonusFish());

        if (AvailableFishes.Count < 4)
        {
            throw new Exception("Number of fish must be at least 4");
        }
    }

    void GetAll()
    {
        AvailableFishes = GetDefault();
    }

    public List<AFish> AddBonusFish()
    {
        var bonus = new List<AFish>();
        var _c = GameStaticInfo._CONFIG_INSTANCE;

        if (_c.AlphaFishUnlocked)
            bonus.Add(new AlphaFish());

        return bonus;
    }

    List<AFish> GetDefault()
    {
        return new List<AFish>()
        {
            new Guppy(),
            new Nemo(),
            new PufferFish(),
            new Lanternfish(),
            //new Camo(),
        };
    }
}
