namespace Cofi
{
    // one shop: singleton
    public class CoffeeShop
    {
        private static CoffeeShop instance;

        private CoffeeShop() { }

        public static CoffeeShop Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new CoffeeShop();
                }
                return instance;
            }
        }

        public void WelcomeMessage()
        {
            Console.WriteLine("Welcome to the Coffee Shop!");
        }

        public void ThankMessage()
        {
            Console.WriteLine("Thank you for visiting the Coffee Shop!");
        }

        public void DisplayMessage(string message)
        {
            Console.WriteLine(message);
        }
    }
}
