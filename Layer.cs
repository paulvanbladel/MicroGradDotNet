using System;
using System.Collections.Generic;

public class Layer : Module
{
    private List<Neuron> Neurons { get; set; }

    public Layer(int inputSize, int outputSize, bool nonLinear = true)
    {
        Neurons = new List<Neuron>();
        for (int i = 0; i < outputSize; i++)
        {
            Neurons.Add(new Neuron(inputSize, nonLinear));
        }
    }

    public List<Value> Call(List<Value> inputs)
    {
        var output = new List<Value>();
        foreach (var neuron in Neurons)
        {
            output.Add(neuron.Call(inputs));
        }
        return output;
    }

    public override IEnumerable<Value> Parameters()
    {
        var parameters = new List<Value>();
        foreach (var neuron in Neurons)
        {
            parameters.AddRange(neuron.Parameters());
        }
        return parameters;
    }

    public override string ToString()
    {
        return $"Layer of [{string.Join(", ", Neurons)}]";
    }
}


