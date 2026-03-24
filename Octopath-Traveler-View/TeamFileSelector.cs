namespace Octopath_Traveler_View;

public class TeamFileSelector
{
    private View _view;
    private string _teamsFolder;

    public TeamFileSelector(View view, string teamsFolder)
    {
        _view = view;
        _teamsFolder = teamsFolder;
    }

    public string SelectFile()
    {
        string[] files = Directory.GetFiles(_teamsFolder);

        ShowFiles(files);

        int index = ReadOption();
        return files[index];
    }

    private void ShowFiles(string[] files)
    {
        _view.WriteLine("Elige un archivo para cargar los equipos");

        for (int i = 0; i < files.Length; i++)
        {
            _view.WriteLine($"{i}: {Path.GetFileName(files[i])}");
        }
    }

    private int ReadOption()
    {
        return Convert.ToInt32(_view.ReadLine());
    }
}