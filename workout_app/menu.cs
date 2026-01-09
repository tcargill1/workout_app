// Will have 3 buttons (log workout, review workouts, exit menu)
// log workout button brings you to a new interface with 2 more buttons (create new workout,
// edit workout), create new workout will ask for name of session and automatically record date
// the exit button closes the app. also when you click on the other buttons there will also be 
// a back button to use
// after creating new name, will bring you to interface in new_workout.cs
// when editing name, will bring you to workouts.cs

using System.Runtime.InteropServices;
using System.Collections.Generic;

namespace workout_app.Menu
{
    public class mainInterface
    {
        static public void Interface(string name)
        {
            bool running = true;

            while (running)
            {
                Console.WriteLine("Welcome to Workout App" + name + "!");
                Console.WriteLine("[w] Log Workout | [R] Review Workout | [Q] Quit");

                var key = Console.ReadKey(true).Key;

                switch (key)
                {
                    case ConsoleKey.Q:
                        running = false;
                        break;
                    
                    case ConsoleKey.W:
                        NewWorkout();
                        break;
                    
                    case ConsoleKey.R:
                        Review();
                        break;
                    
                    default:
                        Console.WriteLine("Please press W, R, or Q to continue.");
                        break;
                }
            }
        }

        static private void NewWorkout()
        {
            // Create a workout object for this and a collection of workouts object as well
            Console.WriteLine("What would you like to name today's workout?");
            string day = Console.ReadLine() ?? "";
        }

        static private void Review()
        {
            Console.WriteLine("Reviewing");
        }
    }
}