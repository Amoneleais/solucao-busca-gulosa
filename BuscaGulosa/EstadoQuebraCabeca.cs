public class EstadoQuebraCabeca
{
    public int[,] Estado { get; }
    public int Custo { get; }
    public int Heuristica { get; }
    public EstadoQuebraCabeca Anterior { get; }

    public EstadoQuebraCabeca(int[,] estado, int custo, int heuristica, EstadoQuebraCabeca anterior = null)
    {
        Estado = estado;
        Custo = custo;
        Heuristica = heuristica;
        Anterior = anterior;
    }
}