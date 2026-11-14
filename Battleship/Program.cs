using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;

char[,] PlayerBoard = new char[10, 10];
char[,] PCBoard = new char[10, 10];
char[,] PlayerShots = new char[10, 10];
char[,] PCShots = new char[10, 10];

int[] ShipSizes = new int[] { 5, 4, 3, 3, 2 };

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

        } while (positionIsValid==false);

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


InitializeBoards();
PositionShips(PCBoard);
DisplayBoards(PCBoard);
