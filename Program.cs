// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

Value a = new Value(-4.0);
Value b = new Value(2.0);
Value c = a + b;
Value d = a * b + b.Pow(3);
c += c + 1;
c += 1 + c + (-a);
d += d * 2 + (b + a).ReLU();
d += 3 * d + (b - a).ReLU();
Value e = c - d;
Value f = e.Pow(2);
Value g = f / 2.0;
g += 10.0 / f;

Console.WriteLine($"{g.Data:F4}"); // prints 24.7041, the outcome of this forward pass
g.Backward();
Console.WriteLine($"{a.Grad:F4}"); // prints 138.8338, i.e. the numerical value of dg/da
Console.WriteLine($"{b.Grad:F4}"); // prints 645.5773, i.e. the numerical value of dg/db

Console.ReadLine();