namespace Cofi
{
    // Factories! All of them
    public abstract class CoffeeFactory
    {
        public abstract Coffee CreateCoffee();
    }

    public class BlackCoffeeFactory : CoffeeFactory
    {
        public override Coffee CreateCoffee()
        {
            return new BlackCoffee();
        }
    }

    public class CappuccinoFactory : CoffeeFactory
    {
        public override Coffee CreateCoffee()
        {
            return new Cappuccino();
        }
    }

    public class LatteFactory : CoffeeFactory
    {
        public override Coffee CreateCoffee()
        {
            return new Latte();
        }
    }

    public interface IMenu
    {
        void DisplayMenu();
    }

    public class Barista
    {
        public void TakeOrder()
        {
            Console.WriteLine("Barista takes the customer's order.");
        }
    }

    // Adapter: Barista <-> Menu
    public class MenuAdapter : IMenu
    {
        private Barista barista;

        public MenuAdapter(Barista barista)
        {
            this.barista = barista;
        }

        public void DisplayMenu()
        {
            barista.TakeOrder();
        }
    }
   

    // strategy declared:
    public interface IPaymentStrategy
    {
        void ProcessPayment(double amount);
    }

    public class CardPaymentStrategy : IPaymentStrategy
    {
        public void ProcessPayment(double amount)
        {
            Console.WriteLine($"Processing card payment of {amount:C}");
        }
    }

    public class CashPaymentStrategy : IPaymentStrategy
    {
        public void ProcessPayment(double amount)
        {
            Console.WriteLine($"Collecting cash payment of {amount:C}");
        }
    }

    // Inheritance
    public abstract class Coffee
    {
        public abstract string GetDescription();
        public abstract int Cost();
    }

    public class BlackCoffee : Coffee
    {
        public override string GetDescription()
        {
            return "Simple Coffee";
        }

        public override int Cost()
        {
            return 600;
        }
    }
    public class Cappuccino : Coffee
    {
        public override string GetDescription()
        {
            return "Cappuccino";
        }

        public override int Cost()
        {
            return 800;
        }
    }
    public class Latte : Coffee
    {
        public override string GetDescription()
        {
            return "Latte";
        }

        public override int Cost()
        {
            return 1000;
        }
    }

    



    class Program
    {
        static void Main()
        {
            CoffeeShop coffeeShop = CoffeeShop.Instance;
            coffeeShop.WelcomeMessage();
            Barista barista = new Barista();
            IMenu menuAdapter = new MenuAdapter(barista);

            Console.WriteLine("Enter your name:");
            string customerName = Console.ReadLine();

            Dictionary<string, CoffeeFactory> coffeeFactories = new Dictionary<string, CoffeeFactory>
        {
            {"1", new BlackCoffeeFactory()},
            {"2", new CappuccinoFactory()},
            {"3", new LatteFactory()}
        };

            
            while (true)
            {
                Console.WriteLine("\nChoose a coffee from the menu (enter the corresponding number):");
                Console.WriteLine("1. Simple Coffee");
                Console.WriteLine("2. Cappuccino");
                Console.WriteLine("3. Latte");
                Console.WriteLine("Q. Quit");

                string userInput = Console.ReadLine().Trim().ToUpper();

                if (userInput == "Q")
                {
                    coffeeShop.ThankMessage();
                    return;
                }

                if (coffeeFactories.TryGetValue(userInput, out CoffeeFactory coffeeFactory))
                {
                    ICommand orderCoffeeCommand = new OrderCoffeeCommand(menuAdapter, coffeeFactory, customerName);
                    orderCoffeeCommand.Execute();
                }
                else
                {
                    Console.WriteLine("Invalid choice. Please try again.");
                }
            }
        }
    }
}
