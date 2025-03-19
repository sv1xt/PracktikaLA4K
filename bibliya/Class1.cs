using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bibliya
{
    // Класс, представляющий информацию о товаре на складе (полученную из API)
    public class WarehouseItem
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int WarehouseId { get; set; }
        public string WarehouseName { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }

    public class WarehouseInfo
    {
        // 1. Подсчет количества товаров

        // 1.1 Общее количество товара (по всем складам)
        public static int CountTotalProductQuantity(List<WarehouseItem> items, int productId)
        {
            // Фильтруем по ID товара и суммируем количество
            return items.Where(item => item.ProductId == productId).Sum(item => item.Quantity);
        }

        // 1.2 Количество товаров на конкретном складе (сумма всех позиций)
        public static int CountWarehouseProductQuantity(List<WarehouseItem> items, int warehouseId)
        {
            // Фильтруем по ID склада и суммируем количество
            return items.Where(item => item.WarehouseId == warehouseId).Sum(item => item.Quantity);
        }

        // 1.3 (Перегрузка) Количество товара на конкретном складе по ID товара.
        public static int CountWarehouseProductQuantity(List<WarehouseItem> items, int warehouseId, int productId)
        {
            return items.Where(item => item.WarehouseId == warehouseId && item.ProductId == productId)
                       .Sum(item => item.Quantity);
        }

        // 2. Подсчет суммы стоимости товаров

        // 2.1 Сумма стоимости товаров на конкретном складе
        public static decimal CalculateWarehouseTotalValue(List<WarehouseItem> items, int warehouseId)
        {
            return items.Where(item => item.WarehouseId == warehouseId)
                       .Sum(item => item.Quantity * item.Price);
        }

        // 2.2 (Перегрузка) Сумма стоимости товаров на конкретном складе для определенного товара
        public static decimal CalculateWarehouseTotalValue(List<WarehouseItem> items, int warehouseId, int productId)
        {
            return items.Where(item => item.WarehouseId == warehouseId && item.ProductId == productId)
                       .Sum(item => item.Quantity * item.Price);
        }
        // 2.3 Сумма стоимости товаров по всем складам (общая)
        public static decimal CalculateTotalValue(List<WarehouseItem> items)
        {
            return items.Sum(item => item.Quantity * item.Price);
        }

        // 2.4 (Перегрузка) Общая сумма стоимости определенного товара
        public static decimal CalculateTotalValue(List<WarehouseItem> items, int productId)
        {
            return items.Where(item => item.ProductId == productId)
                         .Sum(item => item.Quantity * item.Price);
        }


        // 3. Подсчет товара по категориям

        // 3.1 Количество товара по категориям на складе
        public static Dictionary<string, int> CountProductsByCategoryInWarehouse(List<WarehouseItem> items, int warehouseId)
        {
            // Группируем по названию категории и считаем сумму количества в каждой группе
            return items.Where(item => item.WarehouseId == warehouseId)
                       .GroupBy(item => item.CategoryName)
                       .ToDictionary(group => group.Key, group => group.Sum(item => item.Quantity));
        }

        // 3.2 Количество товара по всем складам, сгруппированное по категориям и складам.
        public static Dictionary<string, Dictionary<string, int>> CountProductsByCategory(List<WarehouseItem> items)
        {
            return items.GroupBy(item => item.WarehouseName)
                       .ToDictionary(
                           warehouseGroup => warehouseGroup.Key, // Ключ словаря - название склада
                           warehouseGroup => warehouseGroup
                               .GroupBy(item => item.CategoryName)
                               .ToDictionary(
                                   categoryGroup => categoryGroup.Key, // Ключ вложенного словаря - название категории
                                   categoryGroup => categoryGroup.Sum(item => item.Quantity) // Значение влож. словаря - количество
                               )
                       );
        }

        //3.3 (Перегрузка) Количество товаров на всех складах по определенной категории.
        public static int CountProductsByCategory(List<WarehouseItem> items, int categoryId)
        {
            return items.Where(item => item.CategoryId == categoryId)
                       .Sum(item => item.Quantity);

        }

        //3.4 (Перегрузка) Количество товаров определенной категории на складе.
        public static int CountProductsByCategoryInWarehouse(List<WarehouseItem> items, int warehouseId, int categoryId)
        {
            return items.Where(item => item.WarehouseId == warehouseId && item.CategoryId == categoryId)
                         .Sum(item => item.Quantity);
        }
    }
}

