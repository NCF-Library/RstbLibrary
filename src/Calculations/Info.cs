using System.Runtime.CompilerServices;

namespace RstbLibrary.Calculations;

internal static class Info
{
    public static (uint, int) GetFactoryInfo(string ext, Endianness endian)
    {
        return ext switch {
            "sarc" or "pack" or "bactorpack" or
            "bmodelsh" or "beventpack" or "stera" or
            "stats" => Define(be: 0x3C, le: 0x68),
            "Tex.bfres" or "Tex1.bfres" or
            "Tex2.bfres" => Define(be: 0x20, le: 0x38),
            "bfres" => Define(be: 0x20, le: 0x38, -1),
            "bcamanim" => Define(be: 0x2C, le: 0x50, -1),
            "batpl" or "bnfprl" => Define(be: 0x24, le: 0x40),
            "bplacement" => Define(be: 0x14, le: 0x48),
            "hks" or "lua" => Define(be: 0x14, le: 0x38),
            "bactcapt" => Define(be: 0x3B4, le: 0x538),
            "bitemico" => Define(be: 0xD0, le: 0x60),
            "jpg" => Define(be: 0x174, le: 0x80),
            "bmaptex" => Define(be: 0xD0, le: 0x60, 0),
            "bstftex" or "bmapopen" or
            "breviewtex" => Define(be: 0xD0, le: 0x60),
            "bgdata" => Define(be: 0xCC, le: 0x140),
            "bgsvdata" => Define(be: 0x14, le: 0x38, 0),
            "hknm2" => Define(be: 0x28, le: 0x48, -1),
            "bmscdef" => Define(be: 0x1FC, le: 0x2A8, -1),
            "bars" => Define(be: 0x84, le: 0xB0, -1),
            "bxml" => Define(be: 0x4A8, le: 0x778, -1),
            "bgparamlist" => Define(be: 0x248, le: 0x2C0, -1),
            "bmodellist" => Define(be: 0x508, le: 0x7D0, -1),
            "baslist" => Define(be: 0x2F4, le: 0x410, -1),
            "baiprog" => Define(be: 0x30C, le: 0x448, -1),
            "bphysics" => Define(be: 0x324, le: 0x470, -1),
            "bchemical" => Define(be: 0x2CC, le: 0x3C0, -1),
            "bas" => Define(be: 0x2D0, le: 0x3C8, -1),
            "batcllist" => Define(be: 0x2E4, le: 0x3F0, -1),
            "batcl" => Define(be: 0x344, le: 0x428, -1),
            "baischedule" => Define(be: 0x244, le: 0x2B8, 0),
            "bdmgparam" => Define(be: 0x9F0, le: 0x11D0, 0x3C0, 0x790),
            "brgconfiglist" => Define(be: 0x2D4, le: 0x3D0, -1),
            "brgconfig" => Define(be: 0x2ACC, le: 0x42D8, 0),
            "brgbw" => Define(be: 0x248, le: 0x2C0, -1),
            "bawareness" => Define(be: 0x70C, le: 0xB38, 0),
            "bdrop" => Define(be: 0x27C, le: 0x320, -1),
            "bshop" => Define(be: 0x27C, le: 0x320, -1),
            "brecipe" => Define(be: 0x27C, le: 0x320, -1),
            "blod" => Define(be: 0x2CC, le: 0x3C0, 0),
            "bbonectrl" => Define(be: 0x564, le: 0x8D0, -1),
            "blifecondition" => Define(be: 0x35C, le: 0x4B0, -1),
            "bumii" => Define(be: 0x244, le: 0x2B8, 0),
            "baniminfo" => Define(be: 0x24C, le: 0x2C8, -1),
            "byaml" => Define(be: 0x14, le: 0x20, 0),
            "bassetting" => Define(be: 0x1D8, le: 0x260, -1),
            "hkrb" => Define(be: 0x14, le: 0x20, 40, 0),
            "hkrg" => Define(be: 0x14, le: 0x20, 0),
            "bphyssb" => Define(be: 0x384, le: 0x5B0, -1),
            "hkcl" => Define(be: 0xB8, le: 0xE8, -1),
            "hksc" => Define(be: 0xE8, le: 0x140, -1),
            "hktmrb" => Define(be: 0x28, le: 0x48, -1),
            "brgcon" => Define(be: 0x28, le: 0x48, -1),
            "esetlist" => Define(be: 0x20, le: 0x38, 0),
            "bdemo" => Define(be: 0x6CC, le: 0xB20, 0),
            "bfevfl" => Define(be: 0x24, le: 0x40, 0),
            "bfevtm" => Define(be: 0x24, le: 0x40, 0),
            _ => Define(be: 0x20, le: 0x38)
        };

        // 
        // Specify a negative parse size
        // to implement the Complex type

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        (uint, int) Define(uint be, uint le, int? beParseSize = null, int? leParseSize = 0)
        {
            return endian == Endianness.Big ? (be, beParseSize ?? 0) : (le, leParseSize ?? 0);
        }
    }
}
