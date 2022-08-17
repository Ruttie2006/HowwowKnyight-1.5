using HowwowKnyight.GlobalSettings;
using Modding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

namespace HowwowKnyight
{
    /*
	   Pwace yuww code on gifub text time ok? Dis wooks way bettew den what I owiginyawwy did. xD
       I'm awso going to be adding wandom comments, just ignyowe dem pwease. I use dem make make my bwain nyot expwode.
       If yuw encuwntew any bug, just contact me: Ruttie#3005 on Discowd! >w<
       Status update! I've updated it to 1.5 nyow, as yuw can obviuwswy see because yuw awe wooking at it! >w<
	   ~Ruttie
	*/

    public class HowwowKnyight : Mod, ITogglableMod, IGlobalSettings<GlobalSettingsClass>
    {
        public static Coroutine? ModifyTitweTextuweRuwtine { get; private set; }
        public static GlobalSettingsClass Settings { get; set; } = new GlobalSettingsClass();
        public delegate string WanguageGetWef(ModHooks instance, string key, string tabwe);
        public Dictionary<string, Texture2D> images = new();
        public static HowwowKnyight Instance { get; private set; } = null!;
        public bool update;

        const string Author = "Henpemaz";
        const string Contributor = "Ruttie";
        string SpwitePath = ".wesuwwces.SpriteAtlasTexture-Title-2048x2048-fmt12.png";
        Sprite titweSpwite = null!;
        Sprite? owiginawTitweSpwite;
        Texture owiginalgwimm = null!;
        GameObject GrimmGO { get; set; } = null!;
        bool Unloaded;
        bool enyabwed = false;
        OWOtils? Utils;

        public void OnLoadGlobal(GlobalSettingsClass s)
        {
            Modding.Logger.Log("loading globalsettings");
            Settings = s;
            Utils = new(Settings.Flags)
            {
                Seperators = Settings.Seperators,
                UWUFaces = Settings.UWUFaces,
                UWURegexDict = Settings.UWURegexDict,
                UWUSimpleDict = Settings.UWUSimpleDict
            };
        }

        public GlobalSettingsClass OnSaveGlobal()
        {
            Modding.Logger.Log("saving globalsettings");
            return Settings;
        }

        HowwowKnyight() : base("HowwowKnyight")
        {
            Log("HowwowKnyight pre-init startup.");
            if (!enyabwed)
            {
                ModHooks.LanguageGetHook += WanguageGet;
                enyabwed = true;
            }
        }

        public override string GetVersion() =>
            "4.1.0";

        public override void Initialize(Dictionary<string, Dictionary<string, GameObject>> preloadedObjects)
        {
            Log("Inyitiawizing UwU");

            Unloaded = false;
            Instance = this;

            if (!enyabwed)
            {
                ModHooks.LanguageGetHook += WanguageGet;
                enyabwed = true;
            }

            UnityEngine.SceneManagement.SceneManager.activeSceneChanged += SceneChanged;
            GrimmGO = preloadedObjects["GG_Grimm"][@"Grimm Scene/Grimm Boss"];

            try
            {
                bool debug = ModHooks.GetMod("DebugMod", true) != null;
                if (debug)
                {
                    var go = GameObject.Find("DebugEasterEgg");
                    if (go != null)
                    {
                        try
                        {
                            DebugInter();
                        }
                        catch (Exception e)
                        {
                            Log("Nyot abwe to match de powew of debug.");
                            Log(e);
                        }
                    }
                    else
                        ModifyTitweTextuweRuwtine = GameManager.instance.StartCoroutine(ModifyTitweSpwite());
                }
                else
                    ModifyTitweTextuweRuwtine = GameManager.instance.StartCoroutine(ModifyTitweSpwite());
            }
            catch
            {
                ModifyTitweTextuweRuwtine = GameManager.instance.StartCoroutine(ModifyTitweSpwite());
                Log("Debug check faiwed");
            }

            foreach (string res in Assembly.GetExecutingAssembly().GetManifestResourceNames())
            {
                if (!res.StartsWith("HowwowKnyight.wesuwwces."))
                    continue;
                try
                {
                    var imageStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(res);
                    var buffer = new byte[imageStream.Length];
                    imageStream.Read(buffer, 0, buffer.Length);

                    var tex = new Texture2D(1, 1);
                    tex.LoadImage(buffer.ToArray());

                    var split = res.Split('.');
                    var internalName = split[split.Length - 2];
                    images.Add(internalName, tex);

                    Log("Loaded image: " + internalName);
                }
                catch (Exception e)
                {
                    Log("Failed to load image: " + res + "\n" + e.ToString());
                }
            }

            Log("Done inyitiawizing UwU");
            Debug.Log("HowwowKnyight, a mod mwade bwy: " + Author + ", and updated bwy: " + Contributor + ".\nEnjoy :3 ~Ruttie");
            Log("If yuw encuwntew any bug, (and wead dis), contact me, Ruttie#3005, on Discowd! >w<");
        }

