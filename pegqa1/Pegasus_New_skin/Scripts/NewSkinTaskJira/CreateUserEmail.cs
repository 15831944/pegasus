using System;
using System.IO;
using LinqToExcel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class CreateUserEmail : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin_Task")]
        [TestCategory("Fail")]
        [TestCategory("TS4")]
        [TestCategory("NewSkinTaskJira")]
        public void createUserEmail()
        {
            string[] username = null;
            string[] password = null;
            string[] log = null;
            var oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            username = oXMLData.getData("settings/Credentials", "username_office");
            password = oXMLData.getData("settings/Credentials", "password");
            log = oXMLData.getData("settings/URL", "logout");


            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var office_UserHelper = new Office_UserHelper(GetWebDriver());
            var yopMailHelper = new YopMailHelper(GetWebDriver());

            // Variables.
            var name = "User" + RandomNumber(1, 999);
            String JIRA = "";
            String Status = "Pass";

            try
            {
            executionLog.Log("CreateUserEmail", "Login with valid username and password");
            Login(username[0], password[0]);
            Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

            executionLog.Log("CreateUserEmail", "Verify Page title");
            VerifyTitle("Dashboard");
            Console.WriteLine("Redirected at Dashboard screen.");

            executionLog.Log("CreateUserEmail", "Go to Create user page");
            VisitOffice("users/create");
            office_UserHelper.WaitForWorkAround(3000);

            executionLog.Log("CreateUserEmail", "Verify title");
            VerifyTitle("Create User");

            executionLog.Log("CreateUserEmail", "Select parnter association as type");
            office_UserHelper.SelectByText("Usertype", "Partner Association");

            executionLog.Log("CreateUserEmail", "Click on Create new");
            office_UserHelper.ClickElement("CreateNew");
            office_UserHelper.WaitForWorkAround(1000);

            executionLog.Log("CreateUserEmail", "Click on Auto generte checkbox");
            office_UserHelper.ClickElement("AutogenerateChk");
            office_UserHelper.WaitForWorkAround(1000);

            executionLog.Log("CreateUserEmail", "Enter Username");
            office_UserHelper.TypeText("UserName", name);

            executionLog.Log("CreateUserEmail", "Enter password");
            office_UserHelper.TypeText("UserPassword", "123456789");

            executionLog.Log("CreateUserEmail", "Enter First name");
            office_UserHelper.TypeText("FirstName", name + " First");

            executionLog.Log("CreateUserEmail", "Enter Last name");
            office_UserHelper.TypeText("LastName", name + " Last");

            executionLog.Log("CreateUserEmail", "Enter email");
            office_UserHelper.TypeText("PrimaryEmail", name + "@yopmail.com");

            executionLog.Log("CreateUserEmail", "Click on Avatar");
            office_UserHelper.ClickElement("AvtarPartnerAssociation");

            executionLog.Log("CreateUserEmail", "Enter Birthdate");
            office_UserHelper.TypeText("PAssBirthdate", "1989-10-15");

            executionLog.Log("CreateUserEmail", "Click on Save button");
            office_UserHelper.ClickElement("Save1099");

            executionLog.Log("CreateUserEmail", "Wait for confirmation message");
            office_UserHelper.WaitForText("The user is successfully added", 10);

            executionLog.Log("CreateUserEmail", "Log out from the application");
            VisitOffice("logout");
            office_UserHelper.WaitForWorkAround(3000);

            executionLog.Log("CreateUserEmail", "Verify title");
            VerifyTitle("Login");
            //office_UserHelper.WaitForWorkAround(5000);

            executionLog.Log("CreateUserEmail", "Go to yopmail");
            GetWebDriver().Navigate().GoToUrl("http://www.yopmail.com/en/");
            office_UserHelper.WaitForWorkAround(3000);

            executionLog.Log("CreateUserEmail", "Verify Title");
            VerifyTitle("YOPmail");
            //office_UserHelper.WaitForWorkAround(5000);

            executionLog.Log("CreateUserEmail", "Enter username");
            yopMailHelper.TypeText("Yopmail", name);

            executionLog.Log("CreateUserEmail", "Click on Button");
            yopMailHelper.ClickElement("YopmailClick");
            office_UserHelper.WaitForWorkAround(4000);

            executionLog.Log("CreateUserEmail", "Switch frame");
            yopMailHelper.switchFrame("ifinbox");
            //yopMailHelper.WaitForWorkAround(3000);

            executionLog.Log("CreateUserEmail", "Click on Email");
            yopMailHelper.ClickElement("UserYop");
            office_UserHelper.WaitForWorkAround(3000);

            executionLog.Log("CreateUserEmail", "Out of the fame");
            yopMailHelper.outFrame();
            //office_UserHelper.WaitForWorkAround(3000);

            executionLog.Log("CreateUserEmail", "Switch frame");
            yopMailHelper.switchFrame("ifmail");
            //office_UserHelper.WaitForWorkAround(3000);

            executionLog.Log("CreateUserEmail", "Click on Link");
            yopMailHelper.ClickElement("OfficeLink");
            office_UserHelper.WaitForWorkAround(3000);

            executionLog.Log("CreateUserEmail", "Switch window");
            yopMailHelper.SwitchNewWindow("Login");
            //office_UserHelper.WaitForWorkAround(3000);

            executionLog.Log("CreateUserEmail", "Verify Title");
            VerifyTitle("Login");

            executionLog.Log("CreateUserEmail", "Verify activated text available");
            office_UserHelper.WaitForText("Thank you, your account is activated now", 10);

            executionLog.Log("CreateUserEmail", "Login with new user");
            Login(name, "123456789");
            office_UserHelper.WaitForWorkAround(3000);

            executionLog.Log("CreateUserEmail", "Click on Edit details button");
            office_UserHelper.ClickElement("EditDetails");
            office_UserHelper.WaitForWorkAround(3000);

            executionLog.Log("CreateUserEmail", "Verify Primary email is available in the field");
            string text = office_UserHelper.GetAtrributeByLocator("//*[@id='PartnerAssociationElectronicAddress0ElectronicContent']", "value");
            Assert.IsTrue(text.Contains(name + "@yopmail.com"));

            VisitOffice("logout");

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("CreateUserEmail");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Create User Email");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Create User Email", "Bug", "Medium", "User page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Create User Email");
                        TakeScreenshot("CreateUserEmail");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\CreateUserEmail.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("CreateUserEmail");
                        string id = loginHelper.getIssueID("Create User Email");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\CreateUserEmail.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Create User Email"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Create User Email");
                // executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("CreateUserEmail");
                executionLog.WriteInExcel("Create User Email", Status, JIRA, "Office Admin");
            }
        }
    }
} 