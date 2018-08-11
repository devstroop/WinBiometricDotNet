﻿using System;
using System.Windows;
using FrameworkTester.Services.Interfaces;
using FrameworkTester.ViewModels.Interfaces;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;
using WinBiometricDotNet;

namespace FrameworkTester.ViewModels
{

    public sealed class WinBioEnrollCaptureWithCallbackViewModel : WinBioViewModel, IWinBioEnrollCaptureWithCallbackViewModel
    {

        #region Fields

        private readonly IDispatcherService _DispatcherService;

        #endregion

        #region Constructors

        public WinBioEnrollCaptureWithCallbackViewModel()
        {
            this._DispatcherService = SimpleIoc.Default.GetInstance<IDispatcherService>();

            WinBiometric.EnrollCaptured += this.WinBiometricEnrollCaptured;

            this.WaitCallback = false;
        }

        #endregion

        #region Properties

        private RelayCommand _CancelCommand;

        public RelayCommand CancelCommand
        {
            get
            {
                return this._CancelCommand ?? (this._CancelCommand = new RelayCommand(() =>
                {
                    try
                    {
                        this.BiometricService.Cancel();

                        this.WaitCallback = false;
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.Message);

                        this.WaitCallback = true;
                    }
                }, () => this._WaitCallback));
            }
        }

        private RelayCommand _ExecuteCommand;

        public override RelayCommand ExecuteCommand
        {
            get
            {
                return this._ExecuteCommand ?? (this._ExecuteCommand = new RelayCommand(() =>
                {
                    this.RejectDetail = 0;

                    try
                    {
                        this.BiometricService.CaptureEnrollWithCallback();
                        this.Result = "WAIT";
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.Message);
                        this.Result = "FAIL";
                    }
                }));
            }
        }

        public override string Name => "WinBioEnrollCaptureWithCallback";

        private RejectDetails _RejectDetail;

        public RejectDetails RejectDetail
        {
            get
            {
                return this._RejectDetail;
            }
            private set
            {
                this._RejectDetail = value;
                this.RaisePropertyChanged();
            }
        }

        private bool _WaitCallback;

        private bool WaitCallback
        {
            get
            {
                return this._WaitCallback;
            }
            set
            {
                this._WaitCallback = value;

                this.RaisePropertyChanged();

                this.ExecuteCommand.RaiseCanExecuteChanged();
                this.CancelCommand.RaiseCanExecuteChanged();
            }
        }

        #endregion

        #region Methods

        #region Event Handlers

        private void WinBiometricEnrollCaptured(object sender, EnrollCapturedEventArgs e)
        {
            this._DispatcherService.SafeAction(() =>
            {
                this.WaitCallback = false;
            });

            switch (e.OperationStatus)
            {
                case OperationStatus.OK:
                    this.Result = "OK";
                    break;
                case OperationStatus.BadCapture:
                    this.Result = "BadCapture";
                    break;
                case OperationStatus.Canceled:
                    this.Result = "Canceled";
                    break;
                case OperationStatus.Unknown:
                    this.Result = "Unknown";
                    break;
            }

            this.RejectDetail = e.RejectDetails;
        }

        #endregion

        #endregion

    }

}