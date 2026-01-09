// See https://aka.ms/new-console-template for more information
// workout app
using System.Text;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;
using workout_app.Data;
using workout_app.Models;
using System.Runtime.InteropServices.Marshalling;
using workout_app.Menu;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;

namespace workout_app.Workout
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
            var correct = false;
            User? currentUser = null;
            while (true)
            {
                Console.WriteLine("Would you like to login or sign up?");
                string answer = (Console.ReadLine() ?? string.Empty).ToLowerInvariant();

                if (answer == "login")
                {
                    currentUser = Login(db);
                    if (currentUser == null) 
                    {
                        continue;
                    }
                    break;
                }
                else if (answer == "sign up" || answer == "signup")
                {
                    currentUser = welcome(correct, db);
                    break;
                }
                else
                {
                    Console.WriteLine("Please type login or sign up. Try Again.");
                }
            }

            // menu starts when logged in or signed up
            if (currentUser != null)
            {
                foreach (var u in db.Users)
                {
                    Console.WriteLine($"{u.Id}: {u.Name} ({u.Age})");
                }
                // TODO: add menu component here from menu.cs
                mainInterface.Interface(currentUser.Name);
            }
        }

        static string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(password);
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }

        static User? Login(AppDbContext db)
        {
            string username = EnterExistingUsername(db);
            if (username == "")
            {
                return null;
            }
            string passwordHash = EnterExistingPassword();

            var user = db.Users.FirstOrDefault(u =>
                u.Username == username &&
                u.PasswordHash == passwordHash
            );

            if (user == null)
            {
                return null;
            }

            return user;
        }

        static User? welcome(bool correct, AppDbContext db)
        {
            string name = "", username = "", passwordHash = "";
            int age = 0;
            while (!correct) {
                name = EnterName();
                age = EnterAge();
                username = EnterNewUsername(db);
                passwordHash = EnterNewPassword();

                Console.WriteLine("Welcome " + username + " to this workout app." 
                + " Your name is " + name + ". You are " + age + " years old.");
                Console.WriteLine("Is this correct? (reply with yes or no)");
                string answer = (Console.ReadLine() ?? string.Empty).ToLowerInvariant();

                if (answer == "yes")
                {
                    correct = true;
                }
                if (answer != "yes" && answer != "no")
                {
                    Console.WriteLine("You didn't write a yes or no answer, please try again.");
                }
            }

            var user = new User 
            {
                Name = name,
                Age = age,
                Username = username,
                PasswordHash = passwordHash
            };

            db.Users.Add(user);
            db.SaveChanges();
            return user;
        }

        static string EnterNewUsername(AppDbContext db)
        {
            while (true)
            {
                Console.Write("Enter your username: ");
                string username = Console.ReadLine() ?? string.Empty;
                if (string.IsNullOrWhiteSpace(username))
                {
                    Console.WriteLine("That is not a valid username. Please try again.");
                    continue;
                }
                
                if (db.Users.Any(u => u.Username == username))
                {
                    Console.WriteLine("That username is already taken. Please try again.");
                    continue;
                }

                return username;
            }
        }

        static string EnterExistingUsername(AppDbContext db)
        {
            while (true)
            {
                Console.Write("Enter your username: ");
                string username = Console.ReadLine() ?? string.Empty;
                if (string.IsNullOrWhiteSpace(username))
                {
                    Console.WriteLine("That is not a valid username. Please try again.");

                    Console.WriteLine("Would you like to go back to the sign up page? (Type yes if so)");
                    string ans = Console.ReadLine() ?? string.Empty;
                    if (ans == "yes")
                    {
                        return "";
                    }

                    continue;
                }
                
                if (!db.Users.Any(u => u.Username == username))
                {
                    Console.WriteLine("No Account with that username. Please try again.");

                    Console.WriteLine("Would you like to go back to the sign up page? (Type yes if so)");
                    string ans = (Console.ReadLine() ?? string.Empty).Trim().ToLowerInvariant();;
                    if (ans == "yes")
                    {
                        return "";
                    }

                    continue;
                }

                

                return username;
            }
        }

        static string EnterNewPassword()
        {   
            while (true)
            {
                Console.Write("Enter your password: ");
                string password = Console.ReadLine() ?? string.Empty;
                
                Console.Write("Write your password again: ");
                string secondPassword = Console.ReadLine() ?? string.Empty;

                if (string.IsNullOrWhiteSpace(password))
                {
                    Console.WriteLine("That is not a valid password. Please try again.");
                    continue;
                }
                if (password != secondPassword)
                {
                    Console.WriteLine("The password don't match. Please try again.");
                    continue;
                }

                return HashPassword(password);
            }
        }

        static string EnterExistingPassword()
        {
            while (true)
            {
                Console.Write("Enter your password: ");
                string password = Console.ReadLine() ?? string.Empty;


                if (string.IsNullOrWhiteSpace(password))
                {
                    Console.WriteLine("That is not a valid password. Please try again.");
                    continue;
                }
                return HashPassword(password);
            }
        }

        static string EnterName()
        {
            // Function to get name of user
            while (true) {
                Console.Write("Enter your name: ");
                string name = Console.ReadLine() ?? string.Empty;
                if (string.IsNullOrWhiteSpace(name))
                {
                    Console.WriteLine("That is not a valid name. Please try again.");
                    continue;
                }
                // reject names that are purely numeric
                if (int.TryParse(name, out _))
                {
                    Console.WriteLine("That is not a valid name. Please try again.");
                    continue;
                }
                return name;
            }
        }

        static int EnterAge()
        {
            while (true)
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
                continue;
            }
            
            if (user_age < 18)
            {
                Console.Write("Sorry but you need to be 18 years or older to use this app.");
                continue;
            }

            return user_age;
            }
        }
    }
}