﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Kartoteka
{
    class CommandToSearch : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            SearchWindow SearchWin = new Kartoteka.SearchWindow();
            SearchWin.Show();
        }
    }
}
