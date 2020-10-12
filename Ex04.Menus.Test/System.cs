using System;
using System.Collections.Generic;
using System.Text;
using Ex04.Menus.Delegates;
using Ex04.Menus.Interfaces;
using MenuItem = Ex04.Menus.Delegates.MenuItem;

namespace Ex04.Menus.Test
{
    public class System : ISelectedListener
    {
        private enum eMenuOption
        {
            CountSpaces = 1, ShowVersion, ShowDate, ShowTime
        }

        public void NotifySelected(int i_ID)
        {
            // i_ID is a valid enum value, because that's how it was assigned in the first place
            eMenuOption choice = (eMenuOption)i_ID;

            switch (choice)
            {
                case eMenuOption.CountSpaces:
                    countSpaces();
                    break;
                case eMenuOption.ShowVersion:
                    showVersion();
                    break;
                case eMenuOption.ShowDate:
                    showDate();
                    break;
                case eMenuOption.ShowTime:
                    showTime();
                    break;
                default:
                    throw new ArgumentException("invalid reply");
            }

            Console.WriteLine("press enter to continue");
            Console.ReadLine();
        }

        private void menuItem_Select(Delegates.MenuItem i_Invoker)
        {
            if (i_Invoker.ID.HasValue)
            {
                // using the same switch-method as with the interfaces
                NotifySelected(i_Invoker.ID.Value);
            }
        }        

        public Interfaces.MainMenu BuildMenuWithInterfaces()
        {            
            Interfaces.MainMenu mainMenu = new Interfaces.MainMenu("Main Menu");
            Interfaces.MenuItem subMenu1 = new Interfaces.MenuItem("Version and Spaces");
            Interfaces.MenuItem subMenu2 = new Interfaces.MenuItem("Show Date/Time");

            Interfaces.MenuItem item1 = new Interfaces.MenuItem((int)eMenuOption.CountSpaces, "Count Spaces");
            item1.AddSelectedListener(this);
            Interfaces.MenuItem item2 = new Interfaces.MenuItem((int)eMenuOption.ShowVersion, "Show Version");
            item2.AddSelectedListener(this);
            Interfaces.MenuItem item3 = new Interfaces.MenuItem((int)eMenuOption.ShowDate, "Show Date");
            item3.AddSelectedListener(this);
            Interfaces.MenuItem item4 = new Interfaces.MenuItem((int)eMenuOption.ShowTime, "Show Time");
            item4.AddSelectedListener(this);

            mainMenu.AddMenuItem(subMenu1);
            mainMenu.AddMenuItem(subMenu2);
            subMenu1.AddMenuItem(item1);
            subMenu1.AddMenuItem(item2);
            subMenu2.AddMenuItem(item3);
            subMenu2.AddMenuItem(item4);

            return mainMenu;
        }       

        public Delegates.MainMenu BuildMenuWithDelegates()
        {
            Delegates.MainMenu mainMenu = new Delegates.MainMenu("Main Menu");
            Delegates.MenuItem subMenu1 = new Delegates.MenuItem("Version and Spaces");
            Delegates.MenuItem subMenu2 = new Delegates.MenuItem("Show Date/Time");

            Delegates.MenuItem item1 = new Delegates.MenuItem((int)eMenuOption.CountSpaces, "Count Spaces");
            item1.AttachListener(menuItem_Select);
            Delegates.MenuItem item2 = new Delegates.MenuItem((int)eMenuOption.ShowVersion, "Show Version");
            item2.AttachListener(menuItem_Select);
            Delegates.MenuItem item3 = new Delegates.MenuItem((int)eMenuOption.ShowDate, "Show Date");
            item3.AttachListener(menuItem_Select);
            Delegates.MenuItem item4 = new Delegates.MenuItem((int)eMenuOption.ShowTime, "Show Time");
            item4.AttachListener(menuItem_Select);

            mainMenu.AddMenuItem(subMenu1);
            mainMenu.AddMenuItem(subMenu2);
            subMenu1.AddMenuItem(item1);
            subMenu1.AddMenuItem(item2);
            subMenu2.AddMenuItem(item3);
            subMenu2.AddMenuItem(item4);

            return mainMenu;
        }

        private void countSpaces()
        {
            Console.Write("enter a sentence: ");
            string str = Console.ReadLine();
            int counter = 0;
            foreach (char ch in str)
            {
                if (ch == ' ')
                {
                    counter++;
                }
            }

            Console.WriteLine("there are {0} spaces in the sentence", counter);
        }

        private void showVersion()
        {
            Console.WriteLine("Version: 20.3.4.8920");
        }

        private void showDate()
        {
            Console.WriteLine(DateTime.Now.ToShortDateString());
        }

        private void showTime()
        {
            Console.WriteLine(DateTime.Now.ToShortTimeString());
        }
    }
}
