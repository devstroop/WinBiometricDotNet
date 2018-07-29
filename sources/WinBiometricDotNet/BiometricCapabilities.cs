﻿using System;

namespace WinBiometricDotNet
{

    [Flags]
    public enum BiometricCapabilities
    {

        Sensor = (0x00000001),

        Matching = (0x00000002),

        Database = (0x00000004),

        Proccessing = (0x00000008),

        Encryption = (0x00000010),

        Navigation = (0x00000020),

        Indicator = (0x00000040)

    }

}