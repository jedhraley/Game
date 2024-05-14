using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;
using System.Timers;
using static Game.Actions;
using static Game.Animations;
using static Game.Maine;
using static Game.Serializations;
using static Program;

namespace Game
{

    internal static class Misc
    {
        private static int keycount = 0;

        private static bool timerelapsed = false;
        internal static void Adventure(Hero hero, List<Spell> spells)
        {
            _consoleCtrlHandler += _ => { SerializeAll(hero, items, spells); Environment.Exit(0x1); return false; };
            _ = SetConsoleCtrlHandler(_consoleCtrlHandler, true);
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
                            Enemy enemy = new Enemy(Math.Ceiling(50 + (hero.exp * 0.5)));
                            Battle(enemy, hero, spells);
                            break;

                    }
                }

            }
        }
        // добавить очевидные способы выхода из магаза
        internal static void Shop(Hero hero, List<Item> items, List<Spell> spells)
        {
            _consoleCtrlHandler += _ => { SerializeAll(hero, items, spells); Environment.Exit(0x1); return false; };
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
                    string str = string.Format($"{count}) " + item.shopdesc, item.cost, item.val1, item.val2, item.val3, item.val4, item.val5);
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
                        _ = items.Remove(items[sel - 1]);
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
        private static void ApplyItem(this Item item, Hero hero, List<Spell> spells)
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
        private static void ShopReturn(Hero hero, List<Item> items, List<Spell> spells)
        {
            _consoleCtrlHandler += _ => { SerializeAll(hero, items, spells); Environment.Exit(0x1); return false; };
            SetConsoleCtrlHandler(_consoleCtrlHandler, true);
            Print("Вернуться в магазин? Нажмите любую клавишу, чтобы не возвращаться.\n");
            System.Timers.Timer timer = new System.Timers.Timer(5000) { AutoReset = false, Enabled = true };
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

        private const int VK_F24 = 0x87;
        private const int WM_KEYDOWN = 0x100;
        private static void CaseReturn(object src, ElapsedEventArgs e) // метод для броска нажатия F24 чтобы выйти из ReadKey когда таймер прокнет его
        {
            IntPtr hWnd = System.Diagnostics.Process.GetCurrentProcess().MainWindowHandle;
            _ = PostMessage(hWnd, WM_KEYDOWN, VK_F24, 0);
            return;
        }
        internal static void Profile(Hero hero, List<Spell> spells)
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
        internal static void Useables(Enemy enemy, Hero hero, List<Spell> spells)
        {

            if (hero.food)
            {
                Print("1) Кошачий корм - +20 хп");
            }
            else
            {
                Print("У вас нет расходников, зря зря...");
                Print("Вы потратили ход, а враг наносит критический удар в спину.");
                hero.hp -= enemy.atk * 10;
                Battle(enemy, hero, spells);
            }
            bool parsed = int.TryParse(Console.ReadLine(), out int sel);
            if (parsed == true)
            {
                switch (sel)
                {
                    case 1:
                        hero.hp += 20;
                        hero.hp -= enemy.atk;
                        Battle(enemy, hero, spells);
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
        
        internal static bool Flee()
        {
            Random rand = new Random();
            int r = rand.Next(1, 11);
            if (r <= 3)
            {
                Print("Вы успешно убежали.");
                return true;
            }
            else if (r > 3 & r <= 6)
            {
                Print("сделаю последствия тут");
                return true;
            }
            return false;
        }
        internal static void Spellcast(this Spell spell, Enemy enemy, Hero hero)
        {
            if (spell.unlocked & hero.mp >= spell.mpcost & hero.hp >= spell.hpcost & spell.id >= 801 & spell.id <= 803)
            {
                Console.Clear();
                hero.mp -= spell.mpcost;
                hero.hp -= spell.hpcost;
                enemy.hp -= spell.dmg * hero.spellamp;
                AnimSpell(enemy, spell);
            }
            else if (spell.unlocked & hero.mp >= spell.mpcost & hero.hp >= spell.hpcost & spell.id == 804)
            {
                Console.Clear();
                keycount = 0;
                timerelapsed = false;
                hero.mp -= spell.mpcost;
                hero.hp -= spell.hpcost;
                System.Timers.Timer timer = new System.Timers.Timer(6000) { AutoReset = false, Enabled = false };
                timer.Elapsed += EndKeyCount; // бросит нажатие F24 после 10 сек
                Thread.Sleep(300);
                Random rand = new Random();
                while (!timerelapsed)
                {
                    if (!timer.Enabled) timer.Enabled = true;
                    int randkey = rand.Next(65, 91);
                    ConsoleKey chosenkey = (ConsoleKey)randkey;
                    Console.WriteLine($"Выбрана клавиша {chosenkey}!");
                    ConsoleKey str = Console.ReadKey(true).Key;
                    if (str.Equals(ConsoleKey.F24) | str.Equals(ConsoleKey.Pause)) // F24 для того чтобы не могли дотянуться ручками
                    {
                        timer.Stop();
                        break;
                    }
                    else if (str.Equals(chosenkey))
                    {
                        keycount++;
                    }
                }
                double dmg = hero.atk * 1.5 * keycount;
                Console.WriteLine($"Вы натыкали {dmg} урона!");
                Thread.Sleep(1000);
                enemy.hp -= dmg;
                int frametime = 300;
                int animcount = 3;
                if (keycount != 0)
                {
                    frametime = (int)Math.Ceiling((double)(5000 / keycount / 6));
                    animcount += (keycount / 3);
                }
                for (int i = 1; i <= animcount; i++)
                {
                    if (i == 1)
                    {
                        StartWhirl(frametime);
                    }
                    else if (i > 1 & i < animcount)
                    {
                        Whirl(frametime);
                    }
                    else if (i == animcount)
                    {
                        EndWhirl(frametime);
                    }
                }
                if (enemy.hp <= 0)
                {
                    EnemyDeathVar1();
                }
            }
            else Console.WriteLine("Заклинание недоступно.");
        }
        private static void EndKeyCount(object o, ElapsedEventArgs e)
        {
            timerelapsed = true;
        }
    }
}
