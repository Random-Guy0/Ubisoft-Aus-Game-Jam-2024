using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Jam.Managers
{ 

    public class PlayManager : Singleton<PlayManager>
    {


        // Maximum number of trash allowed to be present
        public const int MAX_TRASH_COUNT = 30;
        private int maxTrashCount = MAX_TRASH_COUNT;



        private int trashCount = 0;
        public int TrashCount { get { return trashCount; } }




        // Callbacks
        public delegate void GameOver();
        /// <summary>
        /// Fired if trashCount >= maxTrashCount.
        /// </summary>
        public GameOver OnGameOver;




        /// <summary>
        /// Add (or subtract) trash count.
        /// Invoke OnGameOverCall if trashCount >= maxTrashCount.
        /// </summary>
        /// <param name="count"></param>
        public void AddTrash(int count=1)
        {
            trashCount += count;

            if(trashCount >= maxTrashCount)
            {
                OnGameOver?.Invoke();
            }


        }
    }

}