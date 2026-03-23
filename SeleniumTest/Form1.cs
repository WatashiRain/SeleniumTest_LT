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

        private void button1_Click(object sender, EventArgs e)
        {
            {
                ChromeOptions options = new ChromeOptions();
                IWebDriver driver = new ChromeDriver(options);
                driver.Manage().Window.Maximize();

                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

                string baseUrl = "https://localhost:44329";

                try
                {
                    driver.Navigate().GoToUrl(baseUrl);

                    // ================== 1. HOVER → CLICK REGISTER ==================
                    Actions action = new Actions(driver);

                    // Hover vào menu tài khoản (cậu sửa lại selector nếu khác)
                    IWebElement accountMenu = wait.Until(d => d.FindElement(By.XPath("/html/body/header/div/div/div[2]/div[1]")));
                    action.MoveToElement(accountMenu).Perform();
                    Thread.Sleep(1500);

                    // Click đăng ký trong dropdown
                    IWebElement registerBtn = wait.Until(d => d.FindElement(By.LinkText("Đăng ký")));
                    registerBtn.Click();

                    // ================== REGISTER ==================
                    string code = new Random().Next(1000, 10000).ToString();
                    string phoneGen = "09" + new Random().Next(10000000, 100000000).ToString();
                    string username = "Chun" + code;
                    string password = "La@1234532";
                    string address = "HUFLIT NEVER DIE BABYYYYYY";
                    wait.Until(d => d.FindElement(By.Id("CustomerName"))).SendKeys("Phạm Tăng Thiên Bảo");
                    driver.FindElement(By.Id("CustomerPhone")).SendKeys(phoneGen);
                    driver.FindElement(By.Id("CustomerEmail")).SendKeys(username + "@gmail.com");
                    driver.FindElement(By.Id("CustomerAddress")).SendKeys(address);
                    driver.FindElement(By.Id("Username")).SendKeys(username);
                    driver.FindElement(By.Id("Password")).SendKeys(password);
                    driver.FindElement(By.Id("ConfirmPassword")).SendKeys(password);

                    ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true); " +
                        "arguments[0].click();", driver.FindElement(By.CssSelector("button[type='submit']")));

                    Thread.Sleep(3500);

                    // ================== 2. LOGIN ==================                 
                    driver.FindElement(By.Id("UserName")).SendKeys(username);
                    driver.FindElement(By.Id("Password")).SendKeys(password);
                    driver.FindElement(By.Id("RememberMe")).Click();
                    driver.FindElement(By.CssSelector("button[type='submit']")).Click();
                    Thread.Sleep(3500);

                    // ================== 3.SEARCH ==================
                    driver.FindElement(By.Id("searchInput")).SendKeys("Máy in");
                    driver.FindElement(By.CssSelector("button[type='submit']")).Click();

                    Thread.Sleep(3500);

                    // ================== 4.FILTER ==================
                    IWebElement menu = wait.Until(d => d.FindElement(By.XPath("//div[contains(@class,'category-header-text')]")));
                    action.MoveToElement(menu).Perform();
                    Thread.Sleep(1500);
                    IWebElement item = wait.Until(d => d.FindElement(By.XPath("//a[contains(text(),'Tay Cầm Chơi Game')]")));
                    item.Click();

                    //// ================== 5. ADD TO CART ==================
                    ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true); " +
                        "arguments[0].click();", driver.FindElement(By.XPath("/html/body/div/div/div/div[2]/div[2]/div[4]/div/div/button")));                   
                    IAlert alert = wait.Until(d => d.SwitchTo().Alert());
                    alert.Accept();
                    Thread.Sleep(3500);

                    //// ================== 6. CHECKOUT ==================
                    ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true); " +
                        "arguments[0].click();", driver.FindElement(By.Id("cartIcon")));
                    Thread.Sleep(3500);
                    ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true); " +
                        "arguments[0].click();", driver.FindElement(By.Id("checkout-btn")));
                    Thread.Sleep(3500);
                    driver.FindElement(By.Id("shippingAddress")).SendKeys(address);
                    ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true); " +
                        "arguments[0].click();", driver.FindElement(By.XPath("/html/body/div/div/form/div/div[1]/div/div[2]/div[2]/label")));
                    ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true); " +
                        "arguments[0].click();", driver.FindElement(By.XPath("/html/body/div/div/form/div/div[1]/div/div[3]/div[2]/label")));
                    driver.FindElement(By.XPath("/html/body/div/div/form/div/div[2]/div/div[3]/input")).Click();
                    Thread.Sleep(3500);
                    //// ================== 7. ORDER HISTORY ==================
                    ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true); " +
                        "arguments[0].click();", driver.FindElement(By.XPath("/html/body/div/div/div/div[2]/a[1]")));

                    IWebElement accountHistory = wait.Until(d => d.FindElement(By.XPath("/html/body/header/div/div/div[2]/div[1]/i")));
                    action.MoveToElement(accountHistory).Perform();
                    Thread.Sleep(1500);

                    IWebElement historyBtn = wait.Until(d => d.FindElement(By.LinkText("Lịch sử mua hàng")));
                    historyBtn.Click();
                    Thread.Sleep(3500);
                    // ================== 8. ADD PRODUCT ==================
                    driver.FindElement(By.XPath("/html/body/header/div/div/div[2]/div[1]")).Click();
                    Thread.Sleep(1500);

                    IWebElement logoutBtn1 = wait.Until(d => d.FindElement(By.XPath("/html/body/header/div/div/div[2]/div[1]/div/a[2]")));
                    logoutBtn1.Click();
                    Thread.Sleep(3500);

                    driver.FindElement(By.Id("UserName")).SendKeys("AdminTNC");
                    driver.FindElement(By.Id("Password")).SendKeys("Hau180305#");
                    driver.FindElement(By.Id("RememberMe")).Click();
                    driver.FindElement(By.CssSelector("button[type='submit']")).Click();
                    Thread.Sleep(3500);

                    driver.FindElement(By.XPath("/html/body/div[1]/ul/li[2]/a")).Click();
                    driver.FindElement(By.LinkText("Thêm Sản Phẩm Mới")).Click();
                    SelectElement dropdown = new SelectElement(driver.FindElement(By.Id("CategoryID")));
                    dropdown.SelectByValue("18");
                    driver.FindElement(By.Id("ProductName")).SendKeys("Máy in có màu");
                    driver.FindElement(By.Id("ProductDescription")).SendKeys("Máy in có màu bán chạy và rẻ nhất thị trường");
                    driver.FindElement(By.Id("ProductPrice")).SendKeys("100");
                    driver.FindElement(By.Id("ProductImage")).SendKeys("https://encrypted-tbn3.gstatic.com/shopping?q=tbn:ANd9GcRYRoFBCr4W6FOHL7AkziJvR-06vznLpn9Aj-Syy5KODdpxBb0ZaN7sLJqeh3h4M83Iqz6_EVNUK6jh0U2AOuE61PJWIdbDfg");
                    Thread.Sleep(1500);
                    driver.FindElement(By.XPath("/html/body/div[2]/div[2]/form/div/div[6]/div/input")).Click();
                    Thread.Sleep(3500);

                    driver.FindElement(By.LinkText("Đăng xuất")).Click();
                    driver.FindElement(By.Id("UserName")).SendKeys(username);
                    driver.FindElement(By.Id("Password")).SendKeys(password);
                    driver.FindElement(By.Id("RememberMe")).Click();
                    driver.FindElement(By.CssSelector("button[type='submit']")).Click();
                    Thread.Sleep(1500);

                    driver.FindElement(By.Id("searchInput")).SendKeys("Máy in");
                    driver.FindElement(By.CssSelector("button[type='submit']")).Click();
                    Thread.Sleep(3500);
                    // ================== 9. DELETE PRODUCT ==================
                    driver.FindElement(By.XPath("/html/body/header/div/div/div[2]/div[1]")).Click();
                    Thread.Sleep(1500);

                    IWebElement logoutBtn2 = wait.Until(d => d.FindElement(By.XPath("/html/body/header/div/div/div[2]/div[1]/div/a[2]")));
                    logoutBtn2.Click();
                    Thread.Sleep(3500);

                    driver.FindElement(By.Id("UserName")).SendKeys("AdminTNC");
                    driver.FindElement(By.Id("Password")).SendKeys("Hau180305#");
                    driver.FindElement(By.Id("RememberMe")).Click();
                    driver.FindElement(By.CssSelector("button[type='submit']")).Click();
                    Thread.Sleep(2500);

                    driver.FindElement(By.XPath("/html/body/div[1]/ul/li[2]/a")).Click();
                    driver.FindElement(By.Id("searchTerm")).SendKeys("Máy in");
                    driver.FindElement(By.CssSelector("button[type='submit']")).Click();

                    wait.Until(d => d.FindElements(By.XPath("//table/tbody/tr")).Count > 0);
                    var rows1 = driver.FindElements(By.XPath("//table/tbody/tr"));
                    IWebElement lastRow1 = rows1[rows1.Count - 1];
                    ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", lastRow1);
                    Thread.Sleep(1500);
                    lastRow1.FindElement(By.XPath(".//a[contains(text(),'Xóa')]")).Click();
                    driver.FindElement(By.XPath("/html/body/div[2]/div[2]/form/div/input")).Click();
                    Thread.Sleep(3500);

                    driver.FindElement(By.LinkText("Đăng xuất")).Click();
                    driver.FindElement(By.Id("UserName")).SendKeys(username);
                    driver.FindElement(By.Id("Password")).SendKeys(password);
                    driver.FindElement(By.Id("RememberMe")).Click();
                    driver.FindElement(By.CssSelector("button[type='submit']")).Click();
                    Thread.Sleep(1500);

                    driver.FindElement(By.XPath("//*[@id=\"searchInput\"]")).Click();
                    driver.FindElement(By.Id("searchInput")).SendKeys("Máy in");
                    driver.FindElement(By.CssSelector("button[type='submit']")).Click();
                    Thread.Sleep(3500);

                    // ================== 10. ADMIN BAN USER ==================
                    driver.FindElement(By.XPath("/html/body/header/div/div/div[2]/div[1]/i")).Click();
                    Thread.Sleep(1500);

                    IWebElement logoutBtn3 = wait.Until(d => d.FindElement(By.XPath("/html/body/header/div/div/div[2]/div[1]/div/a[2]")));
                    logoutBtn3.Click();
                    Thread.Sleep(3500);

                    driver.FindElement(By.Id("UserName")).SendKeys("AdminTNC");
                    driver.FindElement(By.Id("Password")).SendKeys("Hau180305#");
                    driver.FindElement(By.Id("RememberMe")).Click();
                    driver.FindElement(By.CssSelector("button[type='submit']")).Click();
                    Thread.Sleep(2500);

                    driver.FindElement(By.XPath("/html/body/div[1]/ul/li[5]/a")).Click();
                    wait.Until(d => d.FindElements(By.XPath("//table/tbody/tr")).Count > 0);
                    var rows2 = driver.FindElements(By.XPath("//table/tbody/tr"));
                    IWebElement lastRow2 = rows2[rows2.Count - 1];
                    ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", lastRow2);
                    Thread.Sleep(1500);
                    lastRow2.FindElement(By.XPath(".//a[@title='Khóa/Mở khóa']")).Click();
                    driver.FindElement(By.Id("IsActive")).Click();
                    driver.FindElement(By.XPath("/html/body/div[2]/div[2]/form/div/div[3]/div/input")).Click();
                    driver.FindElement(By.LinkText("Đăng xuất")).Click();
                    Thread.Sleep(2000);
                    driver.FindElement(By.Id("UserName")).SendKeys(username);
                    driver.FindElement(By.Id("Password")).SendKeys(password);
                    driver.FindElement(By.Id("RememberMe")).Click();
                    driver.FindElement(By.CssSelector("button[type='submit']")).Click();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("❌ Lỗi: " + ex.Message);
                }

            }
        }
    }
}
