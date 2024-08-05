using System;
using System.Collections.Generic;
using System.Linq;

public class QuebraCabeca
{
    private static readonly int[,] EstadoObjetivo = new int[,] {
        { 1, 2, 3 },
        { 4, 5, 6 },
        { 7, 8, 0 }
    };

    private static readonly (int, int)[] Direcoes = { (-1, 0), (1, 0), (0, -1), (0, 1) };

    public static void ResolverQuebraCabeca(int[,] estadoInicial)
    {
        var listaAbertos = new FilaPrioridade<EstadoQuebraCabeca>();
        var listaFechados = new HashSet<string>();

        listaAbertos.Enfileirar(new EstadoQuebraCabeca(estadoInicial, 0, ObterHeuristica(estadoInicial)), 0);

        int tentativas = 0;

        while (listaAbertos.Quantidade > 0 && tentativas < 1000)
        {
            tentativas++;

            var atual = listaAbertos.Desenfileirar();
            var estado = atual.Estado;

            Console.WriteLine($"Tentativa {tentativas}:");
            ImprimirEstado(estado);
            Console.WriteLine();

            if (VerificarEstadoObjetivo(estado))
            {
                ImprimirSolucao(atual);
                return;
            }

            listaFechados.Add(EstadoParaString(estado));

            foreach (var movimento in ObterMovimentosPossiveis(estado))
            {
                var novoEstado = AplicarMovimento(estado, movimento);
                var novoEstadoString = EstadoParaString(novoEstado);

                if (!listaFechados.Contains(novoEstadoString))
                {
                    var novaHeuristica = ObterHeuristica(novoEstado);
                    var novoCusto = atual.Custo + 1;
                    listaAbertos.Enfileirar(new EstadoQuebraCabeca(novoEstado, novoCusto, novaHeuristica, atual), novoCusto + novaHeuristica);
                }
            }
        }

        Console.WriteLine("Não foi possível resolver o quebra-cabeça em 1000 tentativas.");
    }

    public static void ImprimirEstado(int[,] estado)
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                Console.Write($"{estado[i, j]} ");
            }
            Console.WriteLine();
        }
    }

    public static int[,] GerarEstadoInicialAleatorio()
    {
        var estado = new int[3, 3]
        {
            { 1, 2, 3 },
            { 4, 5, 6 },
            { 7, 8, 0 }
        };

        var random = new Random();
        var lista = Enumerable.Range(0, 9).OrderBy(x => random.Next()).ToArray();

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                estado[i, j] = lista[i * 3 + j];
            }
        }

        return estado;
    }

    private static bool VerificarEstadoObjetivo(int[,] estado)
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (estado[i, j] != EstadoObjetivo[i, j])
                {
                    return false;
                }
            }
        }
        return true;
    }

    private static void ImprimirSolucao(EstadoQuebraCabeca estado)
    {
        Console.WriteLine("Solução encontrada:");
        var caminho = new Stack<int[,]>();
        while (estado != null)
        {
            caminho.Push(estado.Estado);
            estado = estado.Anterior;
        }

        foreach (var passo in caminho)
        {
            ImprimirEstado(passo);
            Console.WriteLine();
        }

        Console.WriteLine("Final");
    }

    private static IEnumerable<(int, int)> ObterMovimentosPossiveis(int[,] estado)
    {
        int linhaZero = 0;
        int colunaZero = 0;

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (estado[i, j] == 0)
                {
                    linhaZero = i;
                    colunaZero = j;
                    break;
                }
            }
        }

        foreach (var (dl, dc) in Direcoes)
        {
            int novaLinha = linhaZero + dl;
            int novaColuna = colunaZero + dc;

            if (novaLinha >= 0 && novaLinha < 3 && novaColuna >= 0 && novaColuna < 3)
            {
                yield return (novaLinha, novaColuna);
            }
        }
    }

    private static int[,] AplicarMovimento(int[,] estado, (int, int) movimento)
    {
        var novoEstado = (int[,])estado.Clone();
        int linhaZero = 0;
        int colunaZero = 0;

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (estado[i, j] == 0)
                {
                    linhaZero = i;
                    colunaZero = j;
                    break;
                }
            }
        }

        int novaLinha = movimento.Item1;
        int novaColuna = movimento.Item2;

        novoEstado[linhaZero, colunaZero] = novoEstado[novaLinha, novaColuna];
        novoEstado[novaLinha, novaColuna] = 0;

        return novoEstado;
    }

    private static int ObterHeuristica(int[,] estado)
    {
        int heuristica = 0;

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                int valor = estado[i, j];
                if (valor != 0)
                {
                    int linhaObjetivo = (valor - 1) / 3;
                    int colunaObjetivo = (valor - 1) % 3;
                    heuristica += Math.Abs(i - linhaObjetivo) + Math.Abs(j - colunaObjetivo);
                }
            }
        }

        return heuristica;
    }

    private static string EstadoParaString(int[,] estado)
    {
        return string.Join(",", estado.Cast<int>());
    }
}
