//
// Copyright (C) 2015 crosire & kagikn & contributors
// License: https://github.com/scripthookvdotnet/scripthookvdotnet#license
//

using System;

namespace GTA
{
    [Obsolete("The v2 API is deprecated, use the v3 API instead.")]
    public sealed class ScaleformArgumentTXD
    {
        internal string _txd;

        public ScaleformArgumentTXD(string s)
        {
            _txd = s;
        }
    }
}
