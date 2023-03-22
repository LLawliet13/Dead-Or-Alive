using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scenes.Scripts.Managers
{
    /// <summary>
    /// Manager nay chay truoc scene manager, đang ki cac event ma scene manager can
    /// </summary>
    public class SaveGameManager : MonoBehaviour
    {
        GameObject character;
        public static string path = "Assets/Resources/UserData/savegame.txt";
        public static string hightScorePath = "Assets/Resources/UserData/highscore.txt";
        bool isSaveGameServicesSignUp;
        private void Awake()
        {
            isSaveGameServicesSignUp = false;
            SignUpServices();
        }
        private bool SignUpServices()
        {
            try
            {
                SceneManager sceneManager = GameObject.Find("GameMaster").GetComponent<SceneManager>();
                if (sceneManager != null)
                {
                    UnityEvent<int, string, string[], ItemDropSaveGame[]> saveGameEvent = new UnityEvent<int, string, string[], ItemDropSaveGame[]>();
                    saveGameEvent.AddListener(SaveGameToFile);
                    sceneManager.SaveGameEvent = saveGameEvent;

                    UnityEvent<DateTime, int> saveHighscoreEvent = new UnityEvent<DateTime, int>();
                    saveHighscoreEvent.AddListener(SaveHighScore);
                    sceneManager.SaveHighscoreEvent = saveHighscoreEvent;
                }
            }
            catch
            {
                return false;
            }
            isSaveGameServicesSignUp = true;
            return true;
        }
        private void Update()
        {
            if (!isSaveGameServicesSignUp)
                SignUpServices();
        }



        public bool CheckIfDataExist()
        {
            if (new FileInfo(path).Length == 0)
                return false;
            return true;
        }
        public bool CheckIfHighScoreExist()
        {
            if (new FileInfo(hightScorePath).Length == 0)
                return false;
            return true;
        }
        public void SaveGameToPlayerPrefs(int player_level, int currentHp, string[] skillnames)
        {
        }


        public void LoadGameFromPlayerPrefs()
        {
        }

        public void SaveGameToFile(int player_level, string currentHpandcurrentExpAndCurrentPoint, string[] skillnames, ItemDropSaveGame[] items)
        {
            CharacterSaveGame characterData = new CharacterSaveGame
            {
                level = player_level,
                currentHp = int.Parse(currentHpandcurrentExpAndCurrentPoint.Split(",")[0]),
                currentExp = int.Parse(currentHpandcurrentExpAndCurrentPoint.Split(",")[1]),
                CurrentPoint = int.Parse(currentHpandcurrentExpAndCurrentPoint.Split(",")[2]),
                skillList = skillnames,
                items = items
            };

            string data = JsonConvert.SerializeObject(characterData, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            FileInfo fi = new FileInfo(path);
            StreamWriter writer = new StreamWriter(fi.Open(FileMode.Truncate));
            writer.WriteLine(data);
            writer.Close();
        }
        /// <summary>
        /// tra ve trang thai man choi gan nhat, neu lan choi truoc game chua ket thuc. Co the tra ve null neu khong co data
        /// </summary>
        /// <returns></returns>
        public CharacterSaveGame LoadGameFromFile()
        {
            StreamReader reader = new StreamReader(path);
            string data = reader.ReadToEnd();
            reader.Close();
            //xoa du lieu sau khi load file
            File.WriteAllText(path, String.Empty);
            if (String.IsNullOrEmpty(data))
                return null;

            CharacterSaveGame characterData = JsonConvert.DeserializeObject<CharacterSaveGame>(data);
            return characterData;
        }
        public void SaveHighScore(DateTime date, int score)
        {
            StreamReader reader = new StreamReader(hightScorePath);
            string data = reader.ReadToEnd();
            reader.Close();

            List<HighScore> highScores;
            if (String.IsNullOrEmpty(data))
                highScores = new List<HighScore>();
            else highScores = JsonConvert.DeserializeObject<List<HighScore>>(data);

            highScores.Add(new HighScore { CompleteTime = date, score = score });
            string saveData = JsonConvert.SerializeObject(highScores, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            FileInfo fi = new FileInfo(hightScorePath);
            StreamWriter writer = new StreamWriter(fi.Open(FileMode.Truncate));
            writer.WriteLine(saveData);
            writer.Close();
        }
        /// <summary>
        /// tra ve bang diem sap xep tu cao den thap. Co the tra ve null neu khong co data
        /// </summary>
        /// <returns></returns>
        public List<HighScore> LoadHightScore()
        {
            StreamReader reader = new StreamReader(hightScorePath);
            string data = reader.ReadToEnd();
            reader.Close();
            if (String.IsNullOrEmpty(data))
                return null;
            FileInfo fi = new FileInfo(hightScorePath);
            List<HighScore> highScores = JsonConvert.DeserializeObject<List<HighScore>>(data).OrderByDescending<HighScore, int>(h => h.score).ToList();
            return highScores;
        }
        /// <summary>
        /// tra ve so diem cao nhat dat duoc trong cac record ton tai, neu chua co record thi tra ve 0
        /// </summary>
        /// <returns></returns>
        public int GetHighestHighScore()
        {
            int max;
            try
            {
                max = LoadHightScore().OrderByDescending<HighScore, int>(h => h.score).First().score;
            }
            catch
            {
                return 0;
            }
            return max;
        }
    }
    public class HighScore
    {
        public DateTime CompleteTime;
        public int score;
    }
    public class CharacterSaveGame
    {
        public int level;
        public int currentHp;
        public int currentExp;
        public string[] skillList;
        public int CurrentPoint;
        public ItemDropSaveGame[] items;
    }
    public class ItemDropSaveGame
    {
        public string itemName;
        public int value;
        public Vector3 position;
    }
}
