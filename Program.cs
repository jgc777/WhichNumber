using System.Diagnostics;

namespace WhichNumber {
    class Program {
        static void Main(string[] args) {
            try {
                Console.Clear();
                Console.Title = "Which Number?";
                Banner();
                switch (args.Length) {
                    case 0:
                        Menu();
                        break;
                    case 1:
                        switch (args[0][0]) {
                            case 'a':
                                GuessTheNumber(-1);
                                break;
                            case 'b':
                                BinarySearch(100);
                                break;
                            default:
                                Console.WriteLine("Invalid first argument!");
                                break;
                        }
                        break;
                    case 2:
                        switch (args[0][0]) {
                            case 'a':
                                GuessTheNumber(Int32.Parse(args[1]));
                                break;
                            case 'b':
                                BinarySearch(Int32.Parse(args[1]));
                                break;
                            default:
                                Console.WriteLine("Invalid first argument!");
                                break;
                        }
                        break;
                    default:
                        Console.WriteLine("Too many arguments!");
                        break;
                }
            } catch (Exception ex) {
                Console.Write($"\nError: {ex.Message}\n");
            }
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
b) Make your computer guess your number");
            switch (Console.ReadKey().KeyChar.ToString().ToLower()) {
                case "a":
                    GuessTheNumber(-1);
                    break;
                case "b":
                    BinarySearch(100);
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
                attempt = attempt + 1;
                Console.Clear();
                Console.WriteLine("Is your number higher (+) lower (-) or equal (=) to " + reference + "?");
                switch (Console.ReadKey().KeyChar) {
                    case '+':
                        min = reference + 1;
                        if (max == -1) {
                            reference = reference * 2;
                        } else {
                            reference = (max + min) / 2;
                        }
                        break;
                    case '-':
                        max = reference - 1;
                        reference = (max + min) / 2;
                        break;
                    case '=':
                        Console.Clear();
                        Console.WriteLine($"Found your number ({reference}) on {attempt} attempts!");
                        finished = true;
                        break;
                    default:
                        Console.WriteLine("Invalid option!");
                        attempt--;
                        break;
                }
            } while (!finished);
            return reference;
        }
        static void GuessTheNumber(int number) {
            Console.Clear();
            if (!(Math.Abs(number) == number)) {
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
                if (!(input == "")) {
                    attempt++;
                    guess = Int32.Parse(input);
                    Console.Clear();
                    if (guess == number) {
                        stopwatch.Stop();
                        Console.WriteLine($"You found the number {number} in {attempt} attempts ({stopwatch.Elapsed:hh\\:mm\\:ss})!");
                        break;
                    } else if (guess > number) {
                        Console.WriteLine($"Lower than {guess}!");
                    } else if (guess < number) {
                        Console.WriteLine($"Higher than {guess}!");

                    } else {
                        Console.WriteLine("Error. Try again.");
                        attempt--;
                    }
                } else {
                    Console.WriteLine("Empty input.");
                    attempt--;
                    System.Threading.Thread.Sleep(2000);
                    GuessTheNumber(number);
                }
                
            } while (true);
        }
    }
}

