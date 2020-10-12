using System;
using System.Collections.Generic;
using System.Text;

namespace Ex04.Menus.Delegates
{
    public class MainMenu 
    {
        private readonly MenuItem r_RootItem;

        public MainMenu(string i_Title)
        {
            r_RootItem = new MenuItem(i_Title);

            // overwriting BackName only for the root-item (main menu)
            r_RootItem.BackName = "Exit"; 
        }

        public void AddMenuItem(MenuItem i_Item)
        {
            r_RootItem.AddMenuItem(i_Item);
        }

        public void RemoveMenuItem(MenuItem i_Item)
        {
            r_RootItem.RemoveMenuItem(i_Item);
        }

        public void Show()
        {
            r_RootItem.Show();
        }
    }
}
