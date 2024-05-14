using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Windows;
using System.Xml.Serialization;
using static Game.Actions;
using static System.IO.Directory;

namespace Game
{
    public enum HeroClass : int
    {
        Маг,
        Воин,
        Лучник
    }
    [Serializable]
    public class Hero
    {
        public HeroClass heroClass;
        public string name;
        public byte lvl;
        public double exp;
        public double maxhp;
        public double hp;
        public double maxmp;
        public double mp;
        public double atk;
        public double spellamp;
        public int money;
        public bool food;
        public Hero() 
        {
            name = "Итта";
            lvl = 1;
            exp = 0;
            maxhp = 100;
            hp = 100;
            maxmp = 100;
            mp = 100;
            atk = 5;
            spellamp = 1;
            money = 5000;
            food = false;

        }
    }
    [Serializable]
    public class Enemy
    {
        internal double maxhp = 50;
        internal double hp = 50;
        internal double atk = 3;
        internal Enemy(double starthp)
        {
            maxhp = starthp;
            hp = starthp;
        }
    }
    [Serializable]
    public class Item
    {
        public bool bought = false;
        public bool isaspell, isaweapon, isusable = false;
        public ushort id;
        public ushort cost;
        public short val1, val2, val3, val4, val5;
        public string shopdesc;
    }
    [Serializable]
    public class Spell
    {
        public bool unlocked = false;
        public bool anyclass = true;
        public HeroClass heroClass;
        public string name;
        public ushort id;
        public int mpcost;
        public int hpcost = 0;
        public int dmg = 0;
    }
    internal class Init
    {
        internal static List<Spell> DefaultSpells()
        {
            Spell spell1 = new Spell
            {
                heroClass = HeroClass.Маг,
                id = 801,
                unlocked = true,
                name = "Огненный шар",
                mpcost = 10,
                dmg = 10,

            };
            Spell spell2 = new Spell
            {
                heroClass = HeroClass.Маг,
                id = 802,
                unlocked = false,
                name = "Пиробласт",
                mpcost = 90,
                dmg = 100
            };
            Spell spell3 = new Spell
            {
                heroClass = HeroClass.Маг,
                id = 803,
                unlocked = false,
                name = "Сжатая кровь",
                mpcost = 25,
                hpcost = 10,
                dmg = 50
            };
            Spell spell4 = new Spell
            {
                heroClass = HeroClass.Маг,
                id = 804,
                unlocked = true,
                name = "Вихрь",
                mpcost = 33,
                dmg = 5
            };
            return new List<Spell> { spell1, spell2, spell3, spell4 };
        }
        internal static List<Item> DefaultItems()
        {
            Item item1 = new Item
            {
                bought = false,
                isusable = true,
                cost = 10000,
                val1 = 20,
                shopdesc = "Расходник - Годовой запас кошачьего корма: Восстанавливает {1} здоровья при использовании. Бесконечен.\n" +
                "Стоимость: {0} монет \n",

            };
            Item item2 = new Item
            {
                bought = false,
                isaweapon = true,
                cost = 50,
                val1 = 5,
                shopdesc = "Улучшение атак - Когтеточка: Ваши атаки наносят на {1} больше урона.\n" +
                "Стоимость: {0} монет \n"
            };
            Item item3 = new Item
            {
                bought = false,
                isaspell = true,
                id = 802,
                cost = 1000,
                val1 = 90,
                val2 = 100,
                shopdesc = "Заклинание - Пиробласт: Наносит {2} урона, опустошая вашу ману\n" +
                "Требует маны: {1} маны\n" +
                "Стоимость: {0} монет \n"
            };
            Item item4 = new Item
            {
                bought = false,
                isaspell = true,
                id = 803,
                cost = 500,
                val1 = 50,
                val2 = 25,
                val3 = 10,
                shopdesc = "Заклинание - Сжатая кровь: Наносит {1} урона ценой {3} единиц твоего здоровья.\n" +
                "Требует маны: {2}\n" +
                "Стоимость: {0} монет \n"

            };
            return new List<Item>() { item1, item2, item3, item4 };
        }
    }
    internal class Serializations
    {
        internal static string rootgamepath = GetParent(GetParent(GetParent(GetCurrentDirectory()).FullName).FullName).FullName;
        internal static void SaveDirCheck()
        {
            if (!Exists($"{rootgamepath}\\save\\"))
            {
                CreateDirectory($"{rootgamepath}\\save\\");
            }
        }
        internal static void SerializeAll(Hero hero, List<Item> items, List<Spell> spells, bool init = false)
        {
            Console.Clear();
            SaveDirCheck();
            Serialize(init, items);
            Serialize(init, spells);
            Serialize(init, hero);
        }
        internal static void SerializeAll(Hero hero, List<Spell> spells)
        {
            Console.Clear();
            bool init = false;
            SaveDirCheck();
            Serialize(init, spells);
            Serialize(init, hero);
        }
        internal static void SerializeAll(Hero hero, List<Item> items)
        {
            Console.Clear();
            bool init = false;
            SaveDirCheck();
            Serialize(init, items);
            Serialize(init, hero);
        }

