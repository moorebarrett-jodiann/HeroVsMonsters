using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_FinalProject
{
    public class Armor
    {
        private int _armorId;
        private string _armorName;
        private int _power;

        public int ArmorId
        {
            get { return _armorId; }
        }

        public string ArmorName
        {
            get { return _armorName; }
        }

        public void SetName(string name)
        {
            if (name.Length > 3)
            {
                _armorName = name;
            }
            else
            {
                throw new ArgumentException("Armor name must contain 3 or more characters");
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

        public Armor(int id, string name, int power)
        {
            _armorId = id;
            SetName(name);
            Power = power;
        }
    }
}
