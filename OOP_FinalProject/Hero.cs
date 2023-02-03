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
        public int CurrentHealth
        {
            get { return _currentHealth; }
            set
            {
                if(value > 0 && value < OriginalHealth)
                {
                    _currentHealth = value;
                } 
                else
                {
                    throw new ArgumentException("Current health must be greater than 0 and less than Original Health");
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

        public void GetStats()
        {
            Console.WriteLine($"Hero’s Name: {HeroName}, BaseStrength: {BaseStrength}, BaseDefence: {BaseDefense}, OriginalHealth: {OriginalHealth}, and CurrentHealth: {CurrentHealth}");
        }

        public void EquipWeapon(Weapon weapon)
        {
            Inventory.Weapon = weapon;
        }
        
        public void EquipArmor(Armor armor)
        {
            Inventory.Armor = armor;
        }

        public Hero(int id, string name)
        {
            _heroId = id;
            SetName(name);
            _originalHealth = 100;
            _currentHealth = 100;
            _baseDefense = 100;
            _baseStrength = 100;
        }
                
        public Hero(int id, string name, Weapon weapon, Armor armor, int defense, int strength)
        {
            _heroId = id;
            SetName(name);
            Inventory.Weapon = weapon;
            Inventory.Armor = armor;
            OriginalHealth = 100;
            CurrentHealth = 100;
            BaseDefense = defense;
            BaseStrength = strength;
        }
    }
}
