//
// Copyright (C) 2015 crosire & kagikn & contributors
// License: https://github.com/scripthookvdotnet/scripthookvdotnet#license
//

using System;

namespace GTA
{
    [Obsolete("The v2 API is deprecated, use the v3 API instead.")]
    public enum IntersectOptions
    {
        Everything = -1,
        Map = 1,
        Mission_Entities = 2,
        Peds1 = 12, // 4 and 8 both seem to be peds
        Objects = 16,
        Unk1 = 32,
        Unk2 = 64,
        Unk3 = 128,
        Vegetation = 256,
        Unk4 = 512,
    }
}
