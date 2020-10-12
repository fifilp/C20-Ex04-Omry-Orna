using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Ex04.Menus.Delegates;

namespace Ex04.Menus.Test
{
    public class Program
    {
        public static void Main()
        {
            System system = new System();

            Interfaces.MainMenu mainMenuWithInterfaces = system.BuildMenuWithInterfaces();
            mainMenuWithInterfaces.Show();

            Delegates.MainMenu mainMenuWithDelegates = system.BuildMenuWithDelegates();
            mainMenuWithDelegates.Show();
        }
    }
}
