using System.IO;
using System.Linq;
using System.Reflection;

public static class Hello
{
	public static int Main(string[] args)
	{
	    System.Console.WriteLine(OtherModule.Dep.dep.Prop);

	    return 10;
	}
}