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

    public string GetTeamsFilePath()
    {
        ShowPossibleTeamsFiles();
        string teamChosenInput = _view.ReadLine();
        return GetTeamsFilePath(teamChosenInput);
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

    private string GetTeamsFilePath(string teamChosenInput)
    {
        string[] files = Directory.GetFiles(_teamsFolder);
        string chosenFilePath = "";
        
        int index = 0;
        foreach (string file in files)
        {
            if (FileIsTheChosenOne(index, teamChosenInput))
            {
                chosenFilePath = file;
            }
            index++;
        }

        return chosenFilePath;
    }
    
    private bool FileIsTheChosenOne(int fileIndex, string teamChosenInput) =>
        fileIndex.ToString() == teamChosenInput;

    public void ShowInvalidTeamMessage()
    {
        _view.WriteLine("Archivo de equipos no válido");
    }
}