using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Jam.Managers
{
    public class SoundManager : Singleton<SoundManager>
    {
        
        public AudioSource PlaySound(AudioClip clip, GameObject parent, float volume = 1.0f, bool destroyWhenDone=true)
        {
            var obj = new GameObject();
            obj.transform.parent = parent.transform;
            var source = obj.AddComponent<AudioSource>();
            source.playOnAwake = false;

            source.clip = clip;
            source.Play();
            source.volume = volume;

            if(destroyWhenDone)
                Destroy(obj, clip.length + 1.0f);


            return source;
        }

        public AudioSource PlaySound(AudioClip clip, Vector2 position, float volume = 1.0f, bool destroyWhenDone = true)
        {
            var obj = new GameObject();
            obj.transform.position = position;
            var source = obj.AddComponent<AudioSource>();
            source.playOnAwake = false;

            source.clip = clip;
            source.Play();
            source.volume = volume;

            if (destroyWhenDone)
                Destroy(obj, clip.length + 1.0f);


            return source;
        }


    }
}
