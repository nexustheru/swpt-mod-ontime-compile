using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace OnTimeCompiler
{
    public class watcher : MonoBehaviour
    {
        public bool active = false;
        compiler comp;
        private string fname = "";
        private FileSystemWatcher watchers = new FileSystemWatcher();
        public void setup(string filepath, compiler comps)
        {
            watchers.Path = filepath;
            comp = comps;
            watchers.Filter = "*.cs";
            watchers.IncludeSubdirectories = true;
            watchers.EnableRaisingEvents = true;
            watchers.Changed += OnChanged;
            watchers.Created += OnCreated;
            watchers.Error += OnError;

        }
        private void OnChanged(object sender, FileSystemEventArgs e)
        {
            active = true;
            fname = e.FullPath;

        }

        private void OnCreated(object sender, FileSystemEventArgs e)
        {
            active = true;
            fname = e.FullPath;

        }

        private void OnError(object sender, ErrorEventArgs e)
        {
            active = true;
            Debug.Log(e.GetException());
            active = false;
        }

        private static void PrintException(Exception ex)
        {
            if (ex != null)
            {
                Debug.Log($"Message: {ex.Message}");
                Debug.Log("Stacktrace:");
                Debug.Log(ex.StackTrace);
                PrintException(ex.InnerException);
            }
        }
        void Update()
        {
            if (active == true)
            {
                comp.compile(fname);
                print("compiling..");
                active = false;
            }

        }
    }
}
