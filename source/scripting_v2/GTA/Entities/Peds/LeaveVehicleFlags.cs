//
// Copyright (C) 2015 crosire & kagikn & contributors
// License: https://github.com/scripthookvdotnet/scripthookvdotnet#license
//

using System;

namespace GTA
{
    [Flags]
    [Obsolete("The v2 API is deprecated, use the v3 API instead.")]
    public enum LeaveVehicleFlags
    {
        None = 0,
        WarpOut = 16,
        LeaveDoorOpen = 256,
        BailOut = 4096,
    }
}
