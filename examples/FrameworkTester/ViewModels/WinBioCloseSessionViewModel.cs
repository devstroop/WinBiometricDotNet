﻿using System;
using System.Windows;
using FrameworkTester.Services.Interfaces;
using FrameworkTester.ViewModels.Interfaces;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;

namespace FrameworkTester.ViewModels
{

    public sealed class WinBioCloseSessionViewModel : ViewModelBase, IWinBioCloseSessionViewModel
    {

        #region Fields

        private readonly IWinBiometricService _Service;

        #endregion

        #region Constructors

        public WinBioCloseSessionViewModel()
        {
            this._Service = SimpleIoc.Default.GetInstance<IWinBiometricService>();
        }

        #endregion

        #region Properties

        private RelayCommand _ExecuteCommand;

        public RelayCommand ExecuteCommand
        {
            get
            {
                return this._ExecuteCommand ?? (this._ExecuteCommand = new RelayCommand(() =>
                {
                    try
                    {
                        this._Service.CloseSession();
                        this.Result = "OK";
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.Message);
                        this.Result = "FAIL";
                    }
                }));
            }
        }

        public string Name => "WinBioCloseSession";

        private string _Result;

        public string Result
        {
            get
            {
                return this._Result;
            }
            private set
            {
                this._Result = value;
                this.RaisePropertyChanged();
            }
        }

        #endregion

    }

}