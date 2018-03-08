using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class CreateMerchantTypeAdmin : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Admin")]
        [TestCategory("TS6")]
        [TestCategory("Pegasus_Admin")]
        public void createMerchantTypeAdmin()
        {
            string[] username = null;
            string[] password = null;

            XMLParse oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            username = oXMLData.getData("settings/Credentials", "username_office");
            password = oXMLData.getData("settings/Credentials", "password");

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var masterData_MerchantTypeHelper = new MasterData_MerchantTypeHelper(GetWebDriver());

            // Variable
            String name = "Test" + GetRandomNumber();
            String JIRA = "";
            String Status = "Pass";

            try
            {

                executionLog.Log("CreateMerchantTypeAdmin", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("CreateMerchantTypeAdmin", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("CreateMerchantTypeAdmin", "Redirect To URL");
                VisitOffice("merchant_types");
                masterData_MerchantTypeHelper.WaitForWorkAround(5000);

                executionLog.Log("CreateMerchantTypeAdmin", "Verify title");
                VerifyTitle("Master Merchant Types");

                executionLog.Log("CreateMerchantTypeAdmin", " Click On Create");
                masterData_MerchantTypeHelper.ClickElement("Create");
                masterData_MerchantTypeHelper.WaitForWorkAround(5000);

                executionLog.Log("CreateMerchantTypeAdmin", "Verify title");
                VerifyTitle("Manage Master Merchant Types");

                executionLog.Log("CreateMerchantTypeAdmin", "Enter Merchant type");
                masterData_MerchantTypeHelper.TypeText("MerchantType", name);

                executionLog.Log("CreateMerchantTypeAdmin", "  Click on Save button");
                masterData_MerchantTypeHelper.ClickElement("Save");
                masterData_MerchantTypeHelper.WaitForWorkAround(2000);

                executionLog.Log("CreateMerchantTypeAdmin", "Wait for text");
                masterData_MerchantTypeHelper.WaitForText("The merchant type is successfully created!!", 30);
                masterData_MerchantTypeHelper.WaitForWorkAround(1000);

                executionLog.Log("CreateMerchantTypeAdmin", "Redirect To URL");
                VisitOffice("merchant_types");
                masterData_MerchantTypeHelper.WaitForWorkAround(5000);

                executionLog.Log("CreateMerchantTypeAdmin", "Verify title");
                VerifyTitle("Master Merchant Types");
                masterData_MerchantTypeHelper.WaitForWorkAround(1000);

                executionLog.Log("CreateMerchantTypeAdmin", "Enter merchant Name to search");
                masterData_MerchantTypeHelper.TypeText("SearchMerchanttype", name);
                masterData_MerchantTypeHelper.WaitForWorkAround(4000);

                executionLog.Log("CreateMerchantTypeAdmin", "Click Delete btn  ");
                masterData_MerchantTypeHelper.ClickElement("DeleteIcon");
                masterData_MerchantTypeHelper.WaitForWorkAround(2000);

                executionLog.Log("CreateMerchantTypeAdmin", "Accept alert message. ");
                masterData_MerchantTypeHelper.AcceptAlert();
                masterData_MerchantTypeHelper.WaitForWorkAround(2000);

                executionLog.Log("CreateMerchantTypeAdmin", "Wait for delete message. ");
                masterData_MerchantTypeHelper.WaitForText("The merchant type is successfully deleted!!", 10);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("CreateMerchantTypeAdmin");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Create Merchant Type Admin");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Create Merchant Type Admin", "Bug", "Medium", "Create Merchant Type page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Create Merchant Type Admin");
                        TakeScreenshot("CreateMerchantTypeAdmin");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\CreateMerchantTypeAdmin.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("CreateMerchantTypeAdmin");
                        string id = loginHelper.getIssueID("Create Merchant Type Admin");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\CreateMerchantTypeAdmin.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Create Merchant Type Admin"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Create Merchant Type Admin");
             //   executionLog.DeleteFile("Error");
                throw;
            }
            finally
            {
                executionLog.DeleteFile("CreateMerchantTypeAdmin");
                executionLog.WriteInExcel("Create Merchant Type Admin", Status, JIRA, "Office MasterData ");
            }
        }
    }
}