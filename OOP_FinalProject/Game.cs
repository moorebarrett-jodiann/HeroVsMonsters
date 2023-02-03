using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_FinalProject
{
    public static class Game
    {
        public static HashSet<Hero> Heroes = new HashSet<Hero>();
        public static HashSet<Monster> Monsters = new HashSet<Monster>();
        public static HashSet<Weapon> Weapons = new HashSet<Weapon>();
        public static HashSet<Armor> Armors = new HashSet<Armor>();
        public static HashSet<Inventory> HeroInventory = new HashSet<Inventory>();
        public static HashSet<Fight> Fights = new HashSet<Fight>();

        private static int _heroIdCounter = 1;
        private static int _monsterIdCounter = 1;
        private static int _weaponIdCounter = 1;
        private static int _armorIdCounter = 1;
        private static int _inventoryIdCounter = 1;
        private static int _fightIdCounter = 1;

        public static void CreateHero(string name)
        {
            Hero hero = new Hero(_heroIdCounter, name);
            Heroes.Add(hero);
            _heroIdCounter++;
        }

        public static void Start()
        {

        }
    }
}
