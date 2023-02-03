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

        private List<Monster> _monsters = new List<Monster>();

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

            MonsterTurn(monsterDamage, hero, monster);
        }

        /***
         * The “damage” of the Monster attack is calculated by:
         * Hero Current Health - (Monster Strength - (Hero Base Defence + Equipped Armours Power))
         * **/
        public void MonsterTurn(int damage, Hero hero, Monster monster)
        {
            int heroDamage = damage;

            
            HeroTurn(heroDamage, hero, monster);
        }

        public void Win(int damage, Hero hero, Monster monster)
        {

        }
        
        public void Lose(int damage, Hero hero, Monster monster)
        {

        }

        public void BeginFight(Hero hero)
        {    
            Random rand = new Random();

            // create 5 monster objects and store to list
            for (int i = 0; i < 5; i++)
            {
                //returns random number between 1-20
                int defense = rand.Next(1, 21);
                int strength = rand.Next(1, 21);

                Monster randomMonster = new Monster(monsterId, $"Monster{i+1}", defense, strength);
                _monsters.Add(randomMonster);
            }

            // pick a random monster in list
            int index = rand.Next(_monsters.Count);
            Monster pickedMonster = _monsters[index];



        }

        public Fight(int id, Hero hero, Monster monster)
        {
            _fightId = id;
            Hero = hero;
            Monster = monster;
        }
    }
}
