using Metalama.Framework.Aspects;
using Metalama.Framework.Fabrics;

internal class Fabric : ProjectFabric
{
    public override void AmendProject(IProjectAmender amender) =>
        amender.Outbound
            .SelectMany(compilation => compilation.AllTypes)
            .SelectMany(type => type.AllMethods)
            .Where(method => method.BelongsToCurrentProject)
            .AddAspectIfEligible<LogAttribute>();
}

public class LogAttribute : OverrideMethodAspect
{
    public override dynamic? OverrideMethod()
    {
        Console.WriteLine($"{meta.Target.Method.Name}: start");
        var result = meta.Proceed();
        Console.WriteLine($"{meta.Target.Method.Name}: returning {result}.");

        return result;
    }
}

// https://doc.postsharp.net/il/custompatterns/aspects/applying/attribute-multicasting#all-members

namespace HelloWorld
{
    static class Program
    {
        static void Main()
        {
            Console.WriteLine("Hello, world.");

            Testy();
        }

        static void Testy()
        {
            Console.WriteLine("Inside Testy");

            // Test with bitconverter
            byte[] bytes =
            {
                12,42,52,52
            };
            var x = BitConverter.ToInt32(bytes, 0);

            Console.WriteLine($"{x}");
        }
    }
}
