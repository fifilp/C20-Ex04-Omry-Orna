using System;
using System.Collections.Generic;
using System.Text;

namespace Ex04.Menus.Interfaces
{
    public class MenuItem
    {
        private readonly string r_Title;
        private readonly int? r_ID; // nullable because only action items have IDs with value       
        private readonly List<MenuItem> r_MenuItems = new List<MenuItem>();
        private readonly List<ISelectedListener> r_SelectedListeners = new List<ISelectedListener>();
        private string m_BackName;        

        private MenuItem(int? i_ID, string i_Title, string i_BackName)
        {
            r_ID = i_ID;
            r_Title = i_Title;
            m_BackName = i_BackName;
        }

        // ctor for sub-menu
        public MenuItem(string i_Title) : this(null, i_Title, "Back")
        {
        }

        // ctor for action-item
        public MenuItem(int? i_ID, string i_Title) : this(i_ID, i_Title, null)
        {
        }

        public string Title
        {
            get
            {
                return r_Title;
            }
        }

        internal string BackName
        {
            set
            {
                m_BackName = value;
            }
        }

        public int? ID
        {
            get
            {
                return r_ID;
            }
        }

        public void AddSelectedListener(ISelectedListener i_Listener)
        {
            if (r_MenuItems.Count > 0 || r_ID == null)
            {
                throw new Exception("Can't add a listener to a sub menu item");
            }

            r_SelectedListeners.Add(i_Listener);
        }

        public void RemoveSelectedListener(ISelectedListener i_Listener)
        {
            r_SelectedListeners.Remove(i_Listener);
        }

        private void doWhenSelected()
        {
            // if it's an action-item
            if (r_MenuItems.Count == 0)
            {
                foreach (ISelectedListener clickedListener in r_SelectedListeners)
                {
                    clickedListener.NotifySelected(ID.Value);
                }
            }
            else 
            {
                // if it's a sub-menu
                Show();
            }
        }

        public void AddMenuItem(MenuItem i_Item)
        {
            if (r_SelectedListeners.Count > 0 || r_ID != null)
            {
                throw new Exception("Can't add a sub item to an action item");
            }

            r_MenuItems.Add(i_Item);
        }

        public void RemoveMenuItem(MenuItem i_Item)
        {
            r_MenuItems.Remove(i_Item);
        }

        internal void Show()
        {
            bool back = false;
            while (!back)
            {
                printMenu();

                int choice = getChoice();
                if (choice != 0)
                {
                    // (choice-1) because the back option isn't part of the list
                    r_MenuItems[choice - 1].doWhenSelected();
                }
                else
                {
                    back = true;
                }
            }
        }

        private void printMenu()
        {
            Console.Clear();
            Console.WriteLine(r_Title);
            
            int i = 1;

            foreach (MenuItem item in r_MenuItems)
            {
                Console.WriteLine("{0}. {1}", i, item.Title);
                i++;
            }

            Console.WriteLine("0. {0}", m_BackName);

            string msg = string.Format("Please choose from the menu (enter a number from 0 to {0})", r_MenuItems.Count);
            Console.WriteLine(msg);
        }

        private int getChoice()
        {
            int choice;
            while (!int.TryParse(Console.ReadLine(), out choice))
            {
                Console.WriteLine("enter a number from 0 to {0})", r_MenuItems.Count);
            }

            return choice;
        }
    }
}
