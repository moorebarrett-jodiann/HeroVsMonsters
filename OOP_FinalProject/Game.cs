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
        public static Hero globalHero = null;
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

        // method to initialize Hero
        public static void InitHero(string name)
        {
            Random rand = new Random();

            //generate strength and defense values between 1 - 10
            int defense = rand.Next(1, 11);
            int strength = rand.Next(1, 11);

            Hero newHero = new Hero(_heroIdCounter, name, defense, strength);
            Heroes.Add(newHero);
            globalHero = newHero;
            _heroIdCounter++;
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

        // create 5 monsters based on the current strength, health and defense level of hero
        public static void CreateMonsters(int level = 0)
        {
            Random rand = new Random();
            Monsters.Clear();

            int defense = 0;
            int strength = 0;

            for (int i = 0; i < 5; i++)
            {
                if(level == 0)
                {
                    //generate strength and defense values between 1 - 13
                    defense = rand.Next(1, 14);
                    strength = rand.Next(1, 14);
                } 
                else if (level == 1)
                {
                    //generate strength and defense values between 5 - 16
                    defense = rand.Next(5, 17);
                    strength = rand.Next(5, 17);
                } 
                else if (level == 2)
                {
                    //generate strength and defense values between 5 - 20
                    defense = rand.Next(5, 21);
                    strength = rand.Next(5, 21);
                } 
                else if (level == 3)
                {
                    //generate strength and defense values between 7 - 25
                    defense = rand.Next(7, 26);
                    strength = rand.Next(7, 26);
                }                

                Monster randomMonster = new Monster(_monsterIdCounter, $"Monster {i + 1}", defense, strength);
                Monsters.Add(randomMonster);
                _monsterIdCounter++;
            }
        }
        
        // create 5 weapons with varying values for power
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
        
        // create 5 armor items with varying values for power
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

        // method to initialize inventory
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

        // method to revive all previously defeated monsters if hero loses a single fight 
        public static void ReviveMonsters()
        {
            foreach (Monster m in Monsters)
            {
                m.IsDefeated = false;
                m.CurrentHealth = m.OriginalHealth;
            }
        }

        // method to manage hero weapon and armor
        public static void ManageInventory()
        {

            if (globalHero != null)
            {
                Weapon? weapon = globalHero.Inventory.EquippedWeapon;
                Armor? armor = globalHero.Inventory.EquippedArmor;

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
                    UpdateWeapon(globalHero);
                }
                else if (equipOption == 2)
                {
                    UpdateArmor(globalHero);
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

        // method to update hero armor
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
        
        // method to update hero armor
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

        // method to show game statistics
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

        //method to boost Hero Stats and pay for boost with coins
        public static void BoostHero(Hero hero, string stat, int amount, int cost)
        {
            if(hero != null)
            {
                if(stat.ToLower() == "health")
                {
                    hero.OriginalHealth += amount;
                    hero.CurrentHealth = hero.OriginalHealth;
                    Console.WriteLine($"Health successfully updated");
                }
                else if(stat.ToLower() == "defense")
                {
                    hero.BaseDefense += amount;
                    Console.WriteLine($"Defense successfully updated");
                } 
                else if(stat.ToLower() == "strength")
                {
                    hero.BaseStrength += amount;
                    Console.WriteLine($"Strength successfully updated");
                }

                hero.Coins -= cost;
            } else
            {
                Console.WriteLine("Hero not found.");
            }
        }

        public static void Start()
        {
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;

            WelcomeHeader();

            string heroName = GetUserInput();
            InitHero(heroName);

            // initialize a list of monsters, weapons and armor
            CreateMonsters();
            CreateWeapons(5);
            CreateArmor(5);
            InitInventory(globalHero);

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
                            ManageInventory();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }                        
                        break;
                    case "3":
                        if (globalHero != null)
                        {
                            if(globalHero.Inventory.EquippedWeapon == null || globalHero.Inventory.EquippedArmor == null)
                            {
                                Console.WriteLine($"\nSorry {globalHero.HeroName}! :-( You must have a Weapon and Armor to Fight.");
                            } else
                            {
                                FightHeader();
                                CoinToss(globalHero);                              
                            }
                        }
                        else
                        {
                            Console.WriteLine("Hero Player not Found.");
                        }
                        break;
                    case "4":
                        BoostStatsHeader();
                        if (globalHero != null)
                        {
                            if(globalHero.Coins > 0)
                            {
                                int coins = globalHero.Coins;
                                if(coins >= 10 && coins < 20)
                                {
                                    BoostStats(globalHero, 1);                                        
                                } 
                                else if (coins >= 20 && coins < 30)
                                {
                                    BoostStats(globalHero, 2);
                                } 
                                else if(coins >= 30 )
                                {
                                    BoostStats(globalHero, 3);                                        
                                } else
                                {
                                    Console.WriteLine($"\nSorry {globalHero.HeroName}! :-( You do not have enough coins to boost your stats.");
                                }
                            } else
                            {
                                Console.WriteLine($"\nSorry {globalHero.HeroName}! :-( You do not have enough coins to boost your stats.");
                            }
                        } else
                        {
                            Console.WriteLine("Hero Player not Found.");
                        }
                        break;
                    case "5":
                        if(globalHero != null)
                        {
                            globalHero.Inventory = null;
                            globalHero.Coins = 0;
                            globalHero.ResetFights();
                        }
                        Heroes.Clear();
                        Monsters.Clear();
                        Weapons.Clear();
                        ArmorList.Clear();
                        HeroInventory.Clear();
                        Fights.Clear();
                        globalHero = null;
                        Start();
                        break;
                    case "6":
                        Console.WriteLine("\nThanks for playing!");
                        looping = false;
                        break;
                    default:
                        Console.WriteLine("Invalid input. Please try again.");
                        break;
                }
            }
        }

        // method to boost hero's stats once they've accumulated enough coins
        public static void BoostStats(Hero hero, int level)
        {
            if (level == 1)
            {
                Console.WriteLine("Congratulations! You've earned enough coins for a Level 1 Boost.");
                Console.WriteLine();
                Console.WriteLine("****** Hero Stats ******");
                hero.GetStats();
                Console.WriteLine($"Coins Remaining: {hero.Coins}");
                Console.WriteLine();
                Console.WriteLine("Choose an option:");
                Console.WriteLine("1. 20 Coins - Boost Original Health (+25)");
                Console.WriteLine("2. 15 Coins - Boost Defense (+5)");
                Console.WriteLine("3. 15 Coins - Boost Strength (+5)");
                Console.WriteLine("4. Back to Main Menu");

                int boostOption = Int32.Parse(GetUserInput());

                while (boostOption != 1 && boostOption != 2 && boostOption != 3 && boostOption != 4)
                {
                    Console.WriteLine("Invalid Option Selected");
                    BoostStats(hero, 1);
                    boostOption = Int32.Parse(GetUserInput());
                }

                if (boostOption == 1)
                {
                    BoostHero(hero, "health", 25, 20);
                }
                else if (boostOption == 2)
                {
                    BoostHero(hero, "defense", 5, 15);
                }
                else if (boostOption == 3)
                {
                    BoostHero(hero, "strength", 5, 15);
                }
                else if (boostOption == 4)
                {
                    return;
                }

                // boost monsters' strengths and defenses
                CreateMonsters(1);
            }
            else if (level == 2)
            {
                Console.WriteLine("Congratulations! You've earned enough coins for a Level 2 Boost.");
                Console.WriteLine();
                Console.WriteLine("****** Hero Stats ******");
                hero.GetStats();
                Console.WriteLine($"Coins Remaining: {hero.Coins}");
                Console.WriteLine();
                Console.WriteLine("Choose an option:");
                Console.WriteLine("1. 25 Coins - Boost Original Health (+30)");
                Console.WriteLine("2. 20 Coins - Boost Defense (+10)");
                Console.WriteLine("3. 20 Coins - Boost Strength (+10)");
                Console.WriteLine("4. Back to Main Menu");

                int boostOption = Int32.Parse(GetUserInput());

                while (boostOption != 1 && boostOption != 2 && boostOption != 3 && boostOption != 4)
                {
                    Console.WriteLine("Invalid Option Selected");
                    BoostStats(hero, 2);
                    boostOption = Int32.Parse(GetUserInput());
                }

                if (boostOption == 1)
                {
                    BoostHero(hero, "health", 30, 25);
                }
                else if (boostOption == 2)
                {
                    BoostHero(hero, "defense", 10, 20);
                }
                else if (boostOption == 3)
                {
                    BoostHero(hero, "strength", 10, 20);
                }
                else if (boostOption == 4)
                {
                    return;
                }

                // boost monsters' strengths and defenses
                CreateMonsters(2);
            }
            else if (level == 3)
            {
                Console.WriteLine("Congratulations! You've earned enough coins for a Level 3 Boost.");
                Console.WriteLine();
                Console.WriteLine("****** Hero Stats ******");
                hero.GetStats();
                Console.WriteLine($"Coins Remaining: {hero.Coins}");
                Console.WriteLine();
                Console.WriteLine("Choose an option:");
                Console.WriteLine("1. 30 Coins - Boost Original Health (+40)");
                Console.WriteLine("2. 25 Coins - Boost Defense (+15)");
                Console.WriteLine("3. 25 Coins - Boost Strength (+15)");
                Console.WriteLine("4. Back to Main Menu");

                int boostOption = Int32.Parse(GetUserInput());

                while (boostOption != 1 && boostOption != 2 && boostOption != 3 && boostOption != 4)
                {
                    Console.WriteLine("Invalid Option Selected");
                    BoostStats(hero, 3);
                    boostOption = Int32.Parse(GetUserInput());
                }

                if (boostOption == 1)
                {
                    BoostHero(hero, "health", 40, 30);
                }
                else if (boostOption == 2)
                {
                    BoostHero(hero, "defense", 15, 25);
                }
                else if (boostOption == 3)
                {
                    BoostHero(hero, "strength", 15, 25);
                }
                else if (boostOption == 4)
                {
                    return;
                }

                // boost monsters' strengths and defenses
                CreateMonsters(3);
            }
        }

        // toss coin to randomly allow Hero to begin fight or Monster to begin fight
        public static void CoinToss(Hero hero)
        {
            if (hero != null)
            {
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
                    Console.WriteLine("****** Hero Stats ******");
                    hero.GetStats();
                    Console.WriteLine($"Coins: {globalHero.Coins}");
                    Console.WriteLine("****** Weapon and Armor ******");
                    hero.Inventory.GetInventory();
                    Console.WriteLine("****** Monster Stats ******");
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
        
        public static void BoostStatsHeader()
        {
            Console.WriteLine();
            Console.WriteLine("---------------------*********************************-----------------------");
            Console.WriteLine("**********************--------- BOOST STATS ---------************************");
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
            Console.WriteLine("4. Boost Stats");
            Console.WriteLine("5. Reset Game");
            Console.WriteLine("6. Exit");
        }
    }
}
