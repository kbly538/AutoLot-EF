using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaDataViewer
{

	[Some]
	internal class Car : IEnumerable
	{
		public string Name { get; set; }
		public string Driver { get; set; }
		public string _make;

		[Some("llll")]
		public void Drive()
		{
			
			Console.WriteLine("vıııııın");
		}

		
		public int PlaceDriver(string name)
		{
			Driver = name;
			return 1;
		}

		public IEnumerator GetEnumerator()
		{
			throw new NotImplementedException();
		}
	}
}
