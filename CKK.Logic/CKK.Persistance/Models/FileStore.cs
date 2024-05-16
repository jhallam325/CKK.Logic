using CKK.Persistance.Interfaces;
using CKK.Logic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CKK.Logic.Models;
using System.Runtime.Serialization.Formatters.Binary;
using CKK.Logic.Exceptions;
using System.Xml.Linq;
using System.Reflection;
using System.Collections;

namespace CKK.Persistance.Models
{
    public class FileStore : IStore, ISavable, ILoadable
    {
        // Every Product used to be a StoreItem anad was changed for project deliverable 16
        private int idNumber = 1;
        public List<ShoppingCartItem> Products;

        public static class Globals
        {
            public static readonly string FilePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) +
                Path.DirectorySeparatorChar + "Persistance";

            public static readonly string FileName = Path.DirectorySeparatorChar + "Products.dat";

            public static readonly string FullPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) +
                Path.DirectorySeparatorChar + "Persistance" + Path.DirectorySeparatorChar + "Products.dat";
        }

        public FileStore() 
        {
            CreatePath(Globals.FilePath);
            Products = new List<ShoppingCartItem>();
            // Get ID number and set it to the largst one + 1
        }

        private void CreatePath(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        public void Save()
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream output;

            //output = new FileStream(Globals.FullPath, FileMode.OpenOrCreate, FileAccess.Write);

            
            // If the output file exits, we'll append to it, otherwise create it.
            if (File.Exists(Globals.FullPath))
            {
                output = new FileStream(Globals.FullPath, FileMode.Append, FileAccess.Write);
            }
            else
            {
                output = new FileStream(Globals.FullPath, FileMode.OpenOrCreate, FileAccess.Write);
            }
            
            // We'll save the List of Store Items into the output file
            formatter.Serialize(output, Products);   
            output.Close();
        }

        public void Load() 
        {
            // This ensures that the input variable can access the file.
            if (File.Exists(Globals.FullPath))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream input = new FileStream(Globals.FullPath, FileMode.Open, FileAccess.Read);

                // If the StoreItems.dat file has information, then we can deserialize it.
                // We load the whole list and store it into the StoreItems list variable name
                if (input.Length > 0)
                {
                    Products = (List<ShoppingCartItem>)formatter.Deserialize(input);
                }
                input.Close();
            }
        }

        /*
         * No more StoreItems - This method can probaly be deleted
        public StoreItem? AddStoreItem(Product product, int quantity)
        {
            if (quantity < 0)
            {
                throw new InventoryItemStockTooLowException();
            }

            if (product == null)
            {
                throw new ProductDoesNotExistException();
            }

            StoreItem newItem = new StoreItem(product, quantity);

            // Sets the product number then increases the product number by 1 for
            // the next item.
            product.Id = idNumber++;


            if (Products.Count() <= 0)
            {
                Products.Add(newItem);
                Save();
                return newItem;
            }

            // Contains doesn't work since the quantity doesn't need to match
            foreach (StoreItem item in Products)
            {
                if (item.Product.Id == newItem.Product.Id)
                {
                    int indexOfItem = Products.IndexOf(item);
                    int oldQuantity = Products[indexOfItem].Quantity;
                    Products[indexOfItem].Quantity = (oldQuantity + quantity);
                    return Products[indexOfItem];
                }
            }

            // quanity is > 0 and the new item was not found in _storeItems list
            Products.Add(newItem);
            Save();
            return newItem;
        }
        */

        /*
        public void DeleteStoreItem(int id)
        {
            if (id < 0)
            {
                throw new InvalidIdException();
            }

            StoreItem itemToBeRemoved = FindStoreItemById(id);

            Products.Remove(itemToBeRemoved);
            Save();
        }
        */

        /*
        public StoreItem? FindStoreItemById(int id)
        {
            if (id < 0)
            {
                throw new InvalidIdException();
            }

            foreach (StoreItem item in Products)
            {
                if (id == item.Product.Id)
                {
                    return item;
                }
            }

            return null;
        }
        */

        /*
        public List<StoreItem>? GetStoreItems()
        {
            Load();
            return Products;
        }
        */

        /*
        public StoreItem? RemoveStoreItem(int id, int quantity)
        {
            if (quantity < 0)
            {
                throw new ArgumentOutOfRangeException();
            }

            StoreItem? oldItem = FindStoreItemById(id);

            if (oldItem == null)
            {
                throw new ProductDoesNotExistException();
            }

            if (Products.Contains(oldItem))
            {
                int indexOfItem = Products.IndexOf(oldItem);
                int oldQuantity = Products[indexOfItem].Quantity;
                int difference = oldQuantity - quantity;
                if (difference >= 0)
                {
                    Products[indexOfItem].Quantity = difference;
                }
                else
                {
                    Products[indexOfItem].Quantity = 0;
                }
                Save();
                return Products[indexOfItem];
            }
            else
            {
                throw new ProductDoesNotExistException();
            }
        }
        */

        /*
        public List<StoreItem> GetAllProductsByName(string name)
        {
            string searchKey = name.ToUpper();
            int length = searchKey.Length;
            List<StoreItem> list = new List<StoreItem>();

            foreach (var item in Products)
            {
                string itemName = item.Product.Name.ToUpper();

                // if the search key is larger than the product name, we know the product is not what we are looking
                // for and can skip it. Ex: If an search key is HamSandwich and the item is just Ham, it should not
                // add Ham to the list, even though the first letters match
                if (length <= itemName.Length)
                {
                    // Loop through every letter of the search key
                    for (int i = 0; i < length; i++)
                    {
                        // If any letter doesn't match, we'll break out of the for loop and do the next item.
                        if (itemName[i] != searchKey[i])
                        {
                            break;
                        }

                        // This if statement will only be called if every letter of the search match every letter
                        // of the product name AND we already checked every letter
                        if (i == length - 1)
                        {
                            list.Add(item);
                        }
                    }
                }                
            }

            if (list.Count == 0)
            {
                // Show some sort of error message saying that there are no items in the search
                return Products;
            }
            else
            {
                // Every item has been iterated through and added to the list so I can return it now.
                list.OrderBy(x => x.Product.Name);
                return list;
            }
        }
        */

        /*
        public List<StoreItem> GetProductsByQuantity()
        {
            if (Products.Count <= 1)
            {
                // There is 1 or 0 item in the list and doesn't need to be sorted.
                return Products;
            }
            else
            {
                List<StoreItem> orderedList = Products.OrderBy(x => x.Quantity).ToList();
                return orderedList;
            }
        }
        */

        /*
        public List<StoreItem> GetProductsByPrice()
        {
            if (Products.Count <= 1)
            {
                // There is 1 or 0 item in the list and doesn't need to be sorted.
                return Products;
            }
            else
            {
                List<StoreItem> orderedList = Products.OrderBy(x => x.Product.Price).ToList();
                return orderedList;
            }
        }
        */
    }
}
