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
        public static HashSet<Armor> ArmorList = new HashSet<Armor>();
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

        public static Hero? GetHero(int id)
        {
            Hero hero = null;
            foreach (Hero h in Heroes)
            {
                if (h.HeroId == id)
                {
                    hero = h;
                    break;
                }
            }

            return hero;
        }
        
        public static Monster? GetMonster(int id)
        {
            Monster monster = null;
            foreach (Monster m in Monsters)
            {
                if (m.MonsterId == id)
                {
                    monster = m;
                    break;
                }
            }

            return monster;
        }
        
        public static Weapon? GetWeapon(int id)
        {
            Weapon weapon = null;
            foreach (Weapon w in Weapons)
            {
                if (w.WeaponId == id)
                {
                    weapon = w;
                    break;
                }
            }

            return weapon;
        }
        
        public static Armor? GetArmor(int id)
        {
            Armor armor = null;
            foreach (Armor a in ArmorList)
            {
                if (a.ArmorId == id)
                {
                    armor = a;
                    break;
                }
            }

            return armor;
        }
        
        public static Inventory? GetInventory(int id)
        {
            Inventory inventory = null;
            foreach (Inventory i in HeroInventory)
            {
                if (i.InventoryId == id)
                {
                    inventory = i;
                    break;
                }
            }

            return inventory;
        }
        
        public static Fight? GetFight(int id)
        {
            Fight fight = null;
            foreach (Fight f in Fights)
            {
                if (f.FightId == id)
                {
                    fight = f;
                    break;
                }
            }

            return fight;
        }

        public static void CreateMonsters()
        {
            Random rand = new Random();
            // create 5 monster objects and store to list
            for (int i = 0; i < 5; i++)
            {
                //returns random number between 1-20
                int defense = rand.Next(1, 21);
                int strength = rand.Next(1, 21);

                Monster randomMonster = new Monster(_monsterIdCounter, $"Monster{i + 1}", defense, strength);
                Monsters.Add(randomMonster);
                _monsterIdCounter++;
            }
        }

        public static void Start()
        {
            Menu1();
            string userInput = GetUserInput();
            CreateHero(userInput);


        }

        static string GetUserInput(string promptMsg = "")
        {
            Console.WriteLine(promptMsg);
            string? userInput = Console.ReadLine();
            return userInput!;
        }

        public static void Menu1()
        {
            Console.WriteLine();
            Console.WriteLine("Enter your name:");
        }
        
        public static void Menu2()
        {
            Console.WriteLine();
            Console.WriteLine("Choose an option:");
            Console.WriteLine("1. Add a song to your playlist");
            Console.WriteLine("2. Play the next song in your playlist");
            Console.WriteLine("3. Skip the next song");
            Console.WriteLine("4. Rewind one song");
            Console.WriteLine("5. Exit");
        }
    }
}
