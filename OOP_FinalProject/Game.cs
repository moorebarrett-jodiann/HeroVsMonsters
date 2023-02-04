using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace OOP_FinalProject
{
    public static class Game
    {
        public static HashSet<Hero> Heroes = new HashSet<Hero>();
        public static HashSet<Monster> Monsters = new HashSet<Monster>();
        public static List<Weapon> Weapons = new List<Weapon>();
        public static List<Armor> ArmorList = new List<Armor>();
        public static HashSet<Inventory> HeroInventory = new HashSet<Inventory>();
        public static HashSet<Fight> Fights = new HashSet<Fight>();

        private static int _heroIdCounter = 1;
        private static int _monsterIdCounter = 1;
        private static int _weaponIdCounter = 1;
        private static int _armorIdCounter = 1;
        private static int _inventoryIdCounter = 1;
        private static int _fightIdCounter = 1;

        public static int CreateHero(string name)
        {
            Random rand = new Random();
            //generate strength and defence values between 50-100
            int defense = rand.Next(1, 11);
            int strength = rand.Next(1, 11);

            Hero newHero = new Hero(_heroIdCounter, name, defense, strength);
            Heroes.Add(newHero);
            _heroIdCounter++;

            return newHero.HeroId;
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

        public static void CreateMonsters(int count)
        {
            Random rand = new Random();

            // create 'count' monster objects and store to list
            for (int i = 0; i < count; i++)
            {
                //generate strength and defence values between 50-100
                int defense = rand.Next(1, 11);
                int strength = rand.Next(1, 11);

                Monster randomMonster = new Monster(_monsterIdCounter, $"Monster{i + 1}", defense, strength);
                Monsters.Add(randomMonster);
                _monsterIdCounter++;
            }
        }
        
        public static void CreateWeapons(int count)
        {
            Random rand = new Random();
            // create a random index list to ensure only unique power values for weapons are created
            List<int> randomIndexList = new List<int>();
            // create 'count' weapon objects and store to list
            while(Weapons.Count < count)
            {
                //returns random number between 1-20
                int power = rand.Next(1, 11);                

                // if power value was already generated dont create a weapon with that power
                if (!randomIndexList.Contains(power))
                {
                    // add random value to index list
                    randomIndexList.Add(power);

                    Weapon randomWeapon = new Weapon(_weaponIdCounter, $"Weapon{Weapons.Count + 1}", power);                
                    Weapons.Add(randomWeapon);
                    _weaponIdCounter++;
                }
            }
        }
        
        public static void CreateArmor(int count)
        {
            Random rand = new Random();
            // create a random index list to ensure only unique power values for armor are created
            List<int> randomIndexList = new List<int>();
            // create 'count' armor objects and store to list
            while(ArmorList.Count < count)
            {
                //returns random number between 1-10
                int power = rand.Next(1, 11);                

                // if power value was already generated dont create armor with that power
                if (!randomIndexList.Contains(power))
                {
                    // add random value to index list
                    randomIndexList.Add(power);

                    Armor randomArmor = new Armor(_armorIdCounter, $"Armor{ArmorList.Count + 1}", power);
                    ArmorList.Add(randomArmor);
                    _armorIdCounter++;
                }
            }
        }
        public static void InitInventory(Hero hero)
        {
            if(hero != null)
            {
                Inventory inventory = new Inventory(_inventoryIdCounter, hero);
                HeroInventory.Add(inventory);
                hero.Inventory = inventory;
                _inventoryIdCounter++;
            }
        }

        public static void ReviveMonsters()
        {
            foreach (Monster m in Monsters)
            {
                if (m.IsDefeated)
                {
                    m.IsDefeated = false;
                    m.CurrentHealth = m.OriginalHealth;
                }
            }
        }


        public static void UpdateWeapon(Hero hero)
        {
            Console.WriteLine("Select a Weapon number from the list:");
            Console.WriteLine();
            int index = 0;

            foreach (Weapon weapon in Weapons)
            {
                Console.WriteLine($"{index + 1} - {weapon.WeaponName}: Power ({weapon.Power})");
                index++;
            }

            int selected = Int32.Parse(Console.ReadLine());

            if(selected < 1 || selected > Weapons.Count)
            {
                Console.WriteLine("Selection is invalid. Select a Weapon number from the list:");
                selected = Int32.Parse(Console.ReadLine());
            }

            hero.EquipWeapon(Weapons[selected - 1]);
            Console.WriteLine($"Weapon successfully updated");
        }
        
        public static void UpdateArmor(Hero hero)
        {
            Console.WriteLine("Select an Armor number from the list:");
            Console.WriteLine();
            int index = 0;

            foreach (Armor armor in ArmorList)
            {
                Console.WriteLine($"{index + 1} - {armor.ArmorName}: Power ({armor.Power})");
                index++;
            }

            int selected = Int32.Parse(Console.ReadLine());

            if(selected < 1 || selected > ArmorList.Count)
            {
                Console.WriteLine("Selection is invalid. Select an Armor number from the list:");
                selected = Int32.Parse(Console.ReadLine());
            }

            hero.EquipArmor(ArmorList[selected - 1]);
            Console.WriteLine($"Armor successfully updated");
        }

        public static void Start()
        {
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;

            WelcomeHeader();

            string heroName = GetUserInput();
            int newHero = CreateHero(heroName);

            // initialize a list of monsters, weapons and armor
            CreateMonsters(5);
            CreateWeapons(5);
            CreateArmor(5);
            InitInventory(GetHero(newHero));

            bool looping = true;
            while (looping)
            {
                Menu2();
                string selection = GetUserInput();

                switch (selection)
                {
                    case "1":
                        break;
                    case "2":
                        InventoryHeader();
                        try
                        {
                            Hero? hero = GetHero(newHero);

                            if (hero != null)
                            {
                                Weapon? weapon = hero.Inventory.Weapon;
                                Armor? armor = hero.Inventory.Armor;

                                if (armor == null)
                                {
                                    Console.WriteLine("You have no Armor.");
                                }
                                else
                                {
                                    string currArmor = (armor != null) ? $"Current armor: {armor.ArmorName} with Power: {armor.Power}" : "[not selected]";
                                    Console.WriteLine(currArmor);                                    
                                }

                                if (weapon == null)
                                {
                                    Console.WriteLine("You have no Weapons.");
                                }
                                else
                                {
                                    string currWeapon = (weapon != null) ? $"Current weapon: {weapon.WeaponName} with Power: {weapon.Power}" : "[not selected]";
                                    Console.WriteLine(currWeapon);
                                }

                                Console.WriteLine();
                                Console.WriteLine("Press (1) to Update Weapon, (2) to Update Armor, (3) Go Back to Main Menu");
                                int equipOption = Int32.Parse(Console.ReadLine());

                                if (equipOption == 1)
                                {
                                    UpdateWeapon(hero);
                                }
                                else if (equipOption == 2)
                                {
                                    UpdateArmor(hero);
                                }
                                else if (equipOption == 3)
                                {
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("Invalid Option Selected. Press (1) to Update Weapon or (2) to Update Armor");
                                    equipOption = Int32.Parse(Console.ReadLine());
                                }
                            }
                            else
                            {
                                Console.WriteLine("Hero Player not Found");
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        
                        break;
                    case "3":
                        try
                        {
                            Hero? hero = GetHero(newHero);

                            if (hero != null)
                            {
                                if(hero.Inventory.Weapon == null || hero.Inventory.Armor == null)
                                {
                                    Console.WriteLine("Sorry Hero! :-( You must have a Weapon and Armor to Fight.");
                                } else
                                {
                                    FightHeader();
                                    CoinToss(hero);                              
                                }
                            }
                            else
                            {
                                Console.WriteLine("Hero Player not Found.");
                            }
                        } 
                        catch (Exception ex) 
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;
                    case "4":
                        Console.WriteLine("\nThanks for playing!");
                        looping = false;
                        break;

                    default:
                        Console.WriteLine("Invalid input. Please try again.");
                        break;
                }
            }
        }

        // toss coin to randomly allow Hero to begin fight or Monster to begin fight
        public static void CoinToss(Hero hero)
        {
            Random random = new Random();
            int coin = random.Next(2);
            bool monstersAvailable = false;

            // pick random monster with isDefeated = false
            List<Monster> availableMonsters = new List<Monster>();
            foreach(Monster m in Monsters)
            {
                if (!m.IsDefeated)
                {
                    monstersAvailable = true;
                    break;
                }
            }

            if(monstersAvailable)
            {
                foreach (Monster m in Monsters)
                {
                    if (!m.IsDefeated)
                    {
                        availableMonsters.Add(m);
                    }
                }

                Monster chosenMonster = availableMonsters[random.Next(availableMonsters.Count)];

                Console.WriteLine("Stats:");
                Console.WriteLine();
                hero.GetStats();
                Console.WriteLine($"{hero.Inventory.Weapon.WeaponName}: Power - {hero.Inventory.Weapon.Power}");
                Console.WriteLine($"{hero.Inventory.Armor.ArmorName}: Power - {hero.Inventory.Armor.Power}");
                chosenMonster.GetStats();
                Console.WriteLine();

                Fight newFight = new Fight(_fightIdCounter, hero, chosenMonster);
                _fightIdCounter++;
                Fights.Add(newFight);

                if (coin == 0) // Hero goes first
                {
                    Console.WriteLine("Hero goes first");
                    newFight.HeroTurn(hero, chosenMonster, newFight);
                }
                else if (coin == 1) // Monster goes first
                {
                    Console.WriteLine("Monster goes first");
                    newFight.MonsterTurn(hero, chosenMonster, newFight);
                }
            } 
            else
            {
                Console.WriteLine("No monsters available to fight.");
                return;
            }
        }

        static string GetUserInput(string promptMsg = "")
        {
            Console.WriteLine(promptMsg);
            string? userInput = Console.ReadLine();
            return userInput!;
        }

        public static void WelcomeHeader()
        {
            Console.WriteLine();
            Console.WriteLine("---------------------*********************************-----------------------");
            Console.WriteLine("**********************--------- WELCOME HERO! ---------**********************");
            Console.WriteLine("---------------------*********************************-----------------------");
            Console.WriteLine("Enter your name:");
        }

        public static void StaticsticsHeader()
        {
            Console.WriteLine();
            Console.WriteLine("---------------------*********************************-----------------------");
            Console.WriteLine("*********************----------- STATISTICS -----------**********************");
            Console.WriteLine("---------------------*********************************-----------------------");
            Console.WriteLine();
        }
        
        public static void InventoryHeader()
        {
            Console.WriteLine();
            Console.WriteLine("---------------------*********************************-----------------------");
            Console.WriteLine("**********************----------- INVENTORY -----------**********************");
            Console.WriteLine("---------------------*********************************-----------------------");
            Console.WriteLine();
        }

        public static void FightHeader()
        {
            Console.WriteLine();
            Console.WriteLine("---------------------*********************************-----------------------");
            Console.WriteLine("**********************------------ FIGHT! -----------**********************");
            Console.WriteLine("---------------------*********************************-----------------------");
            Console.WriteLine();
        }

        public static void Menu2()
        {
            Console.WriteLine();
            Console.WriteLine("Choose an option:");
            Console.WriteLine("1. Display Statistics");
            Console.WriteLine("2. Manage Inventory");
            Console.WriteLine("3. Fight");
            Console.WriteLine("4. Exit");
        }
    }
}
