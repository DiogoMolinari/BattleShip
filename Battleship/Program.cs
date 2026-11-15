using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;

char[,] PlayerBoard = new char[10, 10];
char[,] PCBoard = new char[10, 10];
char[,] PlayerShots = new char[10, 10];
char[,] PCShots = new char[10, 10];

int[] ShipSizes = new int[] { 5, 4, 3, 3, 2 };

int playerHits = 0;
int pcHits = 0;
int round = 1;

void InitializeBoards()
{

    for (int i = 0; i < 10; i++)
    {
        for (int j = 0; j < 10; j++)
        {
            PlayerBoard[i, j] = '~';
            PCBoard[i, j] = '~';
            PlayerShots[i, j] = '~';
            PCShots[i, j] = '~';
        }

    }
}

void DisplayBoards(char[,] matrix)
{
    for (int i = 0; i < 10; i++)
    {
        for (int j = 0; j < 10; j++)
        {
            Console.Write(matrix[i, j]);
        }
        Console.WriteLine();
    }
}


void PositionShips(char[,] matrix)
{
    for (int currentShip = 0; currentShip < ShipSizes.Length; currentShip++)
    {
        int shipSize = ShipSizes[currentShip];

        int orientation;
        int line;
        int column;
        bool positionIsValid;

        do
        {
            positionIsValid = true;

            orientation = RandomNumberGenerator.GetInt32(0, 2);

            if (orientation == 0) // horizontal
            {
                line = RandomNumberGenerator.GetInt32(0, 10);
                column = RandomNumberGenerator.GetInt32(0, 10 - shipSize + 1);

                for (int i = 0; i < shipSize; i++)
                {
                    if (matrix[line, column + i] == 'N')
                    {
                        positionIsValid = false;
                        break;
                    }
                }
            }
            else // vertical
            {
                line = RandomNumberGenerator.GetInt32(0, 10 - shipSize + 1);
                column = RandomNumberGenerator.GetInt32(0, 10);

                for (int i = 0; i < shipSize; i++)
                {
                    if (matrix[line + i, column] == 'N')
                    {
                        positionIsValid = false;
                        break;
                    }
                }
            }

        } while (positionIsValid == false);

        // se as posições estiverem != N, então elas serão preenchidas com N para indicar que há um navio
        if (orientation == 0)
        {
            for (int i = 0; i < shipSize; i++)
            {
                matrix[line, column + i] = 'N';
            }
        }
        else
        {
            for (int i = 0; i < shipSize; i++)
            {
                matrix[line + i, column] = 'N';
            }
        }
    }
}


void DisplayGameBoards()
{
    Console.WriteLine("     Seu tabuleiro             Tabuleiro do Computador");

    Console.Write("   ");
    for (int header = 0; header < 10; header++)
    {
        Console.Write(header + " ");
    }

    Console.Write("          ");
    for (int header = 0; header < 10; header++)
    {
        Console.Write(header + " ");
    }
    Console.WriteLine();

    for (int i = 0; i < 10; i++)
    {
        Console.Write(i + "  ");

        for (int j = 0; j < 10; j++)
        {
            Console.Write(PlayerBoard[i, j] + " ");
        }

        Console.Write("       " + i + "  ");

        for (int j = 0; j < 10; j++)
        {
            Console.Write(PlayerShots[i, j] + " ");
        }
        Console.WriteLine();
    }
}

InitializeBoards();
PositionShips(PlayerBoard);
PositionShips(PCBoard);

while (playerHits < 17 && pcHits < 17)
{
    Console.WriteLine("\n========== RODADA " + round + " ==========\n");
    bool playerHit;
    do
    {
        DisplayGameBoards();
        playerHit = PlayerTurn();

    } while (playerHit && playerHits < 17);
    if (playerHits == 17)
        break;


    bool pcHit;
    do
    {
        pcHit = PCTurn();

    } while (pcHit && pcHits < 17);
    round++;
}

if (playerHits == 17)
{
    Console.WriteLine("Muito bem! Você venceu o jogo!");
}
else if (pcHits == 17)
{
    Console.WriteLine("Você foi derrotado, tente novamente!");
}


bool PlayerTurn()
{
    int lineAttack = 0;
    int columnAttack = 0; ;
    bool validShot;

    do
    {
        validShot = true;

        Console.Write("\nSua vez de jogar.\nQual linha deseja atacar? ");

        if (!int.TryParse(Console.ReadLine(), out lineAttack))
        {
            Console.WriteLine("Entrada inválida! Você deve digitar um número inteiro.");
            validShot = false;
            continue;
        }

        if (lineAttack < 0 || lineAttack >= 10)
        {
            Console.WriteLine("Linha inválida! Digite um valor entre 0 e 9.");
            validShot = false;
            continue;
        }

        Console.Write("Qual coluna deseja atacar? ");

        if (!int.TryParse(Console.ReadLine(), out columnAttack))
        {
            Console.WriteLine("Entrada inválida! Você deve digitar um número inteiro.");
            validShot = false;
            continue;
        }

        if (columnAttack < 0 || columnAttack >= 10)
        {
            Console.WriteLine("Coluna inválida! Digite um valor entre 0 e 9.");
            validShot = false;
            continue;
        }

        if (PlayerShots[lineAttack, columnAttack] != '~')
        {
            Console.WriteLine("Você já atirou nessa posição, escolha novamente!");
            validShot = false;
        }

    } while (!validShot);

    if (PCBoard[lineAttack, columnAttack] == 'N')
    {
        PlayerShots[lineAttack, columnAttack] = 'X';
        playerHits++;
        Console.WriteLine("Você acertou uma parte da embarcação!");
        return true;
    }
    else
    {
        PlayerShots[lineAttack, columnAttack] = 'O';
        Console.WriteLine("Seu ataque foi na água.");
        return false;
    }
}


bool PCTurn()
{
    int lineAttack;
    int columnAttack;
    Random random = new Random();

    do
    {
        Console.WriteLine("\nAguarde, vez do computador.");
        lineAttack = random.Next(0, 10);
        columnAttack = random.Next(0, 10);

    } while (PCShots[lineAttack, columnAttack] != '~');

    Thread.Sleep(2500);

    Console.WriteLine("O computador atacou a linha " + lineAttack +
                      " e a coluna " + columnAttack + ".");

    Thread.Sleep(2000);

    if (PlayerBoard[lineAttack, columnAttack] == 'N')
    {
        PCShots[lineAttack, columnAttack] = 'X';
        pcHits++;
        Console.WriteLine("Uma parte da sua embarcação foi atingida!");
        return true;
    }
    else
    {
        PCShots[lineAttack, columnAttack] = 'O';
        Console.WriteLine("Sem perigo, o ataque foi na água.");
        return false;
    }
}
