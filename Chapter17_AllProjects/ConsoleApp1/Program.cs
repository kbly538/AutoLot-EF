using MetaDataViewer;
using System.Reflection;

try
{
	Assembly asm = Assembly.GetExecutingAssembly();
	Type[] v = asm.GetTypes();
	foreach (Type t in v)
	{
		foreach(FieldInfo pi in t.GetFields())
		{
			Console.WriteLine(pi);
		}
	}
	//PropertyInfo p = v.GetProperty("desc");
} catch (Exception ex)
{
	Console.WriteLine(ex.Message);
}

static void ListMethods(Type? t) 
{
	var methods = from m in t.GetMethods() 
				  group m by new {m.Name} into mets 
				  select mets.FirstOrDefault();

	if (!methods.Any())
	{
		Console.WriteLine("{0} has no method.", t);
		return;
	}

	Console.WriteLine("List of methods in " + t);
	foreach (var m in methods)
	{
		var retVal = m.ReturnType.Name;
		string paramInfo = "(";
		int paramIndex = 0;
		
		foreach (ParameterInfo  pi in m.GetParameters())
		{

			paramInfo += string.Format("{0} {1}", pi.ParameterType, pi.Name);
			if (m.GetParameters().Length -1 != paramIndex)
			{
				paramInfo += ", ";
			}

			paramIndex++;
		}
		paramInfo += ")";
		Console.Write("-> {0} {1} \n", m.Name, paramInfo);
		
	}

}
static void ListFields(Type? t) 
{
	var fields = from f in t.GetFields() select f;
	if (!fields.Any())
	{
		Console.WriteLine("{0} has no field.", t);
		return;
	}
	Console.WriteLine("List of fields in " + t);
	foreach (var f in fields)
	{
		Console.WriteLine("-> " + f.Name);
	}
}
static void ListProps(Type? t) 
{
	var props = from p in t.GetProperties() select p;
	if (!props.Any())
	{
		Console.WriteLine("{0} has no property.", t);
		return;
	}
	Console.WriteLine("List of properties in " + t);
	foreach (var p in props)
	{
		Console.WriteLine("-> " + p.Name);
	}
}

static void ListIntefaces(Type? t)
{
	var interfaces = from i in t.GetInterfaces() select i;
	if (!interfaces.Any())
	{
		Console.WriteLine("{0} has no interface.", t);
		return;
	}
	Console.WriteLine("List of properties in " + t);
	foreach (var i in interfaces)
	{
		Console.WriteLine("-> " + i.Name);
	}
}

[Some("hey hey my my")]
static void ListVariousStats(Type? t) 
{ 

	Console.WriteLine("***** Various Statistics *****"); 
	Console.WriteLine("Base class is: {0}", t.BaseType); 
	Console.WriteLine("Is type abstract? {0}", t.IsAbstract); 
	Console.WriteLine("Is type sealed? {0}", t.IsSealed); 
	Console.WriteLine("Is type generic? {0}", t.IsGenericTypeDefinition); 
	Console.WriteLine("Is type a class type? {0}", t.IsClass); 
	Console.WriteLine(); 
}


string typeName = "";
do
{
	Console.WriteLine("Enter a type to evaluate.");
	Console.WriteLine("or enter 'q' to quit.");

	typeName = Console.ReadLine();

	if (typeName.Equals("Q", StringComparison.OrdinalIgnoreCase) || typeName == "")
	{
		break;
	}

	try
	{
		Type a = Type.GetType(typeName, false, true);
		ListVariousStats(a);
		ListFields(a);
		ListProps(a);
		ListIntefaces(a);
		ListMethods(a);
		break;
	}
	catch (Exception ex)
	{
		Console.WriteLine(ex.Message);
	}
} while (!typeName.Equals("Q", StringComparison.OrdinalIgnoreCase));

