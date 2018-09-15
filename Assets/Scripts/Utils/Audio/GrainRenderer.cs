using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Utils.Audio
{
    interface GrainRenderer
    {
        void RenderToBuffer(ref float[] data);
    }
}
