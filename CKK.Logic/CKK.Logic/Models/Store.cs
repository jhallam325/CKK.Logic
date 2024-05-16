using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CKK.Logic.Interfaces;

namespace CKK.Logic.Models
{
    public class Store : Entity, IStore
    {
        public List<StoreItem> StoreItems {get;set;}

        /*
         * AddStoreItem(Product prod)
         * Adds a product to the next available product (product1, product2, product3).
         * If there is no available product, it will not add a product at all
         * If there is an item in spot two, but not spot one or three, then it should 
         * put the item in spot 1 (the next available spot) 
         * 
         * In the AddStoreItem method, if the item already exists in the List, then you 
         * should add the quantity variable of that item 
         * and not add new StoreItem.
        */

        /*
        1. Create a StoreItem that I can add to the list using the constructor:
           public StoreItem(Product product, int quantity)
           {
               SetProduct(product);
               SetQuantity(quantity);
           }
           
           2. If this store item exists in the list of store items, I need to increase the quantity
              by the new quantity inputted
           	
           3.  and I guess return the item just created or updated?

        */
        public StoreItem? AddStoreItem(Product product, int quantity)
        {
            if(quantity <= 0 || product == null)
            {
                return null;
            }

            StoreItem newItem = new StoreItem(product, quantity);
            
            if (StoreItems.Count() <= 0)
            {
                StoreItems.Add(newItem);
                return newItem;
            }

            // Contains doesn't work since the quantity doesn't need to match
            foreach(StoreItem item in StoreItems)
            {
                if(item.Product.Id == newItem.Product.Id)
                {
                    int indexOfItem = StoreItems.IndexOf(item);
                    int oldQuantity = StoreItems[indexOfItem].Quantity;
                    StoreItems[indexOfItem].Quantity = (oldQuantity + quantity);
                    return StoreItems[indexOfItem];
                }
            }

            // quanity is > 0 and the new item was not found in _storeItems list
            StoreItems.Add(newItem);
            return newItem;
        }
        /*
         * RemoveStoreItem(int productNumber)
         * Removes a product from  the desired product
         * If there are no products, does nothing
         * If product is out of range, does nothing
         * It should not shift/move items up in the list when things are removed
         * 
         * In the RemoveStoreItem method, if the value is going to be less than 
         * zero, then it should stay at 0, and NOT remove the item from the store*
         * 1. Find the item with ID => FindStoreItemById(int id)
         *    return null if not found
         *    otherwise:
         *    	2. Check that item.quantity - quantity >= 0
         *    	3. otherwise set the qty to 0
         *    
         */
        public StoreItem? RemoveStoreItem(int id, int quantity)
        {
            StoreItem? oldItem = FindStoreItemById(id);
            if (oldItem == null)
            {
                return null;
            }
            if (StoreItems.Contains(oldItem))
            {
                int indexOfItem = StoreItems.IndexOf(oldItem);
                int oldQuantity = StoreItems[indexOfItem].Quantity;
                int difference = oldQuantity - quantity;
                if (difference >= 0)
                {
                    StoreItems[indexOfItem].Quantity = difference;
                }
                else
                {
                    StoreItems[indexOfItem].Quantity = 0;
                }

                return StoreItems[indexOfItem];
            }
            else
            {
                Console.WriteLine($"The ID {id} is not a product in the " +
                    $"storeItems list");
                return null;
            }
        }
        /*
         * GetStoreItem(int productNumber)
         * This is different from the FindById. This method gets the product by its position (product1, product2, or product3).
         * Should return correct product
         * If it is an invalid productNumber, then it will return null
         * If there is not an item in the desired spot, it will return null
        */
        public List<StoreItem>? GetStoreItems()
        {
            return StoreItems;
        }
        /*
         * FindStoreItemById(int id) 
         * This will return the product that has the same Id (if there is one)
         * If there are no items with that id, then it should return null
         * If there are more than one item with that Id, then it will return the first one
         * 
         * How do I find a StoreItem by an id?
         * A store item has a Product and a Product has an id and a GetID() Method
         * So, for every item in the StoreItem<> list, we can check the product id 
         * and return that product.
        */
        public StoreItem? FindStoreItemById(int id)
        {
            foreach (StoreItem item in StoreItems)
            {
                if (id == item.Product.Id)
                {
                    return item;
                }
            }
            return null;
            
        }
    }
}