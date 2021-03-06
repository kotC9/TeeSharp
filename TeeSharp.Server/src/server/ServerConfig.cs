﻿using System.Collections.Generic;
using TeeSharp.Common.Config;

namespace TeeSharp.Server
{
    public class ServerConfig : Config
    {
        public override void Init(ConfigFlags saveMask)
        {
            base.Init(saveMask);

            AppendVariables(new Dictionary<string, ConfigVariable>()
            {
                { "SvSpoofProtection", new ConfigInt("sv_spoof_protection", 0, 0, 1, ConfigFlags.Server, "Enable spoof protection") },
                { "SvMapWindow", new ConfigInt("sv_map_window", 15, 0, 100, ConfigFlags.Server, "Map downloading send-ahead window") },
                { "SvFastDownload", new ConfigInt("sv_fast_download", 1, 0, 1, ConfigFlags.Server, "Enables fast download of maps") },
                { "SvNetlimit", new ConfigInt("sv_netlimit", 0, 0, 10000, ConfigFlags.Server, "Netlimit: Maximum amount of traffic a client is allowed to use (in kb/s)") },
                { "SvNetlimitAlpha", new ConfigInt("sv_netlimit_alpha", 50, 1, 100, ConfigFlags.Server, "Netlimit: Alpha of Exponention moving average") },
                { "SvMapUpdateRate", new ConfigInt("sv_mapupdaterate", 5, 1, 100, ConfigFlags.Server, "(Tw32) real id <-> vanilla id players map update rate") },
                { "SvReservedSlots", new ConfigInt("sv_reserved_slots", 0, 0, 16, ConfigFlags.Server, "The number of slots that are reserved for special players") },
                { "SvReservedSlotsPass", new ConfigString("sv_reserved_slots_pass", 32, "", ConfigFlags.Server, "The password that is required to use a reserved slot") },

                { "SvAllowOldClients", new ConfigInt("sv_allow_old_clients", 1, 0, 1, ConfigFlags.Server, "Allow clients to connect that do not support the anti-spoof protocol (this presents a DoS risk)") },
                { "SvOldClientsPerInterval", new ConfigInt("sv_old_clients_per_interval", 5, 0, int.MaxValue, ConfigFlags.Server, "Maximum number of clients that can connect per interval set by `sv_old_clients_interval`") },
                { "SvOldClientsInterval", new ConfigInt("sv_old_clients_interval", 20, 0, int.MaxValue, ConfigFlags.Server, "Interval (in seconds) in which `sv_old_clients_per_interval` clients are allowed to connect") },
                { "SvOldClientsSkip", new ConfigInt("sv_old_clients_skip", 20, 0, int.MaxValue, ConfigFlags.Server, "How many legacy connection attempts to ignore before sending a legacy handshake despite the rate limit being hit") },

                { "SvName", new ConfigString("sv_name", 128, "TeeSharp server", ConfigFlags.Server, "Server name") },
                { "SvHostname", new ConfigString("sv_hostname", 128, "", ConfigFlags.Save | ConfigFlags.Server, "Server hostname") },

                { "SvPort", new ConfigInt("sv_port", 8303, 0, 65535, ConfigFlags.Server, "Port to use for the server") },
                { "SvExternalPort", new ConfigInt("sv_external_port", 0, 0, 0, ConfigFlags.Server, "External port to report to the master servers") },
                { "SvMap", new ConfigString("sv_map", 128, "dm1", ConfigFlags.Server, "Map to use on the server") },
                { "SvMaxClients", new ConfigInt("sv_max_clients", 64, 1, 64, ConfigFlags.Server, "Maximum number of clients that are allowed on a server") },
                { "SvMaxClientsPerIP", new ConfigInt("sv_max_clients_per_ip", 64, 1, 64, ConfigFlags.Server, "Maximum number of clients with the same IP that can connect to the server") },
                { "SvMapDownloadSpeed", new ConfigInt("sv_map_download_speed", 2, 1, 16, ConfigFlags.Save | ConfigFlags.Server, "Number of map data packages a client gets on each request") },
                { "SvHighBandwidth", new ConfigInt("sv_high_bandwidth", 0, 0, 1, ConfigFlags.Server, "Use high bandwidth mode. Doubles the bandwidth required for the server. LAN use only") },
                { "SvRegister", new ConfigInt("sv_register", 1, 0, 1, ConfigFlags.Server, "Register server with master server for public listing") },
                { "SvRconPassword", new ConfigString("sv_rcon_password", 32, "", ConfigFlags.Server, "Remote console password (full access)") },
                { "SvRconModPassword", new ConfigString("sv_rcon_mod_password", 32, "", ConfigFlags.Server, "Remote console password for moderators (limited access)") },
                { "SvRconMaxTries", new ConfigInt("sv_rcon_max_tries", 5, 0, 100, ConfigFlags.Server, "Maximum number of tries for remote console authentication") },
                { "SvRconBantime", new ConfigInt("sv_rcon_bantime", 0, 0, 1440, ConfigFlags.Server, "The time a client gets banned if remote console authentication fails. 0 makes it just use kick") },
                { "SvAutoDemoRecord", new ConfigInt("sv_auto_demo_record", 0, 0, 1, ConfigFlags.Server, "Automatically record demos") },
                { "SvAutoDemoMax", new ConfigInt("sv_auto_demo_max", 10, 0, 1000, ConfigFlags.Server, "Maximum number of automatically recorded demos (0 = no limit)") },

                { "SvWarmup", new ConfigInt("sv_warmup", 0, 0, 0, ConfigFlags.Server, "Number of seconds to do warmup before round starts") },
                { "SvMotd", new ConfigString("sv_motd", 900, "", ConfigFlags.Server, "Message of the day to display for the clients") },
                { "SvTeamdamage", new ConfigInt("sv_teamdamage", 0, 0, 1, ConfigFlags.Server, "Team damage") },
                { "SvMaprotation", new ConfigString("sv_maprotation", 768, "", ConfigFlags.Server, "Maps to rotate between") },

                { "SvMatchesPerMap", new ConfigInt("sv_matches_per_map", 1, 1, 100, ConfigFlags.Save | ConfigFlags.Server, "Number of matches on each map before rotating") },
                { "SvMatchSwap", new ConfigInt("sv_match_swap", 1, 0, 1, ConfigFlags.Server, "Swap teams between matches") },
                { "SvPowerups", new ConfigInt("sv_powerups", 1, 0, 1, ConfigFlags.Server, "Allow powerups like ninja") },
                { "SvScorelimit", new ConfigInt("sv_scorelimit", 20, 0, 1000, ConfigFlags.Save | ConfigFlags.Server, "Score limit (0 disables)") },
                { "SvTimelimit", new ConfigInt("sv_timelimit", 0, 0, 1000, ConfigFlags.Server, "Time limit in minutes (0 disables)") },
                { "SvGametype", new ConfigString("sv_gametype", 32, "dm", ConfigFlags.Server, "Game type (dm, tdm, ctf)") },
                { "SvTournamentMode", new ConfigInt("sv_tournament_mode", 0, 0, 2, ConfigFlags.Save | ConfigFlags.Server, "Tournament mode. When enabled, players joins the server as spectator (2=additional restricted spectator chat)") },
                { "SvPlayerReadyMode", new ConfigInt("sv_player_ready_mode", 0, 0, 1, ConfigFlags.Save | ConfigFlags.Server, "When enabled, players can pause/unpause the game and start the game on warmup via their ready state") },
                { "SvSpamprotection", new ConfigInt("sv_spamprotection", 1, 0, 1, ConfigFlags.Server, "Spam protection") },

                { "SvRespawnDelayTDM", new ConfigInt("sv_respawn_delay_tdm", 3, 0, 10, ConfigFlags.Server, "Time needed to respawn after death in tdm gametype") },

                //{ "SvSpectatorSlots", new ConfigInt("SvSpectatorSlots", "sv_spectator_slots", 0, 0, 64, ConfigFlags.Server, "Number of slots to reserve for spectators") },
                { "SvPlayerSlots", new ConfigInt("sv_player_slots", 8, 0, 64 /* TODO */, ConfigFlags.Save | ConfigFlags.Server, "Number of slots to reserve for players") },
                { "SvSkillLevel", new ConfigInt("sv_skill_level", 1, 0, 2 /* TODO */, ConfigFlags.Save | ConfigFlags.Server, "Supposed player skill level") },
                { "SvTeambalanceTime", new ConfigInt("sv_teambalance_time", 1, 0, 1000, ConfigFlags.Server, "How many minutes to wait before autobalancing teams") },
                { "SvInactiveKickTime", new ConfigInt("sv_inactivekick_time", 3, 0, 1000, ConfigFlags.Server, "How many minutes to wait before taking care of inactive players") },
                { "SvInactiveKick", new ConfigInt("sv_inactivekick", 1, 0, 2, ConfigFlags.Server, "How to deal with inactive players (0=move to spectator, 1=move to free spectator slot/kick, 2=kick)") },

                { "SvSilentSpectatorMode", new ConfigInt("sv_silent_spectator_mode", 1, 0, 1, ConfigFlags.Save | ConfigFlags.Server, "Mute join/leave message of spectator") },

                { "SvStrictSpectateMode", new ConfigInt("sv_strict_spectate_mode", 0, 0, 1, ConfigFlags.Server, "Restricts information in spectator mode") },
                { "SvVoteSpectate", new ConfigInt("sv_vote_spectate", 1, 0, 1, ConfigFlags.Server, "Allow voting to move players to spectators") },
                { "SvVoteSpectateRejoindelay", new ConfigInt("sv_vote_spectate_rejoindelay", 3, 0, 1000, ConfigFlags.Server, "How many minutes to wait before a player can rejoin after being moved to spectators by vote") },
                { "SvVoteKick", new ConfigInt("sv_vote_kick", 1, 0, 1, ConfigFlags.Server, "Allow voting to kick players") },
                { "SvVoteKickMin", new ConfigInt("sv_vote_kick_min", 0, 0, 64, ConfigFlags.Server, "Minimum number of players required to start a kick vote") },
                { "SvVoteKickBantime", new ConfigInt("sv_vote_kick_bantime", 5, 0, 1440, ConfigFlags.Server, "The time to ban a player if kicked by vote. 0 makes it just use kick") },
            });
        }
    }
}