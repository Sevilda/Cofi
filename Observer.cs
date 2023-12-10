namespace Cofi
{
    //  Observer: coffee shop <- customers
    public interface ICustomer
    {
        void Notify(string message);
    }

    public class CoffeeCustomer : ICustomer
    {
        private string name;

        public CoffeeCustomer(string name)
        {
            this.name = name;
        }

        public void Notify(string message)
        {
            Console.WriteLine($"{name}: {message}");
        }
    }
    public class CoffeeShopNotifier // service
    {
        private List<ICustomer> customers = new List<ICustomer>();

        public void RegisterCustomer(ICustomer customer)
        {
            customers.Add(customer);
        }

        public void UnregisterCustomer(ICustomer customer)
        {
            customers.Remove(customer);
        }

        public void NotifyCustomers(string message)
        {
            foreach (var customer in customers)
            {
                customer.Notify(message);
            }
        }
    }
}
