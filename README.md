# Problema de Transporte
## Introdução e overview do código

O presente algoritmo busca resolver o problema de minimização dos custos de transportes de mercadorias envolvendo n fontes para n destinos, sendo estes problemas sujeitos a restrições de oferta e demanda.
A questão envolvendo o problema de transporte se dá pois, normalmente, pode-se ter x fontes, y destinos e os custos de transporte, a capacidade de oferta e a demanda por região podem diferir conforme a situação.

Por meio da utilização da abordagem de encontrar a célula com o menor custo por unidade de transporte é possível determinar a quantidade a ser transportada com base nas ofertas e demandas residuais por cada destino. 

Utilizando iterarações para calcular todas as possiblidades de ofertas ou demandas que estão de acordo com as restrições dadas, o algoritmo é capaz de selecionar a célula com o menor custo possível e entregar a possibilidade de rota mais viável. Como resultado final, o script traz a tabela de transporte que ele entende que trouxe o menor custo total que atende as determinadas restrições.

## Exemplo de problema e output do código

### Problema de Transporte: Logística de Distribuição de Produtos

Imagine uma empresa que produz produtos em três fábricas diferentes (A, B e C) e precisa distribuir esses produtos para quatro armazéns (X, Y, Z e W) em diferentes regiões. Cada fábrica tem uma capacidade de produção diária específica, e cada armazém tem uma demanda diária específica para os produtos. O objetivo é determinar a quantidade diária a ser transportada de cada fábrica para cada armazém, minimizando os custos de transporte.

Dados do Problema:

Capacidade de Produção Diária das Fábricas:

Fábrica A: 150 unidades
Fábrica B: 200 unidades
Fábrica C: 180 unidades
Demanda Diária dos Armazéns:

Armazém X: 120 unidades
Armazém Y: 180 unidades
Armazém Z: 150 unidades
Armazém W: 100 unidades
Custos de Transporte  :
De A para X: 5
De A para Y: 7
De A para Z: 4
De A para W: 8
De B para X: 6
De B para Y: 6
De B para Z: 3
De B para W: 9
De C para X: 8
De C para Y: 5
De C para Z: 2
De C para W: 7

### Saída: 
![Exemplo_output_do_código](assets\images\problema_de_transporte.jpg)

### Nomes dos integrantes do grupo:
Thiago, Marcos, Lucas e Ezequiel