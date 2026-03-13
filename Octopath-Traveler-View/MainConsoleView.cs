using Octopath_Traveler_Model;

namespace Octopath_Traveler_View;

public class MainConsoleView
{
    private GameState _gameState;
    private View _view;
    private string _teamsFolder;
    public MainConsoleView(View view, GameState gameState, string teamsFolder)
    {
        _view = view;
        _gameState = gameState;
        _teamsFolder = teamsFolder;
    }

    public TeamsInfo GetTeamsInfo()
    {
        ShowPossibleTeamsFiles();
        _view.ReadLine();
        
        return new TeamsInfo();
    }

    private void ShowPossibleTeamsFiles()
    {
        _view.WriteLine("Elige un archivo para cargar los equipos");
        string[] files = Directory.GetFiles(_teamsFolder);

        int index = 0;
        foreach (string file in files)
        {
            string fileName = Path.GetFileName(file);
            _view.WriteLine($"{index}: {fileName}");
            index++;
        }
    }

    public void ShowInvalidTeamMessage()
    {
        _view.WriteLine("Archivo de equipos no válido");
    }
}