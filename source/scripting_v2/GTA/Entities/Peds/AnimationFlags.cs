//
// Copyright (C) 2015 crosire & kagikn & contributors
// License: https://github.com/scripthookvdotnet/scripthookvdotnet#license
//

using System;

namespace GTA
{
    [Flags]
    [Obsolete("The v2 API is deprecated, use the v3 API instead.")]
    public enum AnimationFlags
    {
        None = 0,
        Loop = 1,
        StayInEndFrame = 2,
        UpperBodyOnly = 16,
        AllowRotation = 32,
        CancelableWithMovement = 128,
        RagdollOnCollision = 4194304,
    }
}
