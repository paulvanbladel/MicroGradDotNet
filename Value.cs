public class Value
{
    public double Data { get; set; }
    public double Grad { get; set; }
    private Action BackwardAction { get; set; }
    private HashSet<Value> Prev { get; set; }
    private string Op { get; set; }

    public Value(double data, Value[]? children = null, string op = "")
    {
        Data = data;
        Grad = 0;
        BackwardAction = () => { };
        Prev = children != null ? new HashSet<Value>(children) : new HashSet<Value>();
        Op = op;
    }

    public static implicit operator Value(double value)
    {
        return new Value(value);
    }

    public static implicit operator Value(int value)
    {
        return new Value(value);
    }

    public static Value operator +(Value a, Value b)
    {
        var outVal = new Value(a.Data + b.Data, new[] { a, b }, "+");

        outVal.BackwardAction = () =>
        {
            a.Grad += outVal.Grad;
            b.Grad += outVal.Grad;
        };

        return outVal;
    }

    public static Value operator *(Value a, Value b)
    {
        var outVal = new Value(a.Data * b.Data, new[] { a, b }, "*");

        outVal.BackwardAction = () =>
        {
            a.Grad += b.Data * outVal.Grad;
            b.Grad += a.Data * outVal.Grad;
        };

        return outVal;
    }

    public Value Pow(double power)
    {
        var outVal = new Value(Math.Pow(Data, power), new[] { this }, $"**{power}");

        outVal.BackwardAction = () =>
        {
            Grad += (power * Math.Pow(Data, power - 1)) * outVal.Grad;
        };

        return outVal;
    }

    public Value ReLU()
    {
        var outVal = new Value(Data < 0 ? 0 : Data, new[] { this }, "ReLU");

        outVal.BackwardAction = () =>
        {
            Grad += (outVal.Data > 0 ? 1 : 0) * outVal.Grad;
        };

        return outVal;
    }

    public void Backward()
    {
        var topo = new List<Value>();
        var visited = new HashSet<Value>();

        void BuildTopo(Value v)
        {
            if (!visited.Contains(v))
            {
                visited.Add(v);
                foreach (var child in v.Prev)
                {
                    BuildTopo(child);
                }
                topo.Add(v);
            }
        }

        BuildTopo(this);

        Grad = 1;
        topo.Reverse();
        foreach (var v in topo)
        {
            v.BackwardAction();
        }
    }

    public static Value operator -(Value a)
    {
        return a * -1;
    }

    public static Value operator -(Value a, Value b)
    {
        return a + (-b);
    }

    public static Value operator /(Value a, Value b)
    {
        return a * b.Pow(-1);
    }

    public override string ToString()
    {
        return $"Value(Data={Data}, Grad={Grad})";
    }
}
