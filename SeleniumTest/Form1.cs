using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using OfficeOpenXml;
using System.IO;
namespace SeleniumTest
{
    public partial class Form1 : Form
    {
        IWebDriver driver;
        WebDriverWait wait;

        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }
        //register
        public (string name, string phone, string email, string address, string username, string password) ReadRegisterData()
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "BestProgrammerData\\TestData.xlsx");

            using (ExcelPackage package = new ExcelPackage(new FileInfo(path)))
            {
                ExcelWorksheet sheet = package.Workbook.Worksheets["REGISTER"];

                string name = sheet.Cells[3, 1].Text;
                string phone = sheet.Cells[3, 2].Text;
                string email = sheet.Cells[3, 3].Text;
                string address = sheet.Cells[3, 4].Text;
                string username = sheet.Cells[3, 5].Text;
                string password = sheet.Cells[3, 6].Text;

                return (name, phone, email, address, username, password);
            }
        }
        //login
        public (string username, string password) ReadLoginData()
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "BestProgrammerData\\TestData.xlsx");

            using (ExcelPackage package = new ExcelPackage(new FileInfo(path)))
            {
                ExcelWorksheet sheet = package.Workbook.Worksheets["LOGIN"];

                string username = sheet.Cells[3, 1].Text;
                string password = sheet.Cells[3, 2].Text;

                return (username, password);
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            var data = ReadRegisterData();

            driver.Navigate().GoToUrl("https://localhost:44329");

            Actions action = new Actions(driver);
            IWebElement accountMenu = wait.Until(d => d.FindElement(By.XPath("/html/body/header/div/div/div[2]/div[1]")));
            action.MoveToElement(accountMenu).Perform();
            Thread.Sleep(1500);

            IWebElement registerBtn = wait.Until(d => d.FindElement(By.LinkText("Đăng ký")));
            registerBtn.Click();

            wait.Until(d => d.FindElement(By.Id("CustomerName"))).SendKeys(data.name);
            Thread.Sleep(500);
            driver.FindElement(By.Id("CustomerPhone")).SendKeys(data.phone);
            Thread.Sleep(500);
            driver.FindElement(By.Id("CustomerEmail")).SendKeys(data.email);
            Thread.Sleep(500);
            driver.FindElement(By.Id("CustomerAddress")).SendKeys(data.address);
            Thread.Sleep(500);
            driver.FindElement(By.Id("Username")).SendKeys(data.username);
            Thread.Sleep(500);
            driver.FindElement(By.Id("Password")).SendKeys(data.password);
            Thread.Sleep(500);
            driver.FindElement(By.Id("ConfirmPassword")).SendKeys(data.password);
            Thread.Sleep(500);
            driver.FindElement(By.CssSelector("button[type='submit']")).Click();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            var data = ReadLoginData();
            driver.Navigate().GoToUrl("https://localhost:44329");

            Actions action = new Actions(driver);
            IWebElement accountMenu = wait.Until(d => d.FindElement(By.XPath("/html/body/header/div/div/div[2]/div[1]")));
            action.MoveToElement(accountMenu).Perform();
            Thread.Sleep(1500);

            IWebElement registerBtn = wait.Until(d => d.FindElement(By.LinkText("Đăng nhập")));
            registerBtn.Click();
            Thread.Sleep(500);
            driver.FindElement(By.Id("UserName")).SendKeys(data.username);
            Thread.Sleep(500);
            driver.FindElement(By.Id("Password")).SendKeys(data.password);
            Thread.Sleep(500);
            driver.FindElement(By.Id("RememberMe")).Click();
            Thread.Sleep(500);
            driver.FindElement(By.CssSelector("button[type='submit']")).Click();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            driver.Navigate().GoToUrl("https://localhost:44329");

            driver.FindElement(By.Id("searchInput")).SendKeys("Máy in");
            Thread.Sleep(500);
            driver.FindElement(By.CssSelector("button[type='submit']")).Click();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            driver.Navigate().GoToUrl("https://localhost:44329");
            Actions action = new Actions(driver);

            IWebElement menu = wait.Until(d => d.FindElement(By.CssSelector(".category-header-text")));
            action.MoveToElement(menu).Perform();
            Thread.Sleep(1500);

            var items = wait.Until(d => d.FindElements(By.CssSelector(".category-header-text + div a")));

            if (items.Count == 0)
            {
                MessageBox.Show("Không tìm thấy danh mục!");
                return;
            }
            Random rd = new Random();
            int index = rd.Next(items.Count);
            items[index].Click();

            // ================== HOẶC CHỌN ITEM THỨ 5 ==================
            // if (items.Count >= 5)
            // {
            //     items[4].Click(); // phần tử thứ 5
            // }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            var data = ReadLoginData();
            driver.Navigate().GoToUrl("https://localhost:44329");

            Actions action = new Actions(driver);
            IWebElement accountMenu = wait.Until(d => d.FindElement(By.XPath("/html/body/header/div/div/div[2]/div[1]")));
            action.MoveToElement(accountMenu).Perform();
            Thread.Sleep(1500);

            IWebElement registerBtn = wait.Until(d => d.FindElement(By.LinkText("Đăng nhập")));
            registerBtn.Click();
            Thread.Sleep(500);
            driver.FindElement(By.Id("UserName")).SendKeys(data.username);
            Thread.Sleep(500);
            driver.FindElement(By.Id("Password")).SendKeys(data.password);
            Thread.Sleep(500);
            driver.FindElement(By.Id("RememberMe")).Click();
            Thread.Sleep(500);
            driver.FindElement(By.CssSelector("button[type='submit']")).Click();

            driver.FindElement(By.Id("searchInput")).SendKeys("Gaming");
            Thread.Sleep(500);
            driver.FindElement(By.CssSelector("button[type='submit']")).Click();
            Thread.Sleep(2000);
            var products = wait.Until(d => d.FindElements(By.CssSelector(".product-card")));

            if (products.Count == 0)
            {
                MessageBox.Show("Không tìm thấy sản phẩm!");
                return;
            }

            Random rd = new Random();
            int index = rd.Next(products.Count);
            IWebElement product = products[index];

            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", product);
            Thread.Sleep(1000);
            IWebElement addBtn = product.FindElement(By.CssSelector(".add-to-cart-btn"));
            addBtn.Click();

            IAlert alert = wait.Until(d => d.SwitchTo().Alert());
            alert.Accept();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string address = txtAddress.Text;
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            var data = ReadLoginData();
            driver.Navigate().GoToUrl("https://localhost:44329");

            Actions action = new Actions(driver);
            IWebElement accountMenu = wait.Until(d => d.FindElement(By.XPath("/html/body/header/div/div/div[2]/div[1]")));
            action.MoveToElement(accountMenu).Perform();
            Thread.Sleep(1500);

            IWebElement registerBtn = wait.Until(d => d.FindElement(By.LinkText("Đăng nhập")));
            registerBtn.Click();
            Thread.Sleep(500);
            driver.FindElement(By.Id("UserName")).SendKeys(data.username);
            Thread.Sleep(500);
            driver.FindElement(By.Id("Password")).SendKeys(data.password);
            Thread.Sleep(500);
            driver.FindElement(By.Id("RememberMe")).Click();
            Thread.Sleep(500);
            driver.FindElement(By.CssSelector("button[type='submit']")).Click();

            driver.FindElement(By.Id("searchInput")).SendKeys("Gaming");
            Thread.Sleep(500);
            driver.FindElement(By.CssSelector("button[type='submit']")).Click();
            Thread.Sleep(2000);
            var products = wait.Until(d => d.FindElements(By.CssSelector(".product-card")));
            int totalProducts = products.Count;
            if (totalProducts == 0)
            {
                MessageBox.Show("Không có sản phẩm để random!");
                return;
            }
            Random rd = new Random();
            int index = rd.Next(0, totalProducts);

            IWebElement product = products[index];

            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", product);
            Thread.Sleep(1000);
            IWebElement addBtn = product.FindElement(By.CssSelector(".add-to-cart-btn"));
            addBtn.Click();

            IAlert alert = wait.Until(d => d.SwitchTo().Alert());
            alert.Accept();

            Thread.Sleep(1000);
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true); " +
                "arguments[0].click();", driver.FindElement(By.Id("cartIcon")));
            Thread.Sleep(2000);
            IWebElement checkoutBtn = wait.Until(d => d.FindElement(By.Id("checkout-btn")));
            checkoutBtn.Click();

            driver.FindElement(By.Id("shippingAddress")).SendKeys(address);
            Thread.Sleep(500);
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true); " +
                "arguments[0].click();", driver.FindElement(By.Id("ship-slow")));
            Thread.Sleep(500);
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true); " +
                "arguments[0].click();", driver.FindElement(By.Id("pay-paypal")));
            Thread.Sleep(3500);
            driver.FindElement(By.CssSelector(".btn-place-order")).Click();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            var data = ReadLoginData();
            driver.Navigate().GoToUrl("https://localhost:44329");

            Actions action = new Actions(driver);
            IWebElement accountMenu = wait.Until(d => d.FindElement(By.XPath("/html/body/header/div/div/div[2]/div[1]")));
            action.MoveToElement(accountMenu).Perform();
            Thread.Sleep(1500);

            IWebElement registerBtn = wait.Until(d => d.FindElement(By.LinkText("Đăng nhập")));
            registerBtn.Click();
            Thread.Sleep(500);
            driver.FindElement(By.Id("UserName")).SendKeys(data.username);
            Thread.Sleep(500);
            driver.FindElement(By.Id("Password")).SendKeys(data.password);
            Thread.Sleep(500);
            driver.FindElement(By.Id("RememberMe")).Click();
            Thread.Sleep(500);
            driver.FindElement(By.CssSelector("button[type='submit']")).Click();
            Thread.Sleep(500);

            IWebElement accountHistory = wait.Until(d => d.FindElement(By.XPath("/html/body/header/div/div/div[2]/div[1]")));
            action.MoveToElement(accountHistory).Perform();
            Thread.Sleep(1500);

            IWebElement historyBtn = wait.Until(d => d.FindElement(By.LinkText("Lịch sử mua hàng")));
            historyBtn.Click();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            var data = ReadLoginData();
            driver.Navigate().GoToUrl("https://localhost:44329");

            Actions action = new Actions(driver);
            IWebElement accountMenu = wait.Until(d => d.FindElement(By.XPath("/html/body/header/div/div/div[2]/div[1]")));
            action.MoveToElement(accountMenu).Perform();
            Thread.Sleep(1500);

            IWebElement registerBtn = wait.Until(d => d.FindElement(By.LinkText("Đăng nhập")));
            registerBtn.Click();

            driver.FindElement(By.Id("UserName")).SendKeys("AdminTNC");
            Thread.Sleep(500);
            driver.FindElement(By.Id("Password")).SendKeys("Hau180305#");
            Thread.Sleep(500);
            driver.FindElement(By.Id("RememberMe")).Click();
            Thread.Sleep(500);
            driver.FindElement(By.CssSelector("button[type='submit']")).Click();
            Thread.Sleep(1500);

            driver.FindElement(By.XPath("/html/body/div[1]/ul/li[2]/a")).Click();
            Thread.Sleep(500);
            driver.FindElement(By.LinkText("Thêm Sản Phẩm Mới")).Click();
            Thread.Sleep(500);
            SelectElement dropdown = new SelectElement(driver.FindElement(By.Id("CategoryID")));
            dropdown.SelectByValue("18");
            Thread.Sleep(500);
            driver.FindElement(By.Id("ProductName")).SendKeys("Máy in có màu");
            Thread.Sleep(500);
            driver.FindElement(By.Id("ProductDescription")).SendKeys("Máy in có màu bán chạy và rẻ nhất thị trường");
            Thread.Sleep(500);
            driver.FindElement(By.Id("ProductPrice")).SendKeys("100");
            Thread.Sleep(500);
            driver.FindElement(By.Id("ProductImage")).SendKeys("https://encrypted-tbn3.gstatic.com/shopping?q=tbn:ANd9GcRYRoFBCr4W6FOHL7AkziJvR-06vznLpn9Aj-Syy5KODdpxBb0ZaN7sLJqeh3h4M83Iqz6_EVNUK6jh0U2AOuE61PJWIdbDfg");
            Thread.Sleep(1500);
            driver.FindElement(By.XPath("/html/body/div[2]/div[2]/form/div/div[6]/div/input")).Click();
            Thread.Sleep(2000);

            driver.FindElement(By.LinkText("Đăng xuất")).Click();
            Thread.Sleep(500);
            driver.FindElement(By.Id("UserName")).SendKeys(data.username);
            Thread.Sleep(500);
            driver.FindElement(By.Id("Password")).SendKeys(data.password);
            Thread.Sleep(500);
            driver.FindElement(By.Id("RememberMe")).Click();
            Thread.Sleep(500);
            driver.FindElement(By.CssSelector("button[type='submit']")).Click();
            Thread.Sleep(1500);

            driver.FindElement(By.Id("searchInput")).SendKeys("Máy in");
            Thread.Sleep(500);
            driver.FindElement(By.CssSelector("button[type='submit']")).Click();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            var data = ReadLoginData();
            driver.Navigate().GoToUrl("https://localhost:44329");

            Actions action = new Actions(driver);
            IWebElement accountMenu = wait.Until(d => d.FindElement(By.XPath("/html/body/header/div/div/div[2]/div[1]")));
            action.MoveToElement(accountMenu).Perform();
            Thread.Sleep(1500);

            IWebElement registerBtn = wait.Until(d => d.FindElement(By.LinkText("Đăng nhập")));
            registerBtn.Click();

            Thread.Sleep(500);
            driver.FindElement(By.Id("UserName")).SendKeys("AdminTNC");
            Thread.Sleep(500);
            driver.FindElement(By.Id("Password")).SendKeys("Hau180305#");
            Thread.Sleep(500);
            driver.FindElement(By.Id("RememberMe")).Click();
            Thread.Sleep(500);
            driver.FindElement(By.CssSelector("button[type='submit']")).Click();
            Thread.Sleep(1500);

            driver.FindElement(By.XPath("/html/body/div[1]/ul/li[2]/a")).Click();
            Thread.Sleep(500);
            driver.FindElement(By.Id("searchTerm")).SendKeys("Máy in");
            Thread.Sleep(500);
            driver.FindElement(By.CssSelector("button[type='submit']")).Click();

            wait.Until(d => d.FindElements(By.XPath("//table/tbody/tr")).Count > 0);
            var rows = driver.FindElements(By.XPath("//table/tbody/tr"));
            IWebElement lastRow = rows[rows.Count - 1];
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", lastRow);
            Thread.Sleep(1500);
            lastRow.FindElement(By.XPath(".//a[contains(text(),'Xóa')]")).Click();
            Thread.Sleep(500);
            driver.FindElement(By.XPath("/html/body/div[2]/div[2]/form/div/input")).Click();
            Thread.Sleep(500);

            driver.FindElement(By.LinkText("Đăng xuất")).Click();
            Thread.Sleep(500);
            driver.FindElement(By.Id("UserName")).SendKeys(data.username);
            Thread.Sleep(500);
            driver.FindElement(By.Id("Password")).SendKeys(data.password);
            Thread.Sleep(500);
            driver.FindElement(By.Id("RememberMe")).Click();
            Thread.Sleep(500);
            driver.FindElement(By.CssSelector("button[type='submit']")).Click();
            Thread.Sleep(500);

            driver.FindElement(By.Id("searchInput")).SendKeys("Máy in");
            Thread.Sleep(500);
            driver.FindElement(By.CssSelector("button[type='submit']")).Click();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            var data = ReadLoginData();
            driver.Navigate().GoToUrl("https://localhost:44329");

            Actions action = new Actions(driver);
            IWebElement accountMenu = wait.Until(d => d.FindElement(By.XPath("/html/body/header/div/div/div[2]/div[1]")));
            action.MoveToElement(accountMenu).Perform();
            Thread.Sleep(1500);

            IWebElement registerBtn = wait.Until(d => d.FindElement(By.LinkText("Đăng nhập")));
            registerBtn.Click();

            Thread.Sleep(500);
            driver.FindElement(By.Id("UserName")).SendKeys("AdminTNC");
            Thread.Sleep(500);
            driver.FindElement(By.Id("Password")).SendKeys("Hau180305#");
            Thread.Sleep(500);
            driver.FindElement(By.Id("RememberMe")).Click();
            Thread.Sleep(500);
            driver.FindElement(By.CssSelector("button[type='submit']")).Click();
            Thread.Sleep(1500);

            driver.FindElement(By.XPath("/html/body/div[1]/ul/li[5]/a")).Click();
            wait.Until(d => d.FindElements(By.XPath("//table/tbody/tr")).Count > 0);
            var rows = driver.FindElements(By.XPath("//table/tbody/tr"));
            IWebElement lastRow = rows[rows.Count - 1];
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", lastRow);
            Thread.Sleep(1500);
            lastRow.FindElement(By.XPath(".//a[@title='Khóa/Mở khóa']")).Click();
            Thread.Sleep(500);
            driver.FindElement(By.Id("IsActive")).Click();
            Thread.Sleep(500);
            driver.FindElement(By.XPath("/html/body/div[2]/div[2]/form/div/div[3]/div/input")).Click();
            Thread.Sleep(500);
            driver.FindElement(By.LinkText("Đăng xuất")).Click();
            Thread.Sleep(500);
            driver.FindElement(By.Id("UserName")).SendKeys(data.username);
            Thread.Sleep(500);
            driver.FindElement(By.Id("Password")).SendKeys(data.password);
            Thread.Sleep(500);
            driver.FindElement(By.Id("RememberMe")).Click();
            Thread.Sleep(500);
            driver.FindElement(By.CssSelector("button[type='submit']")).Click();
            Thread.Sleep(500);
        }

        private void button11_Click(object sender, EventArgs e)
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            var data = ReadLoginData();
            driver.Navigate().GoToUrl("https://localhost:44329");

            Actions action = new Actions(driver);
            IWebElement accountMenu = wait.Until(d => d.FindElement(By.XPath("/html/body/header/div/div/div[2]/div[1]")));
            action.MoveToElement(accountMenu).Perform();
            Thread.Sleep(1500);

            IWebElement registerBtn = wait.Until(d => d.FindElement(By.LinkText("Đăng nhập")));
            registerBtn.Click();
            Thread.Sleep(500);
            driver.FindElement(By.Id("UserName")).SendKeys(data.username);
            Thread.Sleep(500);
            driver.FindElement(By.Id("Password")).SendKeys(data.password);
            Thread.Sleep(500);
            driver.FindElement(By.Id("RememberMe")).Click();
            Thread.Sleep(500);
            driver.FindElement(By.CssSelector("button[type='submit']")).Click();

            driver.FindElement(By.Id("searchInput")).SendKeys("Gaming");
            Thread.Sleep(500);
            driver.FindElement(By.CssSelector("button[type='submit']")).Click();
            Thread.Sleep(2000);
            var products = wait.Until(d => d.FindElements(By.CssSelector(".product-card")));
            int totalProducts = products.Count;
            if (totalProducts == 0)
            {
                MessageBox.Show("Không có sản phẩm để random!");
                return;
            }
            Random rd = new Random();
            int index = rd.Next(0, totalProducts);

            IWebElement product = products[index];
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", product);
            Thread.Sleep(1000);
            IWebElement addBtn = product.FindElement(By.CssSelector(".add-to-cart-btn"));
            addBtn.Click();
            Thread.Sleep(500);

            IAlert alert = wait.Until(d => d.SwitchTo().Alert());
            alert.Accept();

            Thread.Sleep(1000);
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true); " +
                "arguments[0].click();", driver.FindElement(By.Id("cartIcon")));
            Thread.Sleep(2000);
            IWebElement checkoutBtn = wait.Until(d => d.FindElement(By.Id("view-cart-btn")));
            checkoutBtn.Click();
            Thread.Sleep(500);
            IWebElement deleteBtn = wait.Until(d => d.FindElement(By.CssSelector(".btn-remove-item")));
            deleteBtn.Click();

            alert = wait.Until(d => d.SwitchTo().Alert());
            alert.Accept();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            var data = ReadLoginData();
            driver.Navigate().GoToUrl("https://localhost:44329");

            Actions action = new Actions(driver);
            IWebElement accountMenu = wait.Until(d => d.FindElement(By.XPath("/html/body/header/div/div/div[2]/div[1]")));
            action.MoveToElement(accountMenu).Perform();
            Thread.Sleep(1500);

            IWebElement registerBtn = wait.Until(d => d.FindElement(By.LinkText("Đăng nhập")));
            registerBtn.Click();
            Thread.Sleep(500);
            driver.FindElement(By.Id("UserName")).SendKeys(data.username);
            Thread.Sleep(500);
            driver.FindElement(By.Id("Password")).SendKeys(data.password);
            Thread.Sleep(500);
            driver.FindElement(By.Id("RememberMe")).Click();
            Thread.Sleep(500);
            driver.FindElement(By.CssSelector("button[type='submit']")).Click();
            Thread.Sleep(1000);
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true); " +
                "arguments[0].click();", driver.FindElement(By.Id("cartIcon")));
            Thread.Sleep(2000);
            IWebElement checkoutBtn = wait.Until(d => d.FindElement(By.Id("checkout-btn")));
            checkoutBtn.Click();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            var data = ReadLoginData();
            driver.Navigate().GoToUrl("https://localhost:44329");

            Actions action = new Actions(driver);
            IWebElement accountMenu = wait.Until(d => d.FindElement(By.XPath("/html/body/header/div/div/div[2]/div[1]")));
            action.MoveToElement(accountMenu).Perform();
            Thread.Sleep(1500);

            IWebElement registerBtn = wait.Until(d => d.FindElement(By.LinkText("Đăng nhập")));
            registerBtn.Click();
            Thread.Sleep(500);
            driver.FindElement(By.Id("UserName")).SendKeys(data.username);
            Thread.Sleep(500);
            driver.FindElement(By.Id("Password")).SendKeys(data.password);
            Thread.Sleep(500);
            driver.FindElement(By.Id("RememberMe")).Click();
            Thread.Sleep(500);
            driver.FindElement(By.CssSelector("button[type='submit']")).Click();

            driver.FindElement(By.Id("searchInput")).SendKeys("Gaming");
            Thread.Sleep(500);
            driver.FindElement(By.CssSelector("button[type='submit']")).Click();
            Thread.Sleep(2000);
            var products = wait.Until(d => d.FindElements(By.CssSelector(".product-card")));

            if (products.Count == 0)
            {
                MessageBox.Show("Không tìm thấy sản phẩm!");
                return;
            }

            Random rd = new Random();
            int index1 = rd.Next(products.Count);
            IWebElement product1 = products[index1];

            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", product1);
            Thread.Sleep(1000);
            IWebElement addBtn1 = product1.FindElement(By.CssSelector(".add-to-cart-btn"));
            addBtn1.Click();
            IAlert alert = wait.Until(d => d.SwitchTo().Alert());
            alert.Accept();

            int index2 = rd.Next(products.Count);
            IWebElement product2 = products[index2];

            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", product2);
            Thread.Sleep(1000);
            IWebElement addBtn2 = product2.FindElement(By.CssSelector(".add-to-cart-btn"));
            addBtn2.Click();
            alert = wait.Until(d => d.SwitchTo().Alert());
            alert.Accept();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            var data = ReadLoginData();
            driver.Navigate().GoToUrl("https://localhost:44329");

            Actions action = new Actions(driver);
            IWebElement accountMenu = wait.Until(d => d.FindElement(By.XPath("/html/body/header/div/div/div[2]/div[1]")));
            action.MoveToElement(accountMenu).Perform();
            Thread.Sleep(1500);

            IWebElement registerBtn = wait.Until(d => d.FindElement(By.LinkText("Đăng nhập")));
            registerBtn.Click();
            Thread.Sleep(500);
            driver.FindElement(By.Id("UserName")).SendKeys(data.username);
            Thread.Sleep(500);
            driver.FindElement(By.Id("Password")).SendKeys(data.password);
            Thread.Sleep(500);
            driver.FindElement(By.Id("RememberMe")).Click();
            Thread.Sleep(500);
            driver.FindElement(By.CssSelector("button[type='submit']")).Click();

            driver.FindElement(By.Id("searchInput")).SendKeys("Gaming");
            Thread.Sleep(500);
            driver.FindElement(By.CssSelector("button[type='submit']")).Click();
            Thread.Sleep(2000);
            var products = wait.Until(d => d.FindElements(By.CssSelector(".product-card")));

            if (products.Count == 0)
            {
                MessageBox.Show("Không tìm thấy sản phẩm!");
                return;
            }

            Random rd = new Random();

            int index1 = rd.Next(products.Count);
            IWebElement product1 = products[index1];
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", product1);
            Thread.Sleep(1000);
            IWebElement addBtn1 = product1.FindElement(By.CssSelector(".add-to-cart-btn"));
            addBtn1.Click();
            IAlert alert = wait.Until(d => d.SwitchTo().Alert());
            alert.Accept();

            int index2 = rd.Next(products.Count);
            IWebElement product2 = products[index2];
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", product2);
            Thread.Sleep(1000);
            IWebElement addBtn2 = product2.FindElement(By.CssSelector(".add-to-cart-btn"));
            addBtn2.Click();
            alert = wait.Until(d => d.SwitchTo().Alert());
            alert.Accept();

            int index3 = rd.Next(products.Count);
            IWebElement product3 = products[index3];
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", product3);
            Thread.Sleep(1000);
            IWebElement addBtn3 = product3.FindElement(By.CssSelector(".add-to-cart-btn"));
            addBtn3.Click();
            alert = wait.Until(d => d.SwitchTo().Alert());
            alert.Accept();

            Thread.Sleep(1000);
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true); " +
                "arguments[0].click();", driver.FindElement(By.Id("cartIcon")));
            Thread.Sleep(2000);
            IWebElement checkoutBtn = wait.Until(d => d.FindElement(By.Id("view-cart-btn")));
            checkoutBtn.Click();

            var cartItems1 = wait.Until(d => d.FindElements(By.CssSelector(".cart-item-card")));
            int index4 = rd.Next(cartItems1.Count);
            IWebElement selectedItem1 = cartItems1[index4];
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView({block: 'center'});", selectedItem1);
            IWebElement plusBtn1 = selectedItem1.FindElement(By.CssSelector(".plus-btn"));
            Thread.Sleep(1000);
            plusBtn1.Click();

            var cartItems2 = wait.Until(d => d.FindElements(By.CssSelector(".cart-item-card")));
            int index5 = rd.Next(cartItems2.Count);
            IWebElement selectedItem2 = cartItems2[index5];
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView({block: 'center'});", selectedItem2);
            IWebElement plusBtn2 = selectedItem2.FindElement(By.CssSelector(".plus-btn"));
            Thread.Sleep(1000);
            plusBtn2.Click();
        }

            private void button15_Click(object sender, EventArgs e)
            {
                driver = new ChromeDriver();
                driver.Manage().Window.Maximize();
                wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                var data = ReadLoginData();
                driver.Navigate().GoToUrl("https://localhost:44329");

                Actions action = new Actions(driver);
                IWebElement accountMenu = wait.Until(d => d.FindElement(By.XPath("/html/body/header/div/div/div[2]/div[1]")));
                action.MoveToElement(accountMenu).Perform();
                Thread.Sleep(1500);

                IWebElement registerBtn = wait.Until(d => d.FindElement(By.LinkText("Đăng nhập")));
                registerBtn.Click();

                driver.FindElement(By.Id("UserName")).SendKeys("AdminTNC");
                Thread.Sleep(500);
                driver.FindElement(By.Id("Password")).SendKeys("Hau180305#");
                Thread.Sleep(500);
                driver.FindElement(By.Id("RememberMe")).Click();
                Thread.Sleep(500);
                driver.FindElement(By.CssSelector("button[type='submit']")).Click();
                Thread.Sleep(1500);
                driver.FindElement(By.XPath("/html/body/div[1]/ul/li[2]/a")).Click();
                Thread.Sleep(500);

                Random rd = new Random();
                var rows = wait.Until(d => d.FindElements(By.XPath("//tbody/tr")));
                int index = rd.Next(rows.Count);
                IWebElement selectedRow = rows[index];
                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView({block: 'center'});", selectedRow);
                IWebElement editBtn = selectedRow.FindElement(By.XPath(".//a[contains(@href,'Edit')]"));
                Thread.Sleep(1000);
                editBtn.Click();

                Thread.Sleep(500);
                driver.FindElement(By.Id("ProductPrice")).Clear();
                Thread.Sleep(500);
                driver.FindElement(By.Id("ProductPrice")).SendKeys("100000");
                Thread.Sleep(500);
                driver.FindElement(By.CssSelector("input[type='submit']")).Click();
            }

            private void button16_Click(object sender, EventArgs e)
            {
                driver = new ChromeDriver();
                driver.Manage().Window.Maximize();
                wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                //var data = ReadLoginData();
                driver.Navigate().GoToUrl("https://localhost:44329");

                Actions action = new Actions(driver);
                IWebElement accountMenu = wait.Until(d => d.FindElement(By.XPath("/html/body/header/div/div/div[2]/div[1]")));
                action.MoveToElement(accountMenu).Perform();
                Thread.Sleep(1500);

                IWebElement registerBtn = wait.Until(d => d.FindElement(By.LinkText("Đăng nhập")));
                registerBtn.Click();

                driver.FindElement(By.Id("UserName")).SendKeys("AdminTNC");
                Thread.Sleep(500);
                driver.FindElement(By.Id("Password")).SendKeys("Hau180305#");
                Thread.Sleep(500);
                driver.FindElement(By.Id("RememberMe")).Click();
                Thread.Sleep(500);
                driver.FindElement(By.CssSelector("button[type='submit']")).Click();
                Thread.Sleep(1500);
                driver.FindElement(By.XPath("/html/body/div[1]/ul/li[3]/a")).Click();
                Thread.Sleep(500);
                driver.FindElement(By.LinkText("Thêm Danh Mục Mới")).Click();
                Thread.Sleep(500);
                driver.FindElement(By.Id("CategoryName")).SendKeys("Toilet");
                Thread.Sleep(500);
                driver.FindElement(By.XPath("//input[@value='Lưu']")).Click();
            }

            private void button17_Click(object sender, EventArgs e)
            {
                driver = new ChromeDriver();
                driver.Manage().Window.Maximize();
                wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                driver.Navigate().GoToUrl("https://localhost:44329");

                Actions action = new Actions(driver);
                IWebElement accountMenu = wait.Until(d => d.FindElement(By.XPath("/html/body/header/div/div/div[2]/div[1]")));
                action.MoveToElement(accountMenu).Perform();
                Thread.Sleep(1500);

                IWebElement registerBtn = wait.Until(d => d.FindElement(By.LinkText("Đăng nhập")));
                registerBtn.Click();

                driver.FindElement(By.Id("UserName")).SendKeys("AdminTNC");
                Thread.Sleep(500);
                driver.FindElement(By.Id("Password")).SendKeys("Hau180305#");
                Thread.Sleep(500);
                driver.FindElement(By.Id("RememberMe")).Click();
                Thread.Sleep(500);
                driver.FindElement(By.CssSelector("button[type='submit']")).Click();
                Thread.Sleep(1500);
                driver.FindElement(By.XPath("/html/body/div[1]/ul/li[3]/a")).Click();
                Thread.Sleep(500);

                var rows = wait.Until(d => d.FindElements(By.XPath("//tbody/tr")));
                IWebElement lastRow = rows[rows.Count - 1];
                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView({block: 'center'});", lastRow);
                IWebElement deleteBtn = lastRow.FindElement(By.XPath(".//a[contains(@href,'Delete')]"));
                Thread.Sleep(500);
                deleteBtn.Click();
                Thread.Sleep(500);
                driver.FindElement(By.CssSelector("button[type='submit']")).Click();
            }

            private void button18_Click(object sender, EventArgs e)
            {
                driver = new ChromeDriver();
                driver.Manage().Window.Maximize();
                wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                driver.Navigate().GoToUrl("https://localhost:44329");

                Actions action = new Actions(driver);
                IWebElement accountMenu = wait.Until(d => d.FindElement(By.XPath("/html/body/header/div/div/div[2]/div[1]")));
                action.MoveToElement(accountMenu).Perform();
                Thread.Sleep(1500);

                IWebElement registerBtn = wait.Until(d => d.FindElement(By.LinkText("Đăng nhập")));
                registerBtn.Click();

                driver.FindElement(By.Id("UserName")).SendKeys("AdminTNC");
                Thread.Sleep(500);
                driver.FindElement(By.Id("Password")).SendKeys("Hau180305#");
                Thread.Sleep(500);
                driver.FindElement(By.Id("RememberMe")).Click();
                Thread.Sleep(500);
                driver.FindElement(By.CssSelector("button[type='submit']")).Click();
                Thread.Sleep(1500);
                driver.FindElement(By.XPath("/html/body/div[1]/ul/li[4]/a")).Click();
                Thread.Sleep(500);

                var rows = wait.Until(d => d.FindElements(By.XPath("//tbody/tr")));
                var validRows = rows.Where(row =>
                {
                    string status = row.FindElement(By.XPath("./td[6]")).Text.Trim();
                    return !status.Contains("Đã duyệt");
                }).ToList();
                if (validRows.Count > 0)
                {
                    Random rd = new Random();
                    int index = rd.Next(validRows.Count);
                    IWebElement selectedRow = validRows[index];
                    ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView({block:'center'});", selectedRow);
                    Thread.Sleep(500);
                    IWebElement processBtn = selectedRow.FindElement(By.XPath(".//a[contains(@href,'Process')]"));
                    processBtn.Click();
                }
                else
                {
                    Console.WriteLine("Không có đơn nào để xử lý!");
                }
                Thread.Sleep(500);
                driver.FindElement(By.XPath("//button[@value='Approve']")).Click();
                Thread.Sleep(500);
                IAlert alert = wait.Until(d => d.SwitchTo().Alert());
                alert.Accept();
            }

            private void button19_Click(object sender, EventArgs e)
            {
                driver = new ChromeDriver();
                driver.Manage().Window.Maximize();
                wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                driver.Navigate().GoToUrl("https://localhost:44329");

                Actions action = new Actions(driver);
                IWebElement accountMenu = wait.Until(d => d.FindElement(By.XPath("/html/body/header/div/div/div[2]/div[1]")));
                action.MoveToElement(accountMenu).Perform();
                Thread.Sleep(1500);

                IWebElement registerBtn = wait.Until(d => d.FindElement(By.LinkText("Đăng nhập")));
                registerBtn.Click();

                driver.FindElement(By.Id("UserName")).SendKeys("AdminTNC");
                Thread.Sleep(500);
                driver.FindElement(By.Id("Password")).SendKeys("Hau180305#");
                Thread.Sleep(500);
                driver.FindElement(By.Id("RememberMe")).Click();
                Thread.Sleep(500);
                driver.FindElement(By.CssSelector("button[type='submit']")).Click();
                Thread.Sleep(1500);
                driver.FindElement(By.XPath("/html/body/div[1]/ul/li[4]/a")).Click();
                Thread.Sleep(500);

                var rows = wait.Until(d => d.FindElements(By.XPath("//tbody/tr")));
                var validRows = rows.Where(row =>
                {
                    string status = row.FindElement(By.XPath("./td[6]")).Text.Trim();
                    return !status.Contains("Đã hủy");
                }).ToList();
                if (validRows.Count > 0)
                {
                    Random rd = new Random();
                    int index = rd.Next(validRows.Count);
                    IWebElement selectedRow = validRows[index];
                    ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView({block:'center'});", selectedRow);
                    Thread.Sleep(500);
                    IWebElement processBtn = selectedRow.FindElement(By.XPath(".//a[contains(@href,'Process')]"));
                    processBtn.Click();
                }
                else
                {
                    Console.WriteLine("Không có đơn nào để xử lý!");
                }
                Thread.Sleep(500);
                driver.FindElement(By.XPath("//button[@value='Cancel']")).Click();
                Thread.Sleep(500);
                IAlert alert = wait.Until(d => d.SwitchTo().Alert());
                alert.Accept();
            }

        private void button20_Click(object sender, EventArgs e)
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            var data = ReadLoginData();
            driver.Navigate().GoToUrl("https://localhost:44329");

            Actions action = new Actions(driver);
            IWebElement accountMenu = wait.Until(d => d.FindElement(By.XPath("/html/body/header/div/div/div[2]/div[1]")));
            action.MoveToElement(accountMenu).Perform();
            Thread.Sleep(1500);

            IWebElement registerBtn = wait.Until(d => d.FindElement(By.LinkText("Đăng nhập")));
            registerBtn.Click();

            driver.FindElement(By.Id("UserName")).SendKeys("AdminTNC");
            Thread.Sleep(500);
            driver.FindElement(By.Id("Password")).SendKeys("Hau180305#");
            Thread.Sleep(500);
            driver.FindElement(By.Id("RememberMe")).Click();
            Thread.Sleep(500);
            driver.FindElement(By.CssSelector("button[type='submit']")).Click();
            Thread.Sleep(1500);
            driver.FindElement(By.XPath("/html/body/div[1]/ul/li[2]/a")).Click();
            Thread.Sleep(500);

            Random rd = new Random();
            var rows = wait.Until(d => d.FindElements(By.XPath("//tbody/tr")));
            int index = rd.Next(rows.Count);
            IWebElement selectedRow = rows[index];
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView({block: 'center'});", selectedRow);
            IWebElement editBtn = selectedRow.FindElement(By.XPath(".//a[contains(@href,'Edit')]"));
            Thread.Sleep(1000);
            editBtn.Click();

            Thread.Sleep(500);
            driver.FindElement(By.Id("ProductPrice")).Clear();
            Thread.Sleep(500);
            driver.FindElement(By.Id("ProductPrice")).SendKeys("-100000");
            Thread.Sleep(500);
            driver.FindElement(By.CssSelector("input[type='submit']")).Click();
        }
    }
}
