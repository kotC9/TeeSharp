using System;
using System.Runtime.CompilerServices;
using TeeSharp.Core.Extensions;

namespace TeeSharp.Map
{
    public struct MapItemLayerTilemap : IDataFileItem
    {
        public unsafe Span<int> NameBuffer
            => new Span<int>(Unsafe.AsPointer(ref _nameData[0]), 3);

        public string Name
        {
            get => NameBuffer.GetString();
            set => NameBuffer.PutString(value);
        }

        public Layer Layer;
        public int Version;

        public int Width;
        public int Height;
        public int Flags;

        public Color Color;
        public int ColorEnv;
        public int ColorEnvOffset;

        public int Image;
        public int Data;

        private unsafe fixed int _nameData[3];
    }
    
    public struct Color
    {
        public int R;
        public int G;
        public int B;
        public int A;
    }
}