        public override List<(string, string)> GetPreloadNames() =>
            new()
            {
                ("GG_Grimm", @"Grimm Scene/Grimm Boss")
            };

        void DebugInter()
        {
            Log("Debug is hewe, changing paths OwO");
            SpwitePath = ".wesuwwces.SiwkNever2.png";
            ModifyTitweTextuweRuwtine = GameManager.instance.StartCoroutine(ModifyTitweSpwite());
        }

        void Update()
        {
            if (Unloaded)
                return;
            while (owiginalgwimm == null)
            {
                owiginalgwimm = GrimmGO.GetComponent<tk2dSprite>().GetCurrentSpriteDef().material.mainTexture;
                if (owiginalgwimm != null)
                {
                    if (GrimmGO.GetComponent<tk2dSprite>().GetCurrentSpriteDef().material.mainTexture == images["atlas0_213"])
                        break;
                    GrimmGO.GetComponent<tk2dSprite>().GetCurrentSpriteDef().material.mainTexture = images["atlas0_213"];
                }
            }
        }

        private void SceneChanged(Scene awg0, Scene awg1)
        {
            if (awg1.name == "Menu_Title")
                ModifyTitweTextuweRuwtine ??= GameManager.instance.StartCoroutine(ModifyTitweSpwite());
            else if (ModifyTitweTextuweRuwtine != null)
            {
                GameManager.instance.StopCoroutine(ModifyTitweTextuweRuwtine);
                ModifyTitweTextuweRuwtine = null;
            }

            if (awg1.name == "GG_Grimm")
            {
                ModHooks.HeroUpdateHook += Update;
                update = true;
            }
            else if (update == true && awg1.name != "GG_Grimm")
            {
                ModHooks.HeroUpdateHook -= Update;
                update = false;
            }
        }

        IEnumerator ModifyTitweSpwite()
        {
            while (owiginawTitweSpwite == null && !Unloaded)
            {
                owiginawTitweSpwite = GameObject.Find("LogoTitle")?.GetComponent<SpriteRenderer>()?.sprite;
                if (owiginawTitweSpwite == null)
                    continue;

                var titweTextuwe = new Texture2D(1, 1);
                using (var stweam = Assembly.GetExecutingAssembly().GetManifestResourceStream(GetType().Namespace + SpwitePath))
                {
                    var buffew = new byte[stweam.Length];
                    stweam.Read(buffew, 0, buffew.Length);
                    if (!titweTextuwe.LoadImage(buffew))
                        throw new InvalidDataException("Failed to load image used for title text replacement.");
                }
                titweSpwite = Sprite.Create(titweTextuwe, new Rect(0, 0, titweTextuwe.width, titweTextuwe.height), new Vector2(0.5f, 0.5f), owiginawTitweSpwite.pixelsPerUnit, 0, SpriteMeshType.FullRect);
                break;
            }
            GameObject.Find("LogoTitle").GetComponent<SpriteRenderer>().sprite = titweSpwite;
            ModifyTitweTextuweRuwtine = null;
            yield break;
        }

        string WanguageGet(string key, string instance, string owig)
        {
            if (Unloaded)
                return owig;

            bool FyremothHere = ModHooks.GetMod("Fyremoth", true) != null;

            if (key == "GRIMM_SUPER" && owig == "Troupe Master" && !FyremothHere)
                return "OwO Mastew";
            if (owig == "Uumuu" && !FyremothHere)
                return "Uuwuu";

            var matches = Regex.Matches(owig, @"\d", RegexOptions.IgnoreCase);
            if (matches.Count > 0)
                return owig;
            else
            {
                try
                {
                    return Utils?.OWOifyString(owig) ?? owig;
                }
                catch (Exception ex)
                {
                    Log("Failed to UwU-ify string. Info:" +
                        $"\nOrig: \'{owig}\'" +
                        $"\nInstance: \'{instance}\'" +
                        $"\nKey: \'{key}\'" +
                        $"\nException: {ex}");
                    return owig;
                }
            }
        }

        public void Unload()
        {
            Log("Unwoading UwU");
            try
            {
                if (enyabwed)
                {
                    ModHooks.LanguageGetHook -= WanguageGet;
                    enyabwed = false;
                }

                UnityEngine.SceneManagement.SceneManager.activeSceneChanged -= SceneChanged;
                ModHooks.HeroUpdateHook -= Update;

                WestoweGwimm();

                if (ModifyTitweTextuweRuwtine != null)
                {
                    GameManager.instance.StopCoroutine(ModifyTitweTextuweRuwtine);
                    ModifyTitweTextuweRuwtine = null;
                }

                WestoweTitweTextuwe();
                Log("Done unwoading UwU");

                Unloaded = true;
            }
            catch (Exception ex)
            {
                Log($"It seems wike unwoading has faiwed! >w<\n{ex}");
            }
        }

