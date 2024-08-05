using System;
using System.Collections.Generic;
using System.Linq;

public class FilaPrioridade<T>
{
    private readonly SortedList<int, Queue<T>> _elementos = new SortedList<int, Queue<T>>();

    public int Quantidade => _elementos.Values.Sum(q => q.Count);

    public void Enfileirar(T item, int prioridade)
    {
        if (!_elementos.ContainsKey(prioridade))
        {
            _elementos[prioridade] = new Queue<T>();
        }
        _elementos[prioridade].Enfileirar(item);
    }

    public T Desenfileirar()
    {
        if (_elementos.Count == 0)
            throw new InvalidOperationException("A fila está vazia.");

        var primeiroPar = _elementos.First();
        var item = primeiroPar.Value.Desenfileirar();

        if (primeiroPar.Value.Count == 0)
        {
            _elementos.Remove(primeiroPar.Key);
        }

        return item;
    }
}
