// See https://aka.ms/new-console-template for more information
// workout app
namespace Workout
{
    class User
    {
        static void Main(string[] args)
        {
            Console.Write("Enter your name: ");
            string UserName = Console.ReadLine() ?? string.Empty;
            Console.Write("Enter your age: ");
            string ageInput = Console.ReadLine() ?? string.Empty;
            if (!int.TryParse(ageInput, out int userAge))
            {
                userAge = 0; // fallback value if parsing fails
            }
            Console.WriteLine("Hello World, I'm " + UserName + " and I'm " + userAge + "!");
        }
    }
}