//
// Copyright (C) 2015 crosire & kagikn & contributors
// License: https://github.com/scripthookvdotnet/scripthookvdotnet#license
//

using System;

namespace GTA
{
    [Obsolete("The v2 API is deprecated, use the v3 API instead.")]
    public enum ForceType
    {
        MinForce,
        MaxForceRot,
        MinForce2,
        MaxForceRot2, // Stable, good for custom handling
        ForceNoRot,
        ForceRotPlusForce,
    }
}
