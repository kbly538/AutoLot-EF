using CommonSnappableTypes;
using System.Reflection;


Console.WriteLine("***TypeViwer****");
string typeName = "";
do
{
	Console.WriteLine("Enter a snapin to load: ");

	typeName = Console.ReadLine();

	if (typeName == "" || typeName.Equals("Q", StringComparison.OrdinalIgnoreCase))
	{
		break;
	}
	try
	{
		LoadExternalModule(typeName);
	} catch (Exception ex) {
		Console.WriteLine("Cant find any snapin: ");
	};

} while (!typeName.Equals("Q", StringComparison.CurrentCultureIgnoreCase));



static void LoadExternalModule(string assemblyName)
{
	
	Assembly theSnapInAsm = null;
	try
	{
		theSnapInAsm = Assembly.LoadFrom("CSharpSnapIn" );
	} catch (Exception ex)
	{
		Console.WriteLine($"An error occured loading snapin{ex.Message}");
		Console.WriteLine($"An error occured loading snapin{ex.StackTrace}");
		Console.WriteLine($"An error occured loading snapin{ex.InnerException}");
		Console.WriteLine($"An error occured loading snapin{ex.Data}");
		Console.WriteLine($"An error occured loading snapin{ex.Source}");
		return;
	}

	var theClassTypes = theSnapInAsm
		.GetTypes()
		.Where(t => t.IsClass && (t.GetInterface("IAppFunctionality") != null))
		.ToList();

	if (!theClassTypes.Any())
	{
		Console.WriteLine("Nothing implemenets IAppFunctionality interace.");
	}

	foreach(Type t in theClassTypes)
	{
		IAppFunctionality itfApp = (IAppFunctionality)theSnapInAsm.CreateInstance(t.FullName, true);
		itfApp?.DoIt();
		DisplayCompanyData(t);
	}
}

static void DisplayCompanyData(Type t)
{
	var compInfo = t
		.GetCustomAttributes()
		.Where(ci => ci is CompanyInfoAttribute);

	foreach (CompanyInfoAttribute ci in compInfo)
	{
		Console.WriteLine($"More info about {ci.CompanyName} can be found at {ci.CompanyUrl}");
	}
}