using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;

// Класс для представления предмета
public class Product
{
    public int Id { get; set; } // XD
    public string Name { get; set; } // Название
    public double Price { get; set; } // Цена 
    public int Quantity { get; set; } // Количество в инвентаре

    // Конструктор класса Product
    public Product(int id, string name, double price, int quantity)
    {
        Id = id;
        Name = name;
        Price = price;
        Quantity = quantity;
    }
}

// Класс для управления инвентарем
public class Inventory
{
    private List<Product> products; // Список предметов в инвентаре
    private string fileName; // Имя файла для хранения данных в формате JSON

    // Конструктор класса Inventory
    public Inventory(string fileName)
    {
        this.fileName = fileName;
        products = new List<Product>(); // Создаем пустой список
        LoadData(); // Загружаем данные из файла
    }

    // Метод для загрузки данных из файла в формате JSON
    private void LoadData()
    {
        if (File.Exists(fileName))
        {
            string jsonString = File.ReadAllText(fileName); // Считываем содержимое файла в строку
            products = JsonSerializer.Deserialize<List<Product>>(jsonString); // Десериализуем строку в список предметов
        }
    }

    // Метод для сохранения данных в файл в формате JSON
    private void SaveData()
    {
        string jsonString = JsonSerializer.Serialize(products); // Сериализуем список предметов в строку
        File.WriteAllText(fileName, jsonString); // Записываем строку в файл
    }

    // Метод для добавления нового предмета в инвентарь
    public void AddProduct(Product product)
    {        
        products.Add(product); 
        SaveData(); 
        Console.WriteLine($"Предмет {product.Name} Добавлен.");
    }

    // Метод для удаления существующего предмета из инвентаря по идентификатору
    public void RemoveProduct(int id)
    {       
        // Ищем предмет по идентификатору в списке
        Product product = products.Find(p => p.Id == id);
        if (product != null) // Если нашли
        {
            products.Remove(product); // Удаляем предмет из списка
            SaveData(); 
            Console.WriteLine($"Предмет {product.Name} удалён.");
        }
        else // Если не нашли
        {
            Console.WriteLine($"Предмета с {id} не найден.");
        }
    }

    // Метод для очистки всего инвентаря
    public void ClearInventory()
    {         
        products.Clear();
        SaveData();
        Console.WriteLine("Инвентарь очищен.");
    }

    // Метод для вывода всех предметов в инвентаре на консоль
    public void PrintAll()
    {       
        Console.WriteLine($"В инвенторе есть {products.Count} предметов:");
        foreach (Product product in products)
        {
            Console.WriteLine($"Id: {product.Id}");
            Console.WriteLine($"Название: {product.Name}");
            Console.WriteLine($"Стоимость: {product.Price}");
            Console.WriteLine($"Кол-во: {product.Quantity}");
            Console.WriteLine();
        }
    }
    public Product InputProduct()
    {    
        // Создаем переменные для хранения данных
        int id;
        string name;
        double price;
        int quantity;

        // Спрашиваем пользователя о каждом параметре
        Console.Write("Введите ID предмета: ");
        id = Convert.ToInt32(Console.ReadLine());
        Console.Write("Введите имя предмета: ");
        name = Console.ReadLine();
        Console.Write("Введите цену предмета: ");
        price = Convert.ToDouble(Console.ReadLine());
        Console.Write("Введите кол-во: ");
        quantity = Convert.ToInt32(Console.ReadLine());

        // Создаем и возвращаем объект Product
        return new Product(id, name, price, quantity);
    }

    // Метод для поиска предмета по имени в списке products
    public int FindProduct(string name)
    {
        // Ищем индекс предмета с заданным именем
        int index = products.FindIndex(p => p.Name == name);
        return index; 
    }

