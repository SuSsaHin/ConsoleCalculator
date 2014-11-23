using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCalculator
{
	public class Operator
	{
		public Func<double, double, double> Function { get; set; }
		public uint Priority { get; set; }
	}
}
