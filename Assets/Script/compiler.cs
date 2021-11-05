using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;
using System.Numerics;
using Microsoft.CSharp;
using System.CodeDom.Compiler;
using UnityEngine;

public class compiler : MonoBehaviour
{
        public DirectoryInfo d;
        string folderpath = "";
        private OnTimeCompiler.watcher wch = new OnTimeCompiler.watcher();
        public Assembly compileScript(string source)
        {
            var provider = new CSharpCodeProvider();
            var param = new CompilerParameters();
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                param.ReferencedAssemblies.Add(assembly.Location);
            }
            param.GenerateExecutable = false;
            param.GenerateInMemory = true;
            var result = provider.CompileAssemblyFromSource(param, source);

            if (result.Errors.Count > 0)
            {
                var msg = new StringBuilder();
                foreach (CompilerError error in result.Errors)
                {
                    msg.AppendFormat("Error ({0}): {1}\n",
                        error.ErrorNumber, error.ErrorText);
                }
                throw new Exception(msg.ToString());
            }

            // Return the assembly
            return result.CompiledAssembly;
        }
        public void Init()
        {
            folderpath = "C://Users//Tess//Documents//UnityProjects//swptMod//RawScripts";
            if (!Directory.Exists(folderpath))
            {
                print("Creating mod folder");
                Directory.CreateDirectory(folderpath);
            }
            print("folder already exist");
            d = new DirectoryInfo(folderpath);         
            foreach (var f in d.GetFiles("*.cs"))
            {
            print("compiling " + f.FullName);
            compile(f.FullName);
            }
        print("compiled first set of files");

        wch.setup(folderpath,this);
        print("filewatcher setup");
    }      

        public void compile(string filename)
        {
            string res = File.ReadAllText(filename);
            Assembly ass = compileScript(res);
            for (int i = 0; i < ass.GetTypes().Length; i++)
            {
                var typename = ass.GetTypes().GetValue(i).ToString();
                var method = ass.GetType(typename).GetMethod("Start");
                var del = (Action)Delegate.CreateDelegate(typeof(Action), method);        
                del.Invoke();
               
            }
     
            //Type typo = ass.GetType("Program");
            //MethodInfo metho = typo.GetMethod("Main");
            //metho.Invoke(null, null);
        }

        void Start()
        {
        Init();
        }
   
}