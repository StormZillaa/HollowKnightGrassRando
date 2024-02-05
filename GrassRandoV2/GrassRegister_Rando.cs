using GrassCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrassRandoV2
{
    internal class GrassRegister_Rando : GrassRegister
    {
        public static GrassRegister Instance;

        static GrassRegister_Rando() => Instance ??= new GrassRegister_Rando();

        public GrassRegister_Rando() => Instance ??= this;
    }
}
