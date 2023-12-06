using System;
using System.Collections.Generic;
using System.Drawing;

using System.Drawing;

public class Nodo
{
    public int valor;
    public Color color; // Agregar esta línea
    public Nodo izquierdo;
    public Nodo derecho;

    public Nodo(int valor)
    {
        this.valor = valor;
        this.color = Color.LightBlue; // Color predeterminado
        izquierdo = derecho = null;
    }
}

public class ArbolBinario
{
    public Nodo raiz;

    public ArbolBinario()
    {
        raiz = null;
    }

    public void Insertar(int valor)
    {
        raiz = Insertar(raiz, valor);
    }

    private Nodo Insertar(Nodo nodo, int valor)
    {
        if (nodo == null)
        {
            nodo = new Nodo(valor);
        }
        else if (valor < nodo.valor)
        {
            nodo.izquierdo = Insertar(nodo.izquierdo, valor);
        }
        else
        {
            nodo.derecho = Insertar(nodo.derecho, valor);
        }

        return nodo;
    }

    public void Eliminar(int valor)
    {
        raiz = Eliminar(raiz, valor);
    }

    private Nodo Eliminar(Nodo nodo, int valor)
    {
        if (nodo == null)
        {
            return nodo;
        }

        if (valor < nodo.valor)
        {
            nodo.izquierdo = Eliminar(nodo.izquierdo, valor);
        }
        else if (valor > nodo.valor)
        {
            nodo.derecho = Eliminar(nodo.derecho, valor);
        }
        else
        {
            if (nodo.izquierdo == null)
            {
                return nodo.derecho;
            }
            else if (nodo.derecho == null)
            {
                return nodo.izquierdo;
            }

            nodo.valor = MinValor(nodo.derecho);
            nodo.derecho = Eliminar(nodo.derecho, nodo.valor);
        }

        return nodo;
    }

    private int MinValor(Nodo nodo)
    {
        int minv = nodo.valor;

        while (nodo.izquierdo != null)
        {
            minv = nodo.izquierdo.valor;
            nodo = nodo.izquierdo;
        }

        return minv;
    }

    public bool Buscar(int valor)
    {
        return Buscar(raiz, valor);
    }

    private bool Buscar(Nodo nodo, int valor)
    {
        if (nodo == null)
        {
            return false;
        }
        else if (valor < nodo.valor)
        {
            return Buscar(nodo.izquierdo, valor);
        }
        else if (valor > nodo.valor)
        {
            return Buscar(nodo.derecho, valor);
        }
        else
        {
            return true;
        }
    }

    // Recorrido en Preorden
    public void RecorridoPreorden(Action<Nodo> visitar)
    {
        RecorridoPreorden(raiz, visitar);
    }

    private void RecorridoPreorden(Nodo nodo, Action<Nodo> visitar)
    {
        if (nodo != null)
        {
            visitar(nodo);
            RecorridoPreorden(nodo.izquierdo, visitar);
            RecorridoPreorden(nodo.derecho, visitar);
        }
    }

    // Recorrido en Inorden
    public void RecorridoInorden(Action<Nodo> visitar)
    {
        RecorridoInorden(raiz, visitar);
    }

    private void RecorridoInorden(Nodo nodo, Action<Nodo> visitar)
    {
        if (nodo != null)
        {
            RecorridoInorden(nodo.izquierdo, visitar);
            visitar(nodo);
            RecorridoInorden(nodo.derecho, visitar);
        }
    }

    // Recorrido en Postorden
    public void RecorridoPostorden(Action<Nodo> visitar)
    {
        RecorridoPostorden(raiz, visitar);
    }

    private void RecorridoPostorden(Nodo nodo, Action<Nodo> visitar)
    {
        if (nodo != null)
        {
            RecorridoPostorden(nodo.izquierdo, visitar);
            RecorridoPostorden(nodo.derecho, visitar);
            visitar(nodo);
        }
    }

    // Recorrido en Anchura (BFS)
    public void RecorridoAnchura(Action<Nodo> visitar)
    {
        if (raiz == null)
            return;

        Queue<Nodo> cola = new Queue<Nodo>();
        cola.Enqueue(raiz);

        while (cola.Count > 0)
        {
            Nodo actual = cola.Dequeue();
            visitar(actual);

            if (actual.izquierdo != null)
                cola.Enqueue(actual.izquierdo);
            if (actual.derecho != null)
                cola.Enqueue(actual.derecho);
        }
    }
}
