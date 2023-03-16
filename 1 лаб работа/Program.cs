﻿static void Main()
{
    Cart cart = new Cart();
    cart.take_products();
    Console.WriteLine("Нажмите 1, чтобы сбалансировать\n0 - ничего не делать");
    int balance = Convert.ToInt32(Console.ReadLine());
    if (balance == 1)
    {
        cart.balance();
    }
    Console.WriteLine("Ваша корзина:");
    for (int i = 0; i < cart.cart.Count(); i++)
    {
        Console.WriteLine($"{cart.cart[i].Name}");
    }
}

Main();

public class Product
{
    public string? Name { get; set; }
    public string? Elem { get; set; }
}

public class Market
{
    public List<Product> Snacks = new List<Product>(new Product[] {new Product{Name="ChocolateBar", Elem="Carbohydratese"}, 
                                                               new Product{Name="BalykCheese", Elem="Proteins"},
                                                               new Product{Name="Crisps", Elem="Fats"}});
    public List<Product> semiFinishedProduct = new List<Product>(new Product[] {new Product{Name="Chicken", Elem="Proteins"}, 
                                                               new Product{Name="Fruit", Elem="Carbohydratese"},
                                                               new Product{Name="OliveOil", Elem="Fats"}});
    public List<Product> firtstProducts =  new List<Product>(new Product[] {new Product{Name="DumplingsMeat", Elem="Proteins"}, 
                                                               new Product{Name="DumplingsBerries", Elem="Carbohydratese"},
                                                               new Product{Name="Cheburek", Elem="Fats"}});
    public List<Product> allProducts = new List<Product>();
    

    public Market()
    {
        var all = this.Snacks.Concat(this.semiFinishedProduct).ToList().Concat(this.firtstProducts).ToList();
        this.allProducts = all;
    }
}


public class Cart
{
    public List<Product> cart = new List<Product>();
    public Market market = new Market();
    public List<String> elements = new List<string>();
    public List<String> mainElems = new List<string>() {"Proteins", "Fats", "Carbohydratese"};

    public List<Product> products = new List<Product>();

    public void take_products()
    {
        Console.WriteLine("Выберите категорию\n0 - все\n1 - Snacks\n2 - semiFinishedProduct\n3 - firstProducts");
        int choise1 = Convert.ToInt32(Console.ReadLine());
        if (choise1 == 1) 
        {
            this.products = this.market.Snacks;
        }
        else if (choise1 == 2)
        {
            this.products = this.market.semiFinishedProduct;
        }
        else if (choise1 == 3)
        {
            this.products = this.market.firtstProducts;
        }
        else
        {
            this.products = this.market.allProducts;
        }
        while (true)
        {
            Console.WriteLine("Введите номер товара, 0 для завершения\n");
            Console.WriteLine("Список товаров:");
            for (int i = 0; i < products.Count(); i++) 
            {
                int number = i + 1;
                Console.WriteLine($"{number.ToString()}) {this.products[i].Name}");
            }
            int choise = Convert.ToInt32(Console.ReadLine());
            if (choise == 0)
            {
                return;
            }
            if (!this.cart.Contains(this.products[choise - 1]))
            {
                this.cart.Add(this.products[choise - 1]);
                if (!this.elements.Contains(this.products[choise - 1].Elem) && this.mainElems.Contains(this.products[choise - 1].Elem))
                {
                    this.elements.Add(this.market.allProducts[choise - 1].Elem);
                }
                Console.WriteLine($"{this.products[choise - 1].Name} добавлен!");
            }
            else
            {
                Console.WriteLine($"{this.products[choise - 1].Name} уже в корзине!");
            }
        }
    }

    public void balance()
    {
        bool balancer = false;
        if (this.elements.Count() >= 3)
        {
            Console.WriteLine("Корзина уже сбалансирована");
            balancer = true;
        }
        else
        {
            for (int i = 0; i < this.products.Count(); i++)
            {
                Product product = this.products[i];
                if (!this.elements.Contains(product.Elem) && this.mainElems.Contains(product.Elem) && !this.cart.Contains(product)) 
                {
                    this.cart.Add(product);
                    this.elements.Add(product.Elem);
                }
                if (this.elements.Count() >= 3)
                {
                    Console.WriteLine("Корзина сбалансирована");
                    balancer = true;
                }
            }
            if (this.elements.Count() < 3)
            {
                Console.WriteLine("Корзину сбалансировать нельзя, но мы сделали всё, что могли!");
                balancer = true;
            }
        }
    }
}