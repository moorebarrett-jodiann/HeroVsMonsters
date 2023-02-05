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

        // inventory object to hold hero equipped weapon and equipped armor
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
            if(name.Length > 2 && name.All(char.IsLetterOrDigit))
            {
                _heroName = name;
            } else
            {
                throw new ArgumentException("Hero name must contain 3 or more alphanumeric characters");
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

        public int CurrentHealth
        {
            get { return _currentHealth; }
            set
            {
                if (value < 0 || value > OriginalHealth)
                {
                    throw new ArgumentException("Current health cannot exceed Original Health or be a negative value");
                }
                else
                {
                    _currentHealth = value;
                }
            }
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

        // Displays the Hero’s Name, BaseStrength, BaseDefence, OriginalHealth, and CurrentHealth.
        public string GetStats()
        {
            string stats = $"Hero’s Name: {HeroName}, BaseStrength: {BaseStrength}, BaseDefence: {BaseDefense}, OriginalHealth: {OriginalHealth}, and CurrentHealth: {CurrentHealth}";
            Console.WriteLine(stats);
            return stats;
        }

        // change the Hero's equipped weapon
        public void EquipWeapon(Weapon weapon)
        {
            Inventory.EquippedWeapon = weapon;
        }
        
        // change the Hero's equipped armor
        public void EquipArmor(Armor armor)
        {
            Inventory.EquippedArmor = armor;
        }

        public Hero(int id, string name, int defense, int strength)
        {
            _heroId = id;
            SetName(name);
            OriginalHealth = 100;
            CurrentHealth = 100;
            BaseDefense = defense;
            BaseStrength = strength;
        }
                
        public Hero(int id, string name, Weapon weapon, Armor armor, int defense, int strength)
        {
            _heroId = id;
            SetName(name);
            Inventory.EquippedWeapon = weapon;
            Inventory.EquippedArmor = armor;
            OriginalHealth = 100;
            CurrentHealth = 100;
            BaseDefense = defense;
            BaseStrength = strength;
        }
    }
}
