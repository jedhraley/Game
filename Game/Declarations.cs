using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Windows;
using System.Xml.Serialization;
using static Game.Actions;

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
        public string name = "Итта";
        public byte lvl = 1;
        public double exp = 0;
        public double maxhp = 100;
        public double hp = 100;
        public double maxmp = 100;
        public double mp = 100;
        public double atk = 5;
        public double spellamp = 1;
        public int money = 5000;
        public bool food = false;
    }
    [Serializable]
    public class Enemy
    {
        public double maxhp = 50;
        public double hp = 50;
        public double atk = 3;
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
        public int id;
        public int mpcost;
        public int hpcost = 0;
        public int dmg = 0;
    }
    public class Init
    {
        public static List<Spell> DefaultSpells()
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
            return new List<Spell> { spell1, spell2, spell3 };
        }
        public static List<Item> DefaultItems()
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
                shopdesc = "Заклинание - Пиробласт: Наносит {1} урона, опустошая вашу ману\n" +
                "Требует маны: {1}% манапула\n" +
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
    public class Serializations
    {
        public static void SaveDirCheck()
        {
            if (!Directory.Exists("..\\..\\..\\save\\"))
            {
                Directory.CreateDirectory("..\\..\\..\\save\\");
            }
        }
        public static void SerializeAll(bool init, Hero hero, List<Item> items, List<Spell> spells)
        {
            Console.Clear();
            SaveDirCheck();
            Serialize(init, items);
            Serialize(init, spells);
            Serialize(init, hero);
        }
        public static void SerializeAll(Hero hero, List<Item> items, List<Spell> spells)
        {
            Console.Clear();
            bool init = false;
            SaveDirCheck();
            Serialize(init, items);
            Serialize(init, spells);
            Serialize(init, hero);
        }
        public static void SerializeAll(Hero hero, List<Spell> spells)
        {
            Console.Clear();
            bool init = false;
            SaveDirCheck();
            Serialize(init, spells);
            Serialize(init, hero);
        }
        public static void SerializeAll(Hero hero, List<Item> items)
        {
            Console.Clear();
            bool init = false;
            SaveDirCheck();
            Serialize(init, items);
            Serialize(init, hero);
        }

        static void Serialize(bool init, List<Spell> spells)
        {
            if (!File.Exists("..\\..\\..\\save\\Spells.xml") | !init)
            {
                XmlSerializer sSpell = new XmlSerializer(typeof(List<Spell>));
                using (TextWriter tSpell = new StreamWriter("..\\..\\..\\save\\Spells.xml"))
                {
                    sSpell.Serialize(tSpell, spells);
                }
            }

        }
        static void Serialize(bool init, List<Item> items)
        {
            if (!File.Exists("..\\..\\..\\save\\Items.xml") | !init)
            {
                XmlSerializer sItem = new XmlSerializer(typeof(List<Item>));
                using (TextWriter tItem = new StreamWriter("..\\..\\..\\save\\Items.xml"))
                {
                    sItem.Serialize(tItem, items);
                }
            }
        }

        static void Serialize(bool init, Hero hero)
        {
            if (!File.Exists("..\\..\\..\\save\\Hero.xml") | !init)
            {
                XmlSerializer sHero = new XmlSerializer(typeof(Hero));
                TextWriter tHero = new StreamWriter("..\\..\\..\\save\\Hero.xml");
                sHero.Serialize(tHero, hero);
                tHero.Close();
            }
        }
        public static (Hero, List<Item>, List<Spell>) Deserialize()
        {
            string savepath = "..\\..\\..\\save\\";
            if (!Directory.Exists(savepath))
            {
                Directory.CreateDirectory(savepath);
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

            (Hero, List<Item>, List<Spell>) result = (hero, items, spells);
            return result;
        }
        public static void DeleteSave()
        {
            Console.Clear();
            Print("Уверен?? Меряемся хуями, если твой короче, то таки удаляем сохранение", 30);
            if (int.Parse(Console.ReadLine()) < 18)
            {
                Console.WriteLine("Сохранение удалено, прости");
                Directory.Delete("..\\..\\..\\save\\", true);
            }
            else
            {
                Console.WriteLine("Живи еще один день...");
            }
        }
    }
    public static class Actions
    {
        public static void Print(string str)
        {
            char[] array = str.ToCharArray();
            foreach (char c in array)
            {
                Console.Write(c);
                Thread.Sleep(10);
            }
            Console.WriteLine();
        }
        public static void Print(string str, int sleeptime)
        {
            char[] array = str.ToCharArray();
            foreach (char c in array)
            {
                Console.Write(c);
                Thread.Sleep(sleeptime);
            }
            Console.WriteLine();
        }
        public static void ClassSelection(Hero hero)
        {
            Serializations.SaveDirCheck();
            if (!File.Exists("..\\..\\..\\save\\Hero.xml"))
            {
                Print("Выбери свой класс, путник.\n");
                Console.WriteLine("1) Маг: его способности требуют много ума и маны, но зато наносят большой урон.\n" +
                                  "Имеет мало здоровья, но кому оно нужно, если ты можешь кидать фаерболлы, правда?");
                Console.WriteLine("2) Воин: ");
                Console.WriteLine("3) Лучник: ");
                bool parsed = int.TryParse(Console.ReadLine(), out int sel);
                if (parsed == true)
                {
                    switch (sel)
                    {
                        case 1:
                            hero.heroClass = 0;
                            break;
                        case 2:
                            hero.heroClass = (HeroClass)1;
                            break;
                        case 3:
                            hero.heroClass = (HeroClass)2;
                            break;
                    }
                }
                else
                {
                    Console.Clear();
                    Print("Пиши не гадости, а циферку");
                }
            }
        }
        public static void ApplyItem(this Item item, Hero hero, List<Spell> spells)
        {
            hero.money -= item.cost;
            item.bought = true;
            if (item.isusable) // 1 штука
            {
                hero.food = true;
            }
            if (item.isaspell) // 2 штуки
            {
                foreach (Spell spell in spells)
                {
                    if (item.id == spell.id && ((hero.heroClass == spell.heroClass) | spell.anyclass))
                    {
                        spell.unlocked = true;
                        return;
                    }
                }
            }
            if (item.isaweapon) // 1 штука....
            {
                hero.atk += 5;
            }
            return;
        }
        public static void CallVid()
        {
            Console.Clear();
            Application app = new Application();
            UserControl1 u = new UserControl1();
            app.Run(u);
        }
    }
}
