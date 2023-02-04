using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
         * The “damage” of that attack is calculated by:
         * Monster health - Damage: (Hero Base Strength + Equipped Weapon Power - Monster Defense)
         * 
         * Monster Health(20) - (5 + 10 - 3)
         * 
         * For example, if the hero's Base Strength is 5, and the Equipped Weapon's Power is 10, and the Monster's Defence is 3, the resulting damage is 5 + 10 - 3 = 12. If the Monster has 20 health, it is reduced by 12, to 8. 
         * **/
        public void HeroTurn(Hero hero, Monster monster, Fight fight)
        {
            int monsterDamage = 0;
            Console.WriteLine($"Monster current health: {monster.CurrentHealth}");

            // calculate damage
            int monsterHealth = monster.CurrentHealth;
            int attack = hero.BaseStrength + hero.Inventory.Weapon.Power - monster.Defense;

            monsterDamage = monsterHealth - attack;

            // update monster current health
            if(monsterDamage < 0)
            {
                monster.CurrentHealth = 0;
                Console.WriteLine($"Monster Damage: 0");
            } 
            else if (monsterDamage > monster.OriginalHealth)
            {
                monster.CurrentHealth = monster.OriginalHealth;
                Console.WriteLine($"Monster Damage: {monster.OriginalHealth}");
            } else
            {
                monster.CurrentHealth = monsterDamage;
                Console.WriteLine($"Monster Damage: {monsterDamage}");
            }

            // if monster is still alive, invoke MonsterTurn
            if(monster.CurrentHealth > 0)
            {
                Console.WriteLine("\nMonster's Turn");
                MonsterTurn(hero, monster, fight);
            } else
            {
                Win(hero, monster, fight);
            }
        }

        /***
         * The “damage” of the Monster attack is calculated by:
         * Hero Current Health - (Monster Strength - Hero Base Defence - Equipped Armours Power)
         * 
         * Hero Health(10) - (20 - 3 - 7)
         * 
         * For example, if the Monster's Strength is 20, and the Hero's Base Defence is 3, and their Equipped Armour's Power is 7, then the resulting damage is 20 - 3 - 7 = 10. If the Hero's current health is 10, then it is reduced to 0.
         * **/
        public void MonsterTurn(Hero hero, Monster monster, Fight fight)
        {            
            int heroDamage = 0;
            Console.WriteLine($"Hero current health: {hero.CurrentHealth}");

            // calculate damage
            int heroHealth = hero.CurrentHealth;
            int attack = monster.Strength - hero.BaseDefense - hero.Inventory.Armor.Power;

            heroDamage = heroHealth - attack;

            // update hero current health
            if(heroDamage < 0)
            {
                hero.CurrentHealth = 0;
                Console.WriteLine($"Hero Damage: 0");
            }
            else if(heroDamage > hero.OriginalHealth)
            {
                hero.CurrentHealth = hero.OriginalHealth;
                Console.WriteLine($"Hero Damage: {hero.OriginalHealth}");
            }
            else
            {
                hero.CurrentHealth = heroDamage;
                Console.WriteLine($"Hero Damage: {heroDamage}");
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
