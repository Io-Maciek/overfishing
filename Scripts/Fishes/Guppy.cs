using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Overfishing.Scripts.Fishes
{
    public class Guppy : AFish
    {
        public override string SpriteName => "guppy";

        public override string ActionDescription => "NA";


        string _name = "Guppy";
        public override string Name { get => _name; set => _name = value; }

        public override void Ability()
        {
        }
    }
}
