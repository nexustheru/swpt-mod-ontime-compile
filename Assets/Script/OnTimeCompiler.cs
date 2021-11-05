using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using BepInEx;
using HarmonyLib;
using HarmonyLib.Tools;

namespace OnTimeCompilertest
{
    [BepInPlugin("org.bepinex.plugins.OnTimeCompiler", "OnTimeCompiler", "1.0.0.0")]
    class OnTimeCompiler : BaseUnityPlugin
    {
        private Harmony _harmonyInstance;
        private compiler Timecompiler;
        private void debuglog(string str = "")
        {
            Logger.LogInfo(str);
        }
        private void Awake()
        {
            debuglog("OnTimeCompiler is loaded!");

            HarmonyFileLog.Enabled = true;
            this.transform.parent = null;
            DontDestroyOnLoad(this);
            _harmonyInstance = Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly());
            Timecompiler = new compiler();

        }
        private void Start()
        {
            try
            {
                Timecompiler.Init();
            }
            catch(Exception ex)
            {
                print(ex.Message + Environment.NewLine);
                print(ex.StackTrace + Environment.NewLine);
            }
        }
    }
}
