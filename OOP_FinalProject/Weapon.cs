using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_FinalProject
{
    public class Weapon
    {
        private int _weaponId;
        private string _weaponName;
        private int _power;

        public int WeaponId
        {
            get { return _weaponId; }
        }

        public string WeaponName
        {
            get { return _weaponName; }
        }

        public void SetName(string name)
        {
            if (name.Length > 3)
            {
                _weaponName = name;
            }
            else
            {
                throw new ArgumentException("Weapon name must contain 3 or more characters");
            }
        }

        public int Power
        {
            get { return _power; }
            set
            {
                if (value < 1)
                {
                    throw new ArgumentException("Power must be greater than 0");
                }
                else
                {
                    _power = value;
                }
            }
        }

        public Weapon(int id, string name, int power)
        {
            _weaponId = id;
            SetName(name);
            Power = power;
        }
    }
}
