using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Overfishing.Scripts.Fishes
{
    public class Camo : AFish
    {
        public override string SpriteName => "camo";

        public override string ActionDescription => "Can't be hooked via fishing rod";


        string _name = "Camo";
        public override string Name { get => _name; set => _name = value; }

        public override void Ability()
        {
        }
    }
}
