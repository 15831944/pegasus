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
    public class CreatePricingPlanAdmin : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Admin")]
        [TestCategory("Temp")]
        [TestCategory("TS6")]
        [TestCategory("Pegasus_Admin")]
        public void createPricingPlanAdmin()
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
            var masterData_PricingPlanHelper = new MasterData_PricingPlanHelper(GetWebDriver());

            // Variable
            var name = "Price" + RandomNumber(1, 9999);
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("CreatePricingPlanAdmin", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("CreatePricingPlanAdmin", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("CreatePricingPlanAdmin", "Click On  Admin");
                VisitOffice("admin");

                executionLog.Log("CreatePricingPlanAdmin", "Redirect To Pricing Plan");
                VisitOffice("pricing_plans");

                executionLog.Log("CreatePricingPlanAdmin", "Verify title");
                VerifyTitle("Master Pricing Plans");

                executionLog.Log("CreatePricingPlanAdmin", " Click On Create");
                masterData_PricingPlanHelper.ClickElement("Create");

                executionLog.Log("CreatePricingPlanAdmin", "Verify title");
                VerifyTitle("Manage Master Pricing Plans");

                executionLog.Log("CreatePricingPlanAdmin", "Enter Pricing Plan");
                masterData_PricingPlanHelper.TypeText("PricingPlan", name);

                executionLog.Log("CreatePricingPlanAdmin", " Select Processor Name");
                masterData_PricingPlanHelper.Selectbytext("Processor", "First Data Omaha");

                executionLog.Log("CreatePricingPlanAdmin", "  Click on Save button");
                masterData_PricingPlanHelper.ClickElement("Save");

                executionLog.Log("CreatePricingPlanAdmin", "Wait for Confirmation");
                masterData_PricingPlanHelper.WaitForText("The pricing plan is successfully created!!", 20);

                executionLog.Log("CreatePricingPlanAdmin", "Redirect To Pricing Plan");
                VisitOffice("pricing_plans");               

                executionLog.Log("CreatePricingPlanAdmin", "Enter Name to search");
                masterData_PricingPlanHelper.TypeText("SearchPricingPlan", name);
                masterData_PricingPlanHelper.WaitForWorkAround(4000);

                executionLog.Log("CreatePricingPlanAdmin", "Click Delete btn  ");
                masterData_PricingPlanHelper.ClickElement("DeleteIcon");

                executionLog.Log("CreatePricingPlanAdmin", "Accept alert message. ");
                masterData_PricingPlanHelper.AcceptAlert();

                executionLog.Log("CreatePricingPlanAdmin", "Wait for delete message. ");
                masterData_PricingPlanHelper.WaitForText("The pricing plan is successfully deleted!!", 10);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("CreatePricingPlanAdmin");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Create Pricing Plan Admin");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Create Pricing Plan Admin", "Bug", "Medium", "Pricing Plan page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Create Pricing Plan Admin");
                        TakeScreenshot("CreatePricingPlanAdmin");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\CreatePricingPlanAdmin.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("CreatePricingPlanAdmin");
                        string id = loginHelper.getIssueID("Create Pricing Plan Admin");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\CreatePricingPlanAdmin.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Create Pricing Plan Admin"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Create Pricing Plan Admin");
            //    executionLog.DeleteFile("Error");
                throw;
            }
            finally
            {
                executionLog.DeleteFile("CreatePricingPlanAdmin");
                executionLog.WriteInExcel("Create Pricing Plan Admin", Status, JIRA, "Office Master Data");
            }
        }
    }
}
