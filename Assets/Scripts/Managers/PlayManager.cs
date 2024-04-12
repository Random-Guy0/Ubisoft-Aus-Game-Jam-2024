using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace Jam.Managers
{ 

    public class PlayManager : Singleton<PlayManager>
    {


        // Maximum number of trash allowed to be present
        public const int MAX_TRASH_COUNT = 10;
        public const int MAX_WALKER_COUNT = 5;
        public const int MAX_TOSSER_COUNT = 3;
        public const int MAX_WALKING_TOSSER_COUNT = 5;
        public const int MAX_VEHICLE_COUNT = 4;

        private int trashCount = 0;
        private int walkerCount = 0;
        private int tosserCount = 0;
        private int walkingTosserCount = 0;
        private int vehicleCount = 0;
        private int score = 0;
        private int scoreMultiplier = 100;

        private bool gameOver = false;



        public int TrashCount => trashCount;
        public int Score => score;
        public int WalkerCount { get { return walkerCount; } set { walkerCount = value; } }
        public int TosserCount { get { return tosserCount; } set { tosserCount = value; } }
        public int WalkingTosserCount { get { return walkingTosserCount; } set { walkingTosserCount = value; } }
        public int VehicleCount { get { return vehicleCount; } set { vehicleCount = value; } }




        // Callbacks
        public delegate void GameOver();
        /// <summary>
        /// Fired if trashCount >= maxTrashCount.
        /// </summary>
        public event GameOver OnGameOver;

        public delegate void TrashCountChange(int newTrashCount);

        public event TrashCountChange OnTrashCountChange;

        public delegate void ScoreChange(int newScore);

        public event ScoreChange OnScoreChange;


        /// <summary>
        /// Add (or subtract) trash count.
        /// Invoke OnGameOverCall if trashCount >= maxTrashCount.
        /// </summary>
        /// <param name="count"></param>
        public void AddTrash(int count=1)
        {
            trashCount += count;
            OnTrashCountChange?.Invoke(trashCount);

            if(trashCount >= MAX_TRASH_COUNT && !gameOver)
            {
                gameOver = true;
                OnGameOver?.Invoke();
                GameObject newGameObject = new GameObject();
                newGameObject.AddComponent<FinalScore>().Score = score;
                Invoke(nameof(ChangeToGameOverScene), 5f);
            }
        }

        public void RemoveTrash(int count = 1)
        {
            trashCount -= count;
            OnTrashCountChange?.Invoke(trashCount);
        }

        public void AddScore()
        {
            score += scoreMultiplier;
            OnScoreChange?.Invoke(score);
        }

        public bool AddWalker()
        {
            if (walkerCount >= MAX_WALKER_COUNT)
                return false;

            walkerCount++;
            return true;
        }

        public bool AddWalkingTosser()
        {
            if (walkingTosserCount >= MAX_WALKING_TOSSER_COUNT)
                return false;

            walkingTosserCount++;
            return true;
        }

        public bool AddTosser()
        {
            if (tosserCount >= MAX_TOSSER_COUNT)
                return false;

            tosserCount++;
            return true;
        }

        public bool AddVehicle()
        {
            if (vehicleCount >= MAX_VEHICLE_COUNT)
                return false;

            vehicleCount++;
            return true;
        }

        private void ChangeToGameOverScene()
        {
            SceneManager.LoadScene(2);
        }
    }

}