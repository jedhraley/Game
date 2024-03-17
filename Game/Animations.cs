using System;
using System.Threading;
using static Game.Actions;
namespace Game
{
    public class Animations
    {
        public static void AnimSpell(Enemy enemy, Spell spell)
        {
            if (spell.id == 801)
            {
                Console.WriteLine("\n" +
                "   /;_/;                                        ;\\_;\\   \n" +
                "  ( D wD) *                                     (0~ 0 )  \n" +
                "   │=+=│つ@                                      /│=+=│    \n" +
                " ║ │   │   *                                   * │   │ ║  \n" +
                " ╙═│___│                                         │___│═╜  \n" +
                "    ╙ ╙                                           │ │     \n" +
                "");
                Thread.Sleep(200);
                Console.Clear();
                Console.WriteLine("\n" +
                "   /;_/;                                        ;\\_;\\   \n" +
                "  ( D wD)     *                                 (0~ 0 )  \n" +
                "   │=+=│つ      @                                /│=+=│    \n" +
                " ║ │   │    *                                  * │   │ ║  \n" +
                " ╙═│___│                                         │___│═╜  \n" +
                "    ╙ ╙                                           │ │     \n" +
                "");
                Thread.Sleep(200);
                Console.Clear();
                Console.WriteLine("\n" +
                "   /;_/;                                        ;\\_;\\   \n" +
                "  ( D wD)           *                           (0~ 0 )  \n" +
                "   │=+=│つ            @*                         /│=+=│    \n" +
                " ║ │   │           *                           * │   │ ║  \n" +
                " ╙═│___│     *                                   │___│═╜  \n" +
                "    ╙ ╙                                           │ │     \n" +
                "");
                Thread.Sleep(200);
                Console.Clear();
                Console.WriteLine("\n" +
                "   /;_/;                                        ;\\_;\\   \n" +
                "  ( D wD)                       *               (0~ 0 )  \n" +
                "   │=+=│つ                   *   @*              /│=+=│    \n" +
                " ║ │   │                                       * │   │ ║  \n" +
                " ╙═│___│                                         │___│═╜  \n" +
                "    ╙ ╙           *                               │ │     \n" +
                "");
                Thread.Sleep(200);
                Console.Clear();
                Console.WriteLine("\n" +
                "   /;_/;                                        ;\\_;\\   \n" +
                "  ( D wD)                              *        (0~ 0 )  \n" +
                "   │=+=│つ                          *   @*       /│=+=│    \n" +
                " ║ │   │                                       * │   │ ║  \n" +
                " ╙═│___│                            *            │___│═╜  \n" +
                "    ╙ ╙           *                               │ │     \n" +
                "");
                Thread.Sleep(200);
                Console.Clear();
                Console.WriteLine("\n" +
                "   /;_/;                                        ;\\_;\\   \n" +
                "  ( D wD)                                    *  (0~ 0 )  \n" +
                "   │=+=│つ                               *  @*   /│=+=│    \n" +
                " ║ │   │                                       * │   │ ║  \n" +
                " ╙═│___│                                         │___│═╜  \n" +
                "    ╙ ╙                               *           │ │     \n" +
                "");
                Thread.Sleep(200);
                Console.Clear();
                Console.WriteLine("\n" +
                "   /;_/;                                       *      ;\\_;\\ *    \n" +
                "  ( D wD)                                     *     \\(>w < )   \n" +
                "   │=+=│つ                                        *   /=+=/      \n" +
                " ║ │   │                                            /   /║  \n" +
                " ╙═│___│                                           /___/═╜  \n" +
                "    ╙ ╙                               *             │ │     \n" +
                "");
                Thread.Sleep(200);
                if (enemy.hp > 0)
                {
                    Print("Вы попали по противнику.");
                }
                else
                {
                    EnemyDeathVar1();
                }
            }
            if (spell.id == 802)
            {
                Console.WriteLine("\n" +
                "   /;_/;                                        ;\\_;\\   \n" +
                "  ( D wD) *                                     (0~ 0 )  \n" +
                "   │=+=│つ(0)                                    /│=+=│    \n" +
                " ║ │   │   *                                   * │   │ ║  \n" +
                " ╙═│___│                                         │___│═╜  \n" +
                "    ╙ ╙                                           │ │     \n" +
                "");
                Thread.Sleep(200);
                Console.Clear();
                Console.WriteLine("\n" +
                "   /;_/;                                        ;\\_;\\   \n" +
                "  ( D wD)       *                               (0~ 0 )  \n" +
                "   │=+=│つ      (0)*                             /│=+=│    \n" +
                " ║ │   │       *                               * │   │ ║  \n" +
                " ╙═│___│    *                                    │___│═╜  \n" +
                "    ╙ ╙                                           │ │     \n" +
                "");
                Thread.Sleep(200);
                Console.Clear();
                Console.WriteLine("\n" +
                "   /;_/;                                        ;\\_;\\   \n" +
                "  ( D wD)           *                           (0~ 0 )  \n" +
                "   │=+=│つ            (0)*                       /│=+=│    \n" +
                " ║ │   │           *                           * │   │ ║  \n" +
                " ╙═│___│     *                                   │___│═╜  \n" +
                "    ╙ ╙                                           │ │     \n" +
                "");
                Thread.Sleep(200);
                Console.Clear();
                Console.WriteLine("\n" +
                "   /;_/;                                        ;\\_;\\   \n" +
                "  ( D wD)                       *               (0~ 0 )  \n" +
                "   │=+=│つ                   *   (0)*            /│=+=│    \n" +
                " ║ │   │                                       * │   │ ║  \n" +
                " ╙═│___│                                         │___│═╜  \n" +
                "    ╙ ╙           *                               │ │     \n" +
                "");
                Thread.Sleep(200);
                Console.Clear();
                Console.WriteLine("\n" +
                "   /;_/;                                        ;\\_;\\   \n" +
                "  ( D wD)                              *        (0~ 0 )  \n" +
                "   │=+=│つ                          *   (0)*     /│=+=│    \n" +
                " ║ │   │                                       * │   │ ║  \n" +
                " ╙═│___│                            *            │___│═╜  \n" +
                "    ╙ ╙           *                               │ │     \n" +
                "");
                Thread.Sleep(200);
                Console.Clear();
                Console.WriteLine("\n" +
                "   /;_/;                                        ;\\_;\\   \n" +
                "  ( D wD)                                    *  (0~ 0 )  \n" +
                "   │=+=│つ                               *  (0)* /│=+=│    \n" +
                " ║ │   │                                       * │   │ ║  \n" +
                " ╙═│___│                                         │___│═╜  \n" +
                "    ╙ ╙                               *           │ │     \n" +
                "");
                Thread.Sleep(200);
                Console.Clear();
                Console.WriteLine("\n" +
                "   /;_/;                                       *      ;\\_;\\ *    \n" +
                "  ( D wD)                                     *     \\(>w < )   \n" +
                "   │=+=│つ                                        *   /=+=/      \n" +
                " ║ │   │                                            /   /║  \n" +
                " ╙═│___│                                           /___/═╜  \n" +
                "    ╙ ╙                               *             │ │     \n" +
                "");
                Thread.Sleep(200);
                if (enemy.hp > 0)
                {
                    Print("Вы попали по противнику.");
                }
                else
                {
                    EnemyDeathVar1();
                }
            }
            if (spell.id == 803)
            {
                Console.WriteLine("\n" +
                "   /;_/;                                        ;\\_;\\   \n" +
                "  ( D wD)                                       (0~ 0 )  \n" +
                "   │=+=│つ*                                      /│=+=│    \n" +
                " ║ │   │                                       * │   │ ║  \n" +
                " ╙═│___│                                         │___│═╜  \n" +
                "    ╙ ╙                                           │ │     \n" +
                "");
                Thread.Sleep(300);
                Console.Clear();
                Console.WriteLine("\n" +
                "   /;_/;                                        ;\\_;\\   \n" +
                "  ( - w7)  *                                    (0~ 0 )  \n" +
                "   │=+=│つ   *                                   /│=+=│    \n" +
                " ║ │   │   *                                   * │   │ ║  \n" +
                " ╙═│___│                                         │___│═╜  \n" +
                "    ╙ ╙                                           │ │     \n" +
                "");
                Thread.Sleep(120);
                Console.Clear();
                Console.WriteLine("\n" +
                "   /;_/;                                        ;\\_;\\   \n" +
                "  ( - w7)     *                                 (0~ 0 )  \n" +
                "   │=+=│つ *                                     /│=+=│    \n" +
                " ║ │   │     *                                 * │   │ ║  \n" +
                " ╙═│___│                                         │___│═╜  \n" +
                "    ╙ ╙                                           │ │     \n" +
                "");
                Thread.Sleep(120);
                Console.Clear();
                Console.WriteLine("\n" +
                "   /;_/;                                        ;\\_;\\   \n" +
                "  ( - w7)  *                                    (0~ 0 )  \n" +
                "   │=+=│つ   *                                   /│=+=│    \n" +
                " ║ │   │   *                                   * │   │ ║  \n" +
                " ╙═│___│                                         │___│═╜  \n" +
                "    ╙ ╙                                           │ │     \n" +
                "");
                Thread.Sleep(120);
                Console.Clear();
                Console.WriteLine("\n" +
                "   /;_/;                                        ;\\_;\\   \n" +
                "  ( - w7)    *                                  (0~ 0 )  \n" +
                "   │=+=│つ==>                                    /│=+=│    \n" +
                " ║ │   │   *                                   * │   │ ║  \n" +
                " ╙═│___│                                         │___│═╜  \n" +
                "    ╙ ╙                                           │ │     \n" +
                "");
                Thread.Sleep(120);
                Console.Clear();
                Console.WriteLine("\n" +
                "   /;_/;                                        ;\\_;\\   \n" +
                "  ( - w7)         *                             (0~ 0 )  \n" +
                "   │=+=│つ==========>                            /│=+=│    \n" +
                " ║ │   │  *                                    * │   │ ║  \n" +
                " ╙═│___│                                         │___│═╜  \n" +
                "    ╙ ╙                                           │ │     \n" +
                "");
                Thread.Sleep(100);
                Console.Clear();
                Console.WriteLine("\n" +
                "   /;_/;                                        ;\\_;\\   \n" +
                "  ( 0 w0)*          *                           (G~ G )  \n" +
                "   │=+=│つ======================>                /│=+=│    \n" +
                " ║ │   │     *                                 * │   │ ║  \n" +
                " ╙═│___│                                         │___│═╜  \n" +
                "    ╙ ╙                                           │ │     \n" +
                "");
                Thread.Sleep(200);
                Console.Clear();
                Console.WriteLine("\n" +
                "   /;_/;                                        ;\\_;\\   \n" +
                "  ( D wD) *      *                    *         (<~ < )  \n" +
                "   │=+=│つ=====================================> /│=+=│    \n" +
                " ║ │   │     *                                 * │   │ ║  \n" +
                " ╙═│___│                *                        │___│═╜  \n" +
                "    ╙ ╙                                           │ │     \n" +
                "");
                Thread.Sleep(200);
                Console.Clear();
                Console.WriteLine("\n" +
                "   /;_/;                                              ;\\_;\\ *       * \n" +
                "  ( D wD) *                     *              *    \\(>w < )*   *     \n" +
                "   │=+=│つ============================================/=+=/======>   \n" +
                " ║ │   │     *                                    * /   /║ *    *     \n" +
                " ╙═│___│                  *                        /___/═╜         \n" +
                "    ╙ ╙                                             │ │      *      \n" +
                "");
                Thread.Sleep(200);
                if (enemy.hp > 0)
                {
                    Print("Вы попали по противнику.");
                }
                else
                {
                    EnemyDeathVar1();
                }
            }
        }
        public static void EnemyDeathVar1()
        {
            Console.WriteLine("\n" +
            "   /;_/;                                                  \n" +
            "  ( D wD)                                       ;\\_;\\   \n" +
            "   │=+=│つ                                      (X~ X )   \n" +
            " ║ │   │                                        /│=+=│ ║  \n" +
            " ╙═│___│                                         │___│═╜  \n" +
            "    ╙ ╙                                           │ │     \n" +
            "");
            Thread.Sleep(100);
            Console.Clear();
            Console.WriteLine("\n" +
            "   /;_/;                                                    \n" +
            "  ( D wD)                                                   \n" +
            "   │=+=│つ                                      ;\\_;\\     \n" +
            " ║ │   │                                        (X~ X )     \n" +
            " ╙═│___│                                        /│=+=│ ║    \n" +
            "    ╙ ╙                                          │___│═╜    \n" +
            "");
            Thread.Sleep(100);
            Console.Clear();
            Console.WriteLine("\n" +
            "   /;_/;                                                           \n" +
            "  ( D wD)                                                           \n" +
            "   │=+=│つ                                                            \n" +
            " ║ │   │                                          ;\\_/;              \n" +
            " ╙═│___│                                         (X , X)             \n" +
            "    ╙ ╙                                         =/______\\=           \n" +
            "");
            Thread.Sleep(100);
            Console.Clear();
            Console.WriteLine("\n" +
            "   /;_/;                                                           \n" +
            "  ( D wD)                                                           \n" +
            "   │=+=│つ                                                          \n" +
            " ║ │   │                                          ________          \n" +
            " ╙═│___│                                         /  RIP \\\\         \n" +
            "    ╙ ╙                                          |______|_|         \n" +
            "");
            Thread.Sleep(100);
            Console.Clear();
            Console.WriteLine("\n" +
            "   /;_/;                                                           \n" +
            "  ( D wD)                                                           \n" +
            "   │=+=│つ                                        ________           \n" +
            " ║ │   │                                         /  RIP \\\\        \n" +
            " ╙═│___│                                         | X  X | |         \n" +
            "    ╙ ╙                                          |______|_|         \n" +
            "");
            Thread.Sleep(100);
            Console.Clear();
            Console.WriteLine("\n" +
            "   /;_/;                                         Game OVER          \n" +
            "  ( D wD)                                         ________          \n" +
            "   │=+=│つ                                       /  RIP \\\\        \n" +
            " ║ │   │                                         | X  X | |        \n" +
            " ╙═│___│                                         |      | |         \n" +
            "    ╙ ╙                                          |______|_|         \n" +
            "");
            Thread.Sleep(200);
            Console.Clear();
            Print("Вы убили противника.");
        }
    }
}
