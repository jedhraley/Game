using System;
using System.Collections.Generic;
using System.IO;
using System.Media;
using static Game.Actions;
using static Game.Init;
using static Game.Misc;
using static Game.Serializations;
using static Program;
using static System.IO.Directory;



namespace Game
{
    internal static class Maine
    {
        internal static Hero hero = new Hero();
        internal static List<Item> items = DefaultItems();
        internal static List<Spell> spells = DefaultSpells();

        [STAThread]
        private static void Main()
        {
            string rootgamepath = GetParent(GetParent(GetParent(GetCurrentDirectory()).FullName).FullName).FullName;
            SoundPlayer typewriter = new SoundPlayer
            {
                SoundLocation = $"{rootgamepath}\\Game\\Resources\\DDLC-Yuri-sdeath.wav"
            };
            typewriter.PlayLooping();
            ClassSelection(hero);
            SerializeAll(hero, items, spells, true);
            (hero, items, spells) = Deserialize();
            MainLoop(hero, items, spells);
        }
        private static void MainLoop(Hero hero, List<Item> items, List<Spell> spells)
        {
            while (hero.hp > 0)
            {
                _consoleCtrlHandler += _ => { SerializeAll(hero, items, spells); Environment.Exit(0x1); return false; };
                _ = SetConsoleCtrlHandler(_consoleCtrlHandler, true);
                Console.WriteLine("1) Путешествие");
                Console.WriteLine("2) Магазик");
                Console.WriteLine("3) Зайти домой");
                Console.WriteLine("4) Удаление сохранения");
                Console.WriteLine("5) секретные документы о которых путин не хочет чтобы вы знали");
                bool parsed = int.TryParse(Console.ReadLine(), out int sel);
                if (parsed == true)
                {
                    switch (sel)
                    {
                        case 1:
                            Adventure(hero, spells); break;
                        case 2:
                            Shop(hero, items, spells); break;
                        case 3:
                            Home(); break;
                        case 4:
                            DeleteSave(); return;
                        case 5:
                            CallVid(); return;
                        default:
                            Console.Clear(); break;
                    }
                }
                else
                {
                    Console.Clear();
                    Print("Пиши не гадости, а циферку\n");
                }
            }
        }
        internal static void Battle(Enemy enemy, Hero hero, List<Spell> spells)
        {
            while (hero.hp > 0 && enemy.hp > 0)
            {
                _consoleCtrlHandler += _ => { SerializeAll(hero, items, spells); Environment.Exit(0x1); return false; };
                _ = SetConsoleCtrlHandler(_consoleCtrlHandler, true);
                Print($"У вас {hero.hp} здоровья и {hero.mp} маны. У врага {enemy.hp} здоровья.");
                Print($"Выберите действие:\n1) Атака \n2) Заклинание \n3) Расходники \n4) уходим по ******");
                bool parsed = int.TryParse(Console.ReadLine(), out int sel);
                if (parsed == true)
                {
                    switch (sel)
                    {
                        case 1:
                            enemy.hp -= hero.atk;
                            if (enemy.hp > 0)
                            {
                                hero.hp -= enemy.atk;
                                Print("");
                            }
                            break;
                        case 2:
                            SpellSelect(enemy, hero, spells); break;
                        case 3:
                            Useables(enemy, hero, spells); break;
                        case 4:
                            Console.Clear();
                            if (Flee() == true)
                            {
                                return;
                            }
                            else break;
                        default:
                            Console.Clear(); break;
                    }
                }
                else
                {
                    Console.Clear();
                    Print("Пиши не гадости, а циферку");
                }
            }
            if (hero.hp > 0 & enemy.hp <= 0)
            {
                hero.exp += Math.Round(enemy.maxhp * 0.1);
                Console.WriteLine("Враг повержен! Вы получили 5 опыта.");
            }

        }
        static void Home()
        {
            Console.WriteLine("1) Посмотреться в зеркало");
            Console.WriteLine("2) Пойти баиньки");
            bool parsed = int.TryParse(Console.ReadLine(), out int sel);
            if (parsed)
            {
                switch (sel)
                {
                    case 1:
                        Profile(hero, spells); break;
                    case 2:
                        Sleep(hero); break;
                }
            }
        }
        static void Sleep(Hero hero)
        {
            _consoleCtrlHandler += _ => { SerializeAll(hero, items, spells); Environment.Exit(0x1); return false; };
            SetConsoleCtrlHandler(_consoleCtrlHandler, true);
            var currhp = hero.hp;
            var currmp = hero.mp;
            while (hero.hp < hero.maxhp | hero.mp < hero.maxmp)
            {
                hero.hp += 5;
                hero.mp += 5;
                if (hero.hp > hero.maxhp) hero.hp = hero.maxhp;
                if (hero.mp > hero.maxmp) hero.mp = hero.maxmp;
                Console.Clear();
                Animations.SleepAnim();
            }
            Console.WriteLine($"Вы восстановили {hero.hp - currhp} здоровья и {hero.mp - currmp} маны пока спали.");

        }
        private static void SpellSelect(Enemy enemy, Hero hero, List<Spell> spells)
        {
            _consoleCtrlHandler += _ => { SerializeAll(hero, items, spells); Environment.Exit(0x1); return false; };
            _ = SetConsoleCtrlHandler(_consoleCtrlHandler, true);
            Print($"Ваши заклинания:\n");
            List<Spell> currentspells = spells;
            _ = currentspells.CheckSpells(hero);
            int count = 0;
            foreach (Spell spell in currentspells)
            {
                if (spell.unlocked)
                {
                    count++;
                    Console.WriteLine($"{count}) {spell.name}: наносит врагу {spell.dmg * hero.spellamp} урона");
                    Console.WriteLine($"Требует маны: {spell.mpcost}");
                    if (spell.hpcost != 0) Console.WriteLine($"Требует здоровья: {spell.hpcost}");
                }
                else if (spell.anyclass | spell.heroClass == hero.heroClass)
                {
                    count++;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"{count}) {spell.name} (Недоступно.)");
                    Console.ResetColor();
                }
            }

            bool parsed = int.TryParse(Console.ReadLine(), out int sel);
            if (parsed == true & sel >= 1 & sel <= count)
            {
                currentspells[sel - 1].Spellcast(enemy, hero);
            }
            else
            {
                Console.Clear();
                Print("Пиши не гадости, а циферку\n");
            }

        }
        private static void ClassSelection(Hero hero)
        {
            SaveDirCheck();
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
    }

}
