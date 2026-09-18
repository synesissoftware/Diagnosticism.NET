using Diagnosticism;
using Diagnosticism.Diagnostics;
using Diagnosticism.Reflection;
using Diagnosticism.Testing;

Console.WriteLine($"Diagnosticism.NET {LibraryVersion.VersionString}");

var attrs = new { verbose = true, path = "." };
IDictionary<string, object?> map = AnonymousUtil.ConvertToDictionary(
    attrs,
    StructureConversionOptions.None);

Console.WriteLine("Reflection:");
foreach (KeyValuePair<string, object?> pair in map)
{
    Console.WriteLine($"  {pair.Key} = {pair.Value}");
}

TimingsMap<string> timings = new();
timings.Add("noop", () => { });
timings.Add("noop", () => { });

IList<Timing> noopTimings = timings.First(pair => pair.Key == "noop").Value;
Timing mean = TimingsMap<string>.TimingMean(noopTimings);
Console.WriteLine($"TimingsMap noop mean ticks: {mean.Ticks}");

string captured = Assist.ExecuteAroundWriter(writer =>
{
    writer.Write("Assist: ");
    writer.Write("ok");
});
Console.WriteLine(captured);
