using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace OOP_FinalProject
{
    public class Fight
    {
        private int _fightId;
        private bool _heroWins;

        private Hero _hero;
        private Monster _monster;

        public int FightId
        {
            get { return _fightId; }
        }

        public Hero Hero
        {
            get { return _hero; }
            set { _hero = value; }
        }

        public Monster Monster
        {
            get { return _monster; }
            set { _monster = value; }
        }

        public bool HeroWins
        {
            get { return _heroWins; }
            set { _heroWins = value; }
        }

        /***
         * Damage Equation:
         * 
         * Original equation was throwing off damage results. Did some research and found a solution 
         * to account for possible negative values and values that exceed original health
         * 
         * if (attack >= defense) {
         *      damage = attack * 2 - defense;
         * } else {
         *      damage = attack * attack / defense;
         * }
         * 
         * https://tung.github.io/posts/simplest-non-problematic-damage-formula/#:~:text=damage%20%3D%20attack%20*%20attack%20%2F%20defense,the%20damage%20value%20will%20skyrocket
         * **/
        public void HeroTurn(Hero hero, Monster monster, Fight fight)
        {
            int damage = 0;
            int newHealth = 0;

            // calculate damage
            int monsterHealth = monster.CurrentHealth;
            int defense = monster.Defense;
            int attack = hero.BaseStrength; 
            int weaponPower = hero.Inventory.EquippedWeapon.Power;

            if (attack >= defense)
            {
                damage = (((attack * 2) + weaponPower) - defense);
                if (damage == 0)
                    damage = 1;
                Console.WriteLine($"Damage: {damage}");
            }
            else
            {
                damage = (((attack * attack) + weaponPower) / defense);
                if (damage == 0)
                    damage = 1;
                Console.WriteLine($"Damage: {damage}");
            }
            
            newHealth = monsterHealth - damage;

            // update monster health
            if (newHealth > 0)
            {
                monster.CurrentHealth = monsterHealth - damage;
                Console.WriteLine($"Monster current health: {monster.CurrentHealth}");
            }
            else
            {
                monster.CurrentHealth = 0;
                Console.WriteLine($"Monster current health: 0");
            }

            // if monster is still alive, invoke MonsterTurn
            if (monster.CurrentHealth > 0)
            {
                Console.WriteLine("\nMonster's Turn");
                MonsterTurn(hero, monster, fight);
            }
            else
            {
                Win(hero, monster, fight);
            }
        }

        public void MonsterTurn(Hero hero, Monster monster, Fight fight)
        {            
            int damage = 0;
            int newHealth = 0;
            
            // calculate damage
            int heroHealth = hero.CurrentHealth;
            int defense = hero.BaseDefense; 
            int attack = monster.Strength;
            int armorPower = hero.Inventory.EquippedArmor.Power;

            if (attack >= defense)
            {
                damage = (((attack * 2) + armorPower) - defense);
                if (damage == 0)
                    damage = 1; 
                Console.WriteLine($"Damage: {damage}");
            }
            else
            {
                damage = (((attack * attack) + armorPower) / defense);
                if (damage == 0)
                    damage = 1;
                Console.WriteLine($"Damage: {damage}");
            }

            newHealth = heroHealth - damage;

            // update hero health
            if(newHealth > 0)
            {
                hero.CurrentHealth = heroHealth - damage;
                Console.WriteLine($"Hero current health: {hero.CurrentHealth}");
            } 
            else
            {
                hero.CurrentHealth = 0;
                Console.WriteLine($"Hero current health: 0");
            }

            // if hero is still alive, invoke HeroTurn
            if (hero.CurrentHealth > 0)
            {
                Console.WriteLine("\nHero's Turn");
                HeroTurn(hero, monster, fight);
            } else
            {
                Lose(hero, monster, fight);
            }
        }

        /***
         *  If the Hero wins, the Monster should no longer appear in the game, until the Hero Loses.
         * **/
        public void Win(Hero hero, Monster monster, Fight fight)
        {
            // set monster isdefeated to true
            // reset hero current health to original health
            // set Fight flag to 'HeroWins = true'
            // add fight to hero fight Hashset
            Console.WriteLine("\nHero Wins!");
            hero.CurrentHealth = hero.OriginalHealth;
            monster.IsDefeated = true;
            HeroWins = true;
            hero.AddFightHistory(fight);
        }

        /***
         *  If the Hero Loses, their CurrentHealth is set to equal their OriginalHealth, and any Monsters that were previously defeated can appear again
         * **/
        public void Lose(Hero hero, Monster monster, Fight fight)
        {
            // reset hero current health to original health
            // revive any previously defeated monsters
            // set Fight flag to 'HeroWins = false'
            // add fight to hero fight Hashset
            Console.WriteLine("\nHero Loses!");
            hero.CurrentHealth = hero.OriginalHealth;
            Game.ReviveMonsters();
            HeroWins = false;
            hero.AddFightHistory(fight);
        }

        public Fight(int id, Hero hero, Monster monster)
        {
            _fightId = id;
            Hero = hero;
            Monster = monster;
            HeroWins = false;
        }
    }
}
