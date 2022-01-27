using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaDataViewer
{
	[AttributeUsage(AttributeTargets.All)]
	public sealed class SomeAttribute: Attribute
	{
		public int v = 1453;
		public string desc = "One thousand four hundred fifty three";
		public SomeAttribute()
		{
			SendMes(desc);
		}
		public SomeAttribute(string desc)
		{
			this.desc = desc;
			SendMes(desc);
			Console.WriteLine("Why would yo uchange it?");
		}

		public static void SendMes(string message)
		{
			Console.WriteLine("Messangör");
		}
	}
}
