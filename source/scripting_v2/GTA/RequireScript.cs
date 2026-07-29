//
// Copyright (C) 2015 crosire & kagikn & contributors
// License: https://github.com/scripthookvdotnet/scripthookvdotnet#license
//

using System;

namespace GTA
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    [Obsolete("The v2 API is deprecated, use the v3 API instead.")]
    public sealed class RequireScript : Attribute
    {
        public RequireScript(Type dependency)
        {
        }
    }
}
