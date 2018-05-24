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
                        result = new StartupPage(); break;
                    case ApplicationPage.HotelPage:
                        result = new HotelPage(); break;
                    case ApplicationPage.OrderPage:
                        result = new OrderPage(); break;
                    case ApplicationPage.LoginPage:
                        result = new LoginPage(); break;
                    case ApplicationPage.HotelEditPage:
                        result = new HotelEditPage(); break;
                    case ApplicationPage.RoomsManagerPage:
                        result = new RoomsManagerPage(); break;
                    case ApplicationPage.ReportsPage:
                        result = new ReportsPagePage(); break;
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
                case HotelEditPage p:
                    return ApplicationPage.HotelEditPage;
                case RoomsManagerPage p:
                    return ApplicationPage.RoomsManagerPage;
                case ReportsPagePage p:
                    return ApplicationPage.ReportsPage;
            }
            return default(ApplicationPage);
        }
    }
}