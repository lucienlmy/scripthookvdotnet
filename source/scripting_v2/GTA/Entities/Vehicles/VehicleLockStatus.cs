//
// Copyright (C) 2015 crosire & kagikn & contributors
// License: https://github.com/scripthookvdotnet/scripthookvdotnet#license
//

using System;

namespace GTA
{
    [Obsolete("The v2 API is deprecated, use the v3 API instead.")]
    public enum VehicleLockStatus
    {
        None,
        Unlocked,
        Locked,
        LockedForPlayer,
        /// <summary>
        /// Doesn't allow players to exit the vehicle with the exit vehicle key.
        /// </summary>
        StickPlayerInside,
        /// <summary>
        /// Can be broken into the car. If the glass is broken, the value will be set to 1.
        /// </summary>
        CanBeBrokenInto = 7,
        CanBeBrokenIntoPersist,
        CannotBeTriedToEnter = 10,
    }
}
