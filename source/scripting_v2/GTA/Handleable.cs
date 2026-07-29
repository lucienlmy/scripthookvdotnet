//
// Copyright (C) 2015 crosire & kagikn & contributors
// License: https://github.com/scripthookvdotnet/scripthookvdotnet#license
//

using GTA.Math;
using System;

namespace GTA
{
    [Obsolete("The v2 API is deprecated, use the v3 API instead.")]
    public interface ISpatial
    {
        Vector3 Position
        {
            get; set;
        }
        Vector3 Rotation
        {
            get; set;
        }
    }

    [Obsolete("The v2 API is deprecated, use the v3 API instead.")]
    public interface IHandleable
    {
        int Handle { get; }

        bool Exists();
    }
}
