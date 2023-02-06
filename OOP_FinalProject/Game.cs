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

        public static int InitHero(string name)
        {
            Hero newHero = new Hero(_heroIdCounter, name);
            Heroes.Add(newHero);
            _heroIdCounter++;

            return newHero.HeroId;
        }

        public static void UpdateHeroStats(int id)
        {
            Hero? hero = GetHero(id);

            if (hero != null)
            {
                Random rand = new Random();
                //generate strength and defense values between 5 - 15
                int defense = rand.Next(5, 16);
                int strength = rand.Next(5, 16);

                hero.BaseDefense = defense;
                hero.BaseStrength = strength;
            } else
            {
                Console.WriteLine("Hero not found.");
            }
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
                //generate strength and defense values between 1 - 20
                int defense = rand.Next(1, 21);
                int strength = rand.Next(1, 21);

                Monster randomMonster = new Monster(_monsterIdCounter, $"Monster {i + 1}", defense, strength);
                Monsters.Add(randomMonster);
                _monsterIdCounter++;
            }
        }
        
        // create specific number of weapons with random values for power
        public static void CreateWeapons(int count)
        {
            Dictionary<string, int> weapons = new Dictionary<string, int>()
            {
                {"Knife", 2 },
                {"Grenade", 10 },
                {"Crossbow", 8 },
                {"Hammer", 4 },
                {"Dual Pistol", 9 }
            };

            // create weapon objects and store to list
            foreach (KeyValuePair<string, int> weapon in weapons)
            {
                Weapon newWeapon = new Weapon(_weaponIdCounter, weapon.Key, weapon.Value);
                Weapons.Add(newWeapon);
                _weaponIdCounter++;
            }
        }
        
        // create specific number of armor with random values for power
        public static void CreateArmor(int count)
        {
            Dictionary<string, int> armorItems = new Dictionary<string, int>()
            {
                {"Shield", 10 },
                {"Helmet", 1 },
                {"Arm Guard", 3 },
                {"Magic Cloak", 9 },
                {"Chest Armor", 7 }
            };

            // create weapon objects and store to list
            foreach (KeyValuePair<string, int> armorItem in armorItems)
            {
                Armor newArmor = new Armor(_armorIdCounter, armorItem.Key, armorItem.Value);
                ArmorList.Add(newArmor);
                _armorIdCounter++;
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
                m.IsDefeated = false;
                m.CurrentHealth = m.OriginalHealth;
            }
        }

        public static void ManageInventory(int heroId)
        {
            Hero? hero = GetHero(heroId);

            if (hero != null)
            {
                Weapon? weapon = hero.Inventory.EquippedWeapon;
                Armor? armor = hero.Inventory.EquippedArmor;

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

                ManageInventoryMenu();
                int equipOption = Int32.Parse(GetUserInput());

                while(equipOption != 1 && equipOption != 2 && equipOption != 3)
                {
                    Console.WriteLine("Invalid Option Selected");
                    ManageInventoryMenu();
                    equipOption = Int32.Parse(GetUserInput());
                }

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
                    return;
                }
            }
            else
            {
                Console.WriteLine("Hero Player not Found");
            }
        }

        public static void UpdateWeapon(Hero hero)
        {
            Console.WriteLine();
            Console.WriteLine("Select a Weapon number from the list:");
            Console.WriteLine();
            int index = 0;

            foreach (Weapon weapon in Weapons)
            {
                Console.WriteLine($"{index + 1}. {weapon.WeaponName}: Power ({weapon.Power})");
                index++;
            }

            int selected = Int32.Parse(GetUserInput());

            if(selected < 1 || selected > Weapons.Count)
            {
                Console.WriteLine("Selection is invalid. Select a Weapon number from the list:");
                selected = Int32.Parse(GetUserInput());
            }

            hero.EquipWeapon(Weapons[selected - 1]);
            Console.WriteLine($"Weapon successfully updated");
        }
        
        public static void UpdateArmor(Hero hero)
        {
            Console.WriteLine();
            Console.WriteLine("Select an Armor number from the list:");
            Console.WriteLine();
            int index = 0;

            foreach (Armor armor in ArmorList)
            {
                Console.WriteLine($"{index + 1} - {armor.ArmorName}: Power ({armor.Power})");
                index++;
            }

            int selected = Int32.Parse(GetUserInput());

            if(selected < 1 || selected > ArmorList.Count)
            {
                Console.WriteLine("Selection is invalid. Select an Armor number from the list:");
                selected = Int32.Parse(GetUserInput());
            }

            hero.EquipArmor(ArmorList[selected - 1]);
            Console.WriteLine($"Armor successfully updated");
        }

        public static void ShowStatistics()
        {
            int totalFights = Fights.Count;
            int fightsWon = 0;
            int fightsLost = 0;

            foreach (Fight f in Fights)
            {
                if(f.HeroWins)
                {
                    fightsWon++;
                } 
                else
                {
                    fightsLost++;
                }
            }

            Console.WriteLine($"Total Fights: {totalFights}");
            Console.WriteLine($"Fights Won: {fightsWon}");
            Console.WriteLine($"Fights Lost: {fightsLost}");
        }

        public static void Start()
        {
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;

            WelcomeHeader();

            string heroName = GetUserInput();
            int newHero = InitHero(heroName);

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
                        StaticsticsHeader();
                        ShowStatistics();
                        break;
                    case "2":
                        InventoryHeader();
                        try
                        {
                            ManageInventory(newHero);
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
                                if(hero.Inventory.EquippedWeapon == null || hero.Inventory.EquippedArmor == null)
                                {
                                    Console.WriteLine($"\nSorry {hero.HeroName}! :-( You must have a Weapon and Armor to Fight.");
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
                    case "5":
                        Heroes.Clear();
                        Monsters.Clear();
                        Weapons.Clear();
                        ArmorList.Clear();
                        HeroInventory.Clear();
                        Fights.Clear();
                        Start();
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
            if (hero != null)
            {
                UpdateHeroStats(hero.HeroId);

                Random random = new Random();
                int coin = random.Next(2);
                bool monstersAvailable = false;

                // search Monster list to create subcollection of undefeated monsters to fight against hero
                List<Monster> availableMonsters = new List<Monster>();
                foreach (Monster m in Monsters)
                {
                    if (!m.IsDefeated)
                    {
                        monstersAvailable = true;
                        break;
                    }
                }

                if (monstersAvailable)
                {
                    foreach (Monster m in Monsters)
                    {
                        if (!m.IsDefeated)
                        {
                            availableMonsters.Add(m);
                        }
                    }
                    // select a random monster from list of available monsters
                    Monster chosenMonster = availableMonsters[random.Next(availableMonsters.Count)];

                    // display hero and monster stats before fight
                    Console.WriteLine(" Battle Info:");
                    Console.WriteLine("--------------");
                    hero.GetStats();
                    Console.WriteLine("Weapon and Armor:");
                    hero.Inventory.GetInventory();
                    chosenMonster.GetStats();
                    Console.WriteLine();

                    // initialize new Fight object
                    Fight newFight = new Fight(_fightIdCounter, hero, chosenMonster);
                    _fightIdCounter++;
                    Fights.Add(newFight);

                    Console.WriteLine(" BEGIN:");
                    Console.WriteLine("--------------");

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
                else // if no monsters are avilable, Hero has cleared the level
                {
                    Console.WriteLine("AWESOME JOB! Level Cleared!");
                    return;
                }
            }
            else
            {
                Console.WriteLine("Hero not found.");
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
            Console.WriteLine("**********************-------- WELCOME HERO! --------************************");
            Console.WriteLine("---------------------*********************************-----------------------");
            Console.WriteLine("Enter your name:");
        }

        public static void StaticsticsHeader()
        {
            Console.WriteLine();
            Console.WriteLine("---------------------*********************************-----------------------");
            Console.WriteLine("**********************------- GAME STATISTICS -------************************");
            Console.WriteLine("---------------------*********************************-----------------------");
            Console.WriteLine();
        }
        
        public static void InventoryHeader()
        {
            Console.WriteLine();
            Console.WriteLine("---------------------*********************************-----------------------");
            Console.WriteLine("**********************----------- INVENTORY ---------************************");
            Console.WriteLine("---------------------*********************************-----------------------");
            Console.WriteLine();
        }

        public static void FightHeader()
        {
            Console.WriteLine();
            Console.WriteLine("---------------------*********************************-----------------------");
            Console.WriteLine("**********************------------- FIGHT! ----------************************");
            Console.WriteLine("---------------------*********************************-----------------------");
            Console.WriteLine();
        }
        
        public static void ManageInventoryMenu()
        {
            Console.WriteLine();
            Console.WriteLine("Choose an option:");
            Console.WriteLine("1. Equip Weapon");
            Console.WriteLine("2. Equip Armor");
            Console.WriteLine("3. Back to Main Menu");
        }

        public static void Menu2()
        {
            Console.WriteLine();
            Console.WriteLine("---------------------*********************************-----------------------");
            Console.WriteLine("**********************----------- MAIN MENU ---------************************");
            Console.WriteLine("---------------------*********************************-----------------------");
            Console.WriteLine();
            Console.WriteLine("Choose an option:");
            Console.WriteLine("1. Display Statistics");
            Console.WriteLine("2. Manage Inventory");
            Console.WriteLine("3. Fight");
            Console.WriteLine("4. Exit");
            if(Fights.Count > 0)
            {
                Console.WriteLine("5. Reset Game");
            }
        }
    }
}
