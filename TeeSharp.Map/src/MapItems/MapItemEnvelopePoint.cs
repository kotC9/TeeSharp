using System.Runtime.CompilerServices;
using System;

namespace TeeSharp.Map
{
    public struct MapItemEnvelopePoint : IDataFileItem
    {
        public unsafe Span<int> ValuesBuffer
            => new Span<int>(Unsafe.AsPointer(ref _values[0]), 4);

        public int Time; // in ms
        public int Curvetype;
        private unsafe fixed int _values[4]; // 1-4 depending on envelope (22.10 fixed point)
    }
}