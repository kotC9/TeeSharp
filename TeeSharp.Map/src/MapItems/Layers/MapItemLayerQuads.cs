using System;
using System.Runtime.CompilerServices;
using TeeSharp.Core.Extensions;

namespace TeeSharp.Map
{
    public struct MapItemLayerQuads : IDataFileItem
    {
        public const int CurrentVersion = 2;
        
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
        
        public int NumQuads;
        public int Data;
        public int Image;
        
        private unsafe fixed int _nameData[3];
    }
}