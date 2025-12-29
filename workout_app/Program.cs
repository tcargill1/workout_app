// See https://aka.ms/new-console-template for more information
// workout app
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;
using workout_app.Data;
using workout_app.Models;


namespace Workout
{
    class Program
    {
        static void Main(string[] args)
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlite("Data source=workout.db")
                .Options;
            
            using var db = new AppDbContext(options);
            db.Database.Migrate();

            // Main function to get name and age of user
            string correct = "no";
            // Skip this if it already exists in database 
            if (welcome(correct, db))
            {
                foreach (var u in db.Users)
                {
                    Console.WriteLine($"{u.Id}: {u.Name} ({u.Age})");
                }
                // add menu component here from menu.cs
            }
        }

        static bool welcome(string correct, AppDbContext db)
        {
            string name = "";
            int age = 0;
            while (correct == "no") {
                name = enterName();
                age = enterAge();

                if (age < 18) {
                    return false;
                }

                Console.WriteLine("Welcome " + name + " to this workout app. You are " + age + 
                " years old.");
                Console.WriteLine("Is this correct? (reply with yes or no)");
                correct = (Console.ReadLine() ?? string.Empty).ToLowerInvariant();

                if (correct != "yes" && correct != "no") {
                    correct = "no";
                }
            }

            db.Users.Add(new workout_app.Models.User { Name=name, Age=age});
            db.SaveChanges();
            return true;
        }

        static string enterName()
        {
            // Function to get name of user
            while (true) {
                Console.Write("Enter your name: ");
                string UserName = Console.ReadLine() ?? string.Empty;
                if (string.IsNullOrWhiteSpace(UserName))
                {
                    Console.WriteLine("That is not a valid name. Please try again.");
                    continue;
                }
                // reject names that are purely numeric
                if (int.TryParse(UserName, out _))
                {
                    Console.WriteLine("That is not a valid name. Please try again.");
                    continue;
                }
                return UserName;
            }
        }

        static int enterAge()
        {
            // Function to get age of user and make sure they're 18+
            int user_age = 0;
            try
            {
                Console.Write("Enter age: ");
                string ageInput = Console.ReadLine() ?? string.Empty;
                user_age = Convert.ToInt32(ageInput);
            }
            catch(FormatException e) 
            {
                Console.WriteLine(e.Message);
            }
            

            if (user_age < 18)
            {
                Console.Write("Sorry but you need to be 18 years or older to use this app.");
            }

            return user_age;
        }
    }
}