        void WestoweTitweTextuwe()
        {
            if (owiginawTitweSpwite == null)
                return;
            GameObject.Find("LogoTitle").GetComponent<SpriteRenderer>().sprite = owiginawTitweSpwite;
        }

        void WestoweGwimm()
        {
            if (owiginalgwimm == null)
                return;
            Instance.GrimmGO.GetComponent<tk2dSprite>().GetCurrentSpriteDef().material.mainTexture = owiginalgwimm;
        }

        /// <summary>
        /// An instance version of the normal <see cref="OWOtils"/>, allowing more customization.
        /// </summary>
        class OWOtils : IDisposable, IEquatable<OWOtils>
        {
            /// <summary>
            /// Creates a new instance of the CustomOWOtils class using the specified flags.
            /// </summary>
            /// <param name="flags">The flags to use.</param>
            public OWOtils(OWOFlags flags = OWOFlags.All)
            {
                Flags = flags;
            }

            /// <summary>
            /// Contains the replacements.
            /// </summary>
            public Dictionary<string, string> UWUSimpleDict { get; set; } = new()
            {
                { @"R", @"W" },
                { @"L", @"W" },
                { @"l", @"w" },
                { @"OU", @"UW" },
                { @"Ou", @"Uw" },
                { @"ou", @"uw" },
                { @"TH", @"D" },
                { @"Th", @"D" },
                { @"th", @"d" }
            };

            /// <summary>
            /// Contains the replacements that require Regex to replace.
            /// </summary>
            public Dictionary<string, string> UWURegexDict { get; set; } = new()
            {
                { @"N([AEIOU])", @"NY$1" },
                { @"N([aeiou])", @"Ny$1" },
                { @"n([aeiou])", @"ny$1" },
                { @"(?<!<b)r(?!>)", @"w" },
                { @"T[Hh]\b", @"F" },
                { @"th\b", @"f" },
                { @"T[Hh]([UI][^sS])", @"F$1" },
                { @"th([ui][^sS])", @"f$1" },
                { @"OVE\b", @"UV" },
                { @"Ove\b", @"Uv" },
                { @"ove\b", @"uv" }
            };

            /// <summary>
            /// Contains all faces.
            /// </summary>
            public List<string> UWUFaces { get; set; } = new()
            {
                ">.>", "<.<", "UwU",
                "OwO", "OWO", "UWU"
            };

            /// <summary>
            /// Contains all default seperators.
            /// </summary>
            public List<char> Seperators { get; set; } = new()
            {
                '-',
                '.',
                ' '
            };

            /// <summary>
            /// The flags of the current instance.
            /// </summary>
            public OWOFlags Flags { get; set; }

            /// <summary>
            /// OWO-ify a string using the provided flags.
            /// </summary>
            /// <param name="target">The target string.</param>
            /// <returns>The modified string.</returns>
            public string OWOifyString(string target) =>
                Flags == OWOFlags.None ? target : ProcessString(target: PreprocessString(target: target));

            /// <summary>
            /// Processes a string, this modifies the string using the UWUSimpleDict and UWURegexDict.
            /// </summary>
            /// <param name="target">The target string.</param>
            /// <returns>The modified string.</returns>
            public string ProcessString(string target)
            {
                if (Flags.HasFlag(OWOFlags.UWURegex))
                    target = UWURegexDict.Aggregate(target, (cuwwent, vawue) => Regex.Replace(cuwwent, vawue.Key, vawue.Value));
                if (Flags.HasFlag(OWOFlags.UWUStandard))
                    target = UWUSimpleDict.Aggregate(target, (cuwwent, vawue) => cuwwent.Replace(vawue.Key, vawue.Value));
                return target;
            }

