using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace OnTimeCompiler
{
    public class watcher  
    {
        bool active = false;
        compiler comp;
        private FileSystemWatcher watchers=new FileSystemWatcher();
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
            comp.compile(e.FullPath);
            Console.WriteLine("file changed, compiling");
        }

        private void OnCreated(object sender, FileSystemEventArgs e)
        {
            active = true;
            comp.compile(e.FullPath);
            Console.WriteLine("file created, compiling");
        }

        private void OnError(object sender, ErrorEventArgs e)
        {
            active = true;
            PrintException(e.GetException());
        }

        private static void PrintException(Exception ex)
        {
            if (ex != null)
            {
                Console.WriteLine($"Message: {ex.Message}");
                Console.WriteLine("Stacktrace:");
                Console.WriteLine(ex.StackTrace);
                Console.WriteLine();
                PrintException(ex.InnerException);
            }
        }

        private void Update()
        {
              if (active)
              {

                 active = false;
              }
        }
        
    }
}
