using Overfishing.Scripts.Fishes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Overfishing.Player
{
	public class PlayerUIModel
	{
		public string AssignedDevice{ get; set; }
		public AFish Fish { get; set; }

		public override string ToString()
		{
			return AssignedDevice;
		}
	}
}
