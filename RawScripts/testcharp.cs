using System;
using System.Text;
using UnityEngine;
using System.IO;
  
public class testcharp: MonoBehaviour
{       
	  public static void Start()
      {
		GameObject go = new GameObject();
        go.name = "created runtime in compiling a raw script";
         Debug.Log("start!");
		 
      }
	    public static void Awake()
      {
         Debug.Log("awake!");
      }
	    public static void test(int lol)
      {
         Debug.Log("update!");
      }
}