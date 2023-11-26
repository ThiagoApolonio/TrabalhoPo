using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    
    //Grupo Thiago Matos de Apolonio E01565, Lucas, Marcos,Ezequiel
    struct SolucaoParcial
    {
        public int[,] Transporte;
        public int CustoTotal;
    }
    // Main ou Index
    static void Main()
    {
        // Definindo os dados do problema
        List<List<int>> custos = new List<List<int>>
        {
            new List<int> {25, 20, 30},
            new List<int> {30, 25, 25},
            new List<int> {20, 15, 23}
        };

        List<int> oferta = new List<int> { 2000, 1500, 1500 };
        List<int> demanda = new List<int> { 2000, 2000, 1000 };

        // Resolvendo o Problema de Transporte com Alocação Mínima
        SolucaoParcial solucaoAlocacaoMinima = ResolverProblemaTransporte(custos, oferta, demanda);

        // Imprimindo a tabela de transporte da solução com Alocação Mínima
        Console.WriteLine("Alocação Mínima:");
        ImprimirTabelaTransporte(solucaoAlocacaoMinima.Transporte);
        Console.WriteLine($"Custo Total: {solucaoAlocacaoMinima.CustoTotal}");
        Console.WriteLine();

        // Resolvendo o Problema de Transporte com Canto Noroeste
        SolucaoParcial solucaoCantoNoroeste = CantoNoroeste(custos, oferta, demanda);

        // Imprimindo a tabela de transporte da solução com Canto Noroeste
        Console.WriteLine("Canto Noroeste:");
        ImprimirTabelaTransporte(solucaoCantoNoroeste.Transporte);
        Console.WriteLine($"Custo Total: {solucaoCantoNoroeste.CustoTotal}");
        Console.WriteLine();

       
  // Definindo os dados do problema
        List<List<int>> custosVoguel = new List<List<int>>
        {
            new List<int> {25, 20, 30},
            new List<int> {30, 25, 25},
            new List<int> {20, 15, 23}
        };

        List<int> ofertaVoguel = new List<int> { 2000, 1500, 1500 };
        List<int> demandaVoguel = new List<int> { 2000, 2000, 1000 };
        // Resolvendo o Problema de Transporte com Aproximação de Vogel
        SolucaoParcial solucaoAproximacaoVogel = AproximacaoVogel(custosVoguel, ofertaVoguel, demandaVoguel);

        // Imprimindo a tabela de transporte da solução com Aproximação de Vogel
        Console.WriteLine("Aproximação de Vogel:");
        ImprimirTabelaTransporte(solucaoAproximacaoVogel.Transporte);
        Console.WriteLine($"Custo Total: {solucaoAproximacaoVogel.CustoTotal}");
        Console.WriteLine();

        // Observo o menor valor para Eleger o melhor custo 
        int menorCusto = Math.Min(solucaoAlocacaoMinima.CustoTotal, Math.Min(solucaoCantoNoroeste.CustoTotal, solucaoAproximacaoVogel.CustoTotal));

        if (menorCusto == solucaoAlocacaoMinima.CustoTotal)
        {
            Console.WriteLine("Alocação Mínima é a melhor forma para o transporte.");
        }
        else if (menorCusto == solucaoCantoNoroeste.CustoTotal)
        {
            Console.WriteLine("Canto Noroeste é a melhor forma para o transporte.");
        }
        else
        {
            Console.WriteLine("Aproximação de Vogel é a melhor forma para o transporte.");
        }


    }

    // Função para resolver o Problema de Transporte
    static SolucaoParcial ResolverProblemaTransporte(List<List<int>> custos, List<int> oferta, List<int> demanda)
    {
        int m = oferta.Count;    // Número de fontes
        int n = demanda.Count;   // Número de destinos

        // Heurística para encontrar uma solução inicial (pode ser substituída por outros métodos)
        SolucaoParcial solucaoInicial = EncontrarSolucaoInicial(custos, oferta, demanda);

        // Inicializando as variáveis
        int[,] transporte = solucaoInicial.Transporte;
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

        // Calculando o custo total da solução
        int custoTotal = CalcularCustoTotal(transporte, custos);

        // Retornando a solução
        return new SolucaoParcial { Transporte = transporte, CustoTotal = custoTotal };
    }

    // Função para encontrar uma solução inicial (Heurística)
    static SolucaoParcial EncontrarSolucaoInicial(List<List<int>> custos, List<int> oferta, List<int> demanda)
    {
        // Neste exemplo, a heurística é simplesmente a alocação mínima
        int[,] transporteInicial = new int[oferta.Count, demanda.Count];
        int custoInicial = CalcularCustoTotal(transporteInicial, custos);

        return new SolucaoParcial { Transporte = transporteInicial, CustoTotal = custoInicial };
    }

    // Função para calcular o custo total de uma alocação
    static int CalcularCustoTotal(int[,] transporte, List<List<int>> custos)
    {
        int custoTotal = 0;
        for (int i = 0; i < transporte.GetLength(0); ++i)
        {
            for (int j = 0; j < transporte.GetLength(1); ++j)
            {
                custoTotal += transporte[i, j] * custos[i][j];
            }
        }
        return custoTotal;
    }

    // Função para imprimir a tabela de transporte
    static void ImprimirTabelaTransporte(int[,] transporte)
    {
        Console.WriteLine("Tabela de Transporte:");
        for (int i = 0; i < transporte.GetLength(0); ++i)
        {
            for (int j = 0; j < transporte.GetLength(1); ++j)
            {
                Console.Write($"{transporte[i, j],5}\t");
            }
            Console.WriteLine();
        }
    }

    // Função para encontrar uma solução inicial usando o método de Canto Noroeste
    static SolucaoParcial CantoNoroeste(List<List<int>> custos, List<int> oferta, List<int> demanda)
    {
        int m = oferta.Count;    // Número de fontes
        int n = demanda.Count;   // Número de destinos

        int[,] transporteInicial = new int[m, n];
        int i = 0, j = 0;

        while (i < m && j < n)
        {
            int quantidade = Math.Min(oferta[i], demanda[j]);
            transporteInicial[i, j] = quantidade;

            oferta[i] -= quantidade;
            demanda[j] -= quantidade;

            if (oferta[i] == 0)
                i++;
            if (demanda[j] == 0)
                j++;
        }

        int custoInicial = CalcularCustoTotal(transporteInicial, custos);

        return new SolucaoParcial { Transporte = transporteInicial, CustoTotal = custoInicial };
    }

    // Função para encontrar uma solução inicial usando o método de Aproximação de Vogel
    static SolucaoParcial AproximacaoVogel(List<List<int>> custos, List<int> oferta, List<int> demanda)
    {
        int m = oferta.Count;    // Número de fontes
        int n = demanda.Count;   // Número de destinos

        int[,] transporteInicial = new int[m, n];

        // Enquanto houver oferta ou demanda não atendida
        while (oferta.Any(val => val > 0) && demanda.Any(val => val > 0))
        {
            int i = -1, j = -1;
            int maxDifRow = -1, maxDifCol = -1;

            // Calcular as penalidades para as linhas
            for (int row = 0; row < m; ++row)
            {
                if (oferta[row] > 0)
                {
                    List<int> rowCosts = custos[row];
                    List<int> sortedRowCosts = rowCosts.OrderBy(c => c).ToList();
                    int minCost1 = sortedRowCosts[0];
                    int minCost2 = sortedRowCosts[1];

                    int dif = minCost2 - minCost1;

                    if (dif > maxDifRow)
                    {
                        maxDifRow = dif;
                        i = row;
                    }
                }
            }

            // Calcular as penalidades para as colunas
            for (int col = 0; col < n; ++col)
            {
                if (demanda[col] > 0)
                {
                    List<int> colCosts = new List<int>();
                    for (int row = 0; row < m; ++row)
                    {
                        colCosts.Add(custos[row][col]);
                    }

                    int minCost1 = colCosts.Min();
                    colCosts.Remove(minCost1);
                    int minCost2 = colCosts.Min();

                    int dif = minCost2 - minCost1;

                    if (dif > maxDifCol)
                    {
                        maxDifCol = dif;
                        j = col;
                    }
                }
            }

            int quantidade = Math.Min(oferta[i], demanda[j]);
            transporteInicial[i, j] = quantidade;

            oferta[i] -= quantidade;
            demanda[j] -= quantidade;
        }

        int custoInicial = CalcularCustoTotal(transporteInicial, custos);

        return new SolucaoParcial { Transporte = transporteInicial, CustoTotal = custoInicial };
    }




}
//// Codigo Feito Por Thiago Matos De Apolonio  