using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace ConsoleCalculator
{
	public class Operators
	{
		private Dictionary<string, Dictionary<string, IOperator>> operators = new Dictionary<string, Dictionary<string, IOperator>>(); 

		public IOperator Get(string key)
		{
			IOperator result = null;
			if (operators.Any(set => set.Value.TryGetValue(key, out result)))
			{
				return result;
			}
			throw new Exception("Unknown binary operator: " + key);
		}

		public void AddPlugin(string dllPath)
		{
			var dllName = Path.GetFileName(dllPath);
			if (dllName == null)
				throw new Exception("Dll name is null");
			
			if (operators.ContainsKey(dllName))
				throw new Exception("Dll " + dllName + " was always loaded");

			var plugin = Assembly.LoadFile(dllPath);
			var newTypes = plugin.GetTypes().Where(t => t.GetInterfaces().Contains(typeof (IOperator)));
			var newOperators = new Dictionary<string, IOperator>();

			foreach (var type in newTypes)
			{
				var added = (IOperator)Activator.CreateInstance(type);

				foreach (var set in operators)
				{
					if (set.Value.ContainsKey(added.Key))
						throw new Exception(set.Key + " contains operator " + added.Key);
				}

				newOperators.Add(added.Key, added);
			}

			operators.Add(dllName, newOperators);
		}

		public bool DeletePlugin(string dllName)
		{
			return operators.Remove(dllName);
		}
	}
}