    // Метод для изменения данных о существующем предмете с консоли
    public void EditProduct(int index)
    {
         
        // Проверяем, что индекс в допустимом диапазоне
        if (index >= 0 && index < products.Count)
        {
            // Получаем ссылку на предмет по индексу
            Product product = products[index];

            // Спрашиваем пользователя, какой параметр он хочет изменить
            Console.WriteLine("Что хотите изменить сегодня?");
            Console.WriteLine("1 - Id");
            Console.WriteLine("2 - Имя");
            Console.WriteLine("3 - Цену");
            Console.WriteLine("4 - Кол-во");
            Console.Write("Введите свой выбор: ");
            int choice = Convert.ToInt32(Console.ReadLine());

            // Обрабатываем разные варианты выбора
            switch (choice)
            {
                case 1: // Изменяем id
                    Console.Write("Введите новый id: ");
                    product.Id = Convert.ToInt32(Console.ReadLine());
                    break;
                case 2: // Изменяем имя
                    Console.Write("Введите новое Имя: ");
                    product.Name = Console.ReadLine();
                    break;
                case 3: // Изменяем цену
                    Console.Write("Введите новую цену: ");
                    product.Price = Convert.ToDouble(Console.ReadLine());
                    break;
                case 4: // Изменяем количество
                    Console.Write("Введите новое кол-во: ");
                    product.Quantity = Convert.ToInt32(Console.ReadLine());
                    break;
                default: // Неверный выбор
                    Console.WriteLine("Инвалид выбор.");
                    break;
            }

            SaveData();
            Console.WriteLine($"Предмет {product.Name} изменён.");
        }
        else // Неверный индекс
        {
            Console.WriteLine($"это кто: {index} ?");
        }
    }

    // Метод для вывода меню и выбора действия
    public void ShowMenu()
    {
        int choice;

        // Повторяем, пока пользователь не выберет выход
        do
        {
            Console.WriteLine("Инвентарное меню");
            Console.WriteLine("Нажмите \" 1 \" для добавления предмета");
            Console.WriteLine("Нажмите \" 2 \" для удаления предмета");
            Console.WriteLine("Нажмите \" 3 \" для редактирования предмета");
            Console.WriteLine("Нажмите \" 4 \" для демонстрации списка предметов");
            Console.WriteLine("Нажмите \" 5 \" для \"уединения\"");
            Console.Write("Сделай свой выбор: ");

            choice = Convert.ToInt32(Console.ReadLine());

            switch (choice)
            {
                case 1: // Добавляем предмет
                    AddProduct(InputProduct());
                    break;
                case 2: // Удаляем предмет 
                    Console.Write("Выберете id предмета для удаления: ");
                    int id = Convert.ToInt32(Console.ReadLine());
                    RemoveProduct(id); 
                    break;
                case 3: // Редактируем предмет
                    Console.Write("Введите имя предмета: ");
                    string name = Console.ReadLine();
                    id = FindProduct(name);
                    EditProduct(id);
                    break;
                case 4: // Выводим все предметы
                    PrintAll();
                    break;
                case 5: // Выходим из программы

                    Random random = new Random();
                    int number = random.Next(0, 100);
                    if (number < 25)
                    {
                        for (int i = 0; i < number; i++)
                        { 
                        Console.WriteLine();
                        Console.WriteLine("Сritical error when closing a file. please delete the file from your computer");
                        }
                    }
                    else
                    {
                        Console.WriteLine("До встречи!");
                    }
                    break;
                default: // Неверный выбор
                    Console.WriteLine("Вы кажется меня не правильно поняли.");
                    break;
            }

        } while (choice != 5); // Проверяем условие цикла
    }
}

//запуск всего этого
class Program
{
    static void Main(string[] args)
    {
        Inventory inventory = new Inventory("inventory.json"); // Создаем объект класса Inventory с именем файла "inventory.json"

        inventory.ShowMenu();

        // добавлени несколько объектов Product
        //Product apple = new Product(1, "Apple", 15, 10);
        //Product FumoMarisa = new Product(2, "Fumo Marisa", 99999, 1);
        //Product SSD = new Product(3, "SSD", 500, 1);

        // Добавляем предметы в инвентарь
        //inventory.AddProduct(apple);
        //inventory.AddProduct(FumoMarisa);
        //inventory.AddProduct(SSD);

        // Выводим все предметы в инвентаре
        //inventory.PrintAll();

        // Удаляем предмет из инвентаря по id
        //inventory.RemoveProduct(2);

        // Очищаем весь инвентарь
        //inventory.ClearInventory();

    }
}