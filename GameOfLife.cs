/*
 Kevin Bax 3IC
 The Game Of Life - c# version

 - Nel programma sono state inserite diverse possibilità di caricamento
   nella matrice, tra cui una versione manuale per poter verificare casi
   specifici ed effetturare un eventuale debug.

 - è presente inoltre una versione di caricamento semplificata
   che riduce il numero di caselle piene, per evitare un output
   confusionario, data la scarsa ottimizzazione del programma nella console.

 - per utilizzare un caricamento completamente automatico si può utilizzare la funzione
   "PopolaMatrice" presente sotto.

 - Il programma è ottimizzato per gestire matrici di qualsiasi grandezza.

 - le caselle non comprese nel range della matrice prendo valore di default 0
*/

using System;

namespace GameOfLifeBax
{
    class Program
    {
        //Funzione che popola la matrice con valori randomici [0 / 1]
        static void PopolaMatrice(int[,] mat)
        {
            Random r = new Random();
            for (int i = 0; i < mat.GetLength(0); i++)
            {
                for (int x = 0; x < mat.GetLength(1); x++)
                {
                    mat[i, x] = r.Next(0, 2);
                }
            }
        }

        /*
         Per i test verranno un numero minore di quadrati attivi al fine di
         semplificare la visione dell'output data la grandezza della matrice
         piuttosto ridotta. In alternativa si può utilizzare la funzione presenta
         sopra
        */


        /*
         Accorgimento:
         Per svolgere il programma ho notato che non è possibile analizzare bit per bit
         poichè causerebbe un output errato. La soluzione è stata perciò quella di utilizzaee
         una matrice secondaria d'appoggio cosiì da cambiare tutti i bit contemporaneamente
        */

        static void CopiaMatrice(int[,] mat1, int[,] mat2)
        {
            for (int i = 0; i < mat1.GetLength(0); i++)
                for (int x = 0; x < mat1.GetLength(1); x++)
                    mat2[i, x] = mat1[i, x];
        }

        static void PopolaMatriceRid(int[,] mat)
        {
            Random r = new Random();
            for (int i = 0; i < mat.GetLength(0); i++)
            {
                for (int x = 0; x < mat.GetLength(1); x++)
                {
                    int num = r.Next(0, 21);
                    if (num > 0) //in questo modo limito il numero di caselle attive
                        mat[i, x] = 0;
                    else
                        mat[i, x] = 1;
                }
            }
        }

        //Stampa della matrice in output
        static void StampaMatrice(int[,] mat)
        {
            for (int i = 0; i < mat.GetLength(0); i++)
            {
                for (int x = 0; x < mat.GetLength(1); x++)
                {
                    if (mat[i,x] == 0)
                        Console.Write("□ ");
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("■ ");
                        Console.ResetColor();
                    }
                }
                Console.WriteLine();
            }
        }

        //Applicazione delle regole del gioco
        static void EvolviMat(int[,] mat)
        {
            int[,] mat2 = new int[mat.GetLength(0), mat.GetLength(1)]; //matrice di supporto
            //copio la matrice originaria in quella secondaria
            CopiaMatrice(mat, mat2);

            for (int i = 0; i < mat.GetLength(0); i++)
            {
                for (int x = 0; x < mat.GetLength(1); x++)
                {
                    int conta = 0;
                    //sotto
                    if (i + 1 < mat.GetLength(0) && mat[i + 1, x] == 1) //evito di uscire dalla matrice, per i valori al di fuori conta 0
                        conta++;
                    //sopra
                    if (i - 1 >= 0 && mat[i - 1, x] == 1)
                        conta++;                                      
                    //destra
                    if (x + 1 < mat.GetLength(1) && mat[i, x + 1] == 1)
                        conta++;
                    //sinistra
                    if (x - 1 >= 0 && mat[i, x - 1] == 1)       
                        conta++;
                    //diag 1 sup [\]
                    if (i - 1 >= 0 && x - 1 >= 0 && mat[i - 1, x - 1] == 1)
                        conta++;
                    //diag 1 inf [\]
                    if (i + 1 < mat.GetLength(0) && x + 1 < mat.GetLength(1) && mat[i + 1, x + 1] == 1)
                        conta++;
                    //diag 2 sup [/]
                    if (i - 1 >= 0 && x + 1 < mat.GetLength(1) && mat[i - 1, x + 1] == 1)
                        conta++;
                    //diag 2 inf [/]
                    if (i + 1 < mat.GetLength(0) && x - 1 >= 0 && mat[i + 1, x - 1] == 1)
                        conta++;

                    bool flag = true; //può verificarsi una sola condizione a cella, evito controllo aggiuntivi

                    //------------------------------------ESECUZIONE DELLE REGOLE-----------------------------------------//

                    //REGOLA 1: la cella muore se 0 / 1 vicino
                    if (flag && mat[i,x] == 1 && (conta == 0 || conta == 1))
                    {
                        mat2[i, x] = 0; //la cella muore di solitudine
                        flag = false;
                    }

                    //REGOLA 2: la cella muore se ha 4 o + vicini
                    if (flag && mat[i, x] == 1 && conta >= 4)
                    {
                        mat2[i, x] = 0; //la cella muore per sovrappopolazione
                        flag = false;
                    }

                    //REGOLA 3: la cella sopravvive se ha 2 o 3 vicini
                    if(flag == true && mat[i, x] == 1 && (conta == 2 || conta == 3))
                        flag = false;    //la cella resta invariata

                    //REGOLA 4: una cella spenta si riaccende se ha 3 vicini attivi
                    if (flag && mat[i,x] == 0 && conta == 3)
                    {
                        mat2[i, x] = 1; //se ha 3 vicini attivi, diventa a sua volta attiva
                        flag = false;
                    }
                }
            }
            //riassegno alla prima matrice i valori della seconda
            CopiaMatrice(mat2, mat);
        }


        static void Main(string[] args)
        {
            int[,] mat = new int[10, 10]; //modificare qui i valori x aumentare grandezza matrice

            /*
            int[,] mat =
            {
                {0, 0, 0, 0, 0, 1, 1 },
                {0, 0, 0, 0, 0, 1, 1 },
                {0, 0, 0, 0, 0, 0, 0 },
                {0, 0, 1, 1, 1, 0, 0 },
                {0, 0, 1, 1, 1, 0, 0 },
                {0, 0, 1, 1, 1, 0, 0 },
                {0, 0, 0, 0, 0, 0, 0 },
            };
            */

            PopolaMatrice(mat);       // <-- versione con + caselle piene

            //PopolaMatriceRid(mat);     <-- caselle piene limitate

            int ripeti = -1;
            do
            {
                StampaMatrice(mat);
                Console.Write("\n\n--> ");
                Console.ReadKey();
                EvolviMat(mat);
                Console.Clear();

            } while (ripeti == -1); //si ripete all'infinito, modificare con numero diverso xspecificare il numero di iterazioni

            Console.ReadKey();
        }
    }
}

