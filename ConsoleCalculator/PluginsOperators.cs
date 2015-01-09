using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using ConsoleCalculator.MyOperators;

namespace ConsoleCalculator
{
	public class PluginsOperators : IOperators
	{
		private Dictionary<string, Dictionary<string, IOperator>> operators = new Dictionary<string, Dictionary<string, IOperator>>();

		private static string GetKey(string operatorText, int operatorDimension)
		{
			return operatorText + " " + operatorDimension;
		}

		public PluginsOperators()
		{
			var plus = new PlusOperator();
			var division = new DivisionOperator();
			var multiply = new MultiplyOperator();
			var minus = new MinusOperator();
			var minusUnary = new MinusUnaryOperator();

			var operatorsList = new List<IOperator>
			{
				plus,
				division,
				multiply,
				minus,
				minusUnary
			};

			var baseOperators = operatorsList.ToDictionary(op => GetKey(op.Text, op.Dimension));

			operators.Add("base", baseOperators);
		}

		public IOperator Get(string text, int dimension)
		{
			var key = GetKey(text, dimension);

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
			var newTypes = plugin.GetTypes().Where(t => t.GetInterfaces().Contains(typeof(IOperator)));
			var newOperators = new Dictionary<string, IOperator>();

			foreach (var type in newTypes)
			{
				var added = (IOperator)Activator.CreateInstance(type);
				var addedKey = GetKey(added.Text, added.Dimension);

				foreach (var set in operators)
				{
					if (set.Value.ContainsKey(addedKey))
						throw new Exception(set.Key + " contains operator " + addedKey);
				}

				newOperators.Add(addedKey, added);
			}

			operators.Add(dllName, newOperators);
		}

		public bool DeletePlugin(string dllName)
		{
			return operators.Remove(dllName);
		}
	}
}
