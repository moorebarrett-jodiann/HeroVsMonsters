using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_FinalProject
{
    public class Inventory
    {
        private int _inventoryId;
        private Hero _hero;
        private Weapon? _weapon;
        private Armor? _armor;

        public int InventoryId
        {
            get { return _inventoryId; }
        }

        public Weapon? Weapon
        {
            get { return _weapon; }
            set { _weapon = value; }
        }

        public Armor? Armor
        {
            get { return _armor; }
            set { _armor = value; }
        }
        
        public Hero? Hero
        {
            get { return _hero; }
            set { _hero = value; }
        }

        public Inventory(int id, Hero hero)
        {
            _inventoryId = id;
            Armor = null;
            Weapon = null;
            Hero = hero;
        }        
    }
}
