﻿using FrameworkTester.ViewModels.Interfaces;
using WinBiometricDotNet;

namespace FrameworkTester.DesignTimes
{

    public sealed class WinBioGetEnabledSettingViewModel : WinBioViewModel, IWinBioGetEnabledSettingViewModel
    {

        public SettingSourceTypes Source
        {
            get;
        }

        public bool Value
        {
            get;
        }

    }

}