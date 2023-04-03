using Godot;

public partial class main : Node
{
    [Export] private string _gameScene = "res://scenes/ui/splash/splash_screen.tscn";
    [Export] private string _editorScene = "res://scenes/ui/splash/splash_screen.tscn";
    
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        // Code to execute when in editor.
        if (Engine.IsEditorHint())
        {
            // Loads the chosen scene as the first scene
            GetTree().ChangeSceneToFile(_editorScene);
        }
        // Code to execute when in game.
        if (!Engine.IsEditorHint())
        {
            // Loads the chosen scene as the first scene
            GetTree().ChangeSceneToFile(_gameScene);
        }
    }
}