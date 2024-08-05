using System;

public class Program
{
    public static void Main()
    {
        int[,] estadoInicial = QuebraCabeca.GerarEstadoInicialAleatorio();

        Console.WriteLine("Estado inicial: ");
        QuebraCabeca.ImprimirEstado(estadoInicial);
        Console.WriteLine();

        QuebraCabeca.ResolverQuebraCabeca(estadoInicial);
    }
}
