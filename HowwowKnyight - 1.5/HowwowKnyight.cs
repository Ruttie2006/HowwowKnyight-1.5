#region everything
#region usings
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.IO;

using Modding;
using MonoMod.RuntimeDetour;
using HowwowKnyight.GlobalSettings;

using UnityEngine;
using UnityEngine.SceneManagement;

using System.Text.RegularExpressions;
using System.Security;
using System.Security.Permissions;
#endregion

#region System

//[assembly: IgnoresAccessChecksTo("Assembly-CSharp")]
//[assembly: SecurityPermission(SecurityAction.RequestMinimum, SkipVerification = true)]

[module: UnverifiableCode]
namespace System.Runtime.CompilerServices
{

    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
    public class IgnoresAccessChecksToAttribute : Attribute
    {
        public IgnoresAccessChecksToAttribute(string assemblyName)
        {
            AssemblyName = assemblyName;
        }

        public string AssemblyName { get; }
    }
}

#endregion

#region HowwowKnyight

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


        #region SettingStuff

        #region private
        static string SpwitePath = ".wesuwwces.SpriteAtlasTexture-Title-2048x2048-fmt12.png";
        static string TitweTexturename = "SpriteAtlasTexture-Title-2048x2048-fmt12.png";
        static readonly string Author = "Henpemaz";
        static readonly string Contributor = "Ruttie";
        static Sprite titweSpwite;
        static Sprite owiginawTitweSpwite;

        //private Texture2D _tex;
        private bool enyabwed = false;
        private Hook wanguageGetHook;

        //int[] nums = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 }; //sry RedFrog
        //Regex r = new Regex(@"\d", RegexOptions.IgnoreCase); //go SFG
        #endregion
        #region public
        public static Coroutine ModifyTitweTextuweRuwtine { get; private set;}
        public static GlobalSettingsClass settings { get; set; } = new GlobalSettingsClass();
        public delegate string WanguageGetWef(ModHooks instance, string key, string tabwe);

        #endregion

        public void OnLoadGlobal(GlobalSettingsClass s)
        {
            Modding.Logger.Log("loading globalsettings");
            HowwowKnyight.settings = s;
        }
        public GlobalSettingsClass OnSaveGlobal()
        {
            Modding.Logger.Log("saving globalsettings");
            return HowwowKnyight.settings;
        }

        #endregion


        #region Base and version

        public HowwowKnyight() : base("HowwowKnyight") 
        {
            if (!enyabwed)
            {
                ModHooks.LanguageGetHook += WanguageGet;
                enyabwed = true;
            }
            //ModHooks.LanguageGetHook += WanguageGet;
        }

        public override string GetVersion() => "3.7.2 - 1.5.75";

        #endregion


        #region Init/override

        public override void Initialize()
        {
            Log("Inyitiawizing UwU");
            #region Hooks
            if (!enyabwed)
            {
                ModHooks.LanguageGetHook += WanguageGet;
                enyabwed = true;
            }
            #endregion
            #region DebugInterCaller
            try
            {
				bool debug = false;
				foreach (Assembly asm in AppDomain.CurrentDomain.GetAssemblies())
				{
					string asmName = asm.GetName().Name;
					if (asmName == "DebugMod") 
					{
						debug = true;
						break;
					}
				}
				if (debug)
                {
                    var go = GameObject.Find("DebugEasterEgg");
                    Log("Debug Exists. >w<");
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
                    {
                        Log("Debug GO nyot fuwnd");
                        ModifyTitweTextuweRuwtine = GameManager.instance.StartCoroutine(HowwowKnyight.ModifyTitweSpwite());
                    }
                }
                else
                {
                    Log("Nyo debug in mods woaded");
                    ModifyTitweTextuweRuwtine = GameManager.instance.StartCoroutine(HowwowKnyight.ModifyTitweSpwite());
                }
            }
            catch
            {
                ModifyTitweTextuweRuwtine = GameManager.instance.StartCoroutine(HowwowKnyight.ModifyTitweSpwite());
                Log("Debug check faiwed");
            }
            #endregion
            Log("Done inyitiawizing UwU");
            Log("Mwade bwy: " + Author + ", and fyixed annoying softlock bwy: " + Contributor + ". Enjoy :3 ~Ruttie");
            Log("If yuw encuwntew any bug, (and wead dis), contact me: Ruttie#3005 on Discowd! >w<");
            Log("1.5.75 wewease");
        }

        public override List<(string, string)> GetPreloadNames()
        {
            return new List<(string, string)>();
        }

        #endregion


        #region DebugInter

        public void DebugInter()
        {
            Log("Debug is hewe, changing paths OwO");
            TitweTexturename = "SiwkNever2.png";
            SpwitePath = ".wesuwwces.SiwkNever2.png";
            ModifyTitweTextuweRuwtine = GameManager.instance.StartCoroutine(HowwowKnyight.ModifyTitweSpwite());
        }

        #endregion


        #region TitleScreen

        private static void ActiveSceneChangedHandwer(Scene awg0, LoadSceneMode awg1)
        {
            if (awg0.name == "Menu_Title")
            {
                if (ModifyTitweTextuweRuwtine == null)
                {
                    ModifyTitweTextuweRuwtine = GameManager.instance.StartCoroutine(HowwowKnyight.ModifyTitweSpwite());
                }
            }
            else if(ModifyTitweTextuweRuwtine != null)
            {
                GameManager.instance.StopCoroutine(ModifyTitweTextuweRuwtine);
                ModifyTitweTextuweRuwtine = null;
            }
            if (awg0.name == "")
            {

            }
        }



        static System.Collections.IEnumerator ModifyTitweSpwite()
        {
            yield return null;
            while (true)
            {
                while (owiginawTitweSpwite == null)
                {
                    owiginawTitweSpwite = GameObject.Find("LogoTitle").GetComponent<SpriteRenderer>().sprite;
                    if (owiginawTitweSpwite != null)
                    {
                        Texture2D titweTextuwe = new Texture2D(1, 1);
                        using (Stream stweam = Assembly.GetExecutingAssembly().GetManifestResourceStream(typeof(HowwowKnyight).Namespace + SpwitePath))
                        {
                            byte[] buffew = new byte[stweam.Length];
                            stweam.Read(buffew, 0, buffew.Length);
                            titweTextuwe.LoadImage(buffew, false);
                            titweTextuwe.name = TitweTexturename;
                        }
                        titweSpwite = Sprite.Create(titweTextuwe, new Rect(0, 0, titweTextuwe.width, titweTextuwe.height), new Vector2(0.5f, 0.5f), owiginawTitweSpwite.pixelsPerUnit, 0, SpriteMeshType.FullRect);
                    }
                    yield return null;
                }
                GameObject.Find("LogoTitle").GetComponent<SpriteRenderer>().sprite = titweSpwite;
                yield return null;
            }
        }

        #endregion


        #region WanguageGet

        private string WanguageGet(string key, string instance, string owig)
        {
            bool FyremothHere = false;
            foreach (Assembly asm in AppDomain.CurrentDomain.GetAssemblies())
            {
                string asmName = asm.GetName().Name;
                //Modding.Logger.Log(asmName);
                if (asmName == "Fyremoth")
                {
                    FyremothHere = true;
                }
            }
            if (key == "GRIMM_SUPER" && owig == "Troupe Master" && FyremothHere == false)
            {
                owig = "OwO Mastew";
                return (owig);
            }
            Regex r = new Regex(@"\d", RegexOptions.IgnoreCase); //I hope dis wowks SFG
            MatchCollection matches = r.Matches(owig);
            if (matches.Count > 0)
            {
                return (owig);
            }
            else
            {
                try
                {
                    return UWUfyStwing(PwepwocessDiawoge(owig));
                }
                catch
                {
                    Modding.Logger.Log("Something terrible has happened! Please contact me with this info!");
                    return (owig);
                }
            }
        }

        
        #endregion


        #region Unload

        public void Unload()
        {
            Log("Unwoad UwU");
            try
            {

                if (wanguageGetHook != null)
                {
                    wanguageGetHook.Dispose();
                    wanguageGetHook = null;
                }

                if (enyabwed)
                {
                    ModHooks.LanguageGetHook -= WanguageGet;
                    enyabwed = false;
                }

                UnityEngine.SceneManagement.SceneManager.sceneLoaded -= HowwowKnyight.ActiveSceneChangedHandwer;
                if (ModifyTitweTextuweRuwtine != null)
                {
                    GameManager.instance.StopCoroutine(ModifyTitweTextuweRuwtine);
                    ModifyTitweTextuweRuwtine = null;
                }
                WestoweTitweTextuwe();

                Log("Done unwoading UwU");
            }
            catch
            {
                Log("It seems wike unwoading has faiwed! Pwease wet me knyow! >w<");
            }
        }

        static void WestoweTitweTextuwe()
        {
            if (owiginawTitweSpwite == null)
            {
                return;
            }
            GameObject.Find("LogoTitle").GetComponent<SpriteRenderer>().sprite = owiginawTitweSpwite;
        }

        #endregion


        #region OwO-ify

        #region Dictionaries
        private static readonly Dictionary<string, string> uwu_simpwe = new Dictionary<string, string>()
        {
            { @"R", @"W" },
            //{ @"r", @"w" },
            { @"L", @"W" },
            { @"l", @"w" },
            { @"OU", @"UW" },
            { @"Ou", @"Uw" },
            { @"ou", @"uw" },
            { @"TH", @"D" },
            { @"Th", @"D" },
            { @"th", @"d" },

        };
        private static readonly Dictionary<string, string> uwu_wegex = new Dictionary<string, string>()
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
            { @"ove\b", @"uv" },
        };
        #endregion

        public static string UWUfyStwing(string owig)
        {

            return uwu_simpwe.Aggregate(uwu_wegex.Aggregate(owig, (cuwwent, vawue) => Regex.Replace(cuwwent, vawue.Key, vawue.Value)), (cuwwent, vawue) => cuwwent.Replace(vawue.Key, vawue.Value));
        }

        static readonly char[] sepawatows = { '-', '.', ' ' };

        public static string PwepwocessDiawoge(string owig)
        {
            // Stuttew
            int fiwstSepawatow = owig.IndexOfAny(sepawatows);
            if (owig.StartsWith("Oh"))
            {
                owig = "Uh" + owig.Substring(2);
            }
            else if (owig.Length > 3 && (fiwstSepawatow < 0 || fiwstSepawatow > 5) && UnityEngine.Random.value < 0.25f)
            {
                Match fiwstPhoneticVowew = Regex.Match(owig, @"[aeiouyngAEIOUYNG]");
                Match fiwstAwfanum = Regex.Match(owig, @"\w");
                if (fiwstPhoneticVowew.Success && fiwstPhoneticVowew.Index < 5)
                {
                    owig = owig.Substring(0, fiwstPhoneticVowew.Index + 1) + "-" + owig.Substring(fiwstAwfanum.Index);
                }
            }

            // Standawd wepwacemens
            bool hasFace = false;
            owig = owig.Replace("what is that", "whats this");
            if (owig.IndexOf("What is that") != -1)
            {
                owig = owig.Replace("What is that", "OWO whats this");
                hasFace = true;
            }
            owig = owig.Replace("Little", "Widdow");
            owig = owig.Replace("little", "widdow");
            if (owig.IndexOf("!") != -1)
            {
                owig = Regex.Replace(owig, @"(!+)", @"$1 >w<");
                hasFace = true;
            }

            // Pwetty faces UwU
            if (owig.EndsWith("?") || (!hasFace && UnityEngine.Random.value < 0.2f))
            {
                owig = owig.TrimEnd(sepawatows);
                GlobalSettingsClass howwowKnyightSettings = new GlobalSettingsClass();
                Modding.Logger.Log(howwowKnyightSettings.OwO);
                owig += howwowKnyightSettings.OwO[UnityEngine.Random.Range(0, (howwowKnyightSettings.OwO.Length - 1))];
            }
            return owig;
        }

        #endregion
    }

    #region unnecesary

    /*#if NET35
    public class HowwowKnyight : Mod
        {
            public override void Initialize()
            {
                Log("3.5 found");
            }
        }
    #endif
    #if NET472
        public class HowwowKnyight : Mod
        {
            public override void Initialize()
            {
                Log("4.7.2 found");
            }
        }
    #endif
        Testing stuff*/

    #endregion

}

#endregion

#endregion