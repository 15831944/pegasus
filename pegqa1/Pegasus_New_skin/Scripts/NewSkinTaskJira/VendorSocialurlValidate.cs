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
    public class VendorSocialurlValidate : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin_Task")]
        [TestCategory("TS5")]
        [TestCategory("NewSkinTaskJira")]
        public void vendorSocialurlValidate()
        {
            string[] username = null;
            string[] password = null;
            String JIRA = "";
            String Status = "Pass";

            XMLParse oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            username = oXMLData.getData("settings/Credentials", "username_office");
            password = oXMLData.getData("settings/Credentials", "password");

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var equipment_VendorsHelper = new Equipment_VendorsHelper(GetWebDriver());

            // Variable 
            String name = "Test" + RandomNumber(1, 99);
            String Id = "12345" + RandomNumber(1, 99);


            try
            {
                executionLog.Log("VendorSocialurlValidate", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("VendorSocialurlValidate", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("VendorSocialurlValidate", "Redirect To vendor create page");
                VisitOffice("vendors/create");

                executionLog.Log("VendorSocialurlValidate", "Verify title");
                VerifyTitle("Create a New Vendor");

                executionLog.Log("VendorSocialurlValidate", "Enter Invalid facebook URL");
                equipment_VendorsHelper.TypeText("VenFace", "INVALID");

                executionLog.Log("VendorSocialurlValidate", "Enter Invalid Linkedln URL");
                equipment_VendorsHelper.TypeText("VenLnkl", "INVALID");

                executionLog.Log("VendorSocialurlValidate", "Enter Invalid Website URL");
                equipment_VendorsHelper.TypeText("VenWeb", "INVALID");

                executionLog.Log("VendorSocialurlValidate", "Enter Invalid Twiter URL");
                equipment_VendorsHelper.TypeText("VenTwt", "INVALID");

                executionLog.Log("VendorSocialurlValidate", " Click on Save button   ");
                equipment_VendorsHelper.ClickElement("AllButtonSave");

                executionLog.Log("VendorSocialurlValidate", "Verify validation for URL displayed");
                equipment_VendorsHelper.verifyElementDisplayed("VenFaceError");

                executionLog.Log("VendorSocialurlValidate", "Verify validation for URL displayed");
                equipment_VendorsHelper.verifyElementDisplayed("VenTwtError");

                executionLog.Log("VendorSocialurlValidate", "Verify validation for URL displayed");
                equipment_VendorsHelper.verifyElementDisplayed("VenLnklError");

                executionLog.Log("VendorSocialurlValidate", "Verify validation for URL displayed");
                equipment_VendorsHelper.verifyElementDisplayed("VenWebError");

                executionLog.Log("VendorSocialurlValidate", "Go to create shipping page");
                VisitOffice("manage_shipping_carriers");

                executionLog.Log("VendorSocialurlValidate", "Verify title");
                VerifyTitle("Manage Shipping Carrier");

                executionLog.Log("VendorSocialurlValidate", "Enter Invlalid URL");
                equipment_VendorsHelper.TypeText("ShippingTrack", "INVALID");

                executionLog.Log("VendorSocialurlValidate", " Click on Save button   ");
                equipment_VendorsHelper.ClickElement("AllButtonSave");

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("VendorSocialurlValidate");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Vendor Social url Validate");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Vendor Social url Validate", "Bug", "Medium", "Equipment Vendors page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Vendor Social url Validate");
                        TakeScreenshot("VendorSocialurlValidate");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VendorSocialurlValidate.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("VendorSocialurlValidate");
                        string id = loginHelper.getIssueID("Vendor Social url Validate");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VendorSocialurlValidate.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Vendor Social url Validate"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Vendor Social url Validate");
           //     executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("VendorSocialurlValidate");
                executionLog.WriteInExcel("Vendor Social url Validate", Status, JIRA, "Equipment Management");
            }
        }
    }
}