namespace workout_app.Models;

public class User
{
    public int Id {get; set; }
    public string Name {get; set; } = "";
    public int Age {get; set; } = 0;
    public string Username {get; set; } = "";
    public string PasswordHash {get; set; } = "";
}