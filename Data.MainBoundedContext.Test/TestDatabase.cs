using System;
using Data.MainBoundedContext.Repositories;
using Data.MainBoundedContext.UnitOfWork;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Data.MainBoundedContext.Test
{
    [TestClass]
    public class TestDatabase
    {
        [TestMethod]
        public void TestMethod1()
        {
            var menus = new MenuRepository(new MainBCUnitOfWork()).GetAll();

            foreach (var menu in menus)
            {
                Console.WriteLine(menu.Action);
            }
        }
    }
}
