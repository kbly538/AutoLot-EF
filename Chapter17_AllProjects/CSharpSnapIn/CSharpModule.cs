using CommonSnappableTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpSnapIn
{
	[CompanyInfo(CompanyName = "qbcorp", CompanyUrl = "www.qb.com")]
	public class CSharpModule : IAppFunctionality
	{
		void IAppFunctionality.DoIt()
		{
			Console.WriteLine("Just used the c# snap in");
		}
	}
}
