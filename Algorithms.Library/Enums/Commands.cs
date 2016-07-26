﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms.Library
{
    [Flags]
    public enum Commands
    {
        Wait = 0,
        MoveUp = 1,
        MoveDown = 2,
        MoveLeft = 4,
        MoveRight = 8,
    }
}
