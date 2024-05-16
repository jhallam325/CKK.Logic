using CKK.Logic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CKK.tests
{
    public class StoreTests
    {
        [Fact]
        public void AddRemoveProduct_ShouldAddAndRemoveProductCorrectlySomeOfAnItem()
        {
            Store store = new Store();
            Product product1 = new Product();
            Product product2 = new Product();
            int quantity1 = 15;
            int removeQuantity1 = 5;

            product1.SetId(1);

            store.AddStoreItem(product1, quantity1);

            Assert.Equal(quantity1, store.FindStoreItemById(1).GetQuantity());

            store.RemoveStoreItem(1, removeQuantity1);

            Assert.Equal((quantity1 - removeQuantity1), store.FindStoreItemById(1).GetQuantity());
        }

        [Fact]
        public void AddRemoveProduct_ShouldAddAndRemoveProductCorrectlyAllOfAnItem()
        {
            Store store = new Store();
            Product product1 = new Product();
            int quantity1 = 5;
            int removeQuantity1 = 5;

            product1.SetId(1);

            store.AddStoreItem(product1, quantity1);

            Assert.Equal(quantity1, store.FindStoreItemById(1).GetQuantity());

            store.RemoveStoreItem(1, removeQuantity1);

            Assert.Equal((quantity1 - removeQuantity1), store.FindStoreItemById(1).GetQuantity());
        }

        [Fact]
        public void AddProduct_ShouldAddProductCorrectlyWithZeroQty()
        {
            Store store = new Store();
            Product product1 = new Product();
            int quantity1 = 0;

            product1.SetId(1);

            // Add Zero to an item
            store.AddStoreItem(product1, quantity1); // This should produce a null result.

            Assert.Null(store.FindStoreItemById(1));
        }

        [Fact]
        public void AddRemoveProduct_ShouldAddAndRemoveProductCorrectlyWithNegValue()
        {
            Store store = new Store();
            Product product1 = new Product();
            Product product2 = new Product();
            int quantity1 = -1;
            int quantity2 = 10;
            int removeQuantity2 = 100;

            product1.SetId(1);
            product2.SetId(2);

            store.AddStoreItem(product1, quantity1); // A neg qty should be a null item
            store.AddStoreItem(product2, quantity2);

            Assert.Null(store.FindStoreItemById(1));

            Assert.Equal(quantity2, store.FindStoreItemById(2).GetQuantity());

            store.RemoveStoreItem(2, removeQuantity2);

            // We removed more than in the store so this qty should be 0
            Assert.Equal(0, store.FindStoreItemById(2).GetQuantity());
        }

        [Fact]
        public void AddStoreItem_InvalidValueGiven()
        {
            Store store = new Store();
            Product product1 = new Product();
            int quantity1 = 1;
            product1.SetId(1);

            //
            store.AddStoreItem(null, quantity1);
            Assert.Null(store.FindStoreItemById(1));


        }

        [Fact]
        public void AddStoreItem_ShouldAddNewItemWithMultiple()
        {
            Store store = new Store();
            Product product1 = new Product();

            int quantity1 = 15;
            int quantity2 = 5;
            int quantity3 = 10;

            product1.SetId(1);

            store.AddStoreItem(product1, quantity1);
            store.AddStoreItem(product1, quantity2);
            store.AddStoreItem(product1, quantity3);

            Assert.Equal(quantity1 + quantity2 + quantity3, store.FindStoreItemById(1).GetQuantity());
        }

        // * StoreTests AddStoreItem_InvalidValueGiven [FAIL]
    }
}
