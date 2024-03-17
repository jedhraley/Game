using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Timers;
using static Game.Actions;
using static Game.Animations;
using static Game.Serializations;
using static Game.Maine;
using static Program;

namespace Game
{
    public class Misc
    {
        public static void Save()
        {

        }
        public static void Adventure(Hero hero, List<Spell> spells)
        {
            _consoleCtrlHandler += s => { SerializeAll(hero, spells); return true; };
            SetConsoleCtrlHandler(_consoleCtrlHandler, true);
            Console.Clear();
            Random rnd = new Random();
            int roll = rnd.Next(1, 11);
            if (roll > 4 & roll < 8)
            {
                hero.money++;
                Print("Оп, монетка");
                Print($"Теперь у вас всего {hero.money} монет.");
            }
            else if (roll > 8)
            {
                Print("Ого, сундук!");
                int moneyroll = rnd.Next(3, 10);
                hero.money += moneyroll;
                Print($"Вы получили {moneyroll} монет. Теперь у вас всего {hero.money} монет.");
            }
            else
            {
                Print("Вы встретили врага. \n1) Бежать! \n2) Сразиться до смерти.");
                bool parsed = int.TryParse(Console.ReadLine(), out int sel);
                if (parsed == true)
                {
                    switch (sel)
                    {
                        case 1:
                            Console.Clear();
                            Flee();
                            break;
                        case 2:
                            Console.Clear();
                            Enemy enemy = new Enemy
                            {
                                maxhp = 50 + (hero.exp * 0.5),
                                hp = 50 + (hero.exp * 0.5)
                            };
                            Battle(enemy, hero, spells);
                            break;

                    }
                }

            }
        }
        // добавить очевидные способы выхода из магаза
        public static void Shop(Hero hero, List<Item> items, List<Spell> spells)
        {
            _consoleCtrlHandler += s => { SerializeAll(hero, items, spells); return true; };
            SetConsoleCtrlHandler(_consoleCtrlHandler, true);
            Console.Clear();
            Console.WriteLine(" ____________________   \r\n│_____│_МАГАЗИН_│____│  \r\n  │         ♫     │     \r\n  │    ;\\_;\\   ♫  │     \r\n  │  ♫ (■▾❍ )     │     \r\n  │    /|☦=|\\    │     \r\n  │  ----------   │     \r\n  │   │      │    │     \r\n  │   │      │    │\\\\ ");
            Print($"Торговец: Приветствую тебя, {hero.name}, чего ты желаешь приобрести на этот раз?");
            int count = 0;
            foreach (Item item in items)
            {
                if (!item.bought)
                {
                    count++;
                    string str = string.Format($"{count}) " + item.shopdesc, item.cost, item.val1, item.val2, item.val3);
                    Console.WriteLine(str);
                }
            }
            bool parsed = int.TryParse(Console.ReadLine(), out int sel);
            if (parsed == true & sel >= 1 & sel <= count)
            {
                if (items[sel - 1].bought == false)
                {
                    if (hero.money >= items[sel - 1].cost)
                    {
                        hero.money -= items[sel - 1].cost;
                        items[sel - 1].ApplyItem(hero, spells);
                        items.Remove(items[sel - 1]);
                        Print("Торговец: Поздравляю с покупкой!\n");
                        System.Threading.Thread.Sleep(1000);
                        Shop(hero, items, spells);
                    }
                    else
                    {
                        Console.Clear();
                        Print($"Мысли: не могу себе позволить, не хватает {items[sel - 1].cost - hero.money} монет...");
                        Print("Что дальше?");
                        ShopReturn(hero, items, spells);
                    }
                }
                else
                {
                    Print("Мысли: Куда еще больше еды?? \n");
                    ShopReturn(hero, items, spells);
                }
            }
            else
            {
                Print("Пиши не гадости, а циферку");
                System.Threading.Thread.Sleep(1000);
                Console.Clear();
            }
        }

