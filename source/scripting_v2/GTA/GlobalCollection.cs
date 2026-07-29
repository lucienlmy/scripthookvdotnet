//
// Copyright (C) 2015 crosire & kagikn & contributors
// License: https://github.com/scripthookvdotnet/scripthookvdotnet#license
//

using System;

namespace GTA
{
    [Obsolete("The v2 API is deprecated, use the v3 API instead.")]
    public class GlobalCollection
    {
        internal GlobalCollection()
        {
        }

        public Global this[int index]
        {
            get => new Global(index);
            set
            {
                unsafe
                {
                    *(ulong*)SHVDN.NativeMemory.GetGlobalPtr(index).ToPointer() = *value.MemoryAddress;
                }
            }
        }
    }
}
