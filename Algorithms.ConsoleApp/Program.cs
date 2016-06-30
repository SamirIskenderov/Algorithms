using System;
using Algorithms.Library.Menu;

namespace Algorithms.ConsoleApp
{

	internal class Program
	{
		private static void Main()
		{
			Menu menu = new Menu();

            MenuNode head = new MenuNode("HEAD");

            MenuNode newGame = new MenuNode("New game");
            MenuNode loadGame = new MenuNode("Load");
            MenuNode saveGame = new MenuNode("Save");
            MenuNode options = new MenuNode("Options");
            MenuNode exit = new MenuNode("Exit");

            head.Connect(newGame);
            head.Connect(loadGame);
            head.Connect(saveGame);
            head.Connect(options);
            head.Connect(exit);

            MenuNode newGameEasy = new MenuNode("Easy");
            MenuNode newGameNormal = new MenuNode("Normal");
            MenuNode newGameHard = new MenuNode("Hard");
            MenuNode newGameBack = new MenuNode("Back");

            newGame.Connect(newGameEasy);
            newGame.Connect(newGameNormal);
            newGame.Connect(newGameHard);
            newGame.Connect(newGameBack);

            MenuNode optionsVideo = new MenuNode("Video");
            MenuNode optionsAudio = new MenuNode("Audio");
            MenuNode optionsKeyboard = new MenuNode("Keyboard");
            MenuNode optionsAbout = new MenuNode("About");
            MenuNode optionsBack = new MenuNode("Back");

            options.Connect(optionsVideo);
            options.Connect(optionsAudio);
            options.Connect(optionsKeyboard);
            options.Connect(optionsAbout);
            options.Connect(optionsBack);

            menu.AddNode(head);
        }
    }
}