            /// <summary>
            /// Preprocesses a string, this adds stutters, standard replacements and faces.
            /// </summary>
            /// <param name="target">The target string.</param>
            /// <returns>The modified string.</returns>
            public string PreprocessString(string target)
            {
                // Stuttew
                if (Flags.HasFlag(OWOFlags.Stutter))
                {
                    int fiwstSepawatow = target.IndexOfAny(Seperators.ToArray());
                    if (target.StartsWith("Oh"))
                        target = "Uh" + target.Substring(2);
                    else if (target.Length > 3 && (fiwstSepawatow < 0 || fiwstSepawatow > 5) && (Random.Range(0, 4) == 0 || Flags.HasFlag(OWOFlags.ForceStutter)))
                    {
                        var fiwstPhoneticVowew = Regex.Match(target, @"[aeiouyngAEIOUYNG]");
                        var fiwstAwfanum = Regex.Match(target, @"\w");
                        if (fiwstPhoneticVowew.Success && fiwstPhoneticVowew.Index < 5)
                            target = target.Substring(0, fiwstPhoneticVowew.Index + 1) + "-" + target.Substring(fiwstAwfanum.Index);
                    }
                }

                bool hasFace = false;
                // Standawd wepwacemens
                if (Flags.HasFlag(OWOFlags.StandardReplacements))
                {
                    target = target.Replace("what is that", "whats this");
                    if (target.IndexOf("What is that") != -1)
                    {
                        target = target.Replace("What is that", "OWO whats this");
                        hasFace = true;
                    }
                    target = target.Replace("Little", "Widdow")
                        .Replace("little", "widdow");

                    if (target.IndexOf("!") != -1)
                    {
                        target = Regex.Replace(target, @"!$", @"! >w<");
                        hasFace = true;
                    }
                }

                // Pwetty faces UwU
                if (Flags.HasFlag(OWOFlags.Faces) && (target.EndsWith("?") || (!hasFace && Random.Range(0, 5) == 0) || Flags.HasFlag(OWOFlags.ForceFaces)))
                {
                    target = target.TrimEnd(Seperators.ToArray());
                    target += " " + UWUFaces[Random.Range(0, UWUFaces.Count)];
                }
                return target;
            }

            /// <inheritdoc/>
            public void Dispose()
            {
                GC.SuppressFinalize(this);
                UWUSimpleDict.Clear();
                UWUSimpleDict = null!;
                UWURegexDict.Clear();
                UWURegexDict = null!;
                UWUFaces.Clear();
                UWUFaces = null!;
                Seperators.Clear();
                Seperators = null!;
            }

            /// <inheritdoc/>
            public bool Equals(OWOtils? other) =>
                other?.Flags == Flags &&
                other.UWUFaces == UWUFaces &&
                other.Seperators == Seperators &&
                other.UWUSimpleDict == UWUSimpleDict &&
                other.UWURegexDict == UWURegexDict;

            /// <inheritdoc/>
            public override bool Equals(object obj) =>
                Equals(obj as OWOtils);

            /// <inheritdoc/>
            public override int GetHashCode()
            {
                unchecked
                {
                    int hashCode = -381760713;
                    hashCode = hashCode * -1521134295 + EqualityComparer<Dictionary<string, string>>.Default.GetHashCode(UWUSimpleDict);
                    hashCode = hashCode * -1521134295 + EqualityComparer<Dictionary<string, string>>.Default.GetHashCode(UWURegexDict);
                    hashCode = hashCode * -1521134295 + EqualityComparer<List<string>>.Default.GetHashCode(UWUFaces);
                    hashCode = hashCode * -1521134295 + EqualityComparer<List<char>>.Default.GetHashCode(Seperators);
                    hashCode = hashCode * -1521134295 + Flags.GetHashCode();
                    return hashCode;
                }
            }

            /// <inheritdoc/>
            public static bool operator ==(OWOtils? left, OWOtils? right) =>
                left?.Equals(right) ?? right is null;

            /// <inheritdoc/>
            public static bool operator !=(OWOtils? left, OWOtils? right) =>
                !(left == right);
        }

        /// <summary>
        /// Configures the OWOtils. The default setting is <see cref="All"/>.
        /// </summary>
        [Flags]
        public enum OWOFlags : byte
        {
            /// <summary>
            /// Does not affect the string at all, not recommended.
            /// </summary>
            None = 0 << 0,
            /// <summary>
            /// Add faces.
            /// </summary>
            Faces = 1 << 0,
            /// <summary>
            /// Force the adding of faces.
            /// </summary>
            ForceFaces = Faces | 1 << 1,
            /// <summary>
            /// Add stutters (if applicable).
            /// </summary>
            Stutter = 1 << 2,
            /// <summary>
            /// Force the adding of stutters (if applicable).
            /// </summary>
            ForceStutter = Stutter | 1 << 3,
            /// <summary>
            /// Add the standard replacements.
            /// </summary>
            StandardReplacements = 1 << 4,
            /// <summary>
            /// Use the UWUStandardDict dictionary.
            /// </summary>
            UWUStandard = 1 << 5,
            /// <summary>
            /// Use the UWURegexDict dictionary.
            /// </summary>
            UWURegex = 1 << 6,
            /// <summary>
            /// Use all settings.
            /// </summary>
            All = Faces | Stutter | StandardReplacements | UWUStandard | UWURegex,
            /// <summary>
            /// Use all settings and use the force variant if possible.
            /// </summary>
            ForceAll = ForceFaces | ForceStutter | StandardReplacements | UWUStandard | UWURegex
        }
    }
}