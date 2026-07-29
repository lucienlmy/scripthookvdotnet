//
// Copyright (C) 2015 crosire & kagikn & contributors
// License: https://github.com/scripthookvdotnet/scripthookvdotnet#license
//

using GTA.Native;
using System;

namespace GTA
{
    [Obsolete("The v2 API is deprecated, use the v3 API instead.")]
    public sealed class Notification
    {
        private readonly int _handle;

        internal Notification(int handle)
        {
            this._handle = handle;
        }

        public void Hide()
        {
            Function.Call(Hash._REMOVE_NOTIFICATION, _handle);
        }
    }
}
