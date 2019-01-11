﻿using System;
using System.Collections.Generic;
using System.IO;
using TeeSharp.Common.Config;
using TeeSharp.Common.Storage;
using TeeSharp.Core;

namespace TeeSharp.Common.Console
{
    public class GameConsole : BaseGameConsole
    {
        public override ConsoleCommand this[string command]
        {
            get
            {
                if (Commands.ContainsKey(command))
                    return Commands[command];
                throw new Exception("Command not found");
            }
        }

        public GameConsole()
        {
            PrintCallbacks = new List<PrintCallbackInfo>();
            ExecutedFiles = new List<string>();
            Commands = new Dictionary<string, ConsoleCommand>();
        }

        public override void Init()
        {
            Storage = Kernel.Get<BaseStorage>();
            Config = Kernel.Get<BaseConfig>();

            foreach (var pair in Config)
            {
                if (pair.Value is ConfigInt intCfg)
                {
                    AddCommand(
                        intCfg.ConsoleCommand, "?i", 
                        intCfg.Description, 
                        intCfg.Flags, 
                        IntVariableCommand, 
                        intCfg);
                }
                else if (pair.Value is ConfigString strCfg)
                {
                    AddCommand(strCfg.ConsoleCommand, "?s", 
                        strCfg.Description, 
                        strCfg.Flags, 
                        StrVariableCommand,
                        strCfg);
                }
            }
        }

        public override void AddCommand(string cmd, string format, string description, ConfigFlags flags, CommandCallback callback, object data = null)
        {
            if (Commands.ContainsKey(cmd))
            {
                Debug.Warning("console", $"Command {cmd} already exist");
                return;
            }

            var command = new ConsoleCommand(cmd, format, description, flags, data);
            command.Executed += callback;
            Commands.Add(cmd, command);
        }

        public override PrintCallbackInfo RegisterPrintCallback(OutputLevel outputLevel, 
            PrintCallback callback, object data = null)
        {
            var info = new PrintCallbackInfo
            {
                OutputLevel = outputLevel,
                Callback = callback,
                Data = data
            };

            PrintCallbacks.Add(info);
            return info;
        }

        public override ConsoleCommand FindCommand(string cmd, ConfigFlags mask)
        {
            if (string.IsNullOrWhiteSpace(cmd))
                return null;

            cmd = cmd.ToLower();
            return Commands.ContainsKey(cmd) && (Commands[cmd].Flags & mask) != 0
                ? Commands[cmd]
                : null;
        }

        protected override void StrVariableCommand(ConsoleCommandResult commandResult, object data)
        {
            if (commandResult.NumArguments != 0)
                ((ConfigString) data).Value = (string) commandResult[0];
            else
                Print(OutputLevel.Standard, "console", $"Value: {((ConfigString) data).Value}");
        }

        protected override void IntVariableCommand(ConsoleCommandResult commandResult, object data)
        {
            if (commandResult.NumArguments != 0)
                ((ConfigInt) data).Value = (int) commandResult[0];
            else
                Print(OutputLevel.Standard, "console", $"Value: {((ConfigInt) data).Value}");
        }

        protected override bool ParseLine(string line, out ConsoleCommandResult commandResult, out ConsoleCommand command, out string parsedCmd)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                commandResult = null;
                command = null;
                parsedCmd = null;
                return false;
            }

            line = line.TrimStart();
            var space = line.IndexOf(' ');
            parsedCmd = space > 0 ? line.Substring(0, space) : line;

            if (!Commands.TryGetValue(parsedCmd, out command))
            {
                commandResult = null;
                return false;
            }

            var args = string.Empty;
            if (space > 0 && space + 1 < line.Length)
                args = line.Substring(space + 1);

            commandResult = new ConsoleCommandResult(args);
            return true;
        }

        public override void ExecuteFile(string fileName, bool forcibly = false)
        {
            if (!forcibly && ExecutedFiles.Contains(Path.GetFileName(fileName)))
            {
                return;
            }

            using (var file = Storage.OpenFile(fileName, FileAccess.Read))
            {
                if (file == null)
                {
                    Print(OutputLevel.Standard, "console", $"failed to open '{fileName}'");
                    return;
                }

                ExecutedFiles.Add(fileName);
                using (var reader = new StreamReader(file))
                {
                    Print(OutputLevel.Standard, "console", $"executing '{fileName}'");
                    string currentLine;

                    while (!string.IsNullOrWhiteSpace(currentLine = reader.ReadLine()))
                        ExecuteLine(currentLine);
                }
            }
        }

        public override void ParseArguments(string[] args)
        {
            for (int i = 0; i < args.Length; i++)
            {
                if (args[i] == "-f")
                {
                    if (i + 1 < args.Length)
                        ExecuteFile(args[i + 1]);
                    i++;
                }
                else
                {
                    ExecuteLine(args[i]);
                }
            }
        }

        public override void ExecuteLine(string line)
        {
            if (ParseLine(line, out var result, out var command, out var parsedCmd))
            {
                if (result.ParseArguments(command.Format))
                {
                    command.Invoke(result);
                }
                else
                {
                    Print(OutputLevel.Standard, "console", 
                        $"Invalid arguments... Usage: {command.Cmd} {command.Format}");
                }
            }
            else
            {
                if (string.IsNullOrWhiteSpace(parsedCmd))
                    return;

                Print(OutputLevel.Standard, "console", $"No such command: {parsedCmd}.");
            }
        }

        public override void Print(OutputLevel outputLevel, string sys, string format)
        {
            Debug.Log(sys, format);

            for (var i = 0; i < PrintCallbacks.Count; i++)
            {
                if (PrintCallbacks[i] != null &&
                    PrintCallbacks[i].OutputLevel >= outputLevel)
                {
                    PrintCallbacks[i]?.Callback($"[{sys}]: {format}", PrintCallbacks[i].Data);
                }
            }
        }
    }
}