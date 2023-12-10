namespace Cofi
{

    // Decorators
    public abstract class CoffeeDecorator : Coffee
    {
        protected Coffee coffee;

        public CoffeeDecorator(Coffee coffee)
        {
            this.coffee = coffee;
        }

        public override string GetDescription()
        {
            return coffee.GetDescription();
        }

        public override int Cost()
        {
            return coffee.Cost();
        }
    }

    public class MilkDecorator : CoffeeDecorator
    {
        public MilkDecorator(Coffee coffee) : base(coffee)
        {
        }

        public override string GetDescription()
        {
            return $"{base.GetDescription()} with Milk";
        }

        public override int Cost()
        {
            return base.Cost() + 200;
        }
    }

    public class PumpkinSpiceDecorator : CoffeeDecorator
    {
        public PumpkinSpiceDecorator(Coffee coffee) : base(coffee)
        {
        }

        public override string GetDescription()
        {
            return $" ✧ Seasonal {base.GetDescription()} ✧";
        }

        public override int Cost()
        {
            return base.Cost() + 500;
        }
    }
}
