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
            }
            return default(ApplicationPage);
        }
    }
}