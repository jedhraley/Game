using System;
using System.Collections.Generic;
using System.Media;
using static Game.Actions;
using static Game.Init;
using static Game.Misc;
using static Game.Serializations;
using static Program;



namespace Game
{
    class Maine
    {
        [STAThread]
        public static void Main()
        {
            SoundPlayer typewriter = new SoundPlayer
            {
                SoundLocation = "..\\..\\..\\Game\\Resources\\DDLC-Yuri-sdeath.wav"
            };
            typewriter.PlayLooping();
            bool initflag = true;

            Hero hero = new Hero();
            List<Item> items = DefaultItems();
            List<Spell> spells = DefaultSpells();
            ClassSelection(hero);
            SerializeAll(initflag, hero, items, spells);
            (hero, items, spells) = Deserialize();
            MainLoop(hero, items, spells);
        }
        public static void MainLoop(Hero hero, List<Item> items, List<Spell> spells)
        {
            while (hero.hp > 0)
            {
                _consoleCtrlHandler += s => { SerializeAll(hero, items, spells); return true; };
                _ = SetConsoleCtrlHandler(_consoleCtrlHandler, true);
                Print("1) Путешествие");
                Print("2) Магазик");
                Print("3) Данные о тебе (доделать)");
                Print("4) Удаление сохранения");
                Print("5) секретные документы о которых путин не хочет чтобы вы знали");
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
                            Profile(hero, spells); break;
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
        public static void Battle(Enemy enemy, Hero hero, List<Spell> spells)
        {
            while (hero.hp > 0 && enemy.hp > 0)
            {
                _consoleCtrlHandler += s => { SerializeAll(hero, spells); return true; };
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
                            Flee(); break;
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
                hero.exp += enemy.maxhp * 0.1;
                Console.WriteLine("Враг повержен! Вы получили 5 опыта.");
            }

        }
        public static void SpellSelect(Enemy enemy, Hero hero, List<Spell> spells)
        {
            _consoleCtrlHandler += s => { SerializeAll(hero, spells); return true; };
            _ = SetConsoleCtrlHandler(_consoleCtrlHandler, true);
            Print($"Ваши заклинания:\n");
            List<Spell> list = spells;
            int count = 0;
            foreach (Spell spell in list)
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
                else
                {
                    _ = list.Remove(spell); // questionable, should remove such at init perhaps
                }
            }

            bool parsed = int.TryParse(Console.ReadLine(), out int sel);
            if (parsed == true & sel >= 1 & sel <= count)
            {
                Spellcast(enemy, hero, list, list[sel - 1]);
            }
            else
            {
                Console.Clear();
                Print("Пиши не гадости, а циферку\n");
            }

        }
    }

}
