﻿using TeeSharp.Common.Enums;
using TeeSharp.Network;

namespace TeeSharp.Common.Protocol
{
    public class GameMsg_ClSetSpectatorMode : BaseGameMessage, IClampedMaxClients
    {
        public override GameMessage Type => GameMessage.ClientSetSpectatorMode;

        public SpectatorMode SpectatorMode { get; set; }
        public int SpectatorId { get; set; }

        public override bool PackError(MsgPacker packer)
        {
            packer.AddInt((int) SpectatorMode);
            packer.AddInt(SpectatorId);
            return packer.Error;
        }

        public override bool UnPackError(UnPacker unpacker, ref string failedOn)
        {
            SpectatorMode = (SpectatorMode) unpacker.GetInt();
            SpectatorId = unpacker.GetInt();

            if (SpectatorMode < 0 || SpectatorMode >= SpectatorMode.NumModes)
                failedOn = nameof(SpectatorMode);

            return unpacker.Error;
        }

        public void Validate(int maxClients, ref string failedOn)
        {
            if (SpectatorId < -1 || SpectatorId >= maxClients)
                failedOn = nameof(SpectatorId);
        }
    }
}