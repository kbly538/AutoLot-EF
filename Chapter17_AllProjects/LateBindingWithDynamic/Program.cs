using System.Reflection;

static void AddWithReflection()
{
	Assembly asm = Assembly.LoadFrom("MathLibrary");
	try
	{
		Type math = asm.GetType("MathLibrary.SimpleMath");
		object obj = Activator.CreateInstance(math);

		MethodInfo methodInfo = math.GetMethod("Add");

		object[] args = { 10, 70 };

		Console.WriteLine($"10 + 70 = {methodInfo.Invoke(obj, args)}");
	} catch (Exception e)
	{
		Console.WriteLine(e.Message);
	}
}

static void AddWithDynamic()
{
	Assembly asm = Assembly.LoadFrom("MathLibrary");
	try
	{
		Type math = asm.GetType("MathLibrary.SimpleMath");
		dynamic obj = Activator.CreateInstance(math);

		Console.WriteLine($"10 + 70 = {obj.Add(10, 70)}");
	}
	catch (Exception e)
	{
		Console.WriteLine(e.Message);
	}
}


AddWithReflection();
AddWithDynamic();