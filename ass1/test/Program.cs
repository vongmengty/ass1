using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;

namespace test
{
    class Program
    {
        public static string[,] arr = new string[100, 9];
        public static string[,] newarr = new string[100, 9];
        public static ConsoleHelper help = new ConsoleHelper();
        public static ConsoleKeyInfo pressKey;
        public static char[] number = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
        static void Main(string[] args)
        {
            int RowView = 1, notdo = 0; //select one row in table to display(start form 1 to row)
            int row = 0;    // row of data(start form 1 to row)
            Int16 rowTable = 1; // select row in table for edit or delete

            try
            {
                StreamReader Sreader = new StreamReader("data.txt");
                if (File.Exists("data.txt"))
                {
                    while (Sreader.Peek() != -1)
                    {
                        row++;
                        string[] re = Sreader.ReadLine().Split(',');
                        if (re[0] == "$")
                        {
                            arr[row, 0] = re[1];
                            arr[row, 1] = re[2];
                            arr[row, 2] = re[3];
                            arr[row, 3] = re[4];
                            arr[row, 4] = re[5];
                            arr[row, 5] = re[6];
                            arr[row, 6] = re[7];
                            arr[row, 7] = re[8];
                            arr[row, 8] = re[0];
                        }
                    }
                }
                Sreader.Close();
            }
            catch (IOException exc)
            {
                help.gotoxy(1, 1);
                Console.Write("Not have file");
            }
            pressKey = new ConsoleKeyInfo();            
            do
            {
                Console.Clear();       
                help.DisplayMenu();
                notdo = 0;
                pressKey = Console.ReadKey();               
                if (pressKey.Key==ConsoleKey.F)
                {
                    do
                    {
                        row++;
                        Console.Clear();
                        AddFunction(row);
                        help.gotoxy(30, 20);
                        Console.Write("Do you want to add more(Y/N):");
                        pressKey = Console.ReadKey();
                    } while (pressKey.Key != ConsoleKey.N);
                }
                else if(pressKey.Key==ConsoleKey.S)
                {
                    string SearchItem ;
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    help.gotoxy(40, 13);
                    Console.Write("██████████████████████████");
                    help.gotoxy(40, 17);
                    Console.Write("██████████████████████████");
                    Console.ResetColor();
                    help.gotoxy(40, 15);
                    Console.Write("Find :");

                    help.gotoxy(46,15);
                    SearchItem = Console.ReadLine();
                    Console.Clear();
                    for (int i = 1; i <= row; i++)
                    {
                        if (SearchItem == arr[i, 0])
                        {
                            DisplayCostings(i); // DisplayCostings function                                                     
                        }                   
                    }
                    int reseach = 0;
                    do
                    {
                        pressKey = Console.ReadKey();
                        if (pressKey.Key == ConsoleKey.Escape)
                            reseach = 1;
                    } while (reseach != 1);
                }
                else if(pressKey.Key == ConsoleKey.E)
                {
                    Console.Clear();
                    for (int i = 1; i <= row; i++)
                    {
                        help.viewtable();   //form design
                        ViewTable(i);   //show data in table(design)
                    }
                    help.gotoxy(3, short.Parse((rowTable + 2).ToString()));
                    Console.Write("=>");
                    do
                    {
                        pressKey = Console.ReadKey();
                        if (pressKey.Key == ConsoleKey.UpArrow)
                        {
                            Console.Clear();
                            if (rowTable > 1)
                                rowTable--;

                            for (int i = 1; i <= row; i++)
                            {
                                help.viewtable();   //form design
                                ViewTable(i);   //show data in table(design)
                            }
                            help.gotoxy(3, short.Parse((rowTable + 2).ToString()));
                            Console.Write("=>");
                            continue;
                        }
                        else if (pressKey.Key == ConsoleKey.DownArrow)
                        {
                            Console.Clear();
                            if (rowTable < row+1)
                                rowTable++;

                            for (int i = 1; i <= row; i++)
                            {
                                help.viewtable();   //form design
                                ViewTable(i);   //show data in table(design)
                            }
                            help.gotoxy(3, short.Parse((rowTable + 2).ToString()));
                            Console.Write("=>");
                            continue;
                        }
                        else if (pressKey.Key == ConsoleKey.Enter)
                        {
                            Console.Clear();
                            EditFunction(rowTable);
                            StreamWriter Swrite = new StreamWriter("data.txt");
                            for(int i = 1; i <= row; i++)
                            {
                                Swrite.WriteLine(arr[i,8]+","+arr[i, 0]+","+ arr[i, 1]+","+ arr[i, 2]+","+ arr[i, 3] + "," + arr[i, 4] + "," + arr[i, 5] + "," + arr[i, 6] + "," + arr[i, 7]);
                            }
                            Swrite.Close();
                            Console.Clear();
                            for (int i = 1; i <= row; i++)
                            {
                                help.viewtable();   //form design
                                ViewTable(i);   //show data in table(design)
                            }
                            help.gotoxy(3, short.Parse((rowTable + 2).ToString()));
                            Console.Write("=>");
                            continue;
                        }
                        help.btnOK(" Backspace   ", ConsoleColor.Red, 5, 26);
                        Thread.Sleep(200);     //delay 0.2S
                        help.btnOK("  Backspace  ", ConsoleColor.Blue, 5, 26);
                        Thread.Sleep(200);     //delay 0.2S
                    } while (pressKey.Key != ConsoleKey.Backspace);
                    
                }
                else if(pressKey.Key == ConsoleKey.D)
                {
                    Console.Clear();
                    for (int i = 1; i <= row; i++)
                    {
                        help.viewtable();   //form design
                        ViewTable(i);   //show data in table(design)
                    }
                    help.gotoxy(3, short.Parse((rowTable + 2).ToString()));
                    Console.Write("=>");
                    do
                    {
                        pressKey = Console.ReadKey();
                        if (pressKey.Key == ConsoleKey.UpArrow)
                        {
                            Console.Clear();
                            if (rowTable > 1)
                                rowTable--;

                            for (int i = 1; i <= row; i++)
                            {
                                help.viewtable();   //form design
                                ViewTable(i);   //show data in table(design)
                            }
                            help.gotoxy(3, short.Parse((rowTable + 2).ToString()));
                            Console.Write("=>");
                            continue;
                        }
                        else if (pressKey.Key == ConsoleKey.DownArrow)
                        {
                            Console.Clear();
                            if (rowTable < row + 1)
                                rowTable++;

                            for (int i = 1; i <= row; i++)
                            {
                                help.viewtable();   //form design
                                ViewTable(i);   //show data in table(design)
                            }
                            help.gotoxy(3, short.Parse((rowTable + 2).ToString()));
                            Console.Write("=>");
                            continue;
                        }
                        else if (pressKey.Key == ConsoleKey.Enter)
                        {
                            Console.Clear();
                            DeleteFunction(rowTable,row);  // delete function                          
                            row--;
                            StreamWriter Swrite = new StreamWriter("data.txt");
                            for (int i = 1; i <= row; i++)
                            {
                                Swrite.WriteLine(arr[i, 8] + "," + arr[i, 0] + "," + arr[i, 1] + "," + arr[i, 2] + "," + arr[i, 3] + "," + arr[i, 4] + "," + arr[i, 5] + "," + arr[i, 6] + "," + arr[i, 7]);
                            }
                            Swrite.Close();
                            Console.Clear();
                            for (int i = 1; i <= row; i++)
                            {
                                help.viewtable();   //form design
                                ViewTable(i);   //show data in table(design)
                            }
                            rowTable--;
                            help.gotoxy(3, short.Parse((rowTable + 2).ToString()));
                            Console.Write("=>");                           
                            continue;
                        }
                        help.btnOK(" Backspace   ", ConsoleColor.Red, 5, 26);
                        Thread.Sleep(200);     //delay 0.2S
                        help.btnOK("  Backspace  ", ConsoleColor.Blue, 5, 26);
                        Thread.Sleep(200);     //delay 0.2S
                    } while (pressKey.Key != ConsoleKey.Backspace);
                }
                else if(pressKey.Key == ConsoleKey.C)
                {
                    do
                    {
                        if (row <= 0)
                        {
                            Console.Clear();
                            help.ShowTitleView();
                            help.NotFound();
                            help.loadTable();
                            Thread.Sleep(2000);     //delay 2S
                            break;
                        }
                        if (notdo == 0)
                        {
                            Console.Clear();
                            DisplayCostings(RowView);
                            notdo = 1;
                        }
                        pressKey = Console.ReadKey();
                        if (pressKey.Key == ConsoleKey.LeftArrow || pressKey.Key == ConsoleKey.RightArrow || pressKey.Key == ConsoleKey.Enter)
                        {
                            if (pressKey.Key == ConsoleKey.LeftArrow && RowView > 1) //back data
                            {
                                RowView--;
                                help.btnOK("      <--    ", ConsoleColor.Red, 10, 25);
                                Thread.Sleep(200);     //delay 0.2S
                                help.btnOK("     <--     ", ConsoleColor.Blue, 10, 25);
                                Thread.Sleep(200);     //delay 0.2S
                            }
                            else if (pressKey.Key == ConsoleKey.RightArrow && RowView < row) //next data
                            {
                                RowView++;
                                help.btnOK("    -->      ", ConsoleColor.Red, 78, 25);
                                Thread.Sleep(200);     //delay 0.2S
                                help.btnOK("     -->     ", ConsoleColor.Blue, 78, 25);
                                Thread.Sleep(200);     //delay 0.2S
                            }
                            else if (pressKey.Key == ConsoleKey.Enter) // show all data in table
                            {
                                help.btnOK("   Enter     ", ConsoleColor.Red, 40, 25);
                                Thread.Sleep(200);     //delay 0.2S
                                help.btnOK("    Enter    ", ConsoleColor.Blue, 40, 25);
                                Thread.Sleep(200);     //delay 0.2S
                                Console.Clear();
                                for (int i = 1; i <= row; i++)
                                {
                                    help.viewtable();   //form design
                                    ViewTable(i);   //show data in table(design)
                                }
                                pressKey = Console.ReadKey();
                                if (pressKey.Key == ConsoleKey.Backspace)
                                {
                                    help.btnOK(" Backspace   ", ConsoleColor.Red, 5, 26);
                                    Thread.Sleep(200);     //delay 0.2S
                                    help.btnOK("  Backspace  ", ConsoleColor.Blue, 5, 26);
                                    Thread.Sleep(200);     //delay 0.2S
                                    Console.Clear();
                                    notdo = 0;
                                    continue;  // go to Display's detail
                                }
                            }
                            Console.Clear();
                            DisplayCostings(RowView);
                        }
                    } while (pressKey.Key != ConsoleKey.Escape);
                }               
            } while (pressKey.Key != ConsoleKey.X);
        }
        static void AddFunction(int row)
        {
            string year, month, day;
            help.add(); //design form for add
            help.ShowTitleAdd();
            help.gotoxy(62, 7);            
            arr[row, 0] = Console.ReadLine();
            help.gotoxy(62, 8);
            arr[row, 1] = Console.ReadLine();
            help.gotoxy(62, 9);
            arr[row, 2] = Console.ReadLine();
            help.gotoxy(62, 10);
            arr[row, 3] = Console.ReadLine();
            help.gotoxy(62, 15);
            arr[row, 4] = Console.ReadLine();
            help.gotoxy(62, 17);
            y:
            year = Console.ReadLine();
            if (int.Parse(year) > 2015)
                arr[row, 5] = year;
            else
            {
                help.gotoxy(62, 17);
                Console.Write("\t\t");
                help.gotoxy(62, 17);
                goto y;
            }

            help.gotoxy(62, 18);
            m:
            month= Console.ReadLine();
            if (int.Parse(month) > 0 && int.Parse(month) <= 12)
                arr[row, 6] = month;
            else
            {
                help.gotoxy(62, 18);
                Console.Write("\t\t");
                help.gotoxy(62, 18);
                goto m;
            }
            help.gotoxy(62, 19);
            d:
            day= Console.ReadLine();
            if (int.Parse(day) <= int.Parse(help.day(day, month, year)))
                arr[row, 7] = day;
            else
            {
                help.gotoxy(62, 19);
                Console.Write("\t\t");
                help.gotoxy(62, 19);
                goto d;
            }
            string p = "$"+","+arr[row, 0] + "," + arr[row, 1] + "," + arr[row, 2] + "," + arr[row, 3] + "," + arr[row, 4] + "," + arr[row, 5] + "," + arr[row, 6] + "," + arr[row, 7];
            FileStream fappend = new FileStream("data.txt", FileMode.Append, FileAccess.Write);
            StreamWriter Swriter = new StreamWriter(fappend);
            Swriter.WriteLine(p);
            Swriter.Close();
        }
        static void DisplayCostings(int row)
        {
            string name, address, phone, NumOfGuest, date, MealType;
            int service;
            float MealCost, TotalCost, GST, total;

            name = arr[row, 0].Trim();
            address = arr[row, 1].Trim();
            phone = arr[row, 2].Trim();
            NumOfGuest = arr[row, 3].Trim();
            date = (arr[row, 7].Trim() + " " + help.month(arr[row, 6].Trim()) + " " + arr[row, 5].Trim());
            MealType = help.mealtype(arr[row, 4].Trim());
            MealCost = help.mealcost(arr[row, 4].Trim())*int.Parse(NumOfGuest);
            service = help.staff(NumOfGuest);
            TotalCost = MealCost + service;
            GST = TotalCost * 0.1f;
            total = TotalCost + GST;

            help.view();    // design form for view(1 row)
            help.gotoxy(63, 7);
            Console.Write(name);
            help.gotoxy(63, 8);
            Console.Write(address);
            help.gotoxy(63, 9);
            Console.Write(phone);
            help.gotoxy(63, 10);
            Console.Write(NumOfGuest);
            help.gotoxy(63, 11);
            Console.Write(date);
            help.gotoxy(63, 12);
            Console.Write(MealType);
            help.gotoxy(63, 13);
            Console.Write(MealCost);
            help.gotoxy(66, 14);
            Console.Write(service);
            help.gotoxy(66, 15);
            Console.Write(TotalCost);
            help.gotoxy(66, 16);
            Console.Write(GST);
            help.gotoxy(66, 18);
            Console.Write(total);
            
        }
        static void ViewTable(int row)
        {
            string name, address, phone, guest, MealType, date;
            float MealCost;
            name = arr[row, 0].Trim();
            address = arr[row, 1].Trim();
            phone = arr[row, 2].Trim();
            guest = arr[row, 3].Trim();
            MealType = help.mealtype(arr[row, 4].Trim());
            MealCost = help.mealcost(arr[row, 4].Trim()) * int.Parse(guest);
            date = (arr[row, 7].Trim() + " " + help.month(arr[row, 6].Trim()) + " " + arr[row, 5].Trim());           
            help.gotoxy(7,Int16.Parse((row+2).ToString()));
            Console.Write(name);
            help.gotoxy(20, Int16.Parse((row + 2).ToString()));
            Console.Write(address);
            help.gotoxy(40, Int16.Parse((row + 2).ToString()));
            Console.Write(phone);
            help.gotoxy(55, Int16.Parse((row + 2).ToString()));
            Console.Write(guest);
            help.gotoxy(63, Int16.Parse((row + 2).ToString()));
            Console.Write(MealType);
            help.gotoxy(75, Int16.Parse((row + 2).ToString()));
            Console.Write(MealCost);
            help.gotoxy(87, Int16.Parse((row + 2).ToString()));
            Console.Write(date);
        }
        static void EditFunction(int row)
        {                      
            string update = "";
            help.update();  //design form for update
            help.gotoxy(67,10);
            Console.Write(arr[row,0]);
            help.gotoxy(67, 12);
            Console.Write(arr[row,1]);
            help.gotoxy(67, 14);
            Console.Write(arr[row, 2]);
            help.gotoxy(67, 16);
            Console.Write(arr[row, 3]);
            help.gotoxy(67, 18);
            Console.Write(arr[row,7]+" "+help.month(arr[row,6])+" "+arr[row,5]);
            help.gotoxy(67, 20);
            Console.Write(arr[row, 4]);

            RowUpdate(update, 10, row, 0);
            RowUpdate(update, 12, row, 1);
            RowUpdate(update, 14, row, 2);
            RowUpdate(update, 16, row, 3);

            help.gotoxy(67, 19);
            update = Console.ReadLine();
            if (update.Trim() == "y" || update.Trim() == "Y")
            {
                help.gotoxy(67, 18);
                Console.Write("\t\t   ");                
                help.gotoxy(69, 18);
                Console.Write("/");               
                help.gotoxy(72, 18);
                Console.Write("/");
                help.gotoxy(67, 18);
                arr[row, 7] = Console.ReadLine();
                help.gotoxy(70, 18);
                string []s = Console.ReadLine().Split('0');
                if(s[0]==string.Empty)
                    arr[row, 6] = s[1];
                else
                    arr[row, 6] = s[0];
                Console.Write(arr[row, 6]);
                help.gotoxy(73, 18);
                arr[row,5] = Console.ReadLine();
            }
            RowUpdate(update, 20, row, 4);
            pressKey = Console.ReadKey();
            if (pressKey.Key == ConsoleKey.Enter)
            {
                help.btnOK("    Enter    ", ConsoleColor.Red, 78, 25);
                Thread.Sleep(200);     //0.2S
                help.btnOK("    Enter    ", ConsoleColor.Blue, 78, 25);
                Thread.Sleep(200);     //0.2S
            }
            arr[row, 8] = "$";        
        }
        static void RowUpdate(string update,Int16 RowOfTable, int RowOfData, int ColOfData)
        {
            help.gotoxy(67, short.Parse((RowOfTable+1).ToString()));
            update = Console.ReadLine();
            if (update.Trim() == "y" || update.Trim() == "Y")
            {
                help.gotoxy(67, RowOfTable);
                Console.Write("\t\t   ");
                help.gotoxy(67, RowOfTable);
                arr[RowOfData, ColOfData] = Console.ReadLine();
            }
        }
        static void DeleteFunction(int delete,int row)
        {
            for (int i = 1; i <= row; i++)
            {
                if(arr[i,1]!=arr[delete,1] && arr[i, 3] != arr[delete, 3])
                {
                    newarr = arr;
                }
            }
            arr = newarr;   
        }       
    }
}