        private static void Serialize(bool init, List<Spell> spells)
        {
            if (!File.Exists($"{rootgamepath}\\save\\Spells.xml") | !init)
            {
                XmlSerializer sSpell = new XmlSerializer(typeof(List<Spell>));
                using (TextWriter tSpell = new StreamWriter($"{rootgamepath}\\save\\Spells.xml"))
                {
                    sSpell.Serialize(tSpell, spells);
                }
            }

        }
        private static void Serialize(bool init, List<Item> items)
        {
            if (!File.Exists($"{rootgamepath}\\save\\Items.xml") | !init)
            {
                XmlSerializer sItem = new XmlSerializer(typeof(List<Item>));
                using (TextWriter tItem = new StreamWriter($"{rootgamepath}\\save\\Items.xml"))
                {
                    sItem.Serialize(tItem, items);
                }
            }
        }

        private static void Serialize(bool init, Hero hero)
        {
            if (!File.Exists($"{rootgamepath}\\save\\Hero.xml") | !init)
            {
                XmlSerializer sHero = new XmlSerializer(typeof(Hero));
                TextWriter tHero = new StreamWriter($"{rootgamepath}\\save\\Hero.xml");
                sHero.Serialize(tHero, hero);
                tHero.Close();
            }
        }
        internal static (Hero, List<Item>, List<Spell>) Deserialize()
        {
            string savepath = $"{rootgamepath}\\save\\";
            if (!Exists(savepath))
            {
                CreateDirectory(savepath);
            }

            XmlSerializer sHero = new XmlSerializer(typeof(Hero));
            XmlSerializer sItem = new XmlSerializer(typeof(List<Item>));
            XmlSerializer sSpell = new XmlSerializer(typeof(List<Spell>));

            TextReader rHero = new StreamReader($"{savepath}Hero.xml");
            Hero hero = (Hero)sHero.Deserialize(rHero);
            rHero.Close();

            TextReader rItem = new StreamReader($"{savepath}Items.xml");
            List<Item> items = (List<Item>)sItem.Deserialize(rItem);
            rItem.Close();

            TextReader rSpell = new StreamReader($"{savepath}Spells.xml");
            List<Spell> spells = (List<Spell>)sSpell.Deserialize(rSpell);
            rSpell.Close();
            spells = utterAbominations.CheckSpells(spells, hero);

            (Hero, List<Item>, List<Spell>) result = (hero, items, spells);
            return result;
        }
        internal static void DeleteSave()
        {
            Console.Clear();
            Print("Уверен?? Меряемся хуями, если твой короче, то таки удаляем сохранение");
            int i = int.Parse(Console.ReadLine());
            if (i < 18)
            {
                Console.WriteLine("Сохранение удалено, прости");
                Delete("..\\..\\..\\save\\", true);
            }
            else
            {
                Console.WriteLine("Живи еще один день...");
            }
            using (StreamWriter stream = new StreamWriter(rootgamepath + "\\huh.txt", true))
            {
                stream.WriteLine($"{DateTime.Now} - {i} км");
            }
            return;
        }
    }
    internal static class Actions
    {
        internal static void Print(string str)
        {
            char[] array = str.ToCharArray();
            foreach (char c in array)
            {
                Console.Write(c);
                Thread.Sleep(10);
            }
            Console.WriteLine();
        }
        internal static void Print(string str, int sleeptime)
        {
            char[] array = str.ToCharArray();
            foreach (char c in array)
            {
                Console.Write(c);
                Thread.Sleep(sleeptime);
            }
            Console.WriteLine();
        }
        internal static void CallVid()
        {
            Console.Clear();
            Application app = new Application();
            UserControl1 u = new UserControl1();
            app.Run(u);
        }
    }
}
