//
// Copyright (C) 2015 crosire & kagikn & contributors
// License: https://github.com/scripthookvdotnet/scripthookvdotnet#license
//

using System.Drawing;
using System;

namespace GTA
{
    [Obsolete("The v2 API is deprecated, use the v3 API instead.")]
    public interface UIElement
    {
        void Draw();
        void Draw(Size offset);

        bool Enabled
        {
            get; set;
        }

        Point Position
        {
            get; set;
        }

        Color Color
        {
            get; set;
        }
    }
}
