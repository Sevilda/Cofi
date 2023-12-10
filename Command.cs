using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cofi
{
	// behavior: command
	public interface ICommand
	{
		void Execute();
	}

	public class OrderCoffeeCommand : ICommand
	{

		private IMenu menuAdapter;
		private CoffeeFactory coffeeFactory;
		private string customerName;

		public OrderCoffeeCommand(IMenu menuAdapter, CoffeeFactory coffeeFactory, string customerName)
		{
			this.menuAdapter = menuAdapter;
			this.coffeeFactory = coffeeFactory;
			this.customerName = customerName;
		}

		public void Execute()
		{
			menuAdapter.DisplayMenu();
			Coffee coffee = coffeeFactory.CreateCoffee();
			Console.WriteLine($"You ordered: {coffee.GetDescription()}");
			Console.WriteLine($"Base cost: {coffee.Cost()}");

			Console.WriteLine("Would you like try our seasonal spices? (Y/N)");
			string seasonalInput = Console.ReadLine().Trim().ToUpper();

			if (seasonalInput == "Y")
			{
				coffee = new PumpkinSpiceDecorator(coffee);
				Console.WriteLine($"Added Pumpkin spice! New cost: {coffee.Cost()}");
			}

			Console.WriteLine("Would you like to add milk? (Y/N)");
			string addMilkInput = Console.ReadLine().Trim().ToUpper();

			if (addMilkInput == "Y")
			{
				coffee = new MilkDecorator(coffee);
				Console.WriteLine($"Added Milk. New cost: {coffee.Cost()}");
			}

			Console.WriteLine("Preparing your coffee...");
			CoffeeShop.Instance.DisplayMessage("Your coffee is ready!");

			// using observer
			CoffeeShopNotifier notifier = new CoffeeShopNotifier();
			ICustomer customer = new CoffeeCustomer(customerName);
			notifier.RegisterCustomer(customer);
			notifier.NotifyCustomers("Your coffee is ready for pickup!");

			// using strategy
			Console.WriteLine("How would you like to pay? (1. Credit Card, 2. Cash)");
			string paymentChoice = Console.ReadLine().Trim();

			IPaymentStrategy paymentStrategy = GetPaymentStrategy(paymentChoice);
			paymentStrategy.ProcessPayment(coffee.Cost());

			CoffeeShop.Instance.DisplayMessage("Enjoy your coffee!");
		}

		private IPaymentStrategy GetPaymentStrategy(string paymentChoice)
		{
			switch (paymentChoice)
			{
				case "1":
					return new CardPaymentStrategy();
				case "2":
					return new CashPaymentStrategy();
				default:
					Console.WriteLine("Invalid payment choice. Using default payment strategy (Cash).");
					return new CashPaymentStrategy();
			}
		}
	}
}
