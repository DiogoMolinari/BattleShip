# ⚓ Jogo de Batalha Naval em C#

## Sobre o projeto

Este projeto consiste no desenvolvimento de uma versão simplificada do clássico jogo Batalha Naval, utilizando C# e executada no console.

O jogo possui dois tabuleiros de dimensões 10x10, um para o jogador e outro para o computador. Cada participante possui uma frota composta por cinco embarcações:

* 1 embarcação de tamanho 5;
* 1 embarcação de tamanho 4;
* 2 embarcações de tamanho 3;
* 1 embarcação de tamanho 2.

As embarcações são posicionadas aleatoriamente, podendo ocupar posições horizontais ou verticais, sem sobreposição.

## Estrutura do projeto

Para desenvolver a lógica do jogo, foram utilizadas quatro matrizes bidimensionais:

* **PlayerBoard:** armazena as posições originais das embarcações do jogador.
* **PCBoard:** armazena as posições originais das embarcações do computador.
* **PlayerShots:** registra as posições atacadas pelo jogador no tabuleiro adversário, identificando acertos e erros.
* **PCShots:** registra as posições atacadas pelo computador no tabuleiro do jogador.

Essa separação permite controlar os tabuleiros e os ataques de cada participante sem expor ao jogador as posições das embarcações adversárias.

## Principais funções

### InitializeBoards()

Responsável por inicializar as quatro matrizes, preenchendo todas as posições com o caractere `\~`, que representa a água. Dessa forma, todos os tabuleiros começam vazios.

### PositionShips()

Responsável pelo posicionamento aleatório das embarcações.

A função utiliza geração de números aleatórios para definir a orientação de cada embarcação (horizontal ou vertical), além de escolher suas coordenadas iniciais.

Antes de confirmar cada posicionamento, é realizada uma verificação para garantir que nenhuma embarcação ocupe posições já utilizadas por outra, evitando sobreposições.

### DisplayGameBoards()

Responsável pela exibição dos tabuleiros durante a partida, apresentando as coordenadas de linhas e colunas e facilitando a visualização das jogadas.

São exibidos o tabuleiro do jogador (`PlayerBoard`) e o registro dos ataques realizados por ele (`PlayerShots`). Dessa forma, as posições das embarcações do computador permanecem ocultas.

### PlayerTurn()

Responsável por controlar a vez do jogador.

O usuário informa a linha e a coluna que deseja atacar. São realizadas validações para garantir que as coordenadas estejam dentro dos limites do tabuleiro e que a posição escolhida ainda não tenha sido atacada.

Caso o jogador acerte uma embarcação, a posição recebe o caractere `X`, a variável `playerHits` é incrementada e a função retorna `true`, permitindo uma nova jogada.

Caso erre, a posição recebe o caractere `O` e a função retorna `false`, passando a vez para o computador.

### PCTurn()

Possui a mesma finalidade da função `PlayerTurn()`, porém as coordenadas de ataque são geradas aleatoriamente pelo computador.

Também existe uma verificação para impedir que o computador ataque uma posição que já tenha sido escolhida anteriormente.

Assim como na jogada do usuário, os acertos são registrados com `X` e os erros com `O`. A variável `pcHits` contabiliza os acertos do computador.

## Regras e condições de vitória

O jogador e o computador realizam ataques alternadamente. Entretanto, quando um participante acerta uma embarcação, ganha o direito de realizar outro ataque.

Como a frota possui 17 posições ocupadas, vence aquele que atingir primeiro 17 acertos.

Ao final da partida, o programa identifica o vencedor e apresenta a mensagem correspondente.

## Tecnologias e conceitos utilizados

* C#
* Matrizes bidimensionais
* Estruturas condicionais
* Estruturas de repetição (`for`, `while` e `do while`)
* Métodos e parâmetros
* Retorno de métodos (`bool`)
* Geração de números aleatórios
* Validação de entradas com `int.TryParse()`

\---

**Projeto desenvolvido com o objetivo de praticar lógica de programação e conceitos fundamentais de desenvolvimento utilizando C#.**

