using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace test
{
    class ConsoleHelper
    {
        [StructLayout(LayoutKind.Sequential)]
        struct POSITION
        {
            public short x;
            public short y;
        }
        // http://msdn.microsoft.com/en-us/library/ms682073
        [DllImport("kernel32.dll", EntryPoint = "GetStdHandle", SetLastError = true, CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        private static extern int GetStdHandle(int nStdHandle);

        [DllImport("kernel32.dll", EntryPoint = "SetConsoleCursorPosition", SetLastError = true, CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        private static extern int SetConsoleCursorPosition(int hConsoleOutput, POSITION dwCursorPosition);

        public void gotoxy(short x, short y)
        {
            const int STD_OUTPUT_HANDLE = -11;
            int hConsoleHandle = GetStdHandle(STD_OUTPUT_HANDLE);
            POSITION position;
            position.x = x;
            position.y = y;
            SetConsoleCursorPosition(hConsoleHandle, position);
        }
        [StructLayout(LayoutKind.Sequential)]
        struct CONSOLERECT
        {
            public short Left;
            public short Top;
            public short Right;
            public short Bottom;
        }

        [StructLayout(LayoutKind.Sequential)]
        struct CONSOLEBUFFER
        {
            public POSITION size;
            public POSITION position;
            public int attrib;
            public CONSOLERECT window;
            public POSITION maxsize;
        }
        [DllImport("kernel32.dll", EntryPoint = "FillConsoleOutputCharacter", SetLastError = true, CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        private static extern int FillConsoleOutputCharacter(int handleConsoleOutput, byte fillchar, int len, POSITION writecord, ref int numberofbyeswritten);

        [DllImport("kernel32.dll", EntryPoint = "GetConsoleScreenBufferInfo", SetLastError = true, CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        private static extern int GetConsoleScreenBufferInfo(int handleConsoleOutput, ref CONSOLEBUFFER bufferinfo);

        public void ClearScreen()
        {
            const int STD_OUTPUT_HANDLE = -11;
            int hConsoleHandle = GetStdHandle(STD_OUTPUT_HANDLE);

            int hWrittenChars = 0;
            CONSOLEBUFFER strConsoleInfo = new CONSOLEBUFFER();
            POSITION pos;
            pos.x = pos.y = 0;
            GetConsoleScreenBufferInfo(hConsoleHandle, ref strConsoleInfo);
            FillConsoleOutputCharacter(hConsoleHandle, 32, strConsoleInfo.size.x * strConsoleInfo.size.y, pos, ref hWrittenChars);
            SetConsoleCursorPosition(hConsoleHandle, pos);
        }
        public void DisplayMenu()
        {
            ShowTiteMenu();
            loadTable();
            gotoxy(35, 10);
            Console.Write("Enter function details(f)");
            gotoxy(35, 11);
            Console.Write("Search for a function(s)");
            gotoxy(35, 12);
            Console.Write("Edit a function(e)");
            gotoxy(35, 13);
            Console.Write("Delete a function(d)");
            gotoxy(35, 14);
            Console.Write("Display costings(c)");
            gotoxy(35, 15);
            Console.Write("Exit(x)");
        }
        public void ShowTiteMenu()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            gotoxy(38, 2);
            Console.Write("  █   █   ▄▀▀▀ ▄▀▀▀▄ █   █     ");
            gotoxy(38, 3);
            Console.Write(" █  ▀  █  ████ █   █ █   █    ");
            gotoxy(38, 4);
            Console.Write("█       █ ▀▄▄▄ █   █ ▀▄▄▄▀        ");
            Console.ResetColor();
        }
        public void ShowTitleView()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            gotoxy(30, 2);
            Console.Write("▀▀▀▀▀▄ ▀▀█▀▀ ▄▀▀▀▀ ▄▀▀▀▄ █    ▄▀▄  ▀▄ ▄▀ ");
            gotoxy(30, 3);
            Console.Write("  █  █   █    ▀▀▀▄ █▀▀▀  █   █▄▄▄█   █   ");
            gotoxy(30, 4);
            Console.Write("▄▄▄▄▄▀ ▄▄█▄▄ ▄▄▄▄▀ █     █▄▄ █   █   █   ");                                              
            Console.ResetColor();
        }
        public void ShowTitleAdd()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            gotoxy(40, 2);
            Console.Write("   ▄▀▄  ▀▀▀▀▀▄ ▀▀▀▀▀▄");
            gotoxy(40, 3);
            Console.Write("  █▄▄▄█   █  █   █  █");
            gotoxy(40, 4);
            Console.Write("  █   █ ▄▄▄▄▄▀ ▄▄▄▄▄▀");
            Console.ResetColor();
        }
        public void NotFound()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            gotoxy(20, 10);
            Console.Write("▄▀▀▀▄ ▄▀▀▀▄ ▀▀█▀▀  ▀▀▀▀▀█ ▀▀█▀▀ ▄▀▀▀▀ ▄▀▀▀▄ █    ▄▀▄  ▀▄ ▄▀ ");
            gotoxy(20, 11);
            Console.Write("█   █ █   █   █      █  █   █    ▀▀▀▄ █▀▀▀  █   █▄▄▄█   █   ");
            gotoxy(20, 12);
            Console.Write("█   █ ▀▄▄▄▀   █    ▄▄▄▄▄█ ▄▄█▄▄ ▄▄▄▄▀ █     █▄▄ █   █   █   ");
            Console.ResetColor();
        }
        public void loadTable()
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            for (short i = 10; i <= 90; i++)
            {
                gotoxy(i, 1);
                Console.Write("▀");
            }
            for (short i = 1; i <= 23; i++)
            {
                gotoxy(10, i);
                Console.Write("█");
                gotoxy(90, i);
                Console.Write("█");
            }   
            for(short i=10; i<=90; i++)
            {
                gotoxy(i, 24);
                Console.Write("▀");                
            }
            for (short i = 11; i <90; i++)
            {
                gotoxy(i, 6);
                Console.Write("▀");
            }           
            Console.ResetColor();
        }
        public void titleMenu()
        {
            for(short i=40; i<=55; i++)
            {
                gotoxy(i, 4);
                Console.Write("▀");
            }
        }
        public void add()
        {
            loadTable();
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            gotoxy(30, 7);
            Console.Write("Name of client".PadRight(31)+":");
            gotoxy(30, 8);
            Console.Write("Address of function".PadRight(31) + ":");
            gotoxy(30, 9);
            Console.Write("Contact telephone number".PadRight(31) + ":");
            gotoxy(30, 10);
            Console.Write("Number of guests".PadRight(31) + ":");
            gotoxy(30, 11);
            Console.Write("Meal service required");
            gotoxy(30, 12);
            Console.Write("\tF for finger food only");
            gotoxy(30, 13);
            Console.Write("\t2 for 2 course meal");
            gotoxy(30, 14);
            Console.Write("\t3 for 3 course meal");
            gotoxy(30, 15);
            Console.Write("\tB for banquet".PadRight(30) + ":");
            gotoxy(30, 16);
            Console.Write("Date of function is");
            gotoxy(30, 17);
            Console.Write("\tYear".PadRight(30) + ":");
            gotoxy(30, 18);
            Console.Write("\tMonth".PadRight(30) + ":");
            gotoxy(30, 19);
            Console.Write("\tDay".PadRight(30) + ":");
            Console.ResetColor();            
            
        }
        public void view()
        {
            ShowTitleView();
            loadTable();
            gotoxy(30, 7);
            Console.Write("Name of client".PadRight(31) + ":");
            gotoxy(30, 8);
            Console.Write("Address of function".PadRight(31) + ":");
            gotoxy(30, 9);
            Console.Write("Contact telephone number".PadRight(31) + ":");
            gotoxy(30, 10);
            Console.Write("Number of guests".PadRight(31) + ":");
            gotoxy(30, 11);
            Console.Write("Date of function is".PadRight(31) + ":");
            gotoxy(30, 12);
            Console.Write("Meal type".PadRight(31) + ":");
            gotoxy(30, 13);
            Console.Write("Meal costs".PadRight(31) + ": $ ");
            gotoxy(30, 14);
            Console.Write("Service Costs".PadRight(31) + ": $ ");
            gotoxy(30, 15);
            Console.Write("Total Cost".PadRight(31) + ": $ ");
            gotoxy(30, 16);
            Console.Write("GST".PadRight(31) + ": $ ");
            gotoxy(61, 17);
            Console.Write("---------------");
            gotoxy(30, 18);
            Console.Write("Total Cost Plus GST".PadRight(31) + ": $ ");
            gotoxy(61, 19);
            Console.Write("===============");
            btnOK("     <--     ", ConsoleColor.Blue, 10, 25);
            btnOK("    Enter    ", ConsoleColor.Blue, 40, 25);
            btnOK("     -->     ", ConsoleColor.Blue, 78, 25);
        }
        public void viewtable()
        {
            Console.Title = "Display Data";
            Console.ForegroundColor = ConsoleColor.Green;
            gotoxy(7, 2);
            Console.Write("Name");
            gotoxy(20, 2);
            Console.Write("Address");
            gotoxy(40, 2);
            Console.Write("Phone Number");
            gotoxy(55, 2);
            Console.Write("Guests");
            gotoxy(63, 2);
            Console.Write("Meal Type");
            gotoxy(75, 2);
            Console.Write("Meal Cost");
            gotoxy(87, 2);
            Console.Write("Date");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            for(short i = 5; i <= 105; i++)
            {
                gotoxy(i, 1);
                Console.Write("▀");
            }
            for (short i = 1; i <= 25; i++)
            {
                gotoxy(5, i);
                Console.Write("█");
                gotoxy(19, i);
                Console.Write("█");
                gotoxy(39, i);
                Console.Write("█");
                gotoxy(54, i);
                Console.Write("█");
                gotoxy(62, i);
                Console.Write("█");
                gotoxy(74, i);
                Console.Write("█");
                gotoxy(86, i);
                Console.Write("█");
                gotoxy(105, i);
                Console.Write("█");
            }
            for(short i = 5; i <= 105; i++)
            {
                gotoxy(i, 25);
                Console.Write("▀");
            }
            Console.ResetColor();
            btnOK("  Backspace  ", ConsoleColor.Blue, 5, 26);
        }
        public void update()
        {
            loadTable();
            Console.ForegroundColor = ConsoleColor.Cyan;
            gotoxy(30, 10);
            Console.Write("Name of client".PadRight(35) + ":");
            gotoxy(30, 12);
            Console.Write("Address of function".PadRight(35) + ":");
            gotoxy(30, 14);
            Console.Write("Contact Phone Number".PadRight(35) + ":");
            gotoxy(30, 16);
            Console.Write("Number of guests".PadRight(35) + ":");
            gotoxy(30, 18);
            Console.Write("Date of function is".PadRight(35) + ":");
            gotoxy(30, 20);
            Console.Write("Meal type".PadRight(35) + ":");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.White;
            gotoxy(30, 11);
            Console.Write("Update this information(Y or N)".PadRight(35) + ":");
            gotoxy(30, 13);           
            Console.Write("Update this information(Y or N)".PadRight(35) + ":");            
            gotoxy(30, 15);
            Console.Write("Update this information(Y or N)".PadRight(35) + ":");            
            gotoxy(30, 17);
            Console.Write("Update this information(Y or N)".PadRight(35) + ":");            
            gotoxy(30, 19);
            Console.Write("Update this information(Y or N)".PadRight(35) + ":");           
            gotoxy(30, 21);
            Console.Write("Update this information(Y or N)".PadRight(35) + ":");
            Console.ResetColor();
            btnOK("    Enter    ", ConsoleColor.Blue, 78, 25);
        }
        public void btnOK(string nameOfButton, ConsoleColor backgroud, short row,Int16 col)
        {

            Console.ForegroundColor = backgroud;
            gotoxy(row, col);    //29
            Console.Write("▄▄▄▄▄▄▄▄▄▄▄▄▄");
            gotoxy(row, Int16.Parse((col+2).ToString()));    //31
            Console.Write("▀▀▀▀▀▀▀▀▀▀▀▀▀");
            Console.ResetColor();
            Console.BackgroundColor = backgroud;
            gotoxy(row, Int16.Parse((col+1).ToString()));    //30
            Console.Write(nameOfButton);            
            Console.ResetColor();
        }
        public void copyright()
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.Write("Copyright 2016-2017 : ClassC");
            Console.ResetColor();
        }
        public string mealtype(string str)
        {
            if (str.Trim() == "B" || str.Trim() == "b")
                return "Banquet";
            else if (str.Trim() == "2")
                return "2 course";
            else if (str.Trim() == "3")
                return "3 course";
            else if (str.Trim() == "F" || str.Trim() == "f")
                return "Finger";
            else
                return "";
        }
        public float mealcost(string str)
        {
            float cost=0;
            if (str.Trim() == "2")
                cost = 30;
            else if (str.Trim() == "3")
                cost = 47.50f;
            else if (str.Trim() == "b" || str.Trim() == "B")
                cost = 78.90f;
            else if (str.Trim() == "f" || str.Trim() == "F")
                cost = 20;
            return cost;
        }
        public string month(string month)
        {
            string str = "";
            switch (month)
            {
                case "1":
                    str = "January";
                    break;
                case "2":
                    str = "February";
                    break;
                case "3":
                    str = "March";
                    break;
                case "4":
                    str = "April";
                    break;
                case "5":
                    str = "May";
                    break;
                case "6":
                    str = "June";
                    break;
                case "7":
                    str = "July";
                    break;
                case "8":
                    str = "August";
                    break;
                case "9":
                    str = "September";
                    break;
                case "10":
                    str = "October";
                    break;
                case "11":
                    str = "November";
                    break;
                case "12":
                    str = "December";
                    break;
                default:
                    str = "";
                    break;
            }
            return str;
        }
        public string day(string day, string month, string year)
        {
            string result = "0";
            switch (month)
            {
                case "1":
                case "3":
                case "5":
                case "7":
                case "8":
                case "10":
                case "12":
                    result = "31";
                    break;
                case "4":
                case "6":
                case "9":
                case "11":
                    result = "30";
                    break;
                case "2":
                    if(int.Parse(year)%4==0)
                        result = "29";
                    else
                        result = "28";
                    break;
                default:
                     result="0";
                    break;
            }
            return result;

        }
        public int staff(string numberOfgust)
        {
            int str = int.Parse(numberOfgust.Trim());
            if (str > 0 && str < 20)
                return 200;
            else if (str >= 20 && str <= 39)
                return 300;
            else if (str >= 40 && str < 60)
                return 400;
            else if (str >= 60 && str <= 100)
                return 600;
            else
                return 0;
        }
    }
}
