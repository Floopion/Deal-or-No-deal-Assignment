using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;

namespace DealorNoDeal
{
    class Program
    {
        public struct Detail                                        //This struct holds the contestant details that the txt file will be read into.
        {
            public string last;
            public string first;
            public string hobby;
        }

        public struct Object                                        //This is for my Game objects, array slot dictates the case, one boolean is to tell if the case has opened the othe is to tell if it has been chosen on the board. 
        {
            public bool caseOpened;
            public int number;
            public bool boredChosen;
            public int board;
        }

        static void Main(string[] args)                            //Main method is what my menu system is run out of. 
        {

            Detail[] detailList = new Detail[20];                  //New array of typr 'Detail' to hold contestant details.

            bool exit = false,gen10 = false,playgame=false;

            string tmp;
            int choice;

            ReadFile(ref detailList);                              //Run the method to read the text file into the struct for the contestants. Pass in the Array

            do                                                     //Do while loop for my menu structure, if 0 i entered the bool is switched to true and the program will exit. 
            {

                Console.ForegroundColor = ConsoleColor.Yellow;                            //Change Title Color to Yellow  
                Console.WriteLine("   __________________________________________________________________________________________________________________");
                Console.WriteLine("  /                                                                                                                  \\");
                Console.WriteLine("  | ██████╗ ███████╗ █████╗ ██╗          ██████╗ ██████╗     ███╗   ██╗ ██████╗     ██████╗ ███████╗ █████╗ ██╗      |");
                Console.WriteLine("  | ██╔══██╗██╔════╝██╔══██╗██║         ██╔═══██╗██╔══██╗    ████╗  ██║██╔═══██╗    ██╔══██╗██╔════╝██╔══██╗██║      |");
                Console.WriteLine("  | ██║  ██║█████╗  ███████║██║         ██║   ██║██████╔╝    ██╔██╗ ██║██║   ██║    ██║  ██║█████╗  ███████║██║      |");
                Console.WriteLine("  | ██║  ██║██╔══╝  ██╔══██║██║         ██║   ██║██╔══██╗    ██║╚██╗██║██║   ██║    ██║  ██║██╔══╝  ██╔══██║██║      |");
                Console.WriteLine("  | ██████╔╝███████╗██║  ██║███████╗    ╚██████╔╝██║  ██║    ██║ ╚████║╚██████╔╝    ██████╔╝███████╗██║  ██║███████╗ |");
                Console.WriteLine("  | ╚═════╝ ╚══════╝╚═╝  ╚═╝╚══════╝     ╚═════╝ ╚═╝  ╚═╝    ╚═╝  ╚═══╝ ╚═════╝     ╚═════╝ ╚══════╝╚═╝  ╚═╝╚══════╝ |");
                Console.WriteLine("  \\__________________________________________________________________________________________________________________/");
                Console.WriteLine(" ");
                Console.ResetColor();                                                     //Reset text color

                Console.WriteLine("\n\tWelcome to the main menu!\n\n");                   //Menu Options 
                Console.WriteLine("\t1\t List all of the Contestants");
                Console.WriteLine("\t2\t Find and update a player");
                Console.WriteLine("\t3\t Generate ten finalists");
                Console.WriteLine("\t4\t Choose a contestant to play the game");
                Console.WriteLine("\t5\t Start Deal or No Deal");
                Console.WriteLine("\t0\t Exit\n\n");
                Console.Write("\tSelection: ");

                tmp = Console.ReadLine();
                choice = Convert.ToInt32(tmp);

                switch (choice)
                {
                    case 1:                                                             //If user input is '1' clear screen and run 'Show' method Which shows all the contestants that have been read in.  
                        Console.Clear();
                        Show(ref detailList);
                        Console.Clear();
                        break;

                    case 2:
                        Console.Clear();
                        Search(ref detailList);
                        Console.Clear();
                        break;

                    case 3:                                                            //If user input is '3' clear screen and run 'Generate10' method which will generate 10 random contestants.  
                        Console.Clear();
                        Generate10(ref detailList);
                        gen10 = true;
                        Console.Clear();
                        break;

                    case 4:                                                            //If user input is '4' clear screen and run 'ChoosePlayer' method which will chose the final contestant.
                        
                        if (gen10 == true)
                        {
                            Console.Clear();
                            ChoosePlayer();
                            playgame = true;
                            Console.Clear();
                        }
                        
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("\n \tYou must generate finalists before you can pick a contestant to play!");
                            Console.ForegroundColor = ConsoleColor.White; 
                            Thread.Sleep(2000);
                            Console.Clear();
                        }
                        break;

                    case 5:                                                            //If user input is '1' clear screen and run 'PlayGame' method, it runs the game.   

                        if (playgame == true)
                        {
                            Console.Clear();
                            PlayGame();
                            Console.Clear();
                        }

                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("\n \tYou must Pick a finalist before you can start the game!");
                            Console.ForegroundColor = ConsoleColor.White;
                            Thread.Sleep(2000);
                            Console.Clear();
                        }
                        break;

                    case 0:
                        exit = true;                                                  //If input is 0 do Exit changes to true
                        break;

                    default:                                                          //If input is none of these options, show error message in red text.
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\n\t{0} IS NOT A VALID MENU OPTION!", choice);
                        Thread.Sleep(1000);
                        Console.ResetColor();
                        Console.Clear();
                        break;
                }


            } while (exit == false);
        }

        public static void ReadFile(ref Detail[] position)                              //Method for reading in the contestants from a text file.
        {
            StreamReader sr = new StreamReader(@"DealOrNoDeal.txt");

            for (int i = 0; i < position.Length; i++)                                  //For loop that will read in each line of txt and stick it in the proper array slot. 
            {
                position[i].last = sr.ReadLine();
                position[i].first = sr.ReadLine();
                position[i].hobby = sr.ReadLine();
            }
            sr.Close();

            for (int pos = 0; pos < position.Length - 1; pos++)                        //For loop to check that the names are in alphabetical order via bubble sort. 
            {
                if (position[pos + 1].last.CompareTo(position[pos].last) < 0)
                {
                    Bubble(ref position[pos + 1], ref position[pos]);                  //If the name is not in the right order run the method to swap them around. 
                }
            }
        }

        public static void Show(ref Detail[] position)                                 //Method to show all the contestants. 
        {
            Console.ForegroundColor = ConsoleColor.DarkMagenta;                        //Change Title Color to Yellow  
            Console.WriteLine("   _____________________________________________________________________________________________________ ");
            Console.WriteLine("  /                                                                                                    \\");
            Console.WriteLine("  |  ██████╗ ██████╗ ███╗   ██╗████████╗███████╗███████╗████████╗ █████╗ ███╗   ██╗████████╗███████╗    |");
            Console.WriteLine("  | ██╔════╝██╔═══██╗████╗  ██║╚══██╔══╝██╔════╝██╔════╝╚══██╔══╝██╔══██╗████╗  ██║╚══██╔══╝██╔════╝    |");
            Console.WriteLine("  | ██║     ██║   ██║██╔██╗ ██║   ██║   █████╗  ███████╗   ██║   ███████║██╔██╗ ██║   ██║   ███████╗    |");
            Console.WriteLine("  | ██║     ██║   ██║██║╚██╗██║   ██║   ██╔══╝  ╚════██║   ██║   ██╔══██║██║╚██╗██║   ██║   ╚════██║    |");
            Console.WriteLine("  | ╚██████╗╚██████╔╝██║ ╚████║   ██║   ███████╗███████║   ██║   ██║  ██║██║ ╚████║   ██║   ███████║    |");
            Console.WriteLine("  |  ╚═════╝ ╚═════╝ ╚═╝  ╚═══╝   ╚═╝   ╚══════╝╚══════╝   ╚═╝   ╚═╝  ╚═╝╚═╝  ╚═══╝   ╚═╝   ╚══════╝    |");
            Console.WriteLine("  \\___________________________________________________________________________________________________ /");
            Console.WriteLine(" ");
            Console.ResetColor();

            for (int i = 0; i < position.Length; i++)                                 //For loop that itterates through the array using padding to lay it out attractivley. 
            {
                Console.WriteLine("\t{0}\t{1}\t{2}", position[i].last.PadRight(15), position[i].first.PadRight(15), position[i].hobby);
            }

            Console.WriteLine("\nPress any key to exit");
            Console.ReadLine();
        }

        public static void Search(ref Detail[] search)                                //Method for finding and updating a contestant 
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;                            //Change Title Color to Yellow  
            Console.WriteLine("\t   ___________________________________________________ ");
            Console.WriteLine("\t  /                                                   \\");
            Console.WriteLine("\t  | ██╗   ██╗██████╗ ██████╗  █████╗ ████████╗███████╗|");
            Console.WriteLine("\t  | ██║   ██║██╔══██╗██╔══██╗██╔══██╗╚══██╔══╝██╔════╝|");
            Console.WriteLine("\t  | ██║   ██║██████╔╝██║  ██║███████║   ██║   █████╗  |");
            Console.WriteLine("\t  | ██║   ██║██╔═══╝ ██║  ██║██╔══██║   ██║   ██╔══╝  |");
            Console.WriteLine("\t  | ╚██████╔╝██║     ██████╔╝██║  ██║   ██║   ███████╗|");
            Console.WriteLine("\t  |  ╚═════╝ ╚═╝     ╚═════╝ ╚═╝  ╚═╝   ╚═╝   ╚══════╝|");
            Console.WriteLine("\t  \\___________________________________________________/");
            Console.WriteLine(" ");
            Console.WriteLine("");
            Console.ResetColor();                                                     //Reset text color

            StreamReader sr = new StreamReader(@"DealOrNoDeal.txt");
            bool found = false, again = true;

            string wanted, newlast, newhobby, newBrand, tmp;
            Char tmp2;
            do
            {

                for (int i = 0; i < search.Length; i++)                               //Print list of contestants out again.
                {
                    Console.WriteLine("\t{0}\t{1}\t{2}", search[i].last.PadRight(15), search[i].first.PadRight(15), search[i].hobby.PadRight(15));
                }
                Console.Write("\nWhich Contestant would you like to update? (Please enter their");                 //Ask for last name adding colour.
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(" Last Name ");
                Console.ResetColor();
                Console.Write("): ");
                wanted = Console.ReadLine();

                for (int i = 0; i < search.Length; i++)                             //For loop for re writing back to the file, withes the first & last names and hobby on sepearate lines as not to mess up reading.  
                {
                    if (search[i].last == wanted)                                   // if statement to check if the name matches one in the list. 
                    {
                        sr.Close();
                        StreamWriter sw = new StreamWriter(@"DealOrNoDeal.txt");

                        Console.WriteLine("The current details for this contestant are:\n\n  {0}\t{1}\t{2}", search[i].last, search[i].first, search[i].hobby);

                        Console.WriteLine("\n\nWhat would you like the new Surname to be?:");
                        newlast = Console.ReadLine();
                        search[i].last = newlast;

                        Console.WriteLine("\n\nWhat would you like the first name to be?:");
                        newBrand = Console.ReadLine();
                        search[i].first = newBrand;

                        Console.WriteLine("\n\nWhat would you like the new hobby to be?:");
                        newhobby = Console.ReadLine();
                        search[i].hobby = newhobby;

                        Console.WriteLine("\n\nEntry has been changed!");
                        Thread.Sleep(700);

                        for (int j = 0; j < search.Length; j++)                    //Writes back to the file.
                        {
                            sw.WriteLine(search[j].last);
                            sw.WriteLine(search[j].first);
                            sw.WriteLine(search[j].hobby);
                        }

                        sw.Close();                                                //Close stream Writer and change boolean. 
                        found = true;
                    }
                }

                if (found == true)                                                 //Allows you to get out of the while loop if you have found an entry. 
                {
                    again = false;
                }

                if (found == false)                                                //If an entry is not found then display an error message and ask if user wants to try again. 
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n   Error: Entry Not Found!");
                    Console.ResetColor();
                    Console.Write("\nWould you like to try again?");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(" Y/N: ");
                    Console.ResetColor();
                    tmp = Console.ReadLine();
                    tmp2 = Convert.ToChar(tmp.ToUpper().Substring(0, 1));

                    if (tmp2 == 'N')                                               //If the user answers No change boolean to allow exit of while loop. 
                    {
                        again = false;
                    }
                    Console.Clear();
                }
                sr.Close();
            } while (again == true);
        }

        public static void Generate10(ref Detail [] position)
        {
            int[] num = new int[10];
            Random rand = new Random();
            
            for (int i = 0; i < num.Length;i++ )
            {
                int tmp = rand.Next(0, 20);
                int count = 0; 

                while (count <= i)
                {
                    if (tmp == num[count])
                    {
                        count = 0;
                        tmp = rand.Next(0, 20);
                    }

                    else
                    {
                        count = count+1;
                    }
                }

                num[i] = tmp;

            }

            for (int j = 0; j < num.Length; j++)
            {
                Console.WriteLine("{0}\t{1}\t{2}\t{3}",j+1, position[num[j]].last.PadRight(15), position[num[j]].first.PadRight(15), position[num[j]].hobby);
            }
            Console.ReadLine();

        }

        public static void ChoosePlayer()
        {


        }

        public static void Assign(ref Object[] shuffle)                           //Method for shuffling the case values. Starts by initiating random variable and hard coding money and case values into the arrays.
        {
            Random rand = new Random();                                          
            int tmp, num1, num2; ;

            int[] board = new[] { 1, 2, 5, 10, 25, 50, 75, 100, 150, 250, 500, 750, 1000, 1500, 2000, 3000, 5000, 7500, 10000, 25000, 50000, 75000, 100000, 200000, 250000, 400000 };
            int[] tempList = new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25 };

            for (int i = 0; i < shuffle.Length; i++)                            //Reset the values in the struct so that every new game starts properly with no bugs. 
            {
                shuffle[i].board = board[i];
                shuffle[i].caseOpened = false;
                shuffle[i].boredChosen = false;
            }

            for (int i = 0; i < 50; i++)                                        //for loop for shuffling choose a random number
            {
                num1 = rand.Next(0, 26);

                do                                                              //While loop to make sure second random number is not the same as the first. 
                {

                    num2 = rand.Next(0, 26);

                } while (num2 == num1);

                tmp = tempList[num1];                                           //Switch the values at the index of the random numbers. 
                tempList[num1] = tempList[num2];
                tempList[num2] = tmp;
            }

            for (int i = 0; i < shuffle.Length; i++)                            //assign the shuffled values to the struct values.
            {
                shuffle[i].number = board[tempList[i]];
            }
        }

        public static void Bubble(ref Detail second, ref Detail first)         //Method for bubble sorting. recieve two values and swap them.  
        {
            Detail temp;
            temp = second;
            second = first;
            first = temp;
        }

        public static void PlayGame()                                         //Main method for playing the game. 
        {
            int playerCase, casetmp, turn = 1, playerWins = 0;
            string tmp;

            bool takeMoney = false,lastTurn=false;

            Object[] Assets = new Object[26];                                //New Array of struct type 'Object.'

            Assign(ref Assets);

            do                                                              //Do / While to assign the players case, checking that its within the allowed parameters.
            {
                Console.WriteLine("Please Choose a case between 1 and 26:");
                tmp = Console.ReadLine();
                casetmp = Convert.ToInt32(tmp);

                if (casetmp < 1 || casetmp >26)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nError please enter a number between 1 and 26!\n");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("\nPress Enter to Continue");

                    Console.ReadLine();

                }

                Console.Clear();

            } while (casetmp < 1 || casetmp > 26);

            playerCase = Assets[casetmp - 1].number;                        //Change the players chosen case state to 'opened.' 
            Assets[casetmp - 1].caseOpened = true;

            Turn1(ref Assets,casetmp);
            takeMoney = BankOffer(Assets, turn, ref playerWins);

            if (takeMoney == false)                                       //Series of if statments varying in lenght to simulate 'turns.' Checks if the player has chosen 'deal' and if not runs the correct turn and the bank offer method.
            {
                turn = 2;
                Turn2(ref Assets, casetmp);
                takeMoney = BankOffer(Assets, turn, ref playerWins);
            }

            if (takeMoney == false)
            {
                turn = 3;
                Turn3(ref Assets, casetmp);
                takeMoney = BankOffer(Assets, turn, ref playerWins);
            }

            if (takeMoney == false)
            {
                turn = 4;
                Turn4(ref Assets, casetmp);
                takeMoney = BankOffer(Assets, turn, ref playerWins);
            }

            if (takeMoney == false)
            {
                turn = 5;
                Turn5(ref Assets, casetmp);
                takeMoney = BankOffer(Assets, turn, ref playerWins);
            }

            if (takeMoney == false)
            {
                turn = 6;
                Turn6(ref Assets, casetmp);
                takeMoney = BankOffer(Assets, turn, ref playerWins);
            }

            if (takeMoney == false)
            {
                FinalTurn(ref Assets, ref playerWins,playerCase);
                lastTurn = true;
            }

            if (lastTurn == false)                                                            //If its not the final turn, Show how much the player won and what was in their chosen case. 
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("You won : {0:c0}!", playerWins);
                Console.WriteLine("\nYour case contained {0:c0}...", playerCase);
                Console.ReadLine();
            }
        }

        public static bool BankOffer(Object[] Money, int turn, ref int winnings)             //Method to simulate the 'Bank offer' depending on the board state. 
        {
            Console.Clear();
            bool Deal = false;
            int total = 0, offer, average = 0;
            string temp;
            char choice = ' ';

            for (int i = 0; i < Money.Length; i++)                                          //Algorithm for bank offer, adds up the ammount of money left on the board and the average. 
            {
                if (Money[i].boredChosen == false)
                {
                    total = total + Money[i].number;
                    average++;
                }
            }

            offer = ((total / average) * turn) / 10;                                       //Offer is equal to the total divided by the average, times the ammount of turns, divided by 10.

            do
            {
                Console.ForegroundColor = ConsoleColor.White;                              //Display the bank offer and ask player if the want to take the ammount as their prize. 
                Console.WriteLine("The bank offers you ");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("   {0:c0}", offer);
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("\n\nDeal or No Deal? ");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("D / ND : ");
                temp = Console.ReadLine();
                choice = Convert.ToChar(temp.Substring(0, 1).ToUpper());
                Console.ResetColor();

                if (choice != 'D' && choice != 'N')                                         //Check that the input is either D or ND and throw an error message if its not true. 
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n\tERROR!");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("\nPLease enter\n\nD - To take the bank offer\nND - To Keep Playing ");
                    Console.WriteLine("\nPress ANY KEY to continue ...");
                    Console.ReadLine();
                    Console.Clear();
                }

            } while (choice != 'D' && choice != 'N');

            if (choice == 'D')                                                              //If the player takes prize change boolean and assign their "winnings" to a variable.
            {
                Deal = true;
                winnings = offer;
            }

            return Deal;
        }

        public static void Turn1(ref Object[] cases, int casetmp)                           //Series of if statments dictating how many cases are picked per 'turn. 
        {

            for (int i = 0; i < 6; i++)
            {
                TurnSequence(ref cases,casetmp);
            }


        }

        public static void Turn2(ref Object[] cases,int casetmp)
        {
            for (int i = 0; i < 5; i++)
            {
                TurnSequence(ref cases, casetmp);
            }

        }

        public static void Turn3(ref Object[] cases, int casetmp)
        {
            for (int i = 0; i < 4; i++)
            {
                TurnSequence(ref cases, casetmp);
            }

        }

        public static void Turn4(ref Object[] cases, int casetmp)
        {
            for (int i = 0; i < 3; i++)
            {
                TurnSequence(ref cases, casetmp);
            }

        }

        public static void Turn5(ref Object[] cases, int casetmp)
        {
            for (int i = 0; i < 3; i++)
            {
                TurnSequence(ref cases, casetmp);
            }

        }

        public static void Turn6(ref Object[] cases, int casetmp)
        {
            for (int i = 0; i < 3; i++)
            {
                TurnSequence(ref cases, casetmp);
            }

        }

        public static void FinalTurn(ref Object[] cases, ref int winnings,int playerCase)                       //Method for if the layer makes it too the last round.                     
        {
            Console.Clear();

            int finalCase = 0, finalCash = 0;
            string temp;
            char choice = ' ';

            for (int i = 0; i < cases.Length; i++)                                                              //find and assign the final case to a variable. 
            {
                if (cases[i].caseOpened == false)
                {
                    finalCash = cases[i].number;
                    finalCase = i + 1;
                }
            }
            do
            {

                Console.WriteLine("Do you want to take your case or case {0}?", finalCase);                     //Ask player if they would like to stay with their case or take the one left on the board. 
                Console.WriteLine("\n\nPLease enter\n\nS - To stay with your case \nT - To Take case {0}", finalCase);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("S / T : ");
                Console.ResetColor();
                temp = Console.ReadLine();
                choice = Convert.ToChar(temp.Substring(0, 1).ToUpper());
                Console.ResetColor();

                if (choice != 'S' && choice != 'T')                                                             //Check user input is right, throw an error message if it is out of bounds. 
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n\tERROR!");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("\nPLease enter\n\nS - To stay with your case \nT - To Take case {0}", finalCase);
                    Console.WriteLine("\nPress ANY KEY to continue ...");
                    Console.ReadLine();
                    Console.Clear();
                }

            } while (choice != 'S' && choice != 'T');

            if (choice == 'T')                                                                                  //If statments to show the player the value in their chosen case and in the other case. 
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("You won : {0:c0}!", finalCash);
                Console.WriteLine("\nYour case contained {0:c0}...", playerCase);
                Console.ReadLine();
            }

            if (choice == 'S')
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("You won : {0:c0}!", playerCase);
                Console.WriteLine("\nCase {0} contained : {1}!",finalCase,finalCash);
                Console.ReadLine();
            }

        }

        public static void TurnSequence(ref Object[] cases, int casetmp)                          //Method runs everytime the player takes a turn. 
        {
            int choice;
            string tmp;
            Console.Clear();

            do
            {
                do
                {
                    Console.ForegroundColor = ConsoleColor.Green;                            //Change Title Color to Yellow  
                    Console.WriteLine("\t\t   __________________________________________________________________________________________________________________");
                    Console.WriteLine("\t\t  /                                                                                                                  \\");
                    Console.WriteLine("\t\t  | ██████╗ ███████╗ █████╗ ██╗          ██████╗ ██████╗     ███╗   ██╗ ██████╗     ██████╗ ███████╗ █████╗ ██╗      |");
                    Console.WriteLine("\t\t  | ██╔══██╗██╔════╝██╔══██╗██║         ██╔═══██╗██╔══██╗    ████╗  ██║██╔═══██╗    ██╔══██╗██╔════╝██╔══██╗██║      |");
                    Console.WriteLine("\t\t  | ██║  ██║█████╗  ███████║██║         ██║   ██║██████╔╝    ██╔██╗ ██║██║   ██║    ██║  ██║█████╗  ███████║██║      |");
                    Console.WriteLine("\t\t  | ██║  ██║██╔══╝  ██╔══██║██║         ██║   ██║██╔══██╗    ██║╚██╗██║██║   ██║    ██║  ██║██╔══╝  ██╔══██║██║      |");
                    Console.WriteLine("\t\t  | ██████╔╝███████╗██║  ██║███████╗    ╚██████╔╝██║  ██║    ██║ ╚████║╚██████╔╝    ██████╔╝███████╗██║  ██║███████╗ |");
                    Console.WriteLine("\t\t  | ╚═════╝ ╚══════╝╚═╝  ╚═╝╚══════╝     ╚═════╝ ╚═╝  ╚═╝    ╚═╝  ╚═══╝ ╚═════╝     ╚═════╝ ╚══════╝╚═╝  ╚═╝╚══════╝ |");
                    Console.WriteLine("\t\t  \\__________________________________________________________________________________________________________________/");
                    Console.WriteLine(" ");
                    Console.ResetColor();                                                       //Reset text color
                    Console.WriteLine("");
                    Console.WriteLine("");
                    /*
                     * A series of for loops to proceduraly draw each part of the game board line by line.
                     * i,j,l are control variables, i = which slot the bank value is in, l = which case is being drawn, j = which part of the case needs to be drawn. 
                     * Code cycles through each line and anything that is deemed to be 'chosen' is drawn in black so that it 'dissapears' against the console backround. 
                     */
                    int i = 0;
                    int j = 0;
                    int l = 0;
                    string emp = " ";

                    Console.Write("${0:c0}",Money(cases,ref i).PadRight(10));

                    l = 1; 
                    for (int k = 0;k<6;k++)
                    {
                        Console.Write("{0}",CaseDraw(cases,ref l,ref j).PadLeft(20).PadRight(20));
                        j++;
                    }

                    Console.Write("               ${0:c0}\n", Money(cases,ref i));

                    Console.Write("${0}", Money(cases, ref i).PadRight(10));

                    l = 2;
                    j = 0;
                    for (int k = 0; k < 6; k++)
                    {
                        Console.Write("{0}", CaseDraw(cases, ref l, ref j).PadLeft(20).PadRight(20));
                        j++;
                    }

                    Console.Write("               ${0:c0}\n", Money(cases, ref i));

                    Console.Write("${0:c0}", Money(cases, ref i).PadRight(10));

                    l = 3;
                    j = 0;
                    for (int k = 0; k < 6; k++)
                    {
                        Console.Write("{0}", CaseDraw(cases, ref l, ref j).PadLeft(20).PadRight(20));
                        j++;
                    }

                    Console.Write("               ${0:c0}\n", Money(cases, ref i));

                    Console.Write("${0:c0}", Money(cases, ref i).PadRight(10));

                    l = 4;
                    j = 0;
                    for (int k = 0; k < 6; k++)
                    {
                        Console.Write("{0}", CaseDraw(cases, ref l, ref j).PadLeft(20).PadRight(20));
                        j++;
                    }

                    Console.Write("               ${0:c0}\n", Money(cases, ref i));

                    Console.Write("${0:c0}", Money(cases, ref i).PadRight(10));

                    l = 1;
                    j = 6;
                    for (int k = 0; k < 6; k++)
                    {
                        Console.Write("{0}", CaseDraw(cases, ref l, ref j).PadLeft(20).PadRight(20));
                        j++;
                    }

                    Console.Write("               ${0:c0}\n", Money(cases, ref i));

                    Console.Write("${0:c0}", Money(cases, ref i).PadRight(10));

                    l = 2;
                    j = 6;
                    for (int k = 0; k < 6; k++)
                    {
                        Console.Write("{0}", CaseDraw(cases, ref l, ref j).PadLeft(20).PadRight(20));
                        j++;
                    }

                    Console.Write("               ${0:c0}\n", Money(cases, ref i));

                    Console.Write("${0:c0}", Money(cases, ref i).PadRight(10));

                    l = 3;
                    j = 6;
                    for (int k = 0; k < 6; k++)
                    {
                        Console.Write("{0}", CaseDraw(cases, ref l, ref j).PadLeft(20).PadRight(20));
                        j++;
                    }

                    Console.Write("               ${0:c0}\n", Money(cases, ref i));

                    Console.Write("${0:c0}", Money(cases, ref i).PadRight(10));

                    l = 4;
                    j = 6;
                    for (int k = 0; k < 6; k++)
                    {
                        Console.Write("{0}", CaseDraw(cases, ref l, ref j).PadLeft(20).PadRight(20));
                        j++;
                    }
                                      
                    Console.Write("               ${0:c0}\n", Money(cases, ref i));

                    Console.Write("${0:c0}", Money(cases, ref i).PadRight(10));

                    l = 1;
                    j = 12;
                    for (int k = 0; k < 6; k++)
                    {
                        Console.Write("{0}", CaseDraw(cases, ref l, ref j).PadLeft(20).PadRight(20));
                        j++;
                    }

                    Console.Write("               ${0:c0}\n", Money(cases, ref i));

                    Console.Write("${0:c0}", Money(cases, ref i).PadRight(10));

                    l = 2;
                    j = 12;
                    for (int k = 0; k < 6; k++)
                    {
                        Console.Write("{0}", CaseDraw(cases, ref l, ref j).PadLeft(20).PadRight(20));
                        j++;
                    }

                    Console.Write("               ${0:c0}\n", Money(cases, ref i));

                    Console.Write("${0:c0}", Money(cases, ref i).PadRight(10));

                    l = 3;
                    j = 12;
                    for (int k = 0; k < 6; k++)
                    {
                        Console.Write("{0}", CaseDraw(cases, ref l, ref j).PadLeft(20).PadRight(20));
                        j++;
                    }

                    Console.Write("               ${0:c0}\n", Money(cases, ref i));

                    Console.Write("${0:c0}", Money(cases, ref i).PadRight(10));

                    l = 4;
                    j = 12;
                    for (int k = 0; k < 6; k++)
                    {
                        Console.Write("{0}", CaseDraw(cases, ref l, ref j).PadLeft(20).PadRight(20));
                        j++;
                    }

                    Console.Write("               ${0:c0}\n", Money(cases, ref i));

                    Console.Write("${0:c0}", Money(cases, ref i).PadRight(10));

                    l = 1;
                    j = 18;
                    for (int k = 0; k < 6; k++)
                    {
                        Console.Write("{0}", CaseDraw(cases, ref l, ref j).PadLeft(20).PadRight(20));
                        j++;
                    }

                    Console.Write("               ${0:c0}\n", Money(cases, ref i));

                    Console.Write(" {0:c0}", emp.PadRight(10));

                    l = 2;
                    j = 18;
                    for (int k = 0; k < 6; k++)
                    {
                        Console.Write("{0}", CaseDraw(cases, ref l, ref j).PadLeft(20).PadRight(20));
                        j++;
                    }

                    Console.Write("{0:c0}\n", emp.PadLeft(20));

                    Console.Write(" {0:c0}", emp.PadRight(10));

                    l = 3;
                    j = 18;
                    for (int k = 0; k < 6; k++)
                    {
                        Console.Write("{0}", CaseDraw(cases, ref l, ref j).PadLeft(20).PadRight(20));
                        j++;
                    }

                    Console.Write("{0:c0}\n", emp.PadLeft(20));

                    Console.Write(" {0:c0}", emp.PadRight(10));

                    l = 4;
                    j = 18;
                    for (int k = 0; k < 6; k++)
                    {
                        Console.Write("{0}", CaseDraw(cases, ref l, ref j).PadLeft(20).PadRight(20));
                        j++;
                    }

                    Console.Write("{0:c0}\n", emp.PadLeft(20));

                    Console.Write(" {0:c0}", emp.PadRight(50));

                    l = 1;
                    j = 24;
                    for (int k = 0; k < 2; k++)
                    {
                        Console.Write("{0}", CaseDraw(cases, ref l, ref j).PadLeft(20).PadRight(20));
                        j++;
                    }

                    Console.Write("{0:c0}\n", emp.PadLeft(20));

                    Console.Write(" {0:c0}", emp.PadRight(50));

                    l = 2;
                    j = 24;
                    for (int k = 0; k < 2; k++)
                    {
                        Console.Write("{0}", CaseDraw(cases, ref l, ref j).PadLeft(20).PadRight(20));
                        j++;
                    }

                    Console.Write("{0:c0}\n", emp.PadLeft(20));

                    Console.Write(" {0:c0}", emp.PadRight(50));

                    l = 3;
                    j = 24;
                    for (int k = 0; k < 2; k++)
                    {
                        Console.Write("{0}", CaseDraw(cases, ref l, ref j).PadLeft(20).PadRight(20));
                        j++;
                    }

                    Console.Write("{0:c0}\n", emp.PadLeft(20));

                    Console.Write(" {0:c0}", emp.PadRight(50));

                    l = 4;
                    j = 24;
                    for (int k = 0; k < 2; k++)
                    {
                        Console.Write("{0}", CaseDraw(cases, ref l, ref j).PadLeft(20).PadRight(20));
                        j++;
                    }

                    Console.Write("{0:c0}\n", emp.PadLeft(20));

                    Console.ForegroundColor = ConsoleColor.White;                               //Draw line to distinguish board and player options.
                    Console.WriteLine("\n\n+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");
                    Console.WriteLine("\nYour case = {0}",casetmp);

                    Console.WriteLine("\nPick the next case:");
                    tmp = Console.ReadLine();
                    choice = Convert.ToInt32(tmp);

                    if (choice < 1 || choice > 26)                                              //Statement to check if the player choice is out of range of the case numbers.
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\n    {0} is not a valid choice! Please pick a case between 1 and 26", choice);
                        Console.ReadLine();
                        Console.Clear();
                        Console.ResetColor();
                    }

                } while (choice < 1 || choice > 26);

                if (cases[choice - 1].caseOpened == true)                                        //Statement to check if the player choice is an already chosen case.
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n    Case {0} Has already been chosen! Please pick an availible case!", choice);
                    Console.ReadLine();
                    Console.Clear();
                    Console.ResetColor();
                }

            } while (cases[choice - 1].caseOpened == true);

            cases[choice - 1].caseOpened = true;                                                 //Change the state of the players chosen case and money value to 'opened' and 'chosen' via booleans.
            cases[choice - 1].boredChosen = true;

            Console.Write("\nYou picked Case" );                                                 //Display what the player has chosen. 
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("\t{0}", choice);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("\nIt contains ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("\t{0:c0}!", cases[choice - 1].number);
            Console.ForegroundColor = ConsoleColor.White;
            Console.ReadLine();
            Console.Clear();
        }

        public static string Money(Object [] cases, ref int i)                                  //Check the if the 'money' at slot i has been chosen. Draw in colour if 'no', or in black if 'yes.'
        {
            string temp = " ";
            if (cases[i].boredChosen == false)
            {
                Console.ForegroundColor = ConsoleColor.White;
                temp = Convert.ToString(cases[i].number);
            }

            if (cases[i].boredChosen == true)
            {
                Console.ForegroundColor = ConsoleColor.Black;
                temp = Convert.ToString(cases[i].number);
            }

            i++;                                                                                //incirment the value of i.

            return temp;                                                                        //Return the value.
        
        }

        public static string CaseDraw(Object[] cases, ref int l,ref int j)                      //Method for drawing the cases line by line. Colour if not yet chose, blank if chosen.
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            string temp = " ";
            if (cases[j].caseOpened == false)
            {
                switch (l)                                                                      //Switch statment based of the control variable 'l' to decide which part of the case to draw.
                {
                    case 1:
                        temp = "_______";
                        break;

                    case 2:
                        temp = "|     |";
                        break;

                    case 3:
                        if (j > 8)
                        {
                            temp = $"|  {j + 1} |";                                             //Write the case number with the control variable 'j'.
                            break;
                        }

                        else
                        {
                            temp = $"|  {j + 1}  |";                                            //Re-adjust drawing if number is above '9' so all values line up nicely.
                            break;
                        }

                    case 4:
                        temp = "|_____|";
                        break;
                }
            }

            if (cases[j].caseOpened == true)                                                    //Draw blank if case has been chosen. 
            {
                temp = "       ";
            }

                return temp;

        }
    }
    
}
