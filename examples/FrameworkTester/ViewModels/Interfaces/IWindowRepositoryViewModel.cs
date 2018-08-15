﻿using System.Collections.ObjectModel;
using System.ComponentModel;
using WinBiometricDotNet;

namespace FrameworkTester.ViewModels.Interfaces
{

    public interface IWindowRepositoryViewModel<T> : INotifyPropertyChanged
        where T : IHandleViewModel
    {

        void Add(T window);

        T SelectedWindow
        {
            get;
            set;
        }

        ObservableCollection<T> Windows
        {
            get;
        }

    }

}