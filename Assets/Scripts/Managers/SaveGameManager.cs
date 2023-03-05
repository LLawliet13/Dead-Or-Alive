using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scenes.Scripts.Managers
{
    public class SaveGameManager : MonoBehaviour
    {
        GameObject character;

        private void OnDisable()
        {
            SaveCharacterStatus();
            SaveBoss();
            SaveEnemiesStatus();
        }
        public void SaveSkill()
        {

        }
        public void SaveCharacterStatus()
        {

        }
        public void SaveEnemiesStatus()
        {

        }
        public void SaveBoss()
        {

        }
        
    }
    public class CharacterSaveGame
    {

    }
}
