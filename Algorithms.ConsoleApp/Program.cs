using System;
using Algorithms.Library;
using Algorithms.Library.Menu;

namespace Algorithms.ConsoleApp
{
    internal class Program
    {
        private static void Main()
        {
            Menu menu = new Menu();

            MenuNode head = new MenuNode("Head");

            MenuNode newGame = new MenuNode("New game");
            MenuNode loadGame = new MenuNode("Load");
            MenuNode saveGame = new MenuNode("Save");
            MenuNode options = new MenuNode("Options");
            MenuNode exit = new MenuNode("Exit");

            menu.Connect(head, newGame);
            menu.Connect(head, loadGame);
            menu.Connect(head, saveGame);
            menu.Connect(head, options);
            menu.Connect(head, exit);

            MenuNode newGameEasy = new MenuNode("Easy");
            MenuNode newGameNormal = new MenuNode("Normal");
            MenuNode newGameHard = new MenuNode("Hard");
            MenuNode newGameBack = new MenuNode("Back");

            menu.Connect(newGame, newGameEasy);
            menu.Connect(newGame, newGameNormal);
            menu.Connect(newGame, newGameHard);
            menu.Connect(newGame, newGameBack);

            MenuNode optionsVideo = new MenuNode("Video");
            MenuNode optionsAudio = new MenuNode("Audio");
            MenuNode optionsKeyboard = new MenuNode("Keyboard");
            MenuNode optionsAbout = new MenuNode("About");
            MenuNode optionsBack = new MenuNode("Back");

            menu.Connect(options, optionsVideo);
            menu.Connect(options, optionsAudio);
            menu.Connect(options, optionsKeyboard);
            menu.Connect(options, optionsAbout);
            menu.Connect(options, optionsBack);

            menu.AddNode(head);

            foreach (var node in menu.Nodes)
            {
                Console.WriteLine(node.Text);
            }
        }
    }
}