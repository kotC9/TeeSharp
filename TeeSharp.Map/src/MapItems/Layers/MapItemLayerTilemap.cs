using System;
using System.Runtime.CompilerServices;
using TeeSharp.Core.Extensions;

namespace TeeSharp.Map
{
    public struct MapItemLayerTilemap : IDataFileItem
    {
        public const int CurrentVersion = 3;
        public unsafe Span<int> NameBuffer
            => new Span<int>(Unsafe.AsPointer(ref _nameData[0]), 3);

        public string Name
        {
            get => NameBuffer.GetString();
            set => NameBuffer.PutString(value);
        }

        //layer
        public int ItemVersion;
        public int ItemType;
        public int ItemFlags;
        public int Version;

        public int Width;
        public int Height;
        public int Flags;

        //Color
        public int ColorR;
        public int ColorG;
        public int ColorB;
        public int ColorA;
        
        public int ColorEnv;
        public int ColorEnvOffset;

        public int Image;
        public int Data;

        private unsafe fixed int _nameData[3];
    }
    
}