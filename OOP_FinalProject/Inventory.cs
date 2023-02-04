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
        private Weapon? _equippedWeapon;
        private Armor? _equippedArmor;

        public int InventoryId
        {
            get { return _inventoryId; }
        }

        public Weapon? EquippedWeapon
        {
            get { return _equippedWeapon; }
            set { _equippedWeapon = value; }
        }

        public Armor? EquippedArmor
        {
            get { return _equippedArmor; }
            set { _equippedArmor = value; }
        }
        
        public Hero? Hero
        {
            get { return _hero; }
            set { _hero = value; }
        }

        // returns what items the Hero is equipped with
        public string GetInventory()
        {
            string inventory = $"{EquippedWeapon.WeaponName} with Power: {EquippedWeapon.Power}\n{EquippedArmor.ArmorName} with Power {EquippedArmor.Power}";
            Console.WriteLine(inventory);
            return inventory;
        }

        public Inventory(int id, Hero hero)
        {
            _inventoryId = id;
            EquippedArmor = null;
            EquippedWeapon = null;
            Hero = hero;
        }        
    }
}
