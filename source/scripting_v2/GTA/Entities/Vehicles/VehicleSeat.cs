//
// Copyright (C) 2015 crosire & kagikn & contributors
// License: https://github.com/scripthookvdotnet/scripthookvdotnet#license
//

using System;

namespace GTA
{
    [Obsolete("The v2 API is deprecated, use the v3 API instead.")]
    public enum VehicleSeat
    {
        None = -3,
        Any,
        Driver,
        Passenger,
        LeftFront = Driver,
        RightFront = Passenger,
        LeftRear,
        RightRear,
        ExtraSeat1,
        ExtraSeat2,
        ExtraSeat3,
        ExtraSeat4,
        ExtraSeat5,
        ExtraSeat6,
        ExtraSeat7,
        ExtraSeat8,
        ExtraSeat9,
        ExtraSeat10,
        ExtraSeat11,
        ExtraSeat12,
    }
}
