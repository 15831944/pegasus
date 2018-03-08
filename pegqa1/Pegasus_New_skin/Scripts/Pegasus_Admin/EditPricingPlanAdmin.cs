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
    public class EditPricingPlanAdmin : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Admin")]
        [TestCategory("TS6")]
        [TestCategory("Pegasus_Admin")]
        public void editPricingPlanAdmin()
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
            var name = "Test" + GetRandomNumber();
            var num = "Test" + GetRandomNumber();
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("EditPricingPlanAdmin", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("EditPricingPlanAdmin", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("EditPricingPlanAdmin", "Click On  Admin");
                VisitOffice("admin");
                masterData_PricingPlanHelper.WaitForWorkAround(3000);

                executionLog.Log("EditPricingPlanAdmin", "Redirect To URL");
                VisitOffice("pricing_plans");
                masterData_PricingPlanHelper.WaitForWorkAround(5000);

                executionLog.Log("EditPricingPlanAdmin", "Verify title");
                VerifyTitle("Master Pricing Plans");

                executionLog.Log("EditPricingPlanAdmin", " Click On Create");
                masterData_PricingPlanHelper.ClickElement("Create");
                masterData_PricingPlanHelper.WaitForWorkAround(4000);

                executionLog.Log("EditPricingPlanAdmin", "Verify title");
                VerifyTitle("Manage Master Pricing Plans");

                executionLog.Log("EditPricingPlanAdmin", "Enter Pricing Plan");
                masterData_PricingPlanHelper.TypeText("PricingPlan", name);

                executionLog.Log("EditPricingPlanAdmin", "Select processor");
                masterData_PricingPlanHelper.Selectbytext("Processor", "First Data Omaha");

                executionLog.Log("EditPricingPlanAdmin", "  Click on Save button");
                masterData_PricingPlanHelper.ClickElement("Save");
                masterData_PricingPlanHelper.WaitForWorkAround(5000);

                executionLog.Log("EditPricingPlanAdmin", "Verify title");
                VerifyTitle("Master Pricing Plans");

                executionLog.Log("EditPricingPlanAdmin", "Click on Edit icon");
                masterData_PricingPlanHelper.TypeText("SearchPricingPlan", name);
                masterData_PricingPlanHelper.WaitForWorkAround(4000);

                executionLog.Log("EditPricingPlanAdmin", "  Click on Edit button");
                masterData_PricingPlanHelper.ClickElement("Edit");
                masterData_PricingPlanHelper.WaitForWorkAround(4000);

                executionLog.Log("EditPricingPlanAdmin", "Verify title");
                VerifyTitle("Manage Master Pricing Plans");

                executionLog.Log("EditPricingPlanAdmin", "Enter Pricing Plan");
                masterData_PricingPlanHelper.TypeText("PricingPlan", num);

                executionLog.Log("EditPricingPlanAdmin", "  Click on Save button");
                masterData_PricingPlanHelper.ClickElement("Save");
                masterData_PricingPlanHelper.WaitForWorkAround(3000);

                executionLog.Log("EditPricingPlanAdmin", "Wait for updation message.");
                masterData_PricingPlanHelper.WaitForText("The pricing plan is successfully updated!!", 10);

                executionLog.Log("EditPricingPlanAdmin", "Redirect To Pricing Plan");
                VisitOffice("pricing_plans");
                masterData_PricingPlanHelper.WaitForWorkAround(4000);

                executionLog.Log("EditPricingPlanAdmin", "Verify title");
                VerifyTitle("Master Pricing Plans");

                executionLog.Log("EditPricingPlanAdmin", "Enter Name to search");
                masterData_PricingPlanHelper.TypeText("SearchPricingPlan", num);
                masterData_PricingPlanHelper.WaitForWorkAround(4000);

                executionLog.Log("EditPricingPlanAdmin", "Click Delete btn  ");
                masterData_PricingPlanHelper.ClickElement("DeleteIcon");

                executionLog.Log("EditPricingPlanAdmin", "Accept alert message. ");
                masterData_PricingPlanHelper.AcceptAlert();

                executionLog.Log("EditPricingPlanAdmin", "Wait for delete message. ");
                masterData_PricingPlanHelper.WaitForText("The pricing plan is successfully deleted!!", 10);


            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("EditPricingPlanAdmin");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Edit Pricing Plan Admin");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Edit Pricing Plan Admin", "Bug", "Medium", "Pricing page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Edit Pricing Plan Admin");
                        TakeScreenshot("EditPricingPlanAdmin");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\EditPricingPlanAdmin.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("EditPricingPlanAdmin");
                        string id = loginHelper.getIssueID("Edit Pricing Plan Admin");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\EditPricingPlanAdmin.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Edit Pricing Plan Admin"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Edit Pricing Plan Admin");
                // executionLog.DeleteFile("Error");
                throw;
            }
            finally
            {
                executionLog.DeleteFile("EditPricingPlanAdmin");
                executionLog.WriteInExcel("Edit Pricing Plan Admin", Status, JIRA, "Master Data");
            }
        }
    }
}