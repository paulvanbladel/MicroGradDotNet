using System;
using System.Collections.Generic;

public class MLP : Module
{
    private List<Layer> Layers { get; set; }

    public MLP(int inputSize, List<int> outputSizes)
    {
        Layers = new List<Layer>();
        int prevSize = inputSize;
        for (int i = 0; i < outputSizes.Count; i++)
        {
            bool nonLinear = i != outputSizes.Count - 1;
            Layers.Add(new Layer(prevSize, outputSizes[i], nonLinear));
            prevSize = outputSizes[i];
        }
    }

    public List<Value> Call(List<Value> inputs)
    {
        var current = inputs;
        foreach (var layer in Layers)
        {
            current = layer.Call(current);
        }
        return current;
    }

    public override IEnumerable<Value> Parameters()
    {
        var parameters = new List<Value>();
        foreach (var layer in Layers)
        {
            parameters.AddRange(layer.Parameters());
        }
        return parameters;
    }

    public override string ToString()
    {
        return $"MLP of [{string.Join(", ", Layers)}]";
    }
}
