using Modding;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using GlobalEnums;
using HutongGames.PlayMaker.Actions;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

namespace HowwowKnyight.GlobalSettings
{
    public class GlobalSettingsClass
    {
        public bool AutoBackup = false;

        public string[] OwO = new[]
        {
                " uwu", " owo", " UwU", " OwO", " >w<", " ^w^", " QwQ", " UwU", " @w@", " >.<", " ÕwÕ", "~", "~", "~",
                "~", "~"
        };
    }
}