﻿using System.Collections.Generic;
using FrameworkTester.ViewModels.Interfaces;
using WinBiometricDotNet;

namespace FrameworkTester.DesignTimes
{

    public sealed class WinBioDeleteTemplateViewModel : WinBioViewModel, IWinBioDeleteTemplateViewModel, IWinBioAsyncSessionViewModel
    {

        public FingerPosition CurrentFingerPosition
        {
            get;
            set;
        }

        public IEnumerable<FingerPosition> FingerPositions
        {
            get;
        }

        public IBiometricIdentityRepositoryViewModel IdentityRepository
        {
            get;
        }

        public IWindowRepositoryViewModel<ISessionHandleViewModel> WindowRepository
        {
            get;
        }

    }

}