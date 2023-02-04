using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_FinalProject
{
    public class Hero
    {
        private int _heroId;
        private string _heroName;

        private Inventory _inventory;

        private int _originalHealth;
        private int _currentHealth;
        private int _baseStrength;
        private int _baseDefense;

        private HashSet<Fight> _fights = new HashSet<Fight>();

        public int HeroId
        {
            get { return _heroId; } 
        }

        public string HeroName
        {
            get { return _heroName; }
        }

        public void SetName(string name)
        {
            if(name.Length > 3)
            {
                _heroName = name;
            } else
            {
                throw new ArgumentException("Hero name must contain 3 or more characters");
            }
        }

        public int OriginalHealth
        {
            get { return _originalHealth; }
            set
            {
                if(value < 1)
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
        
        public int BaseStrength
        {
            get { return _baseStrength; }
            set
            {
                if(value < 1)
                {
                    throw new ArgumentException("Base Strength health must be greater than 0");
                } 
                else
                {
                    _baseStrength = value;
                }
            }
        }
        
        public int BaseDefense
        {
            get { return _baseDefense; }
            set
            {
                if(value < 1)
                {
                    throw new ArgumentException("Base Defense health must be greater than 0");
                } 
                else
                {
                    _baseDefense = value;
                }
            }
        }
        
        public Inventory Inventory
        {
            get { return _inventory; }
            set { _inventory = value; }
        }

        public HashSet<Fight> GetFights()
        {
            return _fights.ToHashSet();
        }
        
        public void AddFightHistory(Fight fight)
        {
            _fights.Add(fight);
        }

        public string GetStats()
        {
            string stats = $"Hero’s Name: {HeroName}, BaseStrength: {BaseStrength}, BaseDefence: {BaseDefense}, OriginalHealth: {OriginalHealth}, and CurrentHealth: {CurrentHealth}";
            Console.WriteLine(stats);
            return stats;
        }

        public string GetFightStats()
        {
            string stats = "\nStats:\n";
            stats += $"Hero’s Name: {HeroName}, CurrentHealth: {CurrentHealth}";
            return stats;
        }

        public void EquipWeapon(Weapon weapon)
        {
            Inventory.Weapon = weapon;
        }
        
        public void EquipArmor(Armor armor)
        {
            Inventory.Armor = armor;
        }

        public Hero(int id, string name, int defense, int strength)
        {
            _heroId = id;
            SetName(name);
            OriginalHealth = 20;
            CurrentHealth = 20;
            BaseDefense = defense;
            BaseStrength = strength;
        }
                
        public Hero(int id, string name, Weapon weapon, Armor armor, int defense, int strength)
        {
            _heroId = id;
            SetName(name);
            Inventory.Weapon = weapon;
            Inventory.Armor = armor;
            OriginalHealth = 20;
            CurrentHealth = 20;
            BaseDefense = defense;
            BaseStrength = strength;
        }
    }
}
