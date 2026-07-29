//
// Copyright (C) 2015 crosire & kagikn & contributors
// License: https://github.com/scripthookvdotnet/scripthookvdotnet#license
//

using System;

namespace GTA
{
    [Obsolete("The v2 API is deprecated, use the v3 API instead.")]
    public enum DrivingStyle
    {
        Normal = 0xC00AB,
        IgnoreLights = 0x2C0025,
        SometimesOvertakeTraffic = 5,
        Rushed = 0x400C0025,
        AvoidTraffic = 0xC0024,
        AvoidTrafficExtremely = 6,
    }
}
