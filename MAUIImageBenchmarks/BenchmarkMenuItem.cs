namespace MAUIImageBenchmarks;

public class BenchmarkMenuItem
{
    public string Name { get; private set; }
    public Type[] Benchmarks { get; private set; }

    
    public BenchmarkMenuItem(string name, Type benchmark) : this(name, new Type[] { benchmark })
    {
        
    }
    
    public BenchmarkMenuItem(string name, Type[] benchmarks)
    {
        Name = name;
        Benchmarks = benchmarks;
    }
}