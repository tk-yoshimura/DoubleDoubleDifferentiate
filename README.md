# DoubleDoubleDifferentiate
 Double-Double Numerical Differentiation Implements

## Requirement
.NET 6.0

## Install

[Download DLL](https://github.com/tk-yoshimura/DoubleDoubleDifferentiate/releases)  
[Download Nuget](https://www.nuget.org/packages/tyoshimura.doubledouble.differentiate/)  

- Import DoubleDouble(https://github.com/tk-yoshimura/DoubleDouble)

## Usage
```csharp
for (int derivative = 0; derivative <= 16; derivative++) {
    ddouble y = CenteredIntwayDifferential.Differentiate(ddouble.Exp, 0, derivative, 0.125);

    Console.WriteLine($"{derivative}\t{y}");
}
```

## Licence
[MIT](https://github.com/tk-yoshimura/DoubleDoubleDifferentiate/blob/main/LICENSE)

## Author

[T.Yoshimura](https://github.com/tk-yoshimura)
