using Algorithms.Library.Menu;

namespace Algorithms.ConsoleApp
{
    internal class Program
    {
        private static void Main()
        {
            Menu menu = new Menu();

            MenuNode newGame = new MenuNode("New game");
            MenuNode loadGame = new MenuNode("Load");
            MenuNode saveGame = new MenuNode("Save");
            MenuNode options = new MenuNode("Options");
            MenuNode exit = new MenuNode("Exit");

            menu.Head.Connect(newGame);
            menu.Head.Connect(loadGame);
            menu.Head.Connect(saveGame);
            menu.Head.Connect(options);
            menu.Head.Connect(exit);

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
        }
    }
}