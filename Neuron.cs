using System;
using System.Collections.Generic;

public class Neuron : Module
{
    private List<Value> Weights { get; set; }
    private Value Bias { get; set; }
    private bool NonLinear { get; set; }

    public Neuron(int inputSize, bool nonLinear = true)
    {
        var random = new Random();
        Weights = new List<Value>();
        for (int i = 0; i < inputSize; i++)
        {
            Weights.Add(new Value(random.NextDouble() * 2 - 1));
        }
        Bias = new Value(0);
        NonLinear = nonLinear;
    }

    public Value Call(List<Value> inputs)
    {
        var activation = Bias;
        for (int i = 0; i < Weights.Count; i++)
        {
            activation += Weights[i] * inputs[i];
        }
        return NonLinear ? activation.ReLU() : activation;
    }

    public override IEnumerable<Value> Parameters()
    {
        return Weights.Concat(new[] { Bias });
    }

    public override string ToString()
    {
        return $"{(NonLinear ? "ReLU" : "Linear")}Neuron({Weights.Count})";
    }
}



