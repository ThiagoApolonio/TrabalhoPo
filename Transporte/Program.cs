using System;
using System.Collections.Generic;

class Program
{
    // Função para resolver o Problema de Transporte
    static void Transporte(List<List<int>> custos, List<int> oferta, List<int> demanda)
    {
        int m = oferta.Count;    // Número de fontes
        int n = demanda.Count;   // Número de destinos

        // Inicializando as variáveis
        int[,] transporte = new int[m, n];
        List<int> oferta_residual = new List<int>(oferta);
        List<int> demanda_residual = new List<int>(demanda);

        // Enquanto houver oferta ou demanda não atendida
        while (oferta_residual.Exists(val => val > 0) && demanda_residual.Exists(val => val > 0))
        {
            // Encontrando a célula com o menor custo
            int minCost = int.MaxValue;
            int minI = 0, minJ = 0;

            for (int i = 0; i < m; ++i)
            {
                for (int j = 0; j < n; ++j)
                {
                    if (oferta_residual[i] > 0 && demanda_residual[j] > 0 && custos[i][j] < minCost)
                    {
                        minCost = custos[i][j];
                        minI = i;
                        minJ = j;
                    }
                }
            }

            // Determinando a quantidade a ser transportada
            int quantidade = Math.Min(oferta_residual[minI], demanda_residual[minJ]);

            // Atualizando as variáveis
            oferta_residual[minI] -= quantidade;
            demanda_residual[minJ] -= quantidade;
            transporte[minI, minJ] = quantidade;
        }

        // Imprimindo a tabela de transporte
        Console.WriteLine("Tabela de Transporte:");
        for (int i = 0; i < m; ++i)
        {
            for (int j = 0; j < n; ++j)
            {
                Console.Write($"{transporte[i, j],5}\t");
            }
            Console.WriteLine();
        }
    }

    static void Main()
    {
        // Definindo os dados do problema
        List<List<int>> custos = new List<List<int>>
        {
            new List<int> {3, 2, 7},
            new List<int> {2, 4, 5},
            new List<int> {1, 3, 2}
        };

        List<int> oferta = new List<int> { 100, 150, 200 };
        List<int> demanda = new List<int> { 120, 80, 170 };

        // Resolvendo o Problema de Transporte
        Transporte(custos, oferta, demanda);
    }
}
