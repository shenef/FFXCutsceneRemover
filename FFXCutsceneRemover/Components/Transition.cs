﻿using FFX_Cutscene_Remover.ComponentUtil;
using System;
using System.Diagnostics;

namespace FFXCutsceneRemover
{
    class Transition 
    {
        private readonly MemoryWatchers memoryWatchers = MemoryWatchers.Instance;
        private readonly Process process;

        public bool ForceLoad = true;
        /* Only add members here for memory addresses that we want to write the value to.
         * If we only ever read the value then there is no need to add it here. */
        public short? RoomNumber = null;
        public short? Storyline = null;
        public short? SpawnPoint = null;
        public int? BattleState = null;
        public byte? Menu = null;
        public short? Intro = null;
        public short? FangirlsOrKidsSkip = null;
        public sbyte? State = null;
        public float? XCoordinate = null;
        public float? YCoordinate = null;
        public byte? Camera = null;
        public float? CameraRotation = null;
        public byte? EncounterStatus = null;
        public byte? MovementLock = null;
        public byte? MusicId = null;
        public byte? CutsceneAlt = null;
        public byte? AirshipDestinations = null;
        public short? AuronOverdrives = null;
        public byte? PartyMembers = null;
        public byte? Sandragoras = null;
        public int? HpEnemyA = null;
        public byte? GuadoCount = null;

        public Transition()
        {
            process = memoryWatchers.Process;
        }

        public void Execute()
        {
            memoryWatchers.Watchers.UpdateAll(process);

            WriteValue(memoryWatchers.RoomNumber, RoomNumber);
            WriteValue(memoryWatchers.Storyline, Storyline);
            WriteValue(memoryWatchers.SpawnPoint, SpawnPoint);
            WriteValue(memoryWatchers.BattleState, BattleState);
            WriteValue(memoryWatchers.Menu, Menu);
            WriteValue(memoryWatchers.Intro, Intro);
            WriteValue(memoryWatchers.FangirlsOrKidsSkip, FangirlsOrKidsSkip);
            WriteValue(memoryWatchers.State, State);
            WriteValue(memoryWatchers.XCoordinate, XCoordinate);
            WriteValue(memoryWatchers.YCoordinate, YCoordinate);
            WriteValue(memoryWatchers.Camera, Camera);
            WriteValue(memoryWatchers.CameraRotation, CameraRotation);
            WriteValue(memoryWatchers.EncounterStatus, EncounterStatus);
            WriteValue(memoryWatchers.MovementLock, MovementLock);
            WriteValue(memoryWatchers.MusicId, MusicId);
            WriteValue(memoryWatchers.CutsceneAlt, CutsceneAlt);
            WriteValue(memoryWatchers.AirshipDestinations, AirshipDestinations);
            WriteValue(memoryWatchers.AuronOverdrives, AuronOverdrives);
            WriteValue(memoryWatchers.PartyMembers, PartyMembers);
            WriteValue(memoryWatchers.Sandragoras, Sandragoras);
            WriteValue(memoryWatchers.HpEnemyA, HpEnemyA);
            WriteValue(memoryWatchers.GuadoCount, GuadoCount);

            if (ForceLoad)
            {
                ForceGameLoad();
            }
        }

        private void WriteValue<T>(MemoryWatcher watcher, T? value) where T : struct
        {
            if (value.HasValue)
            {
                if (watcher.AddrType == MemoryWatcher.AddressType.Absolute)
                {
                    process.WriteValue(watcher.Address, value.Value);
                }
                else
                {
                    IntPtr finalPointer;
                    if (!watcher.DeepPtr.DerefOffsets(process, out finalPointer))
                    {
                        Console.WriteLine("Couldn't read the pointer path for: " + watcher.Name);
                    }
                    process.WriteValue(finalPointer, value.Value);
                }
            }
        }

        private void ForceGameLoad()
        {
            WriteValue<byte>(memoryWatchers.ForceLoad, 1);
        }
    }
}