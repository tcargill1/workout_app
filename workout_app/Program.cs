// See https://aka.ms/new-console-template for more information
// workout app
using System.Runtime.CompilerServices;
using System.Security.Cryptography;

namespace Workout
{
    class User
    {
        static void Main(string[] args)
        {
            // Main function to get name and age of user
            string correct = "no";

            while (correct == "no") {
                string name = enterName();
                int age = enterAge();

                if (age < 18) {
                    break;
                }

                Console.WriteLine("Welcome " + name + " to this workout app. You are " + age + 
                " years old.");
                Console.WriteLine("Is this correct? (reply with yes or no)");
                correct = (Console.ReadLine() ?? string.Empty).ToLowerInvariant();

                if (correct != "yes" && correct != "no") {
                    correct = "no";
                }
            }
        }

        static string enterName()
        {
            // Function to get name of user
            Console.Write("Enter your name: ");
            string UserName = Console.ReadLine() ?? string.Empty;
            return UserName;
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