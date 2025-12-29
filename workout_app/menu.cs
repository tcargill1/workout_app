// Will have 3 buttons (log workout, review workouts, exit menu)
// log workout button brings you to a new interface with 2 more buttons (create new workout,
// edit workout), create new workout will ask for name of session and automatically record date
// the exit button closes the app. also when you click on the other buttons there will also be 
// a back button to use
// after creating new name, will bring you to interface in new_workout.cs
// when editing name, will bring you to workouts.cs

using System.Runtime.InteropServices;

namespace Menu
{
    public class mainInterface
    {
        static public void Interface(string name)
        {
            bool running = true;

            while (running)
            {
                Console.WriteLine("Welcome to Workout App" + name + "!");
                Console.WriteLine("To exit the app, press Q.");

                ConsoleKeyInfo key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Q)
                {
                    running = false;
                }
            }


        }

        static private void newWorkout()
        {
            
        }

        static private void Review()
        {
            
        }
    }
}