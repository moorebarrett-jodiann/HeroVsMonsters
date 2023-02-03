using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_FinalProject
{
    public class Monster
    {
        private int _monsterId;
        private string _monsterName;
        private int _strength;
        private int _defense;
        private int _originalHealth;
        private int _currentHealth;
        private bool _isDefeated;

        public int MonsterId
        {
            get { return _monsterId; }
        }

        public string MonsterName
        {
            get { return _monsterName; }
        }

        public void SetName(string name)
        {
            if (name.Length > 3)
            {
                _monsterName = name;
            }
            else
            {
                throw new ArgumentException("Monster name must contain 3 or more characters");
            }
        }

        public int OriginalHealth
        {
            get { return _originalHealth; }
            set
            {
                if (value < 1)
                {
                    throw new ArgumentException("Original health must be greater than 0");
                }
                else
                {
                    _originalHealth = value;
                }
            }
        }

        // current health will not have validation as it can have a negative value calculated
        // during a fight
        public int CurrentHealth
        {
            get { return _currentHealth; }
            set { _currentHealth = value; }
        }

        public int Strength
        {
            get { return _strength; }
            set
            {
                if (value < 1)
                {
                    throw new ArgumentException("Strength health must be greater than 0");
                }
                else
                {
                    _strength = value;
                }
            }
        }

        public int Defense
        {
            get { return _defense; }
            set
            {
                if (value < 1)
                {
                    throw new ArgumentException("Defense health must be greater than 0");
                }
                else
                {
                    _defense = value;
                }
            }
        }

        public string GetStats()
        {
            string stats = $"Monster's Name: {MonsterName}, BaseStrength: {Strength}, BaseDefence: {Defense}, OriginalHealth: {OriginalHealth}, and CurrentHealth: {CurrentHealth}";
            Console.WriteLine(stats);
            return stats;
        }

        public string GetFightStats()
        {
            string stats = "\nStats:\n";
            stats += $"Monster’s Name: {MonsterName}, CurrentHealth: {CurrentHealth}";
            return stats;
        }

        public bool IsDefeated
        {
            get { return _isDefeated; }
            set { _isDefeated = value; }
        }

        public Monster(int id, string name, int defense, int strength)
        {
            _monsterId = id;
            SetName(name);
            OriginalHealth = 100;
            CurrentHealth = 100;
            Defense = defense;
            Strength = strength;
            _isDefeated = false;
        }
    }
}
