﻿using TeeSharp.Common;
using TeeSharp.Common.Config;
using TeeSharp.Common.Console;
using TeeSharp.Common.Enums;
using TeeSharp.Common.Protocol;
using TeeSharp.Core;

namespace TeeSharp.Server.Game
{
    public abstract class BaseGameContext : BaseInterface
    {
        public abstract string GameVersion { get; }
        public abstract string NetVersion { get; }
        public abstract string ReleaseVersion { get; }

        public virtual BaseEvents Events { get; protected set; }
        public virtual BasePlayer[] Players { get; protected set; }
        public virtual BaseGameController GameController { get; protected set; }
        public virtual BaseGameWorld World { get; protected set; }
        public virtual BaseGameConsole Console { get; set; }

        public virtual BaseLayers Layers { get; set; }
        public virtual BaseCollision Collision { get; set; }

        protected virtual BaseTuningParams Tuning { get; set; }
        protected virtual BaseConfig Config { get; set; }
        protected virtual BaseServer Server { get; set; }
        protected virtual BaseGameMsgUnpacker GameMsgUnpacker { get; set; }

        protected virtual bool LockTeams { get; set; }

        public abstract void RegisterConsoleCommands();
        public abstract bool IsClientSpectator(int clientId);
        public abstract bool IsClientReady(int clientId);

        public abstract void CheckPureTuning();
        public abstract void SendTuningParams(int clientId);

        public abstract void SendChat(int chatterClientId, bool isTeamChat, string msg);
        public abstract void SendChatTarget(int clientId, string msg);
        public abstract void SendBroadcast(int clientId, string msg);

        public abstract void OnInit();
        public abstract void OnTick();
        public abstract void OnShutdown();
        public abstract void OnMessage(int msgId, Unpacker unpacker, int clientId);
        public abstract void OnBeforeSnapshots();
        public abstract void OnAfterSnapshots();
        public abstract void OnSnapshot(int clientId);
        public abstract void OnClientConnected(int clientId);
        public abstract void OnClientEnter(int clientId);
        public abstract void OnClientDrop(int clientId, string reason);
        public abstract void OnClientPredictedInput(int clientId, SnapObj_PlayerInput input);
        public abstract void OnClientDirectInput(int clientId, SnapObj_PlayerInput input);

        public abstract void CreateExplosion(Vec2 pos, int owner,
            int weapon, bool noDamage);
        public abstract void CreatePlayerSpawn(Vec2 pos);
        public abstract void CreateDeath(Vec2 pos, int clientId);
        public abstract void CreateDamageInd(Vec2 pos, float a, int damage);
        public abstract void CreateHammerHit(Vec2 pos);
        public abstract void CreateSound(Vec2 pos, Sounds sound);
        public abstract void CreateSound(Vec2 pos, Sounds sound, int mask);

        public virtual int MaskAll()
        {
            return -1;
        }

        public virtual int MaskOne(int clientID)
        {
            return 1 << clientID;
        }

        public virtual int MaskAllExceptOne(int clientId)
        {
            return 0b1111111_11111111_11111111_11111111 ^ MaskOne(clientId);
        }

        public virtual bool MaskIsSet(int mask, int clientID)
        {
            return (mask & MaskOne(clientID)) != 0;
        }
    }
}