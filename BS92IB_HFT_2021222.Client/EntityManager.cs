using ConsoleTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BS92IB_HFT_2021222.Client
{
    public abstract class EntityManager<T>
    {
        protected readonly IRestService restService;
        protected readonly string controller;
        private readonly ConsoleMenu menu;

        public EntityManager(IRestService restService, string title, string controller)
        {
            this.restService = restService;
            this.controller = controller;
            menu = new ConsoleMenu()
                .Add("Create", Create)
                .Add("Read", () => List(Show))
                .Add("Update", () => List(Update))
                .Add("Delete", () => List(Delete))
                .Add("Back", ConsoleMenu.Close)
                .Configure(c => {
                    c.Selector = "-->";
                    c.Title = title;
                    c.EnableWriteTitle = true;
                });
        }
        public void Open()
        {
            menu.Show();
        }

        public T Get(int id)
        {
            return restService.Get<T>(id, controller);
        }

        protected abstract void Create();
        protected abstract void Show(T entity);
        protected abstract void Update(T entity);
        protected abstract void Delete(T entity);
        public abstract void List(Action<T> action);
    }
}
