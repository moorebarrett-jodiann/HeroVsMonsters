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

        /***
         * The “damage” of the Hero attack is calculated by:
         * Monster health - Damage: (Hero Base Strength + Equipped Weapon Power)
         * **/
        public void HeroTurn(int damage, Hero hero, Monster monster)
        {
            int monsterDamage = damage;
            // calculate damage
            // update monster current health
            if(monster.CurrentHealth > 0)
            {
                MonsterTurn(monsterDamage, hero, monster);
            } else
            {
                Win(monsterDamage, hero, monster);
            }
        }

        /***
         * The “damage” of the Monster attack is calculated by:
         * Hero Current Health - (Monster Strength - (Hero Base Defence + Equipped Armours Power))
         * **/
        public void MonsterTurn(int damage, Hero hero, Monster monster)
        {
            int heroDamage = damage;
            // calculate damage
            // update hero current health
            if (hero.CurrentHealth > 0)
            {
                HeroTurn(heroDamage, hero, monster);
            } else
            {
                Lose(heroDamage, hero, monster);
            }
        }

        public void Win(int damage, Hero hero, Monster monster)
        {
            // set monster isdefeated to true
        }
        
        public void Lose(int damage, Hero hero, Monster monster)
        {

        }

        public Fight(int id, Hero hero, Monster monster)
        {
            _fightId = id;
            Hero = hero;
            Monster = monster;
        }
    }
}
