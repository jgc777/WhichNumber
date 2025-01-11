using System.Diagnostics;

namespace WhichNumber {
    class Program {
        static void Main(string[] args) {
            try {
                Console.Clear();
                Console.Title = "Which Number?";
                Banner();
                switch (args.Length) {
                    case 0: // No arguments, open menu
                        Menu();
                        break;
                    case 1: // One argument
                        switch (args[0].ToLower()) {
                            case "a":
                                // Run guess the number with no number
                                GuessTheNumber(-1);
                                break;
                            case "b":
                                // Run binary search with default starting point
                                BinarySearch(100);
                                break;
                            case "c":
                                Help();
                                break;
                            case "/s":
                                // Runned as screensaver, open menu
                                Menu();
                                break;
                            case "/?":
                                Help();
                                break;
                            case "-?":
                                Help();
                                break;
                            case "--help":
                                Help();
                                break;
                            case "-h":
                                Help();
                                break;
                            case "/h":
                                Help();
                                break;
                            case "/help":
                                Help();
                                break;
                            default:
                                Console.WriteLine("Invalid first argument!");
                                Help();
                                break;
                        }
                        break;
                    case 2: // Two arguments
                        switch (args[0].ToString().ToLower()[0]) { // First argument, lower case, first character
                            case 'a':
                                // Run guess the number with the number provided in the second argument
                                GuessTheNumber(Int32.Parse(args[1]));
                                break;
                            case 'b':
                                // Run binary search starting with the number provided in the second argument
                                BinarySearch(Int32.Parse(args[1]));
                                break;
                            default: // like for example "-? 100"
                                Console.WriteLine("Invalid first argument for 2 arguments execution!");
                                Help();
                                break;
                        }
                        break;
                    default:
                        Console.WriteLine("Too many arguments!");
                        Help();
                        break;
                }
            } catch (Exception ex) {
                Console.Write($"\nError: {ex.Message}\n");
                // Automatically close
            }
        }
        static void Help() {
            Console.WriteLine("Opening project website...");
            System.Diagnostics.Process.Start("http://jgc.linkpc.net/WhichNumber/");
        }
        static void Banner() {
            Console.WriteLine(@"
                                                    ██╗ ██████╗  ██████╗███████╗
                                                    ██║██╔════╝ ██╔════╝╚════██║
                                                    ██║██║  ███╗██║         ██╔╝
                                               ██   ██║██║   ██║██║        ██╔╝ 
                                               ╚█████╔╝╚██████╔╝╚██████╗   ██║  
                                                ╚════╝  ╚═════╝  ╚═════╝   ╚═╝  
");
            System.Threading.Thread.Sleep(2000);
            Console.Clear();
        }
        static void Menu() {
            Console.WriteLine(@"Do you want to:
a) Guess the computer number
b) Make your computer guess your number
c) Get more info
d) Exit");
            switch (Console.ReadKey().KeyChar.ToString().ToLower()[0]) {
                // Read key, then convert to string, then convert to lower case, then get first character
                case 'a':
                    GuessTheNumber(-1);
                    break;
                case 'b':
                    BinarySearch(100);
                    break;
                case 'c':
                    Help();
                    break;
                case 'd':
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid option!");
                    break;
            }
        }
        static int BinarySearch(int start) {
            bool finished = false;
            int attempt = 0;
            int max = -1;
            int min = 0;
            int reference = 100;
            do {
                attempt++;
                Console.Clear();
                Console.WriteLine("Is your number higher (+) lower (-) or equal (=) to " + reference + "?");
                switch (Console.ReadKey().KeyChar) {
                    case '+':
                        min = reference + 1;
                        if (max == -1) reference = reference * 2; // Maximum not set, so double the reference
                        else reference = (max + min) / 2;
                        break;
                    case '-':
                        max = reference - 1;
                        reference = (max + min) / 2;
                        break;
                    case '=':
                        Console.Clear();
                        Console.WriteLine($"Found your number ({reference}) on {attempt} attempts!");
                        finished = true; // Exit loop
                        break;
                    default:
                        Console.WriteLine("Invalid option!");
                        attempt--; // Do not count this attempt
                        break;
                }
            } while (!finished);
            return reference;
        }
        static int GuessTheNumber(int number) {
            Console.Clear();
            if (Math.Abs(number) != number) {
                // Negative number, so generate a random number
                Console.Write("Minimum number:\n>");
                int min = Int32.Parse(Console.ReadLine());
                Console.Write("Maximum number:\n>");
                int max = Int32.Parse(Console.ReadLine());
                Random random = new Random();
                number = random.Next(min, max + 1);
            }
            string input;
            int attempt = 0;
            int guess = 0;
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            Console.Clear();
            do {
                Console.Write("Write your guess:\n>");
                input = Console.ReadLine();
                if ((input.Length == 0)) {
                    Console.WriteLine("Empty input.");
                } else {
                    attempt++; // Count attempt
                    Console.WriteLine($"Attempt #{attempt}, {stopwatch.Elapsed:hh\\:mm\\:ss} elapsed");
                    guess = Int32.Parse(input);
                    Console.Clear();
                    if (guess == number) {
                        stopwatch.Stop();
                        Console.WriteLine($"You found the number {number} in {attempt} attempts ({stopwatch.Elapsed:hh\\:mm\\:ss})!");
                        break; // Exit loop
                    } else if (guess > number) {
                        Console.WriteLine($"Lower than {guess}!");
                    } else if (guess < number) {
                        Console.WriteLine($"Higher than {guess}!");
                    } else {
                        Console.WriteLine("Error. Try again.");
                        attempt--; // Do not count this attempt
                    }
                }
            } while (true);
            return attempt;
        }
    }
}

