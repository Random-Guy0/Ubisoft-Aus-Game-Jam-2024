using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Jam.Managers
{ 

    /// <summary>
    /// Super class for all Singleton Classes
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {

        private static T instance;

        public static T Instance
        {
            get
            {
                if(instance == null)
                {
                    var obj = new GameObject();
                    instance = obj.AddComponent<T>();
                    obj.name = typeof(T).ToString();
                }

                return instance;
            }
        }
    }

}