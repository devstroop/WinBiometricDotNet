﻿using System;
using System.Windows;
using FrameworkTester.ViewModels.Interfaces;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;
using WinBiometricDotNet;

namespace FrameworkTester.ViewModels
{

    public sealed class WinBioGetCredentialStateViewModel : WinBioViewModel, IWinBioGetCredentialStateViewModel
    {

        #region Constructors

        public WinBioGetCredentialStateViewModel()
        {
            this.IdentityRepository = SimpleIoc.Default.GetInstance<IBiometricIdentityRepositoryViewModel>();
            this.IdentityRepository.PropertyChanged += (sender, args) =>
            {
                this.ExecuteCommand.RaiseCanExecuteChanged();
            };
        }

        #endregion

        #region Properties

        private RelayCommand _ExecuteCommand;

        public override RelayCommand ExecuteCommand
        {
            get
            {
                return this._ExecuteCommand ?? (this._ExecuteCommand = new RelayCommand(() =>
                {
                    this.State = CredentialStates.NotSet;

                    try
                    {
                        this.Result = "WAIT";
                        var result = this.BiometricService.GetCredentialState(this.IdentityRepository.CurrentBiometricIdentity, CredentialTypes.Password);
                        this.Result = "OK";

                        this.State = result;
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.Message, this.Name, MessageBoxButton.OK, MessageBoxImage.Error);
                        this.Result = "FAIL";
                    }
                }, () => this.IdentityRepository?.CurrentBiometricIdentity != null));
            }
        }

        public IBiometricIdentityRepositoryViewModel IdentityRepository
        {
            get;
        }

        public override string Name => "WinBioGetCredentialState";

        private CredentialStates _State;

        public CredentialStates State
        {
            get => this._State;
            private set
            {
                this._State = value;
                this.RaisePropertyChanged();
            }
        }

        #endregion

    }

}