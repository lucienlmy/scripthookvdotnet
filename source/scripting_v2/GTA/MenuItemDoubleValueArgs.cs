//
// Copyright (C) 2015 crosire & kagikn & contributors
// License: https://github.com/scripthookvdotnet/scripthookvdotnet#license
//

using System;

namespace GTA
{
    [Obsolete("The v2 API is deprecated, use the v3 API instead.")]
    public class MenuItemDoubleValueArgs : EventArgs
    {
        public MenuItemDoubleValueArgs(double value)
        {
            Index = value;
        }

        public double Index { get; }
    }
}
