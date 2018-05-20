using HotelsApp.Core.DataModels.Page;
using System.Collections.Generic;

namespace HotelsApp.UI.Pages
{
    public class PageManager
    {
        Dictionary<ApplicationPage, BasePage> pages;

        static PageManager instance;
        public static PageManager Instance => instance ?? (instance = new PageManager());

        public BasePage this[ApplicationPage page] => GetPage(page);

        PageManager()
        {
            pages = new Dictionary<ApplicationPage, BasePage>();
        }

        BasePage GetPage(ApplicationPage page)
        {
            BasePage result = null;
            if (!pages.ContainsKey(page))
            {
                switch (page)
                {
                    case ApplicationPage.StartPage:
                        result = new StartupPage();
                        break;
                    case ApplicationPage.HotelPage:
                        result = new HotelPage();
                        break;
                    case ApplicationPage.OrderPage:
                        result = new OrderPage();
                        break;
                    case ApplicationPage.LoginPage:
                        result = new LoginPage();
                        break;
                }
                if (result != null)
                    pages.Add(page, result);
            }
            else result = pages[page];
            return result;
        }

        public ApplicationPage BasePageType(BasePage page)
        {
            switch (page)
            {
                case StartupPage p:
                    return ApplicationPage.StartPage;
                case HotelPage p:
                    return ApplicationPage.HotelPage;
                case OrderPage p:
                    return ApplicationPage.OrderPage;
                case LoginPage p:
                    return ApplicationPage.LoginPage;
            }
            return default(ApplicationPage);
        }
    }
}