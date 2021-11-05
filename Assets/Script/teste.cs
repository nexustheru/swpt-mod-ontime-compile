using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.IO;
using System.Reflection;
using System.Text;
using UnityEngine;
public class teste : MonoBehaviour
{
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
                msg.AppendFormat("Error ({0}): {1}\n", error.ErrorNumber, error.ErrorText);
            }
            throw new Exception(msg.ToString());
        }

        // Return the assembly
        return result.CompiledAssembly;
    }

    public void Fetch()
    {
        string filepath = "C://Users//Tess//Documents//UnityProjects//swptMod//RawScripts//testcharp.cs";
        DirectoryInfo d = new DirectoryInfo(filepath);
        string res = File.ReadAllText(filepath);
        Assembly ass = compileScript(res);

        for (int i = 0; i < ass.GetTypes().Length; i++)
        {
            var typename = ass.GetTypes().GetValue(i).ToString();
            var method = ass.GetType(typename).GetMethod("Start");
            var del = (Action)Delegate.CreateDelegate(typeof(Action), method);
            del.Invoke();
            //    for (int ip = 0; ip < ass.GetType(typename).GetMethods().Length; ip++)
            //    {
            //        var methodname = ass.GetType(typename).GetMethods().GetValue(ip).ToString();
            //        if (methodname.Contains("Start()"))
            //        {
            //            var method = ass.GetType(typename).GetMethod("Start");
            //            var del = (Action)Delegate.CreateDelegate(typeof(Action), method);
            //            del.Invoke();

            //        }
            //    }

            //}

        }
    }
    void Start()
    {
        Fetch();
    }
}