        private static void ShopReturn(Hero hero, List<Item> items, List<Spell> spells)
        {
            _consoleCtrlHandler += s => { SerializeAll(hero, items, spells); return true; };
            SetConsoleCtrlHandler(_consoleCtrlHandler, true);
            Print("Вернуться в магазин? Нажмите любую клавишу, чтобы не возвращаться.\n");
            Timer timer = new Timer(5000) { AutoReset = false, Enabled = true };
            timer.Elapsed += CaseReturn; // бросит нажатие F24 после 5 сек
            ConsoleKey str = Console.ReadKey(true).Key;
            if (str.Equals(ConsoleKey.F24)) // F24 для того чтобы не могли дотянуться ручками когда хотят выйти
            {
                timer.Stop();
                Shop(hero, items, spells);
            }
            else
            {
                timer.Stop();
                Console.Clear();
            }
        }
        [DllImport("User32.Dll", EntryPoint = "PostMessageA")]
        private static extern bool PostMessage(IntPtr hWnd, uint msg, int wParam, int lParam); // бросает кнопки в лицо приложению

        const int VK_F24 = 0x87;
        const int WM_KEYDOWN = 0x100;
        public static void CaseReturn(object src, ElapsedEventArgs e) // метод для броска нажатия F24 чтобы выйти из ReadKey когда таймер прокнет его
        {
            IntPtr hWnd = System.Diagnostics.Process.GetCurrentProcess().MainWindowHandle;
            PostMessage(hWnd, WM_KEYDOWN, VK_F24, 0);
            return;
        }
        public static void Profile(Hero hero, List<Spell> spells)
        {
            Console.Clear();
            SerializeAll(hero, spells);
            Console.WriteLine("Класс: {0} {1} уровня", hero.heroClass, hero.lvl);
            Console.WriteLine("Опыта: {0}", hero.exp);
            Console.WriteLine("Монет: {0}", hero.money);
            Console.WriteLine("Здоровье: {0}/{1}", hero.hp, hero.maxhp);
            Console.WriteLine("Мана: {0}/{1}", hero.mp, hero.maxmp);
            Console.WriteLine("Атака: {0}", hero.atk);
            Console.WriteLine("Множитель маг. урона: {0}\n", hero.spellamp);

            Console.WriteLine("Способности:\n");
            int count = 0;
            foreach (Spell spell in spells)
            {
                if (spell.unlocked)
                {
                    count++;
                    Console.WriteLine($"{count}) {spell.name}: наносит врагу {spell.dmg * hero.spellamp} урона");
                    Console.WriteLine($"Требует маны: {spell.mpcost}");
                    if (spell.hpcost != 0) Console.WriteLine($"Требует здоровья: {spell.hpcost}");
                    Console.WriteLine();
                }

            }
            Console.ReadKey(true);
            Console.Clear();
        }
        public static void Useables(Enemy enemy, Hero hero, List<Spell> spells)
        {

            if (hero.food)
            {
                Print("1) Кошачий корм - +20 хп");
            }
            else
            {
                Print("У вас нет расходников, зря зря...");
            }
            bool parsed = int.TryParse(Console.ReadLine(), out int sel);
            if (parsed == true)
            {
                switch (sel)
                {
                    case 1:
                        if (hero.food)
                        {
                            hero.hp += 20;
                            hero.hp -= enemy.atk;
                            Battle(enemy, hero, spells);
                        }
                        else
                        {
                            Print("Вы потратили ход, а враг наносит критический удар в спину.");
                            hero.hp -= enemy.atk * 10;
                            Battle(enemy, hero, spells);
                        }
                        break;
                    default:
                        Print("Вы потратили ход, а враг наносит критический удар в спину.");
                        hero.hp -= enemy.atk * 10;
                        Battle(enemy, hero, spells);
                        break;
                }
            }
            else
            {
                Console.Clear();
                Print("Пиши не гадости, а циферку");
            }
        }

        public static void Flee()
        {
            Random rand = new Random();
            int r = rand.Next(1, 11);
            if (r <= 3)
            {
                Print("Вы успешно убежали.");
            }
            else if (r >= 4 & r <= 6)
            {
                Print("");
            }
            else if (r >= 7)
            {

            }
        }
        public static void Spellcast(Enemy enemy, Hero hero, List<Spell> spells, Spell usedspell)
        {
            foreach (Spell spell in spells)
            {
                if (spell.unlocked & hero.mp >= spell.mpcost & hero.hp >= spell.hpcost & spell.id == usedspell.id)
                {
                    Console.Clear();
                    hero.mp -= spell.mpcost;
                    hero.hp -= spell.hpcost;
                    enemy.hp -= spell.dmg * hero.spellamp;
                    AnimSpell(enemy, usedspell);
                    return;
                }
            }
            Console.WriteLine("Заклинание недоступно.");
        }
    }
}
